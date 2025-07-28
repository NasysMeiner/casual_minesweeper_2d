using System;
using System.Collections.Generic;

public class Field 
{
    private int _x;
    private int _y;
    private int _countBomb;
    private List<List<int>> _field = new List<List<int>>();

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

        if(_x * _y < _countBomb)
            _countBomb = _x * _y;

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

    public int GetValue(int x, int y)
    {
        return _field[x][y];
    }

    public void SetValue(int x, int y, int value)
    {
        _field[x][y] = value;
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
