using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerShooter))]
public class Player : MonoBehaviour
{
    private StateMachinePlayer _stateMachinePlayer;

    public event UnityAction Started;

    private void OnEnable()
    {
        Started?.Invoke();
    }

    private void Start()
    {
        _stateMachinePlayer = GetComponent<StateMachinePlayer>();
        _stateMachinePlayer.enabled = true;
    }
}
