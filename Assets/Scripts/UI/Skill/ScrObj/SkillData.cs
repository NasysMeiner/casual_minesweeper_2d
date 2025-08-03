using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public float Off;
    public List<SkillElement> SkillList = new();
}
