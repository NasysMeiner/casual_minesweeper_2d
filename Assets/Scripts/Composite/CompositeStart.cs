using UnityEngine;

public class CompositeRootField : CompositeRoot
{
    [SerializeField] private GameObject _startPointField;
    [SerializeField] private GameObject _startPointResetButton;
    [Space]
    [Header("Components")]
    [Space]
    [Header("Field")]
    [SerializeField] private FieldManager _prefabFieldManager;
    [SerializeField] private CellData _cellData;
    [Space]
    [SerializeField] private ResetButton _prefabResetButton;
    [Space]
    [Header("Input")]
    [SerializeField] private InputHandler _prefabInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDist;
    [Space]
    [Header("Player")]
    [SerializeField] private ScoreCounter _prefabScoreCounter;
    [SerializeField] private PointsCellData _pointsCellData;
    [Space]
    [SerializeField] private HealthManager _prefabHealthManager;
    [SerializeField] private int _countHealth;
    [Space]
    [Header("Anim")]
    [SerializeField] private TextDamage _prefabTextDamage;
    [SerializeField] private float _lifeTime;
    [Space]
    [Header("UI")]
    [SerializeField] private CanvasContainer _prefabCanvas;
    [SerializeField] private Heart _prefabHeart;
    [SerializeField] private float _off;

    private FieldManager _fieldManager;
    private InputHandler _inputHandler;
    private ResetButton _resetButton;
    private ScoreCounter _scoreCounter;
    private HealthManager _healthManager;

    private CanvasContainer _canvas;

    public override void Compose()
    {
        _canvas = Instantiate(_prefabCanvas);

        _inputHandler = Instantiate(_prefabInput, transform);
        _inputHandler.Init(_camera, _maxDist);

        _fieldManager = Instantiate(_prefabFieldManager, transform);
        _fieldManager.transform.position = _startPointField.transform.position;
        _fieldManager.Init(_cellData, _inputHandler);

        _resetButton = Instantiate(_prefabResetButton, transform);
        _resetButton.transform.position = _startPointResetButton.transform.position;
        _resetButton.Init(_fieldManager, _inputHandler);

        _scoreCounter = Instantiate(_prefabScoreCounter, transform);
        _scoreCounter.Init(_fieldManager, _pointsCellData, _canvas.AnimCreator);

        _healthManager = Instantiate(_prefabHealthManager, transform);
        _healthManager.Init(_countHealth, _fieldManager);

        //UI

        _canvas.HealthView.Init(_countHealth, _off, _prefabHeart, _healthManager);
        _canvas.ScoreView.Init(_scoreCounter);
        _canvas.AnimCreator.Init(_camera, _prefabTextDamage, _lifeTime);
    }
}
