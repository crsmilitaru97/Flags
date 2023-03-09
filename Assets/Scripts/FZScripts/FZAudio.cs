using UnityEngine;

//07.12.22

public class FZAudio : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource clickSource;
    public AudioSource soundsSource;

    public AudioClip textSound;

    public static FZAudio Manager;

    void Awake()
    {
        Manager = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        soundsSource.volume = FZSave.Float.Get(FZSave.Constants.Options.Music, 1);
        musicSource.volume = FZSave.Float.Get(FZSave.Constants.Options.Sound, 1);
    }

    public void PlaySound(AudioClip audioClip)
    {
        soundsSource.PlayOneShot(audioClip);
    }
}
