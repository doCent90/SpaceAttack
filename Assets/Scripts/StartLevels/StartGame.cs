using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerMover _playerMover;

    public void StartLevel()
    {
        _player.enabled = true;
        _playerMover.enabled = true;
    }
}
