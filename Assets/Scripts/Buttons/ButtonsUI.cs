using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonsUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonRetry;
    [SerializeField] private Button _buttonContinue;
    [Header("Settings")]
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private Button _openOptions;
    [SerializeField] private Button _closeOptions;
    [Header("Sound")]
    [SerializeField] private Button _onSoundButton;
    [SerializeField] private Button _offSoundButton;

    private SoundsFXSettings _soundMaster;
    private GameLevelsLoader _loadLevel;
    private GameOverField _gameOver;
    private PlayerMover _playerMover;
    private StartGame _game;
    private Player _player;

    private float _elapsedTime = 0;
    private bool _isLevelDone = false;

    private const string LevelComplete = "level_complete";
    private const string TimeSpent = "time_spent_lvl_complete";
    private const string Restart = "restart";

    public event UnityAction Clicked;

    public void StartCurrentLevel()
    {
        _game.StartLevel();
        _buttonStart.gameObject.SetActive(false);
    }

    public void RetryLevel()
    {
        _buttonRetry.gameObject.SetActive(false);
        _loadLevel.Retry();
        Clicked?.Invoke();

        Amplitude.Instance.logEvent(Restart, _loadLevel.Level);
    }

    public void NextLevel()
    {
        _buttonContinue.gameObject.SetActive(false);
        _loadLevel.LoadNext();
        Clicked?.Invoke();

        _isLevelDone = true;

        Amplitude.Instance.logEvent(LevelComplete, _loadLevel.Level);
        Amplitude.Instance.logEvent(TimeSpent, (int)_elapsedTime);
    }

    public void OpenSettings()
    {
        Time.timeScale = 0;
        _openOptions.gameObject.SetActive(false);
        _closeOptions.gameObject.SetActive(true);
        _panelOptions.SetActive(true);
        Clicked?.Invoke();
    }

    public void CloseSettings()
    {
        Time.timeScale = 1;
        _openOptions.gameObject.SetActive(true);
        _closeOptions.gameObject.SetActive(false);
        _panelOptions.SetActive(false);
        Clicked?.Invoke();
    }

    public void EnableSound()
    {
        _soundMaster.EnableSound();

        _onSoundButton.gameObject.SetActive(false);
        _offSoundButton.gameObject.SetActive(true);
        Clicked?.Invoke();
    }

    public void DisableSound()
    {
        _soundMaster.DisableSound();

        _onSoundButton.gameObject.SetActive(true);
        _offSoundButton.gameObject.SetActive(false);
        Clicked?.Invoke();
    }

    private void OnEnable()
    {
        _player = FindObjectOfType<Player>();
        _game = FindObjectOfType<StartGame>();
        _gameOver = FindObjectOfType<GameOverField>();
        _loadLevel = FindObjectOfType<GameLevelsLoader>();
        _playerMover = _player.GetComponent<PlayerMover>();
        _soundMaster = FindObjectOfType<SoundsFXSettings>();

        _playerMover.LastPointCompleted += ShowContinueButton;
        _gameOver.Defeated += ShowRetryButton;

        Init();
    }

    private void OnDisable()
    {
        _playerMover.LastPointCompleted -= ShowContinueButton;
        _gameOver.Defeated -= ShowRetryButton;
    }

    private void Init()
    {
        _panelOptions.SetActive(false);
        _buttonRetry.gameObject.SetActive(false);
        _closeOptions.gameObject.SetActive(false);
        _buttonContinue.gameObject.SetActive(false);

        if (_soundMaster.IsSoundEnable)
        {
            _onSoundButton.gameObject.SetActive(true);
            _offSoundButton.gameObject.SetActive(false);
        }
        else
        {
            _onSoundButton.gameObject.SetActive(false);
            _offSoundButton.gameObject.SetActive(true);
        }
    }
    
    private void ShowRetryButton()
    {
        _buttonRetry.gameObject.SetActive(true);
    }

    private void ShowContinueButton()
    {
        _buttonContinue.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(!_isLevelDone)
            _elapsedTime += Time.deltaTime;
    }
}
