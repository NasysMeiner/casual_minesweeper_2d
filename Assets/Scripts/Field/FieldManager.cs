using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private InputHandler _inputHandler;

    private Field _field;
    private CellArray _cellArray;

    private int _countClick = 0;

    private void OnDisable()
    {
        _inputHandler.DestroyCell -= OnDestroyCell;
    }

    public void Init(CellData cellData, InputHandler inputHandler)
    {
        _inputHandler = inputHandler;

        _field = new Field(cellData.Width, cellData.Height, cellData.CountBombs);
        _cellArray = new CellArray(cellData.Width, cellData.Height, cellData.Off, transform, cellData.Prefab);

        _inputHandler.DestroyCell += OnDestroyCell;
    }

    private void OnDestroyCell(int[] coord)
    {
        _countClick++;

        bool isBomb = _field.GetValue(coord[0], coord[1]) != 0;

        if(isBomb && _countClick == 1)
        {
            isBomb = false;
            int[] newCoore = _field.ChangePositionBomb(coord[0], coord[1]);

            Debug.Log("Last " + coord[0] + " " + coord[1] + " New " + newCoore[0] + " " + newCoore[1]);
        }

        if (_countClick == 1)
            CalculateBomb();

        _cellArray.DestroyCell(coord, isBomb);
    }

    private void CalculateBomb()
    {
        for(int i = 0; i < _field.GetHeight; i++)
        {
            for(int j = 0; j < _field.GetWidth; j++)
            {
                if (_field.GetValue(j, i) == 1)
                    continue;

                int count = 0;

                for(int y = i - 1; y <= i + 1; y++)
                {
                    for(int x = j - 1; x <= j + 1; x++)
                    {
                        count += _field.GetValue(x, y);
                    }
                }

                if(count != 0)
                    _cellArray.SetCountBomb(new int[] { j, i }, count);
            }
        }
    }
}
