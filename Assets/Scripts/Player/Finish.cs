using UnityEngine;

public class Finish : MonoBehaviour
{
    private StateMachinePlayer _machinePlayer;

    private void OnEnable()
    {
        _machinePlayer = GetComponent<StateMachinePlayer>();
        _machinePlayer.enabled = false;
    }
}