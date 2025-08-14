using System;
using System.Collections.Generic;

public class SkillManager
{
    private Field _field;
    private CellArray _cellArray;

    public SkillManager(Field field, CellArray cellArray)
    {
        _field = field;
        _cellArray = cellArray;
    }

    internal CellDestroyedData ApplyCheckBomb(int[] coord)
    {
        int r = 1;
        List<KeyValuePair<int[], bool>> cells = new();

        int h = coord[1] - r < 0 ? 0 : coord[1] - r;
        int w = coord[0] - r < 0 ? 0 : coord[0] - r;

        for (int y = h; y <= coord[1] + r; y++)
        {
            if (y >= _field.GetHeight)
                break;

            for (int x = w; x <= coord[0] + r; x++)
            {
                if (x >= _field.GetWidth)
                    break;

                int[] newCoord = new int[] { x, y };
                cells.Add(new(new int[] { x, y }, _field.GetValue(x, y) == 0 ? false : true));
            }
        }

        _cellArray.CheckBombView(cells);

        return new() { Skill = Skills.CheckBomb };
    }

    internal CellDestroyedData ApplyDrillAlert(int[] coord, bool isBomb)
    {
        throw new NotImplementedException();
    }

    internal CellDestroyedData ApplyExplosion(int[] coord, bool isBomb)
    {
        throw new NotImplementedException();
    }
}
