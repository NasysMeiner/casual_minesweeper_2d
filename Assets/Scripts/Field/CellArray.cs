using System.Collections.Generic;
using UnityEngine;

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

    public void DestroyCell(int[] coord, bool isBomb)
    {
        GetCell(coord).Destroy(isBomb);
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
        GetCell(coord).SetFlag();
    }

    public bool GetIsSetFlag(int[] coord)
    {
        return GetCell(coord).GetIsSetFlag();
    }

    public Vector3 GetPositionCell(int[] coord)
    {
        return GetCell(coord).transform.position;
    }

    private Cell GetCell(int[] coord)
    {
        return _cells[coord[1] * _length + coord[0]];
    }
}
