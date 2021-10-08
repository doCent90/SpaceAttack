using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [SerializeField] private ParticleSystem _shootFX;
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _gun;
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


    private const float RightRange = 359;
    private const float LeftRange = 330;
    private const float StartAngle = 15;
    private const float DelayBetweenShoot = 0.2f;
    private const float TimeScaleRapid = 0.3f;
    private const float TimeScaleSpeed = 3f;

    public event UnityAction<bool> Attacked;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        Attacked?.Invoke(true);
        SetStartAngle();

        _hasRightEdgeDone = true;
        _laser.SetActive(true);
    }

    private void OnDisable()
    {
        Attacked?.Invoke(false);
        _laser.SetActive(false);
        ResetAngle();
    }

    private void Start()
    {        
        _playerShooter = GetComponent<PlayerShooter>();
    }

    private void Update()
    {
        if (Time.timeScale != TimeScaleRapid)
            SlowDownTime();
        else
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
    }

    private void SlowDownTime()
    {
        float time = Time.timeScale;
        time = Mathf.MoveTowards(time, TimeScaleRapid, TimeScaleSpeed * Time.deltaTime);
        Time.timeScale = time;
    }

    private void SetStartAngle()
    {
        Transform positions = _cirlceGunPlace;
        positions.LookAt(_targets[_indexTarget].transform);

        _cirlceGunPlace.localEulerAngles = new Vector3(0, positions.localEulerAngles.y, 0);
        _cirlceGunPlace.localEulerAngles += new Vector3(0, StartAngle, 0);

        _indexTarget++;
    }

    private void ResetAngle()
    {
        _cirlceGunPlace.localEulerAngles = new Vector3(0, 0, 0);
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