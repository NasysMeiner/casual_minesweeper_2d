using UnityEngine;

public class ResponseTaker : MonoBehaviour
{
    //Game
    private FieldManager _fieldManager;
    private ScoreCounter _scoreCounter;

    //UI
    private SkillListView _skillListView;

    private void OnDisable()
    {
        _fieldManager.DestroyCell -= OnDestroyCell;
    }

    public void InitGame(FieldManager fieldManager, ScoreCounter scoreCounter)
    {
        _fieldManager = fieldManager;
        _scoreCounter = scoreCounter;

        _fieldManager.DestroyCell += OnDestroyCell;
    }

    public void InitUi(SkillListView skillListView)
    {
        _skillListView = skillListView;
    }

    public void OnDestroyCell(CellDestroyedData destroyedData)
    {
        _scoreCounter.DestroyCell(destroyedData);

        if(destroyedData.Skill != Skills.Default)
            _skillListView.ResetSkill(destroyedData.Skill);
    }
}
