using UnityEngine;
using UnityEngine.UI;

//02.11.22

public class FZPanel : MonoBehaviour
{
    public FZStory.Message panelInfoMessage;
    public bool subPanel = false;
    public FZPanel[] subPanels;
    public bool canOpen = true;
    public bool opened = false;
    public Button mainButton;

    public void OpenSubPanel(FZPanel subPanel)
    {
        if (!string.IsNullOrEmpty(subPanel.panelInfoMessage.text))
            FZStory.ShowMessageOnce(subPanel.panelInfoMessage);

        subPanel.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void BackToMainPanel()
    {
        this.gameObject.SetActive(true);

        foreach (var subPanel in subPanels)
        {
            subPanel.gameObject.SetActive(false);
        }
    }
}
