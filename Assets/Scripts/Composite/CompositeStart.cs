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

    private FieldManager _fieldManager;
    private InputHandler _inputHandler;
    private ResetButton _resetButton;

    public override void Compose()
    {
        _inputHandler = Instantiate(_prefabInput, transform);
        _inputHandler.Init(_camera, _maxDist);

        _fieldManager = Instantiate(_prefabFieldManager, transform);
        _fieldManager.transform.position = _startPointField.transform.position;
        _fieldManager.Init(_cellData, _inputHandler);

        _resetButton = Instantiate(_prefabResetButton, transform);
        _resetButton.transform.position = _startPointResetButton.transform.position;
        _resetButton.Init(_fieldManager, _inputHandler);
    }
}
