using UnityEngine;

public abstract class SoundsPlaying : MonoBehaviour
{
    [SerializeField] protected AudioSource _soundShot;
    [SerializeField] protected AudioSource _soundDeath;

    protected void PlayShot()
    {
        _soundShot.Play();
    }

    protected void PlayDeath()
    {
        _soundDeath.Play();
    }
}
