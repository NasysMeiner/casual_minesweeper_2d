using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class Field 
{
    private int _x;
    private int _y;
    private int _countBomb;
    private List<List<int>> _field = new List<List<int>>();
    private TypeMap _typeMap;

    public int GetWidth => _x;
    public int GetHeight => _y;

    public Field(int x, int y, int countBomb, TypeMap typeMap = TypeMap.Default)
    {
        _typeMap = typeMap;

        if (typeMap != TypeMap.Default)
        {
            CreateTypeMap(typeMap);
            return;
        }

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

        if (_typeMap != TypeMap.Default)
        {
            CreateTypeMap(_typeMap);
            return;
        }

        if (_x * _y <= _countBomb)
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

    private void CreateTypeMap(TypeMap typeMap)
    {
        switch(typeMap)
        {
            case TypeMap.TestMapDest1:
                CreateTestMapDest();
            break;
            case TypeMap.TestMapDest2:
                CreateTestMapDest2();
            break;
        }
    }

    private void CreateTestMapDest2()
    {
        _x = 9;
        _y = 6;

        CreateEmptyField();

        SetValue(1, 0, 1);
        SetValue(3, 0, 1);
        SetValue(0, 1, 1);
        SetValue(4, 1, 1);
        SetValue(0, 2, 1);
        SetValue(4, 2, 1);
        SetValue(5, 2, 1);
        SetValue(6, 2, 1);
        SetValue(7, 2, 1);
        SetValue(0, 3, 1);
        SetValue(4, 3, 1);
        SetValue(1, 4, 1);
        SetValue(3, 4, 1);
        SetValue(2, 5, 1);
    }

    private void CreateTestMapDest()
    {
        _x = 4;
        _y = 4;

        CreateEmptyField();

        SetValue(1, 1, 1);
        SetValue(1, 2, 1);
        SetValue(2, 2, 1);
        SetValue(0, 3, 1);
    }

    private void CreateEmptyField()
    {
        for (int i = 0; i < _x; i++)
        {
            _field.Add(new List<int>());

            for (int j = 0; j < _y; j++)
                _field[i].Add(0);
        }
    }
}
