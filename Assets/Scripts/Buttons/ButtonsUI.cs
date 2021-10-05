using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonsUI : MonoBehaviour
{
    [SerializeField] private StartGame _game;
    [SerializeField] private GameLevelsLoader _loadLevel;
    [Header("Buttons")]
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonRetry;
    [SerializeField] private Button _buttonContinue;
    [Header("Event")]
    [SerializeField] private Player _player;
    [Header("Settings")]
    [SerializeField] private GameObject _panelOptions;
    [SerializeField] private Button _openOptions;
    [SerializeField] private Button _closeOptions;
    [Header("Sound")]
    [SerializeField] private SoundsFXSettings _soundMaster;
    [SerializeField] private Button _onSoundButton;
    [SerializeField] private Button _offSoundButton;

    private CelebrationState _victory;

    public event UnityAction Clicked;

    public void StartCurrentLevel()
    {
        _game.StartLevel();
        _buttonStart.gameObject.SetActive(false);
    }

    public void RetryLevel()
    {
        _buttonRetry.gameObject.SetActive(false);
        Clicked?.Invoke();
        _loadLevel.Retry();
    }

    public void NextLevel()
    {
        _buttonContinue.gameObject.SetActive(false);
        Clicked?.Invoke();
        _loadLevel.LoadNext();
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
        _victory = _player.GetComponent<CelebrationState>();
        _player.Died += ShowRetryButton;
        _victory.Victory += ShowContinueButton;

        Init();
    }

    private void OnDisable()
    {
        _player.Died -= ShowRetryButton;
        _victory.Victory -= ShowContinueButton;
    }

    private void Init()
    {
        _panelOptions.SetActive(false);
        _buttonRetry.gameObject.SetActive(false);
        _closeOptions.gameObject.SetActive(false);
        _buttonContinue.gameObject.SetActive(false);
        _onSoundButton.gameObject.SetActive(false);

        if (_soundMaster.IsSoundEnable)
        {
            _onSoundButton.gameObject.SetActive(false);
            _offSoundButton.gameObject.SetActive(true);
        }
        else
        {
            _onSoundButton.gameObject.SetActive(true);
            _offSoundButton.gameObject.SetActive(false);
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
}
