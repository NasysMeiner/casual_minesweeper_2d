using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CellArray
{
    private List<Cell> _cells = new List<Cell>();
    private int _length;
    
    public CellArray(int x, int y, float off, Transform pos, Cell prefab)
    {
        _length = x;
        Vector3 startPos = pos.position;

        for(int i = 0; i < y; i++)
        {
            for(int j = 0; j < x; j++)
            {
                Cell newCell = Factory.CreateCell(pos, prefab);
                newCell.Init(j, i);
                _cells.Add(newCell);
                newCell.transform.position = startPos;
                startPos.x += off;
            }

            startPos.y -= off;
            startPos.x -= off * x;
        }
    }

    public void DestroyCell(int[] coord, bool isBomb)
    {
        Cell destroyCell = _cells[coord[1] * _length + coord[0]];
        destroyCell.Destroy(isBomb);
    }
}
