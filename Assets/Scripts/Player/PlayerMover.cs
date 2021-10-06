using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] Transform[] _points;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private int _currentPointIndex = 0;
    private Animator _animator;
    private Rigidbody _rigidbodySpceShip;

    private const string RunAnimation = "Run";

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _rigidbodySpceShip = GetComponent<Rigidbody>();

        HasCurrentPositions = false;
        IsLastWayPoint = false;

        Move();
    }

    private void OnDisable()
    {
        _animator.SetBool(RunAnimation, false);
    }

    private void Update()
    {
        if (transform.position == _points[_currentPointIndex].position)
        {
            _currentPointIndex++;
            HasCurrentPositions = true;
        }

        if (_currentPointIndex == (_points.Length - 1))
        {
            IsLastWayPoint = true;
        }
    }

    private void Rotate()
    {
        if (!IsLastWayPoint)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _points[_currentPointIndex].rotation, _rotateSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        if (!IsLastWayPoint)
        {
            _animator.SetBool(RunAnimation, true);
            HasCurrentPositions = false;

            _rigidbodySpceShip.DOMove(_points[_currentPointIndex].position, _moveSpeed);
        }
    }
}
