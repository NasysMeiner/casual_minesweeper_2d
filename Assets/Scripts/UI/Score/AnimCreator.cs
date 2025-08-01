using System.Collections.Generic;
using UnityEngine;

public class AnimCreator : MonoBehaviour
{
    private Camera _camera;

    private TextDamage _prefabTextDamage;
    private float _lifeTime;

    public void Init(Camera camera, TextDamage prefabTextDamage, float lifeTime)
    {
        _camera = camera;

        _prefabTextDamage = prefabTextDamage;
        _lifeTime = lifeTime;
    }

    public void CreateTextDamageAnim(List<Vector3> arrayPos, int point)
    {
        foreach(Vector3 startPos in arrayPos)
        {
            TextDamage textDamage = Instantiate(_prefabTextDamage, transform);
            textDamage.Init(_lifeTime, _camera, startPos, point);
        }
    }
}
