using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleFire;
    [SerializeField] private GameObject _bullet;

    private float _elapsedTime;
    private bool _hasDestroy;

    private const float LifeTime = 0.35f;
    private const float Speed = 90f;

    private void OnEnable()
    {
        _hasDestroy = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _hasDestroy = true;
            _elapsedTime = 0;
            _bullet.SetActive(false);
        }

        if (collision.TryGetComponent(out Environment environment) && !_hasDestroy)
        {
            _particleFire.Play();
            _elapsedTime = 0;
            _hasDestroy = true;
            _bullet.SetActive(false);
        }
    }

    private void Update()
    {
        if(!_hasDestroy)
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > LifeTime && _hasDestroy)
        {
            Destroy(gameObject);
        }
    }
}
