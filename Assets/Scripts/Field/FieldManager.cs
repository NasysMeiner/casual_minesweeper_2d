using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldManager : MonoBehaviour
{
    private InputHandler _inputHandler;

    private Field _field;
    private CellArray _cellArray;
    private SkillManager _skillManager;

    private int _countClick = 0;

    public event UnityAction<CellDestroyedData> DestroyCell;
    public event UnityAction Damage;
    public event UnityAction ResetInput;

    private void OnDisable()
    {
        _inputHandler.DestroyCell -= OnDestroyCell;
        _inputHandler.SetFlag -= OnSetFlag;
    }

    public void Init(CellData cellData, InputHandler inputHandler)
    {
        _inputHandler = inputHandler;

        _field = new Field(cellData.Width, cellData.Height, cellData.CountBombs);
        _cellArray = new CellArray(cellData.Width, cellData.Height, cellData.Off, transform, cellData.Prefab, cellData.ColorText.Colors);
        _skillManager = new SkillManager(_field, _cellArray);

        _inputHandler.DestroyCell += OnDestroyCell;
        _inputHandler.SetFlag += OnSetFlag;
    }

    public void ResetField()
    {
        _field.GenerateField();
        _cellArray.ResetCells();
        _countClick = 0;

        ResetInput?.Invoke();
    }

    private void OnSetFlag(int[] coord)
    {
        _cellArray.SetFlag(coord);
    }

    private void OnDestroyCell(int[] coord, Skills skill)
    {
        if ((skill != Skills.Default && CheckNeighboringCell(coord)) || (_cellArray.GetIsSetFlag(coord) && skill == Skills.Default) || _cellArray.IsDestroy(coord))
            return;

        _countClick++;

        bool isBomb = _field.GetValue(coord[0], coord[1]) != 0;

        if (_countClick == 1)
            ChangeBombPlace(ref isBomb, coord);

        CellDestroyedData destroyedData = CreateDestroyedData(coord, isBomb, skill); //Create Data Cell Destroyed

        //for(int i = 0; i < destroyedData.Cells.Count; i++)
        //{
        //    Debug.Log(destroyedData.Cells[i].Key[0] + " : " + destroyedData.Cells[i].Key[1] + " " + destroyedData.Cells[i].Value);
        //}

        DestroyCell?.Invoke(destroyedData);
    }

    private void DestroyEmptyCell(List<int[]> coords)
    {
        foreach (int[] coord in coords)
            DestroyEmptyCell(coord);
    }

    private void DestroyEmptyCell(int[] coord)
    {
        List<int[]> cells = new List<int[]>() { coord };

        do
        {
            coord = cells[cells.Count - 1];
            cells.RemoveAt(cells.Count - 1);
            bool isNumberCell = _cellArray.GetCountBomb(coord) > 0;

            int h = coord[1] - 1 < 0 ? 0 : coord[1] - 1;
            int w = coord[0] - 1 < 0 ? 0 : coord[0] - 1;

            for (int y = h; y <= coord[1] + 1; y++)
            {
                if (y >= _field.GetHeight)
                    break;

                for (int x = w; x <= coord[0] + 1; x++)
                {
                    if (x >= _field.GetWidth)
                        break;

                    int[] newCoord = new int[] { x, y };

                    if (!_cellArray.IsDestroy(newCoord))
                    {
                        if (_field.GetValue(x, y) == 0 && _cellArray.GetCountBomb(newCoord) == 0)
                            cells.Add(newCoord);

                        if (_field.GetValue(x, y) != 1 && ((isNumberCell && _cellArray.GetCountBomb(newCoord) == 0) || !isNumberCell))
                            DefaultDestroyCell(newCoord, false);
                    }
                }
            }
        } while (cells.Count != 0);
    }

    private CellDestroyedData CreateDestroyedData(int[] coord, bool isBomb, Skills skill) //Preparation
    {
        bool isLoseHP = true;
        CellDestroyedData data;

        switch (skill)
        {
            case Skills.DrillAlert:
                isLoseHP = false;
                data = _skillManager.ApplyDrillAlert(coord, isBomb);
                break;

            case Skills.CheckBomb:
                isLoseHP = false;
                data = _skillManager.ApplyCheckBomb(coord);
                break;

            case Skills.Explosion:
                data = _skillManager.ApplyExplosion(coord, isBomb);
                break;

            default:
                data = DefaultDestroyCell(coord, isBomb);
                break;
        }

        if (!isBomb && data.EmptyCell.Count != 0)
            DestroyEmptyCell(coord);

        if (isBomb && isLoseHP)
            Damage?.Invoke();

        return data;
    }

    private CellDestroyedData DefaultDestroyCell(int[] coord, bool isBomb)
    {
        _cellArray.DestroyCell(coord, isBomb);
        CellDestroyedData newData = new();
        newData.IsDamage = isBomb;
        newData.AddCell(_cellArray.GetPositionCell(coord), isBomb);
        return newData;
    }

    private void ChangeBombPlace(ref bool isBomb, int[] coord)
    {
        if (isBomb)
        {
            isBomb = false;
            int[] newCoore = _field.ChangePositionBomb(coord[0], coord[1]);

            Debug.Log("Last " + coord[0] + " " + coord[1] + " New " + newCoore[0] + " " + newCoore[1]);
        }

        CalculateBomb();
    }

    private void CalculateBomb()
    {
        for (int i = 0; i < _field.GetHeight; i++)
        {
            for (int j = 0; j < _field.GetWidth; j++)
            {
                if (_field.GetValue(j, i) == 1)
                    continue;

                int count = 0;

                for (int y = i - 1; y <= i + 1; y++)
                {
                    for (int x = j - 1; x <= j + 1; x++)
                    {
                        count += _field.GetValue(x, y);
                    }
                }

                if (count != 0)
                    _cellArray.SetCountBomb(new int[] { j, i }, count);
            }
        }
    }

    private bool CheckNeighboringCell(int[] coord)
    {
        int h = coord[1] - 1 < 0 ? 0 : coord[1] - 1;
        int w = coord[0] - 1 < 0 ? 0 : coord[0] - 1;

        for (int y = h; y <= coord[1] + 1; y++)
        {
            if (y >= _field.GetHeight)
                break;

            for (int x = w; x <= coord[0] + 1; x++)
            {
                if (x >= _field.GetWidth)
                    break;

                if (_cellArray.IsDestroy(new int[] { x, y }))
                    return false;
            }
        }

        return true;
    }
}
