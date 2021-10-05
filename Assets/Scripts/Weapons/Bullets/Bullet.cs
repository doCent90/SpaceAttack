using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected Player _player;

    protected const float Speed = 30f;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public abstract void Update();

    public abstract void OnTriggerEnter(Collider collision);
}
