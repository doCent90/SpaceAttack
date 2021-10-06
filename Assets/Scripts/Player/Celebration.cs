using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Celebration : MonoBehaviour
{
    private StateMachinePlayer _machinePlayer;

    private Animator _animator;

    private const string VictoryAnimation = "Victory";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(VictoryAnimation);

        _machinePlayer = GetComponent<StateMachinePlayer>();
        _machinePlayer.enabled = false;
    }
}