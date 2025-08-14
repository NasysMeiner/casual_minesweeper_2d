using UnityEngine;
using UnityEngine.Events;

public class ScoreCounter : MonoBehaviour
{
    private AnimCreator _animCreator;

    private int _defaultPointCell;
    private int _pointBombCellPlus;
    private int _pointBombCellMinus;

    public int Score { get; private set; }

    public event UnityAction<int> ScoreUpdate;

    public void Init(PointsCellData pointsData, AnimCreator animCreator)
    {
        Score = 0;

        _animCreator = animCreator;

        _defaultPointCell = pointsData.DefaultPointCell;
        _pointBombCellPlus = pointsData.PointBombCellPlus;
        _pointBombCellMinus = pointsData.PointBombCellMinus;
    }

    public void DestroyCell(CellDestroyedData destroyedData)
    {
        int countEmpty = destroyedData.EmptyCell.Count;
        int countBomb = destroyedData.BombCell.Count;

        Score += countEmpty * _defaultPointCell;
        Score += countBomb * (destroyedData.IsDamage ? _pointBombCellMinus : _pointBombCellPlus);

        ScoreUpdate?.Invoke(Score);

        _animCreator.CreateTextDamageAnim(destroyedData.EmptyCell, countEmpty);
    }
}
