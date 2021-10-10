using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [Header("Laser")]
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject _aim;
    [SerializeField] private ParticleSystem _shootFX;
    [Header("Gun Place")]
    [SerializeField] private Transform _gunPlace;
    [SerializeField] private Transform _cirlceGunPlace;
    [Header("Settings of Shot")]
    [SerializeField] private float _speedRotate;

    private OverHeatBar _overHeat;
    private bool _isOverHeated = false;
    private bool _hasRightEdgeDone;
    private int _indexTarget = 1;

    private const float RightRange = 210f;
    private const float LeftRange = 145f;
    private const float StartPositionCirlceGunPlace = 180f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _overHeat = FindObjectOfType<OverHeatBar>();
        _overHeat.OverHeated += ResetAttake;

        ReadyToAttacked?.Invoke(true);
        SetStartAngle();

        _laser.SetActive(false);
        _hasRightEdgeDone = true;
        _aim.SetActive(true);
    }

    private void OnDisable()
    {
        _overHeat.OverHeated -= ResetAttake;

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _laser.SetActive(false);
        _aim.SetActive(false);
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

        if (Input.GetMouseButton(0) && !_isOverHeated)
            Attack(true);
        else
            Attack(false);
    }

    private void SetStartAngle()
    {
        _cirlceGunPlace.localEulerAngles = new Vector3(0, StartPositionCirlceGunPlace, 0);

        if(_indexTarget % 2 == 0)
            _gunPlace.localEulerAngles = new Vector3(0, LeftRange, 0);
        else
            _gunPlace.localEulerAngles = new Vector3(0, RightRange, 0);

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

    private void Attack(bool isShooting)
    {
        Fired?.Invoke(isShooting);

        if (isShooting)
        {
            Shoted?.Invoke();
            _shootFX.Play();
            _laser.SetActive(true);
        }
        else
        {
            _laser.SetActive(false);
        }
    }

    private void ResetAttake(bool isOverHeated)
    {
        _isOverHeated = isOverHeated;
    }
}