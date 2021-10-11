using UnityEngine;

public class EGA_DemoLasers : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _maxLength;
    [SerializeField] private GameObject _prefabLaser;

    private AttackState _attack;
    private GameObject _instance;
    private Laser _laserScript;
    private bool _isAttack = false;


    private void OnEnable()
    {
        _attack = GetComponentInParent<AttackState>();
        _laserScript = _prefabLaser.GetComponent<Laser>();

        _attack.Fired += Attack;
    }

    private void OnDisable()
    {
        _attack.Fired -= Attack;
    }

    private void Attack(bool isAttack)
    {
        _isAttack = isAttack;
    }

    private void Update()
    {
        if (_isAttack)
        {
            Destroy(_instance);
            _instance = Instantiate(_prefabLaser, _firePoint.transform.position, _firePoint.transform.rotation);
            _instance.transform.parent = transform;
        }
        else if (!_isAttack)
        {
            Destroy(_instance, 1);
        }
    }
}
