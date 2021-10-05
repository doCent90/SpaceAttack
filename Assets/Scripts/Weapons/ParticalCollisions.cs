using UnityEngine;

public class ParticalCollisions : MonoBehaviour
{
    private void OnParticleCollision(GameObject collision)
    {
        if (collision.TryGetComponent(out EnemyShooter enemy))
        {
            enemy.enabled = true;
            enabled = false;
        }
    }
}
