using System;
using System.Collections.Generic;

public class Field 
{
    private int _x;
    private int _y;
    private int _countBomb;
    private List<List<int>> _field = new List<List<int>>();

    public int GetWidth => _x;
    public int GetHeight => _y;

    public Field(int x, int y, int countBomb)
    {
        _x = x;
        _y = y;
        _countBomb = countBomb;

        for (int i = 0; i < x; i++)
        {
            _field.Add(new List<int>());

            for (int j = 0; j < y; j++)
                _field[i].Add(0);
        }
            
        GenerateField();
    }

    public void GenerateField()
    {
        ClearField();

        if(_x * _y <= _countBomb)
            _countBomb = _x * _y - 1;

        Random random = new Random();

        for(int i = _countBomb; i > 0; i--)
        {
            int x = random.Next(_x);
            int y = random.Next(_y);

            if (GetValue(x, y) != 1)
                _field[x][y] = 1;
            else
                i++;
        }
    }

    public int[] ChangePositionBomb(int w, int h)
    {
        Random random = new();

        int emptyCell = _x * _y - _countBomb;
        int randomEmpty = random.Next(emptyCell);
        int[] newCoord = new int[2];

        for(int i = 0; i < _y; i++)
        {
            for(int j = 0; j < _x; j++)
            {
                if (_field[j][i] == 0)
                {
                    randomEmpty--;

                    if(randomEmpty <= 0)
                    {
                        newCoord[0] = j;
                        newCoord[1] = i;
                        goto exit;
                    }
                }
            }
        }

    exit:

        _field[newCoord[0]][newCoord[1]] = 1;
        _field[w][h] = 0;

        return newCoord;
    }

    public int GetValue(int x, int y)
    {
        if (x < 0 || x >= _x || y < 0 || y >= _y)
            return 0;

        return _field[x][y];
    }

    public void SetValue(int x, int y, int value)
    {
        _field[x][y] = value;
    }

    public bool IsExists(int x, int y)
    {
        if (x < 0 || x >= _x || y < 0 || y >= _y)
            return false;

        return true;
    }

    private void ClearField()
    {
        for (int i = 0; i < _x; i++)
        {
            for(int j = 0; j < _y; j++)
            {
                _field[i][j] = 0;
            }
        }
    }
}
