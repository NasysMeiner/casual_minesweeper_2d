using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private GameObject _outline;

    private int[] _coord = new int[2];

    public int[] Coord => _coord;

    private void Start()
    {
        DisableOutline();
    }

    public void Init(int x, int y)
    {
        _coord[0] = x;
        _coord[1] = y; 
    }

    public void ActiveOutline()
    {
        _outline.SetActive(true);
    }

    public void DisableOutline()
    {
        _outline.SetActive(false);
    }
}
