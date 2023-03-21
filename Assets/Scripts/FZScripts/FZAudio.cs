using UnityEngine;

//21.03.23

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
        SetVolumes(FZSave.Float.Get(FZSave.Constants.Options.Sound, 1), FZSave.Float.Get(FZSave.Constants.Options.Music, 1));
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            soundsSource2D.PlayOneShot(audioClip);
        }
    }

    public void SetVolumes(float musicVolume, float audioVolume)
    {
        if (soundsSource2D != null)
            soundsSource2D.volume = audioVolume;
        if (musicSource != null)
            musicSource.volume = musicVolume;
        if (clickSource != null)
            clickSource.volume = audioVolume;
    }
}
