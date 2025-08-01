using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{
    private int _health;

    private FieldManager _fieldManager;

    public int Health => _health;

    public event UnityAction Damage;

    private void OnDisable()
    {
        _fieldManager.Damage -= OnDamage;
    }

    public void Init(int health, FieldManager fieldManager)
    {
        _health = health;
        _fieldManager = fieldManager;

        _fieldManager.Damage += OnDamage;
    }

    public void OnDamage()
    {
        _health--;

        Damage?.Invoke();
    }
}
