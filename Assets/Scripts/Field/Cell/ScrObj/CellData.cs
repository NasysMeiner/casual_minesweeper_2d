using UnityEngine;

[CreateAssetMenu(menuName = "Data/CellData")]
public class CellData : ScriptableObject
{
    [Header("Main")]
    public int Width;
    public int Height;
    public float Off;
    public int CountBombs;
    [Space]
    [Header("Graphic")]
    public Cell Prefab;
}
