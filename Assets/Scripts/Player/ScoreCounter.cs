using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private FieldManager _fieldManager;
    private AnimCreator _animCreator;

    private int _defaultPointCell;
    private int _pointBombCellPlus;
    private int _pointBombCellMinus;

    public int Score { get; private set; }

    public event UnityAction<int> ScoreUpdate;

    private void OnDisable()
    {
        _fieldManager.DestroyCell -= OnDestroyCell;
    }

    public void Init(FieldManager fieldManager, PointsCellData pointsData, AnimCreator animCreator)
    {
        Score = 0;

        _fieldManager = fieldManager;
        _animCreator = animCreator;

        _fieldManager.DestroyCell += OnDestroyCell;

        _defaultPointCell = pointsData.DefaultPointCell;
        _pointBombCellPlus = pointsData.PointBombCellPlus;
        _pointBombCellMinus = pointsData.PointBombCellMinus;
    }

    private void OnDestroyCell(CellDestroyedData destroyedData)
    {
        int countEmpty = destroyedData.EmptyCell.Count;
        int countBomb = destroyedData.BombCell.Count;

        Score += countEmpty * _defaultPointCell;
        Score += countBomb * (destroyedData.IsDamage ? _pointBombCellMinus : _pointBombCellPlus);

        ScoreUpdate?.Invoke(Score);

        _animCreator.CreateTextDamageAnim(destroyedData.EmptyCell, countEmpty);
    }
}
