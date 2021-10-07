using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodFX;
    [SerializeField] private bool _isNextStageFighter;

    private TargetDieTransition _transition;
    private EnemyMover _mover;
    private Player _player;

    public Player Player => _player;

    public event UnityAction Died;

    public void TakeDamage()
    {
        _bloodFX.Play();

        if (_isNextStageFighter)
        {
            _transition.OnTargetDied();
            Died?.Invoke();
        }

        _mover.enabled = false;
        enabled = false;
    }

    private void OnEnable()
    {
        _transition = FindObjectOfType<TargetDieTransition>();
        _mover = GetComponent<EnemyMover>();
        _player = FindObjectOfType<Player>();
        _player.Started += Init;
    }

    private void OnDisable()
    {
        _player.Started -= Init;
    }

    private void Init()
    {
        _mover.enabled = true;
    }
}
