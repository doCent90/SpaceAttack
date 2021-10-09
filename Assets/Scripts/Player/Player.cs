using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(TargetDieTransition))]
[RequireComponent(typeof(AttackState))]
[RequireComponent(typeof(StateMachinePlayer))]
public class Player : MonoBehaviour
{
    private StateMachinePlayer _stateMachine;
    private AttackState _attackState;
    private TargetDieTransition _targetDie;
    private PlayerMover _mover;
    private GameOverField _gameOver;

    public event UnityAction Started;

    private void OnEnable()
    {
        Started?.Invoke();
    }

    private void Start()
    {
        _gameOver = FindObjectOfType<GameOverField>();
        _mover = GetComponent<PlayerMover>();
        _stateMachine = GetComponent<StateMachinePlayer>();
        _attackState = GetComponent<AttackState>();
        _targetDie = GetComponent<TargetDieTransition>();

        _gameOver.Defeated += StopGame;
        _stateMachine.enabled = true;
    }

    private void OnDisable()
    {
        _gameOver.Defeated += StopGame;
    }

    private void StopGame()
    {
        enabled = false;
        _mover.enabled = false;
        _stateMachine.enabled = false;
        _attackState.enabled = false;
        _targetDie.enabled = false;
    }
}
