using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UnityEngine.Animator))]
public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private EnemyBullet _bulletEnemyTamplate;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ParticleSystem _shot;
    [SerializeField] private float Delay;

    private UnityEngine.Animator _animator;
    private Enemy _enemy;

    private bool _hasShooted;
    private float _elapsedTime;

    private const string ShootAnimation = "Shoot";

    public event UnityAction ShotFired;

    private void Start()
    {
        _animator = GetComponent<UnityEngine.Animator>();
        _enemy = GetComponent<Enemy>();
        _hasShooted = false;
        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= Delay)
            Shoot();
    }

    public void Shoot()
    {
        if (!_hasShooted && _enemy.enabled)
        {
            var bullet = Instantiate(_bulletEnemyTamplate, _shootPoint.position, Quaternion.Euler(_shootPoint.eulerAngles));

            _elapsedTime = 0;
            _hasShooted = true;
            ShotFired?.Invoke();
            _animator.SetTrigger(ShootAnimation);
            _shot.Play();

            enabled = false;
        }
    }
}
