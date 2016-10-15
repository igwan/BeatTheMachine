using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] Clips;

    AudioClip nextClip;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SetSpeed(0);
    }

    void StartGameMusic()
    {
        if(nextClip == null)
            return;

        AudioSource.clip = nextClip;

        InvokeRepeating("Play", 0f, audioSource.clip.length);
    }

    public void SetSpeed(GameSpeed speed)
    {
        nextClip = Clips[(int)speed];
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
