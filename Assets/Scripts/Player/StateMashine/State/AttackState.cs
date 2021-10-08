using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [SerializeField] private GameObject _laserPlace;
    [SerializeField] private ParticleSystem _laser;
    [SerializeField] private ParticleSystem _shootFX;

    [SerializeField] private Transform _gun;
    [SerializeField] private Transform _gunPlace;
    [SerializeField] private Transform _cirlceGunPlace;
    [Header("Settings of Shoot Position")]
    [SerializeField] private float _speedRotate;
    [Header("Targets")]
    [SerializeField] private Enemy[] _targets;

    private bool _hasRightEdgeDone;
    private int _indexTarget = 0;

    [SerializeField] private float RightRange;
    [SerializeField] private float LeftRange;

    private const float StartAngle = 15;

    public event UnityAction<bool> Attacked;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        Attacked?.Invoke(true);
        SetStartAngle();

        _laserPlace.SetActive(false);
        _hasRightEdgeDone = true;
    }

    private void OnDisable()
    {
        Attacked?.Invoke(false);
        ResetAngle();
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

        if (Input.GetMouseButton(0))
        {
            Attack(true);
        }
        else
            Attack(false);
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
        _cirlceGunPlace.localEulerAngles = new Vector3(0, 45, 0);
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

    private void Attack(bool isShooting)
    {
        if (isShooting)
        {
            Shoted?.Invoke();
            _shootFX.Play();
            _laserPlace.SetActive(true);
            _laser.Play();
        }
        else
        {
            _laserPlace.SetActive(false);
            _laser.Stop();
        }
    }
}