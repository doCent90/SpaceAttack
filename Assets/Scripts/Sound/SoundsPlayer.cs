public class SoundsPlayer : SoundsPlaying
{
    private Player _player;
    private PlayerShooter _playerShooter;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        _playerShooter = GetComponentInParent<PlayerShooter>();

        _player.Died += PlayDeath;
        _playerShooter.ShotFired += PlayShot;
    }

    private void OnDisable()
    {
        _player.Died -= PlayDeath;
        _playerShooter.ShotFired -= PlayShot;
    }
}
