using UnityEngine;
using UnityEngine.Events;

public class AttackState : StatePlayer
{
    [Header("Laser")]
    [SerializeField] private int _laserNumber;
    [SerializeField] private GameObject[] _laser;
    [SerializeField] private GameObject _rayCast;
    [SerializeField] private GameObject _aim;
    [SerializeField] private ParticleSystem _shootFX;
    [Header("Settings of Shot")]
    [SerializeField] private float _speedRotate;
    [SerializeField] private Transform _shootPosition;

    private CirlceGunPlace _cirlceGunPlace;
    private GunPlace _gunPlace;
    private OverHeatBar _overHeat;

    private bool _isOverHeated = false;
    private bool _hasRightEdgeDone;
    private int _indexTarget = 1;

    private GameObject _instance;
    private Hovl_Laser2 _laserPrefab;
    private ButtonsUI _panelUI;

    private const float Delay = 0.3f;
    private const float RightRange = 220f;
    private const float LeftRange = 140f;
    private const float StartPositionCirlceGunPlace = 180f;

    public event UnityAction<bool> ReadyToAttacked;
    public event UnityAction<bool> Fired;
    public event UnityAction Shoted;

    private void OnEnable()
    {
        _panelUI = FindObjectOfType<ButtonsUI>();
        _overHeat = FindObjectOfType<OverHeatBar>();
        _gunPlace = GetComponentInChildren<GunPlace>();
        _cirlceGunPlace = GetComponentInChildren<CirlceGunPlace>();

        _overHeat.OverHeated += ResetAttake;

        ReadyToAttacked?.Invoke(true);
        SetStartAngle();

        _rayCast.SetActive(false);
        _hasRightEdgeDone = true;
        _aim.SetActive(true);
    }

    private void OnDisable()
    {
        _overHeat.OverHeated -= ResetAttake;
        DeactivateLaser(false);

        ReadyToAttacked?.Invoke(false);
        Fired?.Invoke(false);

        _rayCast.SetActive(false);
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

        if (Input.GetMouseButtonDown(0) && !_isOverHeated && !_panelUI.IsPanelOpen)
        {
            Attack(true);
        }

        if(_isOverHeated)
        {
            Attack(false);
        }
    }

    private void SetStartAngle()
    {
        _cirlceGunPlace.transform.localEulerAngles = new Vector3(0, StartPositionCirlceGunPlace, 0);

        if(_indexTarget % 2 == 0)
            _gunPlace.transform.transform.localEulerAngles = new Vector3(0, LeftRange, 0);
        else
            _gunPlace.transform.localEulerAngles = new Vector3(0, RightRange, 0);

        _indexTarget++;
    }

    private void RotateGunLeft()
    {
        _gunPlace.transform.localEulerAngles -= new Vector3(0, _speedRotate * Time.deltaTime, 0);

        if (_gunPlace.transform.localEulerAngles.y < LeftRange)
            _hasRightEdgeDone = false;
    }

    private void RotateGunRihgt()
    {
        _gunPlace.transform.localEulerAngles += new Vector3(0, _speedRotate * Time.deltaTime, 0);

        if (_gunPlace.transform.localEulerAngles.y > RightRange)
            _hasRightEdgeDone = true;
    }

    private void Attack(bool isShooting)
    {
        Fired?.Invoke(isShooting);

        if (isShooting)
        {
            ActivatekLaser();
            Shoted?.Invoke();
            _shootFX.Play();
            _rayCast.SetActive(true);
        }
        else
        {
            DeactivateLaser(isShooting);
            _rayCast.SetActive(false);
        }
    }

    private void ResetAttake(bool isOverHeated)
    {
        _isOverHeated = isOverHeated;
    }

    private void ActivatekLaser()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(_instance);
            _instance = Instantiate(_laser[_laserNumber], _rayCast.transform.position, _rayCast.transform.rotation);

            _instance.transform.parent = _shootPosition;
            _laserPrefab = _instance.GetComponent<Hovl_Laser2>();
        }
    }

    private void DeactivateLaser(bool isReady)
    {
        if (!isReady)
        {
            if (_laserPrefab)
                _laserPrefab.DisablePrepare();

            if (_instance != null)
                _instance.transform.parent = _shootPosition;

            Destroy(_instance, Delay);
        }
    }
}