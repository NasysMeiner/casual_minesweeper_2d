using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private FieldManager _fieldManager;
    private InputHandler _inputHandler;

    private void OnDisable()
    {
        _inputHandler.ResetField -= OnResetField;
    }

    public void Init(FieldManager fieldManager, InputHandler inputHandler)
    {
        _fieldManager = fieldManager;
        _inputHandler = inputHandler;

        _inputHandler.ResetField += OnResetField;
    }

    public void OnResetField()
    {
        _fieldManager.ResetField();
    }
}
