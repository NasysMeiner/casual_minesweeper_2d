using System.Collections.Generic;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    private HealthManager _healthManager;

    private List<Heart> _hearts = new();

    private int _health;

    private void OnDisable()
    {
        _healthManager.Damage -= OnDamage;
    }

    public void Init(int count, float off, Heart prfab, HealthManager healthManager)
    {
        _health = count;
        _healthManager = healthManager;

        _healthManager.Damage += OnDamage;

        for (int i = 0; i < count; i++)
        {
            Heart heart = Instantiate(prfab, transform);
            heart.transform.position = transform.position + new Vector3(i * off, 0, 0);
            _hearts.Add(heart);
        }
    }

    public void OnDamage()
    {
        if(_health != 0)
            _hearts[_health-- - 1].OffHeart();
    }
}
