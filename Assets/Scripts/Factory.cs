using UnityEngine;

public class Factory : MonoBehaviour
{
    public static Cell CreateCell(Transform pos, Cell prefab)
    {
        return Instantiate(prefab, pos);
    }
}
