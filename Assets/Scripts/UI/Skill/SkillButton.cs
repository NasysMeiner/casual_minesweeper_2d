using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _spriteSkill;
    [SerializeField] private Image _outline;

    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inactiveColor;

    private SkillListView _skillListView;
    private Skills _typeSkill;

    private bool _isHovering = false;

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
        _isHovering = true;

        if (_skillListView.CurrentSkill == null || _skillListView.CurrentSkill.TypeSkill != _typeSkill)
        {
            _outline.color = _inactiveColor;
            _outline.enabled = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;

        if (_skillListView.CurrentSkill == null || _skillListView.CurrentSkill.TypeSkill != _typeSkill)
            _outline.enabled = false;
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
}
