using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Player _player;
    private PlayerMover _playerMover;
    private BackGroundMover _backGroundMover;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _playerMover = _player.GetComponent<PlayerMover>();
        _backGroundMover = FindObjectOfType<BackGroundMover>();
    }

    public void StartLevel()
    {
        _player.enabled = true;
        _playerMover.enabled = true;
        _backGroundMover.enabled = true;
    }
}
