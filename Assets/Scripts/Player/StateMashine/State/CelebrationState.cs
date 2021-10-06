using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CelebrationState : StatePlayer
{
    private Animator _animator;

    private const string VictoryAnimation = "Victory";

    public event UnityAction Won;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger(VictoryAnimation);
        Won?.Invoke();

        Debug.Log("Won");
    }
}