using UnityEngine;

public class ParticalCollisions : MonoBehaviour
{
    private void OnParticleCollision(GameObject collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if(enemy.enabled)
                enemy.TakeDamage();
        }
    }
}
