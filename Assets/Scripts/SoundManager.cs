using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(MusicTempo))]
public class SoundManager : Singleton<SoundManager>
{
    public AudioClip deathScream;
    public AudioClip gameOver;
    public AudioClip titleScreen;
    public AudioClip mainTheme;

    AudioClip nextClip;

    public AudioSource musicSource;
    public AudioSource FXSource;

    [HideInInspector] public MusicTempo musicTempo;

    void Awake()
    {
        musicTempo = GetComponent<MusicTempo>();
    }

    public void PlaySound(AudioClip clip)
    {
        FXSource.clip = clip;
        FXSource.Play();
    }

    public void StopSound()
    {
        FXSource.Stop();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
