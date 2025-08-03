using UnityEngine;

public class CanvasContainer : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private AnimCreator _animCreator;
    [SerializeField] private SkillListView _skillListView;

    public HealthView HealthView => _healthView;

    public ScoreView ScoreView => _scoreView;

    public AnimCreator AnimCreator => _animCreator;

    public SkillListView SkillListView => _skillListView;
}
