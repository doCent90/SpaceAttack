public class SoundsPlayer : SoundsPlaying
{
    private PlayerMover _player;
    private PlayerShooter _playerShooter;

    private void Start()
    {
        _player = GetComponentInParent<PlayerMover>();
        _playerShooter = GetComponentInParent<PlayerShooter>();

        _playerShooter.ShotFired += Shot;
        _player.Moved += Fly;
    }

    private void OnDisable()
    {
        _playerShooter.ShotFired -= Shot;
        _player.Moved -= Fly;
    }
}
