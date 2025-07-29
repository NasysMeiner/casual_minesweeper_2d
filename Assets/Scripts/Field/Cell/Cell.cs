using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private OutlineCell _outlineCell;

    private int[] _coord = new int[2];

    public int[] Coord => _coord;
    public bool IsDestroy { get; set; }
    public bool IsBomb { get; set; }
    public int CountBomb { get; set; }

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

    public void Destroy(bool isBomb)
    {
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
}
