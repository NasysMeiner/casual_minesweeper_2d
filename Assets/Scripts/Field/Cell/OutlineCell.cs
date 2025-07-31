using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutlineCell : MonoBehaviour
{
    [SerializeField] private GameObject _outline;
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SpriteRenderer _flag;

    private SpriteRenderer _cellSpriteRenderer;
    private List<Color> _colorText;

    public bool IsSetFlag { get; set; }

    private void Start()
    {
        IsSetFlag = false;

        OffOutline();
        OffFlag();

        _cellSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(List<Color> colorText)
    {
        _colorText = colorText;
    }

    public void ResetOutline()
    {
        Color newColor = _cellSpriteRenderer.color;
        newColor.a = 255f;
        _cellSpriteRenderer.color = newColor;

        newColor = _sr.color;
        newColor.a = 0f;
        _sr.color = newColor;

        _text.text = "";

        OffFlag();
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

    public void SetCountBomb( int count)
    {
        _text.text = count.ToString();

        if(count != 0)
            _text.color = _colorText[count - 1];
    }

    public void SetFlag()
    {
        IsSetFlag = true;
        _flag.gameObject.SetActive(true);
    }

    public void OffFlag()
    {
        IsSetFlag = false;
        _flag.gameObject.SetActive(false);
    }
}
