using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private GameOverField _gameOverField;
    private Enemy _enemy;
    private Player _player;

    private const string Run = "Run";
    private const string Die = "Die";
    private const string Victory = "Victory";

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _player = FindObjectOfType<Player>();

        _player.Started += OnStarted;
        _enemy.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Started -= OnStarted;
        _enemy.Died -= OnDied;
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

    private void Celebrate()
    {
        _animator.SetTrigger(Victory);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out GameOverField gameOverField))
        {
            _animator.SetBool(Run, false);
            Celebrate();
        }
    }
}
