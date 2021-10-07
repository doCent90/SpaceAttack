using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private Enemy _enemy;

    private const string Run = "Run";
    private const string Die = "Die";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();

        _enemy.Player.Started += OnStarted;
        _enemy.Died += OnDied;
    }

    private void OnStarted()
    {
        _animator.SetBool(Run, true);
    }

    private void OnDied()
    {
        _animator.SetBool(Run, false);
        _animator.SetTrigger(Die);
    }
}
