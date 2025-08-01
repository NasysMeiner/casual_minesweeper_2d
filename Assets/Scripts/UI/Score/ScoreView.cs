using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textScore;

    private ScoreCounter _scoreCounter;

    private void OnDisable()
    {
        _scoreCounter.ScoreUpdate -= OnScoreUpdate;
    }

    public void Init(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;

        _scoreCounter.ScoreUpdate += OnScoreUpdate;
    }

    public void OnScoreUpdate(int score)
    {
        _textScore.text = score.ToString();
    }
}
