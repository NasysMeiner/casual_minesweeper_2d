using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreCounter : MonoBehaviour
{
    private FieldManager _fieldManager;

    private int _defaultPointCell;

    public int Score { get; private set; }

    private void OnDisable()
    {
        _fieldManager.DestroyCell -= OnDestroyCell;
    }

    public void Init(FieldManager fieldManager, PointsCellData pointsData)
    {
        Score = 0;

        _fieldManager = fieldManager;

        _fieldManager.DestroyCell += OnDestroyCell;

        _defaultPointCell = pointsData.DefaultPointCell;
    }

    private void OnDestroyCell(int count)
    {
        Score += count * _defaultPointCell;
    }
}
