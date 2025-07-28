using UnityEngine;

public class FieldManager : MonoBehaviour
{
    private Field _field;
    private CellArray _cellManager;

    public void Init(CellData cellData)
    {
        _field = new Field(cellData.Width, cellData.Height, cellData.CountBombs);
        _cellManager = new CellArray(cellData.Width, cellData.Height, cellData.Off, transform, cellData.Prefab);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
