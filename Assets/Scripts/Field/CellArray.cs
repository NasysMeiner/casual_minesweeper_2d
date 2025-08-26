using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CellArray
{
    private List<Cell> _cells = new List<Cell>();
    private int _length;

    public CellArray(int x, int y, float off, Transform pos, Cell prefab, List<Color> colorText)
    {
        _length = x;
        Vector3 startPos = pos.position;

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Cell newCell = Factory.CreateCell(pos, prefab);
                newCell.Init(j, i, colorText);
                _cells.Add(newCell);
                newCell.transform.position = startPos;
                startPos.x += off;
            }

            startPos.y -= off;
            startPos.x -= off * x;
        }
    }

    public void ResetCells()
    {
        foreach(Cell cell in _cells)
            cell.ResetCell();
    }

    public void DestroyCell(int[] coord, bool isBomb, bool isBoom)
    {
        Cell cell = GetCell(coord);
        cell.Destroy(isBomb);
        cell.OnEffectBomb(isBoom);
    }

    public void SetDestroy(int[] coord)
    {
        GetCell(coord).SetDestroy();
    }

    public bool IsDestroy(int[] coord)
    {
        return GetCell(coord).IsDestroy;
    }

    public void SetCountBomb(int[] coord, int count)
    {
        GetCell(coord).SetCountBomb(count);
    }

    public int GetCountBomb(int[] coord)
    {
        return GetCell(coord).CountBomb;
    }

    public void SetFlag(int[] coord)
    {
        Cell cell = GetCell(coord);

        if (!cell.GetIsSetFlag())
            cell.SetFlag();
        else
            cell.OffFlag();
    }

    public bool GetIsSetFlag(int[] coord)
    {
        return GetCell(coord).GetIsSetFlag();
    }

    public Vector3 GetPositionCell(int[] coord)
    {
        return GetCell(coord).transform.position;
    }

    public void CheckBombView(List<KeyValuePair<int[], bool>> cells)
    {
        for(int i = 0; i < cells.Count; i++)
        {
            Cell cell = GetCell(cells[i].Key);

            if(!cell.IsDestroy)
                cell.CheckBombView(cells[i].Value);
        }
    }

    private Cell GetCell(int[] coord)
    {
        return _cells[coord[1] * _length + coord[0]];
    }
}
