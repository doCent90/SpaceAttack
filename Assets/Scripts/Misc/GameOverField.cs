using UnityEngine;
using UnityEngine.Events;

public class GameOverField : MonoBehaviour
{
    private BackGroundMover _groundMover;

    public event UnityAction Defeated;

    private void OnEnable()
    {
        _groundMover = GetComponentInParent<BackGroundMover>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Defeated?.Invoke();
            _groundMover.enabled = false;
        }
    }
}
