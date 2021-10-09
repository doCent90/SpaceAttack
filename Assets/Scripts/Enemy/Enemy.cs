using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodFX;
    [SerializeField] private bool _isNextStageFighter;
    [SerializeField] private Material _dieMaterial;
    [SerializeField] private SkinnedMeshRenderer _renderer;

    private TargetDieTransition _transition;
    private EnemyMover _mover;

    public event UnityAction Died;

    public void TakeDamage()
    {
        _bloodFX.Play();
        _mover.enabled = true;
        Died?.Invoke();
        _renderer.material = _dieMaterial;

        if (_isNextStageFighter)
        {
            _transition.OnTargetDied();
        }

        enabled = false;
    }

    private void OnEnable()
    {
        _transition = FindObjectOfType<TargetDieTransition>();
        _mover = GetComponent<EnemyMover>();
    }
}
