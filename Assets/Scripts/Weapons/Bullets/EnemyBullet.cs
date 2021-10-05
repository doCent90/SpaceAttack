using UnityEngine;

public class EnemyBullet : Bullet
{
    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }

    public override void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
    }
}
