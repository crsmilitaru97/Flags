using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Constants;

public class GameManager : MonoBehaviour
{
    public Text titleGameType;
    public Text countryName;
    public Image countryFlag;
    public GameObject upTile;
    public GameObject downTile;
    public GameObject timeTile;
    public GameObject main;

    public GameObject up_flagGroup;
    public GameObject up_uncoloredFlagGroup;
    public Image[] uncoloredParts;

    public FZButton[] responseButtons;
    public FZButton[] colorButtons;
    public FZButton[] symbolButtons;

    public Button[] hearts;
    public GameObject gameoverTile;
    public GameObject pauseTile;
    public Text finalScore;
    public GameObject highscoreMessage;
    public ParticleSystem responseParticles;
    //UI
    public Color[] UIColors;
    public Image[] shadowsList;
    public Image timeFill;
    string[] gameTypes = { "whichCountryFlag", "canYouDraw", "missingPart" };
    float time = 1;

    public Color colorTrue;
    public Color colorFalse;
    public Color colorOthers;
    public static bool timerResponseRunning;

    int heartsUsed;
    public static bool showCorrectAnswerAfter;
    public static string category = "Flags/world/";

    public GameObject down_colorTile, down_responseTile, down_symbolTile;

    public static Flag currentFlag;
    public static int currentResponseIndex;
    List<int> UsedFlagsIndexes = new List<int>();
    enum GameTypes { Guess, Color, Symbol };
    public static int selectedSymbolIndex;
    public static int mustGameType = -1;
    int completedParts = 0;

    public List<Flag> CurrentListOfFlags = new List<Flag>();

    #region Basic Events
    private void Start()
    {
        AdsManager.Instance.LoadBannerAd();

        UsedFlagsIndexes.Clear();

        heartsUsed = 0;
        Time.timeScale = 1;

        //Set mode
        int gameType = FZSave.Int.Get("mustGameType", -1);
        int level = FZSave.Int.Get("gameDif", 1);
        foreach (var flag in FlagsManager.Manager.Flags)
        {
            if (flag.level == level)
            {
                CurrentListOfFlags.Add(flag);
            }
        }

        LoadNewFlag();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            main.GetComponent<Animator>().SetBool("shown", false);
            Time.timeScale = 0;
            pauseTile.SetActive(true);
        }
    }
    #endregion

    #region User Events - Response
    public void SelectResponse(int index)
    {
        timerResponseRunning = false;
        bool isCorrectAnswer = responseButtons[index].buttonText.text == currentFlag.name;

        HighlightResponse(index, isCorrectAnswer, true, true, responseButtons);

        if (isCorrectAnswer)
        {
            timeTile.SetActive(false);
            countryName.text = currentFlag.name;

            Values.AddPoints(10);
        }
        FinalAnswer(isCorrectAnswer);
    }

    public void SelectColor(int index)
    {
        Color selectedColor = colorButtons[index].buttonImage.color;

        colorButtons[index].interactable = false;

        bool isCorrectAnswer = false;
        for (int i = 0; i < currentFlag.colorParts.Length; i++)
        {
            if (currentFlag.colorParts[i].color == selectedColor)
            {
                isCorrectAnswer = true;
                uncoloredParts[i].sprite = currentFlag.colorParts[i].part;
                uncoloredParts[i].gameObject.SetActive(true);
                completedParts++;
                Values.AddPoints(5);
            }
        }


        if (isCorrectAnswer)
        {
            if (completedParts == currentFlag.colorParts.Length)
            {

                StartCoroutine(TimerAfterResponse());
                HighlightResponse(index, isCorrectAnswer, true, true, colorButtons);
                countryFlag.sprite = currentFlag.sprite;

                FinalAnswer(isCorrectAnswer);
            }
            else
                HighlightResponse(index, isCorrectAnswer, false, false, colorButtons);
        }
        else
        {
            HighlightResponse(index, false, true, true, colorButtons);
            FinalAnswer(isCorrectAnswer);
        }
    }

    public void SelectSymbol(int index)
    {
        bool isCorrectAnswer = symbolButtons[index].buttonImage.sprite == currentFlag.symbol;

        HighlightResponse(index, isCorrectAnswer, true, true, symbolButtons);


        if (isCorrectAnswer)
        {
            Values.AddPoints(10);
            countryFlag.sprite = currentFlag.sprite;
        }
        FinalAnswer(isCorrectAnswer);

        StartCoroutine(TimerAfterResponse());
    }

    private void FinalAnswer(bool isCorrectAnswer)
    {
        if (isCorrectAnswer)
        {
            Values.AddResolvedFlag(1);
        }
        else
        {
            DecreaseLifes();
        }
    }
    #endregion

    #region Helpers  
    private void HighlightResponse(int index, bool isTrue, bool withParticles, bool finalResponse, Button[] buttons)
    {
        Button selectedButton = buttons[index];

        //Particles
        if (withParticles)
        {
            responseParticles.transform.SetParent(selectedButton.transform);
            responseParticles.transform.localPosition = Vector3.zero;
            responseParticles.transform.localScale = Vector3.one;
        }
        ParticleSystem.MainModule psmain = responseParticles.main;


        if (finalResponse)
        {
            foreach (Button button in buttons)
            {
                button.GetComponent<Image>().color = colorOthers;
                button.interactable = false;
            }
        }

        if (isTrue)
        {
            selectedButton.GetComponent<Image>().color = colorTrue;
            psmain.startColor = colorTrue;
        }
        else
        {
            selectedButton.GetComponent<Image>().color = colorFalse;
            psmain.startColor = colorFalse;
        }

        if (finalResponse && showCorrectAnswerAfter)
        {
            buttons[currentResponseIndex].GetComponent<Image>().color = colorTrue;
        }

        if (withParticles)
            responseParticles.Play();

        if (finalResponse)
            StartCoroutine(TimerAfterResponse());
    }

    private void LoadNewFlag()
    {
        AdsManager.Instance.LoadAdIfCase();

        upTile.ReEnable();
        downTile.ReEnable();
        timeTile.ReEnable();

        currentFlag = CurrentListOfFlags.RandomUniqueItem(UsedFlagsIndexes);

        SetGameMode();
        ChangeColors();
    }

    void SetGameMode()
    {
        int gameTypeIndex = mustGameType == -1 ? SelectGameType(currentFlag) : mustGameType;

        switch (gameTypeIndex)
        {
            case (int)GameTypes.Guess: // "whichCountryFlag"
                ActivateOnly(new GameObject[] { up_flagGroup, down_responseTile }, new GameObject[] { up_uncoloredFlagGroup, countryName.gameObject, down_colorTile, down_colorTile, down_symbolTile });

                //Top
                countryFlag.sprite = currentFlag.sprite;

                //Down
                var usedIndexesForResponses = new List<int>
                {
                    currentFlag.ID
                };
                foreach (var button in responseButtons)
                {
                    button.interactable = true;
                    button.buttonText.GetComponent<FZText>().SlowlyWriteText(CurrentListOfFlags.RandomUniqueItem(usedIndexesForResponses).name);
                }
                responseButtons.RandomItem().buttonText.GetComponent<FZText>().SlowlyWriteText(currentFlag.name);

                StartCoroutine(TimerResponse());
                break;

            case (int)GameTypes.Color: // "canYouDraw"
                ActivateOnly(new GameObject[] { down_colorTile, countryName.gameObject, up_flagGroup, up_uncoloredFlagGroup, down_colorTile }, new GameObject[] { timeTile, down_responseTile, down_symbolTile });
                countryName.text = currentFlag.name;
                countryFlag.sprite = currentFlag.grayScaleSprite;

                completedParts = 0;

                foreach (var button in colorButtons)
                {
                    button.interactable = true;
                }

                List<Color> colors = new List<Color>();
                foreach (var cPart in currentFlag.colorParts)
                    colors.Add(cPart.color);
                for (int i = 0; i < colorButtons.Length - currentFlag.colorParts.Length; i++)
                {
                    var randFlag = FlagsManager.Manager.colorFlags.RandomItem();
                    colors.Add(randFlag.colorParts[Random.Range(0, randFlag.colorParts.Length)].color);
                }

                for (int i = 0; i < colorButtons.Length; i++)
                {
                    colorButtons[i].buttonImage.color = colors[i];
                }

                foreach (var image in uncoloredParts)
                {
                    image.gameObject.SetActive(false);
                }
                break;

            case (int)GameTypes.Symbol: // "missingPart"
                ActivateOnly(new GameObject[] { down_symbolTile, countryName.gameObject, up_flagGroup }, new GameObject[] { up_uncoloredFlagGroup, timeTile, down_responseTile, down_colorTile });
                countryName.text = currentFlag.name;
                countryFlag.sprite = currentFlag.noSymbolSprite;

                selectedSymbolIndex = 1;

                foreach (var button in symbolButtons)
                {
                    button.interactable = true;
                }

                List<Sprite> symbols = new List<Sprite>
                {
                    FlagsManager.Manager.colorFlags.RandomItem().symbol,
                    FlagsManager.Manager.colorFlags.RandomItem().symbol,
                    FlagsManager.Manager.colorFlags.RandomItem().symbol
                };
                symbols[Random.Range(0, 2)] = currentFlag.symbol;



                for (int i = 0; i < symbolButtons.Length; i++)
                {
                    symbolButtons[i].buttonImage.sprite = symbols[i];
                }

                break;
        }
        titleGameType.text = UITexts.selectedLanguage[gameTypes[gameTypeIndex]];
    }

    private void ChangeColors()
    {
        Color currentColor = UIColors[Random.Range(0, UIColors.Length)];
        Color currentShadowColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a / 2);
        foreach (var shadow in shadowsList)
        {
            shadow.color = currentShadowColor;
        }

        foreach (var resBtn in responseButtons)
        {
            resBtn.image.color = currentColor;
        }
        foreach (var colorBtn in colorButtons)
        {
            colorBtn.image.color = currentColor;
        }
        foreach (var symBtn in symbolButtons)
        {
            symBtn.image.color = currentColor;
        }
    }

    private IEnumerator TimerResponse()
    {
        timerResponseRunning = true;
        time = 1.1f;
        while (time > 0 && timerResponseRunning)
        {
            yield return new WaitForSeconds(0.03f);
            time -= 0.004f;
            timeFill.fillAmount = time;
        }
        if (time <= 0)
        {
            foreach (Button button in responseButtons)
            {
                button.GetComponent<Image>().color = colorOthers;
                button.interactable = false;
            }

            if (showCorrectAnswerAfter)
                responseButtons[currentResponseIndex].GetComponent<Image>().color = colorTrue;

            DecreaseLifes();
        }
    }

    private IEnumerator TimerAfterResponse()
    {
        if (AdsManager.shouldShowAd)
        {
            yield return new WaitForSeconds(0.5f);
            AdsManager.Instance.ShowAd();
            LoadNewFlag();
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            LoadNewFlag();
        }
    }

    private void ActivateOnly(GameObject[] activeObjects, GameObject[] notActiveObjects)
    {
        foreach (var activeObject in activeObjects)
        {
            activeObject.SetActive(true);
        }
        foreach (var notActiveObject in notActiveObjects)
        {
            notActiveObject.SetActive(false);
        }
    }

    private void DecreaseLifes()
    {
        hearts[hearts.Length - heartsUsed - 1].interactable = false;
        heartsUsed++;

        if (heartsUsed == hearts.Length)
        {
            if (Stats.highscore < Values.resolvedFlags)
            {
                FZSave.Int.Set(FZSave.Constants.Highscore, Values.resolvedFlags);
                highscoreMessage.SetActive(true);
            }
            main.GetComponent<Animator>().SetBool("shown", false);
            gameoverTile.SetActive(true);
            finalScore.text = Values.resolvedFlags.ToString();
            Time.timeScale = 0;
        }
    }

    private int SelectGameType(Flag flag)
    {
        var availabelGameTypesForFlag = new List<int>();

        if (flag.sprite != null) // Can guess
            availabelGameTypesForFlag.Add((int)GameTypes.Guess);
        if (flag.grayScaleSprite != null) // Can color
            availabelGameTypesForFlag.Add((int)GameTypes.Color);
        if (flag.noSymbolSprite != null) // Can symbol
            availabelGameTypesForFlag.Add((int)GameTypes.Symbol);

        return availabelGameTypesForFlag.RandomItem();
    }
    #endregion

    public void BackToMenu()
    {
        AdsManager.Instance.DestroyBannerAd();
        SceneManager.LoadScene(Scenes.Menu);
    }

    public void ContinueGame(GameObject tileToHide)
    {
        main.GetComponent<Animator>().SetBool("shown", true);
        Time.timeScale = 1;
        tileToHide.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(Scenes.Game);
    }
}
