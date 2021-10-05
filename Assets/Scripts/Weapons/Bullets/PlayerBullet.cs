using UnityEngine;

public class PlayerBullet : Bullet
{
    public override void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage();
            Destroy(gameObject);
        }
    }

    public override void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }
}
