using UnityEngine;

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
    private Transform _idlePositison;

    private const string RunAnimation = "Run";

    public bool IsLastWayPoint { get; private set; }
    public bool HasCurrentPositions { get; private set; }

    private void OnDisable()
    {
        _animator.SetBool(RunAnimation, false);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _idlePositison = transform;

        HasCurrentPositions = false;
        IsLastWayPoint = false;
    }

    private void Update()
    {
        Move();

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

    private void Move()
    {
        if (!IsLastWayPoint)
        {
            transform.rotation = Quaternion.RotateTowards(_idlePositison.rotation, _points[_currentPointIndex].rotation, _rotateSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _points[_currentPointIndex].position, _moveSpeed * Time.deltaTime);

            _animator.SetBool(RunAnimation, true);
            HasCurrentPositions = false;
        }
        else
            _animator.SetBool(RunAnimation, false);
    }
}
