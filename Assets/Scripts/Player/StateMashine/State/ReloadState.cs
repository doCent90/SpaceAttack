using UnityEngine;

[RequireComponent(typeof(UnityEngine.Animator))]
public class ReloadState : StatePlayer
{
    private UnityEngine.Animator _animator;

    private const string ReloadAnimation = "Reload";

    private void OnEnable()
    {
        _animator.SetTrigger(ReloadAnimation);
    }

    private void Start()
    {
        _animator = GetComponent<UnityEngine.Animator>();
    }
}
