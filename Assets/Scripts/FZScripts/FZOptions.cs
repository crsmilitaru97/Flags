using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//08.03.22

public class FZOptions : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider soundSlider;
    public Slider musicSlider;
    public Text versionText;
    [Header("Pause")]
    public GameObject pauseWhite;
    public FZPanel pausePanel;
    public FZPanel optionsPanel;
    public Button resumeButton;

    bool paused;
    Resolution[] resolutions;

    private void Start()
    {
        versionText.text = Application.version;

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(QualitySettings.names.ToList());
        qualityDropdown.RefreshShownValue();

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                     resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width
                  && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        LoadSettings(currentResolutionIndex);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (FZPanelsManager.isPanelActive)
            {
                FZPanelsManager.Manager.CloseAllPanels();
            }
            else if (pausePanel != null && !paused)
            {
                PauseGame();
                resumeButton.Select();
            }
            else
            {
                PressResume();
            }

        }
    }

    public void OpenURL(string URL)
    {
        Application.OpenURL(URL);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        qualityDropdown.value = PlayerPrefs.GetInt("Quality", qualityDropdown.options.Count());

        if (PlayerPrefs.HasKey("Resolution"))
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        else
            resolutionDropdown.value = currentResolutionIndex;

        if (PlayerPrefs.HasKey("Fullscreen"))
        {
            fullscreenToggle.isOn = FZSave.Bool.Get("Fullscreen", true);
        }

        musicSlider.value = FZSave.Float.Get(FZSave.Constants.Options.Music, 1);
        soundSlider.value = FZSave.Float.Get(FZSave.Constants.Options.Sound, 1);
    }

    public void PauseGame()
    {
        paused = true;
        FZPanelsManager.Manager.OpenFixedPanel(pausePanel);
        pauseWhite.SetActive(true);
        Time.timeScale = 0;
    }

    #region Set
    public void SetMusicVolume(float volume)
    {
        if (FZAudio.Manager == null)
            return;

        FZAudio.Manager.musicSource.volume = volume;
        FZSave.Float.Set(FZSave.Constants.Options.Music, musicSlider.value);

    }

    public void SetSoundVolume(float volume)
    {
        if (FZAudio.Manager == null)
            return;

        FZAudio.Manager.clickSource.volume = volume;
        FZAudio.Manager.soundsSource2D.volume = volume;
        FZSave.Float.Set(FZSave.Constants.Options.Sound, soundSlider.value);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        FZSave.Bool.Set("Fullscreen", fullscreenToggle.isOn);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        FZSave.Int.Set("Resolution", resolutionDropdown.value);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        FZSave.Int.Set("Quality", qualityDropdown.value);
    }
    #endregion

    #region PauseMenu 
    public void PressOptions()
    {
        FZPanelsManager.Manager.OpenPanel(optionsPanel);
    }

    public void PressResume()
    {
        paused = false;
        Time.timeScale = 1;
        FZPanelsManager.Manager.CloseFixedPanel(pausePanel);
        pauseWhite.SetActive(false);
    }
    #endregion

    public void ResetAllDataAndQuit()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
