using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMover : MonoBehaviour
{
    private Enemy _enemy;
    private WayPoint _wayPoint;
    private EnemiesOnPoint _enemiesOnPoint;
    private bool _isDead = false;

    private const int Chance = 99;
    private const float Speed = 4f;
    private const float Duration = 1f;
    private const float Distance = 24f;

    public event UnityAction Sprinted;

    public void SprintForward()
    {
        int strangeNumber = Random.Range(0, 100);

        if(Chance >= strangeNumber && !_isDead && _wayPoint.NumberPoint < _enemiesOnPoint.CountWayPoint)
        {
            Debug.Log("Sprint");
            _enemy.SetTempInvisible(true);
            var tweenMove = transform.DOMoveZ(Distance, Duration).OnComplete(SetEnemyInvis);
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
        _wayPoint = GetComponentInParent<WayPoint>();
        _enemiesOnPoint = GetComponentInParent<EnemiesOnPoint>();

        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
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
