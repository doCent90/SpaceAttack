using UnityEngine;
using UnityEngine.UI;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private Text _winText;
    [SerializeField] private EnemyMover _lastEnemy;

    private PlayerMover _playerMover;
    private CameraMover _cameraMover;
    private PlayerFinish _celebration;

    private void OnEnable()
    {
        _playerMover = FindObjectOfType<PlayerMover>();
        _cameraMover = FindObjectOfType<CameraMover>();
        _celebration = FindObjectOfType<PlayerFinish>();

        _playerMover.LastPointCompleted += EndLevel;
    }

    private void OnDisable()
    {
        _playerMover.LastPointCompleted -= EndLevel;
    }

    private void EndLevel()
    {
        _lastEnemy.enabled = false;
        _cameraMover.enabled = false;
        _celebration.enabled = true;
        _winText.enabled = true;
    }
}
