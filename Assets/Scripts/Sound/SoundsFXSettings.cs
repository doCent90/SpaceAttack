using UnityEngine;
using UnityEngine.Audio;

public class SoundsFXSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _soundMaster;

    private const string MasterVolume = "Master";
    private const float MinVolume = -70f;

    public bool IsSoundEnable { get; private set; }

    private void OnEnable()
    {
        _soundMaster.audioMixer.GetFloat(MasterVolume, out float value);
        if (value <= MinVolume)
            IsSoundEnable = false;
        else
            IsSoundEnable = true;
    }

    public void EnableSound()
    {
        _soundMaster.audioMixer.SetFloat(MasterVolume, 0f);
        IsSoundEnable = true;
    }

    public void DisableSound()
    {
        _soundMaster.audioMixer.SetFloat(MasterVolume, -80f);
        IsSoundEnable = false;
    }

    
}
