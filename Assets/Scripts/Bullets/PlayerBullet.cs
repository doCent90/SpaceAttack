using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleFire;

    private float _elapsedTime;
    private bool _hasDestroy;

    private const float LifeTime = 0.5f;
    private const float Speed = 90f;

    private void OnEnable()
    {
        _hasDestroy = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage();
            Destroy(gameObject);
        }

        if (collision.TryGetComponent(out Environment environment))
        {
            _particleFire.Play();
            _elapsedTime = 0;
            _hasDestroy = true;
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > LifeTime && _hasDestroy)
        {
            Destroy(gameObject);
        }
    }
}
