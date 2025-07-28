using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private InputHandler _inputHandler;

    private Field _field;
    private CellArray _cellArray;

    private void OnDisable()
    {
        _inputHandler.DestroyCell -= OnDestroyCell;
    }

    public void Init(CellData cellData, InputHandler inputHandler)
    {
        _inputHandler = inputHandler;

        _field = new Field(cellData.Width, cellData.Height, cellData.CountBombs);
        _cellArray = new CellArray(cellData.Width, cellData.Height, cellData.Off, transform, cellData.Prefab);

        _inputHandler.DestroyCell += OnDestroyCell;
    }

    private void OnDestroyCell(int[] coord)
    {
        bool isBomb = _field.GetValue(coord[0], coord[1]) != 0;
        _cellArray.DestroyCell(coord, isBomb);
    }
}
