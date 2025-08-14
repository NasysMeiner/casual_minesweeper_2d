using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _spriteSkill;
    [SerializeField] private Image _outline;
    [SerializeField] private TMP_Text _textCoolDown;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private SkillListView _skillListView;
    private Skills _typeSkill;

    private bool _isHovering = false;
    private bool _isInteractable = true;

    public Skills TypeSkill => _typeSkill;
    public bool IsHovering => _isHovering;

    public void Init(Sprite sprite, SkillListView skillListView, Skills type)
    {
        _skillListView = skillListView;
        _typeSkill = type;

        _spriteSkill.sprite = sprite;
        _outline.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isInteractable)
            return;

        _isHovering = true;

        if (_skillListView.CurrentSkill == null || _skillListView.CurrentSkill.TypeSkill != _typeSkill)
        {
            _outline.color = _inactiveColor;
            _outline.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isInteractable)
            return;

        _isHovering = false;

        if (_skillListView.CurrentSkill == null || _skillListView.CurrentSkill.TypeSkill != _typeSkill)
            _outline.enabled = false;
    }

    public void SetCoolDown(float time)
    {
        StartCoroutine(CoolDown(time));
    }

    public void OnOuline()
    {
        _outline.enabled = true;
        _outline.color = _activeColor;
    }

    public void OffOuline()
    {
        _outline.color = _inactiveColor;

        if (_isHovering)
            _outline.enabled = true;
        else
            _outline.enabled = false;
    }

    public void SetSkill()
    {
        _skillListView.SetSkill(this);
    }

    private IEnumerator CoolDown(float time)
    {
        Button button = gameObject.GetComponent<Button>();
        button.interactable = false;

        while (time > 0)
        {
            time -= Time.deltaTime;
            _textCoolDown.text = ((int)time).ToString();
            yield return null;
        }

        button.interactable = true;
        _textCoolDown.text = "";
    }
}
