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

    private FieldManager _fieldManager;

    public override void Compose()
    {
        _fieldManager = Instantiate(_prefabFieldManager, transform);
        _fieldManager.transform.position = _startPoint.transform.position;

        _fieldManager.Init(_cellData);
    }
}
