using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerShooter))]
public class Player : MonoBehaviour
{
    private Animator _animator;
    private PlayerMover _playerMover;
    private AttackState _attackState;
    private StateMachinePlayer _stateMachinePlayer;

    private const string DieAnimation = "Die";

    public event UnityAction Died;

    public void TakeDamage()
    {
        _playerMover.enabled = false;
        _attackState.enabled = false;
        _animator.SetTrigger(DieAnimation);
        Died?.Invoke();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMover = GetComponent<PlayerMover>();
        _attackState = GetComponent<AttackState>();
        _stateMachinePlayer = GetComponent<StateMachinePlayer>();

        _stateMachinePlayer.enabled = true;
    }
}
