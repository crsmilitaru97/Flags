using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public static bool firstPlay;

    public GameObject moreTile;

    public GameObject optionsTile;
    public GameObject statsTile;
    public GameObject priceGroup;

    public GameObject pointsTip;
    public GameObject[] locks;
    public Animator[] panels;
    public Animator menuPanel;

    public Text priceText;
    public FZButton unlockButton;

    public static bool isOptionsShown = false;

    int selectedToBuy;
    readonly int[] prices = new int[] { 150, 350, 900 };

    void Start()
    {
        firstPlay = FZSave.Bool.Get(FZSave.Constants.IsFirstPlay, true);
        FZSave.Bool.Set(FZSave.Constants.IsFirstPlay, false);

        if (firstPlay)
        {
            ShowTip(pointsTip);
        }

        //Save
        for (int i = 0; i < locks.Length; i++)
        {
            locks[i].SetActive(FZSave.Bool.Get("lock" + i.ToString(), true));
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (priceGroup.activeSelf && priceGroup.GetComponent<Animator>().GetBool("shown"))
                priceGroup.GetComponent<Animator>().SetBool("shown", false);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            foreach (var panel in panels)
            {
                if (panel.GetBool("shown"))
                {
                    panel.SetBool("shown", false);
                    menuPanel.SetBool("shown", true);
                    break;
                }
            }
        }
    }

    #region Menu Buttons
    public void StartGame(int dif)
    {
        FZCanvas.Instance.FadeLoadScene(Constants.Scenes.Game, Color.white);
        FZSave.Int.Set("gameDif", dif);
    }

    public void Learn()
    {
        FZCanvas.Instance.FadeLoadScene(Constants.Scenes.Learn, Color.white);
    }

    public void ShowMenuWithAnim(GameObject tile)
    {
        tile.GetComponent<Animator>().SetBool("shown", true);
    }

    public void HideMenuWithAnim(GameObject tile)
    {
        tile.GetComponent<Animator>().SetBool("shown", false);
    }
    #endregion

    #region More Buttons
    public void ShowMore()
    {
        moreTile.GetComponent<Animator>().SetBool("shown", !moreTile.GetComponent<Animator>().GetBool("shown"));
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
    #endregion

    #region Unlock Buttons
    public void ShowPrice(int index)
    {
        selectedToBuy = index;
        priceText.text = prices[selectedToBuy].ToString();
        unlockButton.interactable = Values.points >= prices[selectedToBuy];
        priceGroup.SetActive(true);
        priceGroup.GetComponent<Animator>().SetBool("shown", true);
    }

    public void Unlock()
    {
        Values.AddPoints(-prices[selectedToBuy]);
        locks[selectedToBuy].SetActive(false);
        FZSave.Bool.Set("lock" + selectedToBuy.ToString(), false);
        priceGroup.GetComponent<Animator>().SetBool("shown", false);
    }
    #endregion

    #region Tips
    public void ShowTip(GameObject tip)
    {
        tip.SetActive(true);
        tip.GetComponent<Animator>().SetBool("shown", true);
        StartCoroutine(TimerTip(tip));
    }

    private IEnumerator TimerTip(GameObject tip)
    {
        yield return new WaitForSeconds(2);
        tip.GetComponent<Animator>().SetBool("shown", false);
    }
    #endregion
}
