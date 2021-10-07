using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyShooter))]
[RequireComponent(typeof(UnityEngine.Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodFX;
    [SerializeField] private bool _isNextStageFighter;

    private TargetDieTransition _transition;
    private UnityEngine.Animator _animator;
    private Player _player;

    private const string DieAnimation = "Die";
    private const string VictoryAnimation = "Victory";

    public event UnityAction Died;

    public void TakeDamage()
    {
        _bloodFX.Play();
        _animator.SetTrigger(DieAnimation);

        if (_isNextStageFighter)
        {
            _transition.OnTargetDied();
            Died?.Invoke();
        }

        enabled = false;
    }

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _animator = GetComponent<UnityEngine.Animator>();

        _player.Died += PlayVictoryAnimation;
    }

    private void Start()
    {
        _transition = FindObjectOfType<TargetDieTransition>();
    }

    private void OnDisable()
    {
        _player.Died -= PlayVictoryAnimation;
    }

    private void PlayVictoryAnimation()
    {
        _animator.SetTrigger(VictoryAnimation);
    }
}
