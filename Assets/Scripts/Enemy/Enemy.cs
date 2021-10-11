using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodFX;
    [SerializeField] private bool _isNextStageFighter;

    private TargetDieTransition _transition;

    public event UnityAction Died;

    public void TakeDamage()
    {
        _bloodFX.Play();

        if (_isNextStageFighter)
        {
            _transition.OnTargetDied();
            Died?.Invoke();
        }

        enabled = false;
    }

    private void OnEnable()
    {
        _transition = FindObjectOfType<TargetDieTransition>();
    }
}
