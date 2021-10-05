public class SoundsEnemy : SoundsPlaying
{
    private Enemy _enemy;
    private EnemyShooter _enemyShooter;

    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _enemyShooter = GetComponentInParent<EnemyShooter>();

        _enemy.Died += PlayDeath;
        _enemyShooter.ShotFired += PlayShot;
    }

    private void OnDisable()
    {
        _enemy.Died -= PlayDeath;
        _enemyShooter.ShotFired -= PlayShot;
    }
}
