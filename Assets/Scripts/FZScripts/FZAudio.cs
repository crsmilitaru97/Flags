using UnityEngine;

//08.03.23

public class FZAudio : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource clickSource;
    public AudioSource soundsSource2D;

    public AudioClip textSound;

    public static FZAudio Manager;

    void Awake()
    {
        Manager = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        if (soundsSource2D != null)
            soundsSource2D.volume = FZSave.Float.Get(FZSave.Constants.Options.Music, 1);
        musicSource.volume = FZSave.Float.Get(FZSave.Constants.Options.Sound, 1);
    }

    public void PlaySound(AudioClip audioClip)
    {
        soundsSource2D.PlayOneShot(audioClip);
    }
}
