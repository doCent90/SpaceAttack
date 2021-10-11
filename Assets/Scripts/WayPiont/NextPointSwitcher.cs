using UnityEngine;

public class NextPointSwitcher : MonoBehaviour
{
    private Enemy[] _enemies;
    private TargetDieTransition _targetDieTransition;

    private int _enemyCount;

    private void OnEnable()
    {
        _targetDieTransition = FindObjectOfType<TargetDieTransition>();
        FillEnemies();
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEmeyDied;
        }
    }

    private void FillEnemies()
    {
        _enemies = GetComponentsInChildren<Enemy>();

        foreach (var enemy in _enemies)
        {
            enemy.Died += OnEmeyDied;
        }

        _enemyCount = _enemies.Length;
    }

    private void OnEmeyDied()
    {
        _enemyCount--;

        if (_enemyCount <= 0)
            _targetDieTransition.OnTargetDied();
    }
}
