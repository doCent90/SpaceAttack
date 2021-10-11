using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverField : MonoBehaviour
{
    [SerializeField] private Text _text;

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
            var enemies = FindObjectsOfType<EnemyMover>();
            foreach (var stickman in enemies)
            {
                stickman.enabled = false;
            }

            Defeated?.Invoke();
            _groundMover.enabled = false;
            _text.enabled = true; 
        }
    }
}
