using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private PlayerMover _playerMover;
    private CameraMover _cameraMover;
    private Celebration _celebration;

    private void OnEnable()
    {
        _playerMover = FindObjectOfType<PlayerMover>();
        _cameraMover = FindObjectOfType<CameraMover>();
        _celebration = FindObjectOfType<Celebration>();

        _playerMover.LastPointCompleted += EndLevel;
    }

    private void OnDisable()
    {
        _playerMover.LastPointCompleted -= EndLevel;
    }

    private void EndLevel()
    {
        _cameraMover.enabled = false;
        _celebration.enabled = true;
    }
}
