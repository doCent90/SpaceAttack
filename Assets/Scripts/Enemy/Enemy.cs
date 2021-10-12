using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Material _dieMaterial;
    [SerializeField] private Color _targetColor;

    private ParticleSystem[] _particalFX;
    private SkinnedMeshRenderer _renderer;
    private EnemyMover _mover;
    private AttackState _attackStatePlayer;
    private SkinnedMeshRenderer _meshRenderer;
    private Color _currentColor;

    private bool _hasInvisible = false;
    private bool _hasCurrentColor = true;
    private bool _isReady = false;
    private float _elapsedTime = 0;

    private const float _delay = 0.5f;

    public event UnityAction Died;

    public void TakeDamage()
    {
        if (!_hasInvisible)
        {
            Died?.Invoke();
            SetDieMaterial();

            PlayFX();
            _mover.enabled = true;

            enabled = false;
        }
    }

    public void SetTempInvisible(bool hasInvis)
    {
        _hasInvisible = hasInvis;
    }

    private void PlayFX()
    {
        foreach (var partical in _particalFX)
        {
            partical.Play();
        }
    }

    private void OnEnable()
    {
        _mover = GetComponent<EnemyMover>();
        _attackStatePlayer = FindObjectOfType<AttackState>();
        _meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _particalFX = GetComponentsInChildren<ParticleSystem>();
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();

        _attackStatePlayer.ReadyToAttacked += OnReadyToAttack;
        _currentColor = _meshRenderer.material.color;

    }

    private void OnDisable()    
    {
        _attackStatePlayer.ReadyToAttacked -= OnReadyToAttack;
    }

    private void OnReadyToAttack(bool isReady)
    {
        _isReady = isReady;
    }

    private void SetDieMaterial()
    {
        _renderer.material = _dieMaterial;
    }

    private void ChangeMaterialColor()
    {
        _meshRenderer.material.color = _targetColor;
    }

    private void ResetMaterialColor()
    {
        _meshRenderer.material.color = _currentColor;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (!_isReady)
        {
            if (!_hasCurrentColor)
                ResetMaterialColor();

            return;
        }
        else
        {
            if (_hasCurrentColor && _elapsedTime > _delay)
            {
                ChangeMaterialColor();
                _hasCurrentColor = false;
                _elapsedTime = 0;
            }
            else if (!_hasCurrentColor && _elapsedTime > _delay)
            {
                ResetMaterialColor();
                _hasCurrentColor = true;
                _elapsedTime = 0;
            }
        }
    }
}
