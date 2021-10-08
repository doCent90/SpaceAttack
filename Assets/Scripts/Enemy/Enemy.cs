using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodFX;
    [SerializeField] private bool _isNextStageFighter;

    private TargetDieTransition _transition;
    private BackGroundMover _mover;

    public event UnityAction Died;

    public void TakeDamage()
    {
        _bloodFX.Play();

        if (_isNextStageFighter)
        {
            _transition.OnTargetDied();
            _mover.enabled = true;
            Died?.Invoke();
        }

        enabled = false;
    }

    private void OnEnable()
    {
        _transition = FindObjectOfType<TargetDieTransition>();
    }
}
