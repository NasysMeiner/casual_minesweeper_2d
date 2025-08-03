using System.Collections.Generic;
using UnityEngine;

public class SkillListView : MonoBehaviour
{
    private InputHandler _inputHandler;

    private float _off;
    private List<SkillButton> _skills = new();

    private SkillButton _currentSkill;

    public SkillButton CurrentSkill => _currentSkill;

    public void Init(SkillData skillData, InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
        _off = skillData.Off;

        _currentSkill = null;

        int count = 0;

        foreach(SkillElement element in skillData.SkillList)
        {
            SkillButton button = Instantiate(element.PrefabButton, transform);
            button.transform.position += new Vector3(0, count++ * _off, 0);
            button.Init(element.SkillImage, this, element.Skill);
            _skills.Add(button);
        }
    }

    public void SetSkill(SkillButton button)
    {
        if (_currentSkill == null || _currentSkill != button)
        {
            _currentSkill?.OffOuline();
            _currentSkill = button;
            _currentSkill.OnOuline();
        }
        else if (_currentSkill == button)
        {
            _currentSkill.OffOuline();
            _currentSkill = null;
        }

        _inputHandler.SetSkill(_currentSkill == null ? Skills.Default : _currentSkill.TypeSkill);
    }
}
