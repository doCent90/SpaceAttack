using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class MoveState : StatePlayer
{
    private PlayerMover _playerMover;

    private const float TimeScaleNormal = 1f;
    private const float TimeScaleSpeed = 3f;

    private void OnEnable()
    {
        _playerMover = GetComponent<PlayerMover>();
        _playerMover.enabled = true;
    }

    private void OnDisable()
    {
        _playerMover.enabled = false;
    }

    private void Update()
    {
        if (Time.timeScale != 1)
            ResetTimeScale();
        else
            return;
    }

    private void ResetTimeScale()
    {
        float time = Time.timeScale;
        time = Mathf.MoveTowards(time, TimeScaleNormal, TimeScaleSpeed * Time.deltaTime);
        Time.timeScale = time;
    }
}
