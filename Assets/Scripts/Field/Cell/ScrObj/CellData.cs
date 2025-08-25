using UnityEngine;

[CreateAssetMenu(menuName = "Data/Cell/CellData")]
public class CellData : ScriptableObject
{
    [Header("Main")]
    public TypeMap TypeMap = TypeMap.Default;
    public int Width;
    public int Height;
    public float Off;
    public int CountBombs;
    public bool IsSafeFirstClick;
    [Space]
    [Header("Graphic")]
    public Cell Prefab;
    public ColorCellData ColorText;
}
