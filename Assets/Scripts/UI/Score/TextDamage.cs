using System.Collections;
using TMPro;
using UnityEngine;

public class TextDamage : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Camera _camera;

    private float _lifeTime;
    private Vector3 _startPosition;

    public void Init(float lifeTime, Camera camera, Vector3 startPosition, int point)
    {
        _camera = camera;
        _lifeTime = lifeTime;
        _startPosition = startPosition + new Vector3(0, 0, -startPosition.z);

        _text.text = point.ToString();
        transform.position = _camera.WorldToScreenPoint(_startPosition);

        StartCoroutine(TextAnimation());
    }

    private IEnumerator TextAnimation()
    {
        Color color;
        float delta;
        Vector3 pos = _startPosition;
        float time = 0;

        while (time < _lifeTime)
        {
            color = _text.color;
            color.a = _lifeTime > time ? _lifeTime - time : 0;
            _text.color = color;

            pos = pos + new Vector3(Time.deltaTime, Time.deltaTime, 0);

            _text.transform.position = _camera.WorldToScreenPoint(pos);

            yield return null;

            time += Time.deltaTime;
        }

        Destroy(gameObject);
    }
}
