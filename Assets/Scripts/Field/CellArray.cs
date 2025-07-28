using System.Collections.Generic;
using UnityEngine;

public class CellArray
{
    private List<Cell> _cells = new List<Cell>();
    
    public CellArray(int x, int y, float off, Transform pos, Cell prefab)
    {
        Vector3 startPos = pos.position;

        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                Cell newCell = Factory.CreateCell(pos, prefab);
                newCell.Init(i, j);
                _cells.Add(newCell);
                newCell.transform.position = startPos;
                startPos.x += off;
            }

            startPos.y -= off;
            startPos.x -= off * x;
        }
    }
}
