using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _moveDuration;
    [SerializeField] private Transform _targetPoint;

    private Rigidbody _rigidbodySoldier;

    public event UnityAction Finished;

    private void OnEnable()
    {
        _rigidbodySoldier = GetComponent<Rigidbody>();

        Move();
    }

    private void Move()
    {
        var tweenMove = _rigidbodySoldier.DOMove(_targetPoint.position, _moveDuration);
        tweenMove.SetEase(Ease.InOutBack).OnComplete(OnPointComplete);
    }

    private void OnPointComplete()
    {
        Finished?.Invoke();
    }
}
