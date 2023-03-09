using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public Transform audioButton;

    public static bool isAudioOn = true;

    void Start()
    {
        Time.timeScale = 1;
        isAudioOn = FZSave.Bool.Get("isAudioOn", true);
        SetAudio(false);
    }

    public void ResetGameData()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void SetAudio(bool invertBool)
    {
        if (invertBool)
        {
            isAudioOn = !isAudioOn;
        }

        if (isAudioOn)
        {
            isAudioOn = true;
            audioButton.GetChild(0).gameObject.SetActive(true);
            audioButton.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            isAudioOn = false;
            audioButton.GetChild(0).gameObject.SetActive(false);
            audioButton.GetChild(1).gameObject.SetActive(true);
        }
    }
}
