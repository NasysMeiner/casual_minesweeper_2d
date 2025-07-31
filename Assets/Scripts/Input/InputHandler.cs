using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;
    private float _maxDist;

    private Cell _activeCell = null;
    private bool _isResetButton = false;

    public event UnityAction<int[]> DestroyCell;
    public event UnityAction<int[]> SetFlag;
    public event UnityAction ResetField;

    private void Update()
    {
        CheckClick();
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit raycast, _maxDist))
        {
            CheckCell(raycast);
            CheckRestButton(raycast);
        }
    }

    public void Init(Camera camera, float maxDist)
    {
        _camera = camera;
        _maxDist = maxDist;
    }

    private void CheckClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (_activeCell != null)
                DestroyCell?.Invoke(_activeCell.Coord);
            else if (_isResetButton)
                ResetField?.Invoke();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (_activeCell != null)
                SetFlag?.Invoke(_activeCell.Coord);
        }

    }

    private void CheckCell(RaycastHit raycast)
    {
        if (raycast.collider.TryGetComponent(out Cell cell))
        {
            if (_activeCell != null)
            {
                _activeCell.OffOutline();
                _activeCell = null;
            }

            cell.OnOutline();
            _activeCell = cell;
        }
        else
        {
            if (_activeCell != null)
            {
                _activeCell.OffOutline();
                _activeCell = null;
            }
        }
    }

    private void CheckRestButton(RaycastHit raycast)
    {
        if (raycast.collider.TryGetComponent(out ResetButton _))
            _isResetButton = true;
        else
            _isResetButton = false;
    }
}
