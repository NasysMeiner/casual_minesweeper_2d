using UnityEngine;

public class CanvasContainer : MonoBehaviour
{
    [SerializeField] private HealthView _healthView;
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private AnimCreator _animCreator;
    [SerializeField] private SkillListView _skillListView;
    [SerializeField] private ResponseTaker _responseTaker;

    public HealthView HealthView => _healthView;

    public ScoreView ScoreView => _scoreView;

    public AnimCreator AnimCreator => _animCreator;

    public SkillListView SkillListView => _skillListView;

    public ResponseTaker ResponseTaker => _responseTaker;
}
