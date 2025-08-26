using System.Collections;
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

    internal CellDestroyedData DefaultDestroyCell(int[] coord, bool isBomb)
    {
        bool isBoom = isBomb;
        _cellArray.DestroyCell(coord, isBomb, isBoom);
        CellDestroyedData newData = new();
        newData.IsDamage = isBomb;
        newData.AddCell(_cellArray.GetPositionCell(coord), isBomb);
        return newData;
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
        bool isBoom = false;
        CellDestroyedData data = new() { Skill = Skills.DrillAlert };

        _cellArray.DestroyCell(coord, isBomb, isBoom);

        if (isBomb && !_cellArray.GetIsSetFlag(coord))
            _cellArray.SetFlag(coord);

        data.AddCell(_cellArray.GetPositionCell(coord), isBomb);

        return data;
    }

    internal CellDestroyedData ApplyExplosion(int[] coord, bool isBomb, out List<int[]> queueDestroy)
    {
        int r = 1;

        CellDestroyedData data = new() { Skill = Skills.Explosion };
        queueDestroy = new();

        if (!isBomb)
        {
            _cellArray.DestroyCell(coord, false, false);
            data.AddCell(_cellArray.GetPositionCell(coord), false);
            return data;
        }

        List<int[]> bombs = new() { coord };
        List<int[]> nextBombs = new();

        queueDestroy.Add(coord);
        queueDestroy.Add(null);
        _cellArray.SetDestroy(coord);

        while (bombs.Count != 0 || nextBombs.Count != 0)
        {
            if (bombs.Count == 0 && nextBombs.Count != 0)
                (bombs, nextBombs) = (nextBombs, bombs);

            int[] bomb = bombs[bombs.Count - 1];
            bombs.RemoveAt(bombs.Count - 1);

            int h = bomb[1] - r < 0 ? 0 : bomb[1] - r;
            int w = bomb[0] - r < 0 ? 0 : bomb[0] - r;

            for (int y = h; y <= bomb[1] + r; y++)
            {
                if (y >= _field.GetHeight)
                    break;

                for (int x = w; x <= bomb[0] + r; x++)
                {
                    if (x >= _field.GetWidth)
                        break;

                    int[] newCoord = { x, y };

                    if(!_cellArray.IsDestroy(newCoord))
                    {
                        _cellArray.SetDestroy(newCoord);
                        queueDestroy.Add(newCoord);

                        if (_field.GetValue(newCoord[0], newCoord[1]) == 1)
                            nextBombs.Add(newCoord);
                    }
                }
            }

            if (bombs.Count == 0)
                queueDestroy.Add(null);
        }

        return data;
    }
}
