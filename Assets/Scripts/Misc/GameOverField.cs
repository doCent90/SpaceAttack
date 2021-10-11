using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOverField : MonoBehaviour
{
    [SerializeField] private Text _text;

    private BackGroundMover _groundMover;
    private GameLevelsLoader _gameLevelsLoader;

    private float _elapsedTime = 0;
    private bool _isLevelDone = false;

    private const string Fail = "fail";
    private const string TimeSpent = "time_spent_fail";

    public event UnityAction Defeated;

    private void OnEnable()
    {
        _gameLevelsLoader = FindObjectOfType<GameLevelsLoader>();
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

            _isLevelDone = true;

            Amplitude.Instance.logEvent(Fail, _gameLevelsLoader.Level);
            Amplitude.Instance.logEvent(TimeSpent, (int)_elapsedTime);
        }
    }

    private void Update()
    {
        if (!_isLevelDone)
            _elapsedTime += Time.deltaTime;
    }
}
