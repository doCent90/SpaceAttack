using System;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Player _player;
    private PlayerMover _playerMover;
    private BackGroundMover _backGroundMover;

    private int _countStartSessions = 0;

    private const string CountSessions = "CountSessions";
    private const string CountDaysGame = "days_in_game";
    private const string GameStart = "game_start";
    private const string RegDay = "reg_day";

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
        SetRegDay();
        SetDaysInGame();
        SetCountSessions();
    }

    private void Init()
    {
        Amplitude amplitude = Amplitude.getInstance();
        amplitude.setServerUrl("https://api2.amplitude.com");
        amplitude.logging = true;
        amplitude.trackSessionEvents(true);
        amplitude.init("????");
    }

    private void SetCountSessions()
    {
        _countStartSessions = PlayerPrefs.GetInt(CountSessions);
        _countStartSessions++;

        PlayerPrefs.SetInt(CountSessions, _countStartSessions);
        Amplitude.Instance.logEvent(GameStart, _countStartSessions);
    }

    private void SetDaysInGame()
    {
        var days = PlayerPrefs.GetInt(CountDaysGame);
        days++;

        PlayerPrefs.SetInt(CountDaysGame, days);
        Amplitude.Instance.logEvent(CountDaysGame, days);
    }

    private void SetRegDay()
    {
        int False = 0;
        int True = 1;

        int day = DateTime.Now.Day;
        int month = DateTime.Now.Month;
        int year = DateTime.Now.Year;

        if (True != PlayerPrefs.GetInt(RegDay))
            return;
        else
        {
            Amplitude.Instance.logEvent(RegDay, day+month+year);
        }

        if (PlayerPrefs.GetInt(RegDay) == False)
            PlayerPrefs.SetInt(RegDay, True);
    }
}
