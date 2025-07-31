using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private OutlineCell _outlineCell;

    private int[] _coord = new int[2];

    public int[] Coord => _coord;
    public bool IsDestroy { get; private set; }
    public bool IsBomb { get; private set; }
    public int CountBomb { get; private set; }

    public void Init(int x, int y, List<Color> colorText)
    {
        _coord[0] = x;
        _coord[1] = y;

        IsDestroy = false;
        IsBomb = false;
        CountBomb = 0;

        _outlineCell = GetComponent<OutlineCell>();
        _outlineCell.Init(colorText);
    }

    public void ResetCell()
    {
        IsDestroy = false;
        IsBomb = false;
        CountBomb = 0;

        _outlineCell.ResetOutline();
    }

    public void Destroy(bool isBomb)
    {
        if (_outlineCell.IsSetFlag)
            return;

        IsDestroy = true;

        _outlineCell.OffSprite();

        if (isBomb)
            _outlineCell.OnBomb();

        if (isBomb)
            Debug.Log("Boom!!!");
    }

    public void OnOutline()
    {
        if(!IsDestroy)
            _outlineCell.OnOutline();
    }

    public void OffOutline()
    {
        _outlineCell.OffOutline();
    }

    public void SetCountBomb(int count)
    {
        CountBomb = count;
        _outlineCell.SetCountBomb(count);
    }

    public void SetFlag()
    {
        if(!IsDestroy)
        {
            if (!_outlineCell.IsSetFlag)
                _outlineCell.SetFlag();
            else
                _outlineCell.OffFlag();
        }
    }

    public bool GetIsSetFlag()
    {
        return _outlineCell.IsSetFlag;
    }
}
