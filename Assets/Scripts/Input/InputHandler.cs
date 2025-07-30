using UnityEngine;
using UnityEngine.Events;

public class InputHandler : MonoBehaviour
{
    private Camera _camera;
    private float _maxDist;

    private Cell _activeCell = null;
    private bool _isResetButton = false;

    public event UnityAction<int[]> DestroyCell;
    public event UnityAction ResetField;

    private void Update()
    {
        CheckClick();
    }

    private void FixedUpdate()
    {
        CheckCell();
    }

    public void Init(Camera camera, float maxDist)
    {
        _camera = camera;
        _maxDist = maxDist;
    }

    private void CheckClick()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if (_activeCell != null)
                DestroyCell?.Invoke(_activeCell.Coord);
            else if (_isResetButton)
                ResetField?.Invoke();
        }
            
    }

    private void CheckCell()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = _camera.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit raycast, _maxDist))
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

            if(raycast.collider.TryGetComponent(out ResetButton reset))
            {
                _isResetButton = true;
            }
            else
            {
                _isResetButton = false;
            }
        }
    }
}
