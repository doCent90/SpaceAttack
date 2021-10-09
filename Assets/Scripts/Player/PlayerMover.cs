using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] private float _moveDuration;

    private int _currentPointIndex = 0;

    private Rigidbody _rigidbodySpceShip;

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

    public event UnityAction LastPointCompleted;
    public event UnityAction<bool> Moved;

    private void OnEnable()
    {
        _rigidbodySpceShip = GetComponent<Rigidbody>();

        HasCurrentPositions = false;
        Move();
    }

    private void OnDisable()
    {
        Moved?.Invoke(false);
        HasCurrentPositions = false;
    }

    private void ChangeCurrentIndexPosition()
    {
        if (_currentPointIndex == (_points.Length - 2))
        {
            _currentPointIndex++;
            Move();
            LastPointCompleted?.Invoke();
            HasCurrentPositions = false;
        }
        else
        {
            HasCurrentPositions = true;         
            _currentPointIndex++;
        }
    }

    private void Move()
    {
        if (_currentPointIndex != _points.Length)
        {
            Moved?.Invoke(true);
            var tweenMove = _rigidbodySpceShip.DOMove(_points[_currentPointIndex].position, _moveDuration);
            tweenMove.SetEase(Ease.InBack);
            tweenMove.OnComplete(ChangeCurrentIndexPosition);
        }
    }
}
