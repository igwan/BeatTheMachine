using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioClip[] Clips;

    AudioClip nextClip;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetSpeed(0);
    }

    public void StartGameMusic()
    {
        if(nextClip == null)
            return;

        audioSource.clip = nextClip;

        InvokeRepeating("Play", 0f, audioSource.clip.length);
    }

    public void SetSpeed(int speed)
    {
        if(Clips.Length < speed + 1)
            return;

        nextClip = Clips[speed];
    }

    void Play()
    {
        if(nextClip != null)
        {
            CancelInvoke();
            StartGameMusic();
            return;
        }

        audioSource.Play();
    }
}
