using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private GameObject _laser;
    [SerializeField] private ParticalCollisions _particalCollisions;
    [SerializeField] private Transform _gunPlace;
    [Header("Settings of Shoot Position")]
    [SerializeField] private float _speed;
    [Header("Targets")]
    [SerializeField] private Enemy[] _targets;

    private PlayerShooter _playerShooter;

    private float _elapsedTime;
    private int _indexTarget = 0;

    private const int LeftRotate = 1;
    private const int RightRotate = -1;
    private const float DelayBetweenShoot = 0.1f;
    private const float TimeScaleNormal = 1f;
    private const float TimeScaleRapid = 0.4f;
    private const float _startAngle = 20f;

    public event UnityAction<bool> Attacked;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        Attacked?.Invoke(true);
        SetStartAngle();

        Time.timeScale = TimeScaleRapid;

        _laser.SetActive(true);
        _particalCollisions.enabled = true;
    }

    private void OnDisable()
    {
        Attacked?.Invoke(false);

        Time.timeScale = TimeScaleNormal;
        _laser.SetActive(false);
    }

    private void Start()
    {        
        _playerShooter = GetComponent<PlayerShooter>();
    }

    private void Update()
    {
        RotateGun();

        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _elapsedTime >= DelayBetweenShoot)
        {
            Attack();
            _elapsedTime = 0;
        }
    }

    private void SetStartAngle()
    {
        _gunPlace.LookAt(_targets[_indexTarget].transform);
        _gunPlace.eulerAngles += new Vector3(0, _startAngle, 0);
        _indexTarget++;
    }


    private void RotateGun()
    {
        _gunPlace.eulerAngles += new Vector3(0, _startAngle, 0);
    }

    private void RotateGun2()
    {
        _gunPlace.Rotate(Vector3.down, _speed * Time.deltaTime);
    }

    private void Attack()
    {
        Shoted?.Invoke();
        _playerShooter.Shoot();
        _shootFX.Play();
    }
}