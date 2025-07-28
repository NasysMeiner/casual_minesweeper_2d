using UnityEngine;

public class CompositeRootField : CompositeRoot
{
    [SerializeField] private GameObject _startPoint;
    [Space]
    [Header("Components")]
    [Space]
    [Header("Field")]
    [SerializeField] private FieldManager _prefabFieldManager;
    [SerializeField] private CellData _cellData;
    [Space]
    [Header("Input")]
    [SerializeField] private InputHandler _prefabInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxDist;

    private FieldManager _fieldManager;
    private InputHandler _inputHandler;

    public override void Compose()
    {
        _inputHandler = Instantiate(_prefabInput, transform);
        _inputHandler.Init(_camera, _maxDist);

        _fieldManager = Instantiate(_prefabFieldManager, transform);
        _fieldManager.transform.position = _startPoint.transform.position;

        _fieldManager.Init(_cellData, _inputHandler);
    }
}
