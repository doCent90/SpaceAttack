using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Player _player;
    private PlayerMover _playerMover;
    private BackGroundMover _backGroundMover;
    private Dictionary<string, int> _session = new Dictionary<string, int>();
    private int _countStartSessions = 0;

    private const string CountSessions = "CountSessions";
    private const string GameStart = "game_start";

    public void StartLevel()
    {
        _player.enabled = true;
        _playerMover.enabled = true;
        _backGroundMover.enabled = true;
    }

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _playerMover = _player.GetComponent<PlayerMover>();
        _backGroundMover = FindObjectOfType<BackGroundMover>();
    }

    private void Start()
    {
        Init();

        _countStartSessions = PlayerPrefs.GetInt(CountSessions);
        _countStartSessions++;

        _session.Add(CountSessions, _countStartSessions);

        PlayerPrefs.SetInt(CountSessions, _countStartSessions);
        Amplitude.Instance.logEvent(GameStart, _session[CountSessions]);

        _session.Clear();
    }

    private void Init()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init("????");
    }
}
