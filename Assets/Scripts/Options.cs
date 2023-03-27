using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public Transform audioButton;

    public static bool isAudioOn = true;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        Time.timeScale = 1;

        isAudioOn = FZSave.Bool.Get(FZSave.Constants.Options.Sound, true);
        SetAudio(false);
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(Constants.Scenes.Menu);
    }

    public void SetAudio(bool reverse)
    {
        if (reverse)
            isAudioOn = !isAudioOn;

        audioButton.GetChild(0).gameObject.SetActive(isAudioOn);
        audioButton.GetChild(1).gameObject.SetActive(!isAudioOn);

        FZSave.Bool.Set(FZSave.Constants.Options.Sound, isAudioOn);
        FZSave.Bool.Set(FZSave.Constants.Options.Music, isAudioOn);
        FZAudio.Manager.SetVolumes(isAudioOn ? 1 : 0, isAudioOn ? 1 : 0);
    }
}
