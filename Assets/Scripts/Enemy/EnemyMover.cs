using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] private bool _isLastQueuEnemy;

    private WayPoint _enemiesPoint;
    private Enemy[] _aliveEnemies;
    private Enemy _enemy;
    private bool _isDead = false;
    private bool _hasSprinted = false;

    private const int Chance = 90;
    private const float Speed = 4f;
    private const float Duration = 2f;
    private const float Distance = 12f;

    public event UnityAction Sprinted;

    public void SprintForward()
    {
        int strangeNumber = Random.Range(0, 100);

        if(Chance >= strangeNumber && !_isDead && !_isLastQueuEnemy && !_hasSprinted)
        {
            _enemy.SetTempInvisible(true);
            transform.DOMoveZ(transform.position.z + Distance, Duration).OnComplete(SetEnemyInvis);
            _hasSprinted = true;
            Sprinted?.Invoke();
        }
    }

    private void SetEnemyInvis()
    {
        _enemy.SetTempInvisible(false);
    }

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _enemiesPoint = GetComponentInParent<WayPoint>();
        _aliveEnemies = _enemiesPoint.GetComponentsInChildren<Enemy>();

        _enemy.Died += OnDied;

        foreach (var enemy in _aliveEnemies)
        {
            enemy.Died += SprintForward;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _aliveEnemies)
        {
            enemy.Died -= SprintForward;
        }

        _enemy.Died -= OnDied;
    }

    private void OnDied()
    {
        _isDead = true;
    }

    private void Update()
    {
        if (!_isDead)
            return;
        else
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
    }
}
