using UnityEngine;

public class OutlineCell : MonoBehaviour
{
    [SerializeField] private GameObject _outline;
    [SerializeField] private SpriteRenderer _sr;

    private SpriteRenderer _cellSpriteRenderer;

    private void Start()
    {
        OffOutline();

        _cellSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnOutline()
    {
        _outline.SetActive(true);
    }

    public void OffOutline()
    {
        _outline.SetActive(false);
    }

    public void OffSprite()
    {
        OffOutline();
        Color newColor = _cellSpriteRenderer.color;
        newColor.a = 0f;
        _cellSpriteRenderer.color = newColor;
    }

    public void OnBomb()
    {
        Color newColor = _sr.color;
        newColor.a = 255f;
        _sr.color = newColor;
    }
}
