using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject _fullHeart;

    public void ResetHeart()
    {
        _fullHeart.SetActive(true);
    }

    public void OffHeart()
    {
        _fullHeart.SetActive(false);
    }
}
