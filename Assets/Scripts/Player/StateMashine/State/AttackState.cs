using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private GameObject _laser;
    [SerializeField] private ParticalCollisions _particalCollisions;
    [SerializeField] private Transform _gunPlace;
    [SerializeField] private Transform _cirlceGunPlace;
    [Header("Settings of Shoot Position")]
    [SerializeField] private float _speedRotate;
    [Header("Targets")]
    [SerializeField] private Enemy[] _targets;

    private PlayerShooter _playerShooter;

    private float _elapsedTime;
    private bool _hasRightEdgeDone;
    private int _indexTarget = 0;

    [SerializeField] private float RightRange = 350;
    [SerializeField] private float LeftRange = 310;

    private const float DelayBetweenShoot = 0.2f;
    private const float TimeScaleNormal = 1f;
    private const float TimeScaleRapid = 0.4f;
    private const float _startAngle = 20f;
    private const float _gunDownAngle = 10f;

    public event UnityAction<bool> Attacked;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        Attacked?.Invoke(true);
        SetStartAngle();

        Time.timeScale = TimeScaleRapid;
        _hasRightEdgeDone = true;

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
        if (_hasRightEdgeDone)
        {
            RotateGunLeft();
        }
        else if (!_hasRightEdgeDone)
        {
            RotateGunRihgt();
        }

        _elapsedTime += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _elapsedTime >= DelayBetweenShoot)
        {
            Attack();
            _elapsedTime = 0;
        }
    }

    private void SetStartAngle()
    {
        _cirlceGunPlace.LookAt(_targets[_indexTarget].transform);
        _cirlceGunPlace.localEulerAngles += new Vector3(0, _startAngle, 0);
        _cirlceGunPlace.localEulerAngles = new Vector3(0, _cirlceGunPlace.localEulerAngles.y, 0);
        _indexTarget++;
    }

    private void RotateGunLeft()
    {
        _gunPlace.localEulerAngles -= new Vector3(0, _speedRotate * Time.deltaTime, 0);

        if (_gunPlace.localEulerAngles.y < LeftRange)
            _hasRightEdgeDone = false;
    }

    private void RotateGunRihgt()
    {
        _gunPlace.localEulerAngles += new Vector3(0, _speedRotate * Time.deltaTime, 0);

        if (_gunPlace.localEulerAngles.y > RightRange)
            _hasRightEdgeDone = true;
    }

    private void Attack()
    {
        Shoted?.Invoke();
        _playerShooter.Shoot();
        _shootFX.Play();
    }
}