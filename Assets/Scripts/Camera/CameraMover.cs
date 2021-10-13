using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private Player _player;

    private const float Distance = 0.5f;

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();

    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z - Distance);
    }
}
