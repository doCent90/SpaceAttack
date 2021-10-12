using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyAnimator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Material _dieMaterial;
    [SerializeField] private Color _targetColor;
    [SerializeField] private ParticleSystem _emoji;

    private ParticleSystem[] _particalFX;
    private SkinnedMeshRenderer _renderer;
    private EnemyMover _mover;
    private AttackState _attackStatePlayer;
    private SkinnedMeshRenderer _meshRenderer;
    private Color _currentColor;

    private float _hitPoints = 1f;
    private bool _hasInvisible = false;
    private bool _hasCurrentColor = true;
    private bool _isReady = false;
    private float _elapsedTime = 0;

    private const float _delay = 0.5f;
    private const float _multiply = 0.2f;

    public event UnityAction Died;

    public void TakeDamage(float damage)
    {
        _hitPoints -= damage;
        _currentColor.r = _hitPoints;
        _currentColor.g = _hitPoints * _multiply;

        _emoji.Play();

        if (!_hasInvisible && _hitPoints <= 0)
        {
            Died?.Invoke();
            SetDieMaterial();

            PlayFX();
            _mover.enabled = true;

            _hitPoints = 0;
            _emoji.Stop();
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
