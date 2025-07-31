using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Cell/ColorCellData")]
public class ColorCellData : ScriptableObject
{
    public List<Color> Colors = new();
}
