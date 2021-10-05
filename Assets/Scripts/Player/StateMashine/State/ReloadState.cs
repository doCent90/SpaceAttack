using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ReloadState : StatePlayer
{
    private Animator _animator;

    private const string ReloadAnimation = "Reload";

    private void OnEnable()
    {
        _animator.SetTrigger(ReloadAnimation);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
}
