using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] private float _moveDuration;

    private int _currentPointIndex = 0;

    private Animator _animator;
    private Rigidbody _rigidbodySpceShip;

    private const string RunAnimation = "Run";

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

    public UnityAction LastPointCompleted;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _rigidbodySpceShip = GetComponent<Rigidbody>();

        HasCurrentPositions = false;
        Move();
    }

    private void OnDisable()
    {
        _animator.SetBool(RunAnimation, false);

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

            Debug.Log("PreLastPoint");
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
            _animator.SetBool(RunAnimation, true);

            var tweenMove = _rigidbodySpceShip.DOMove(_points[_currentPointIndex].position, _moveDuration);
            tweenMove.SetEase(Ease.InOutBack).OnComplete(ChangeCurrentIndexPosition);
        }
    }
}
