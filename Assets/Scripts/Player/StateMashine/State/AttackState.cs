using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class AttackState : StatePlayer
{
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private GameObject _laser;
    [SerializeField] private ParticalCollisions _particalCollisions;
    [SerializeField] private Transform _gunPoint;
    [Header("Settings of Shoot Position")]
    [SerializeField] private float _speed;

    private Animator _animator;
    private PlayerShooter _playerShooter;

    private float _elapsedTime;
    private int _direction = 1;
    private int _attackCount = 1;

    private const int LeftRotate = 1;
    private const int RightRotate = -1;
    private const float DelayBetweenShoot = 0.1f;
    private const float TimeScaleNormal = 1f;
    private const float TimeScaleRapid = 0.4f;
    private const string ShootAnimation = "Shoot";
    private const string TargetAnimation = "LockTarget";

    public event UnityAction<bool> Attacked;

    private void OnEnable()
    {
        Attacked?.Invoke(true);

        SetAngle();

        Time.timeScale = TimeScaleRapid;

        if (_attackCount % 2 == 0)
            _direction = RightRotate;
        else
            _direction = LeftRotate;

        _laser.SetActive(true);
        _particalCollisions.enabled = true;
        _attackCount++;
    }

    private void OnDisable()
    {
        Attacked?.Invoke(false);

        Time.timeScale = TimeScaleNormal;
        _laser.SetActive(false);
        _animator.SetBool(TargetAnimation, false);
    }

    private void Start()
    {        
        _playerShooter = GetComponent<PlayerShooter>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RotateGun();

        _animator.SetBool(TargetAnimation, true);
        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _elapsedTime >= DelayBetweenShoot)
        {
            Attack();
            _elapsedTime = 0;
        }
    }

    private void SetAngle()
    {
        //_spaceShip.eulerAngles = new Vector3(_spaceShip.position.x, 0, 0);
    }

    private void RotateGun()
    {
        if (_direction == LeftRotate)
            _gunPoint.Rotate(Vector3.down, _speed * Time.deltaTime);
        else if (_direction == RightRotate)
            _gunPoint.Rotate(Vector3.up, _speed * Time.deltaTime);
    }

    private void Attack()
    {
        _animator.SetTrigger(ShootAnimation);
        _playerShooter.Shoot();
        _shootFX.Play();
    }
}
