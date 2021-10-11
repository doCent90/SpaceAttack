using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverField : MonoBehaviour
{
    [SerializeField] Text _text;

    private BackGroundMover _groundMover;

    public event UnityAction Defeated;

    private void OnEnable()
    {
        _groundMover = GetComponentInParent<BackGroundMover>();
        _text.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Defeated?.Invoke();
            _groundMover.enabled = false;
            _text.enabled = true;
        }
    }
}
