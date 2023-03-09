using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//22.11.22

public class FZPanelsManager : MonoBehaviour
{
    public Image blur;
    public Image black;
    public FZPanel[] panels;

    float time = 0.015f;
    public static bool isPanelActive;

    public static FZPanelsManager Manager;
    private void Awake() => Manager = this;


    public void OpenFixedPanel(FZPanel panel)
    {
        panel.gameObject.SetActive(true);
    }

    public void CloseFixedPanel(FZPanel panel)
    {
        panel.gameObject.SetActive(false);
    }


    public void OpenPanel(FZPanel panel)
    {
        if (!panel.canOpen || panel.opened)
            return;

        panel.opened = true;
        if (panel.mainButton != null)
            panel.mainButton.Select();
        if (!string.IsNullOrEmpty(panel.panelInfoMessage.text))
            FZStory.ShowMessageOnce(panel.panelInfoMessage);

        if (isPanelActive)
        {
            CloseAllPanels();
        }
        isPanelActive = true;

        StartCoroutine(SlowBackgroundShow());
        panel.gameObject.SetActive(true);
    }

    public void ClosePanel(FZPanel panel)
    {
        panel.opened = false;
        isPanelActive = false;
        StartCoroutine(SlowBackgroundHide());
        panel.gameObject.SetActive(false);
    }


    public void CloseAllPanels()
    {
        isPanelActive = false;

        foreach (var panel in panels)
        {
            panel.opened = false;
            panel.gameObject.SetActive(false);
            foreach (var subPanel in panel.subPanels)
            {
                subPanel.gameObject.SetActive(false);
            }
        }
        StartCoroutine(SlowBackgroundHide());
    }

    #region Helpers
    private IEnumerator SlowBackgroundShow()
    {
        float i = 0;

        blur.gameObject.SetActive(true);
        black.color = FZColors.ChangeAlpha(black.color, i);
        blur.color = FZColors.ChangeAlpha(blur.color, i);

        while (i <= 1)
        {
            yield return new WaitForSecondsRealtime(time);
            if (i < 0.5f)
                black.color = FZColors.ChangeAlpha(black.color, i);
            blur.color = FZColors.ChangeAlpha(blur.color, i);
            i += 0.1f;
        }
    }
    private IEnumerator SlowBackgroundHide()
    {
        while (black.color.a >= 0)
        {
            yield return new WaitForSecondsRealtime(time);
            black.color = FZColors.ChangeAlpha(black.color, black.color.a - 0.1f);
            blur.color = FZColors.ChangeAlpha(blur.color, blur.color.a - 0.1f);
        }
        blur.gameObject.SetActive(false);
    }
    #endregion
}
