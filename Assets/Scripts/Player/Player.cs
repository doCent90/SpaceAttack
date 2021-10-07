using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerShooter))]
public class Player : MonoBehaviour
{
    private PlayerMover _playerMover;
    private AttackState _attackState;
    private StateMachinePlayer _stateMachinePlayer;

    public event UnityAction Died;

    public void TakeDamage()
    {
        _playerMover.enabled = false;
        _attackState.enabled = false;
        Died?.Invoke();
    }

    private void Start()
    {
        _playerMover = GetComponent<PlayerMover>();
        _attackState = GetComponent<AttackState>();
        _stateMachinePlayer = GetComponent<StateMachinePlayer>();

        _stateMachinePlayer.enabled = true;
    }
}
