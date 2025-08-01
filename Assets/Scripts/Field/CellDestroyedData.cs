using System.Collections.Generic;
using UnityEngine;

public class CellDestroyedData
{
    public List<Vector3> EmptyCell = new();
    public List<Vector3> BombCell = new();

    public bool IsDamage = false;

    public void AddCell(Vector3 pos, bool isBomb)
    {
        if (isBomb)
            BombCell.Add(pos);
        else
            EmptyCell.Add(pos);
    }
}
