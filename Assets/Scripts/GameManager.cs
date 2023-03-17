using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Constants;

public class GameManager : MonoBehaviour
{
    public FZText titleGameType;
    public FZText countryName;
    public Image countryFlag;
    public GameObject upTile;
    public GameObject downTile;
    public GameObject timeTile;
    public GameObject main;
    public GameObject resolvedFlagsGroup;

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
    public FZProgressBar progressBar;

    //UI
    public Color[] UIColors;
    public Image[] shadowsList;

    string[] gameTypes = { "whichCountryFlag", "canYouDraw", "missingPart" };

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


    List<Flag> UsedFlags = new List<Flag>();
    enum GameTypes { Guess, Color, Symbol };
    public static int selectedSymbolIndex;
    public static int mustGameType = -1;
    int completedParts = 0;
    IEnumerator timer;

    public List<Flag> CurrentListOfFlags = new List<Flag>();


    int gameType;
    int gameDifficulty;


    #region Basic Events
    private void Start()
    {
        AdsManager.Instance.LoadBannerAd();

        heartsUsed = 0;
        Time.timeScale = 1;

        //Set mode
        mustGameType = FZSave.Int.Get("mustGameType", -1);
        gameDifficulty = FZSave.Int.Get("gameDif", 1);
        foreach (var flag in FlagsManager.Flags)
        {
            if (flag.difLevel == gameDifficulty)
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
            countryName.gameObject.SetActive(true);
            countryName.text = currentFlag.name;

            Values.AddPoints(10);
        }
        FZSave.Int.Set("names", ++Stats.names);
        FinalAnswer(isCorrectAnswer);
    }

    public void SelectColor(int index)
    {
        Color selectedColor = colorButtons[index].buttonImage.color;

        colorButtons[index].interactable = false;

        bool isCorrectAnswer = false;
        for (int i = 0; i < currentFlag.colors.colorParts.Length; i++)
        {
            if (currentFlag.colors.colorParts[i].color == selectedColor)
            {
                isCorrectAnswer = true;
                uncoloredParts[i].sprite = currentFlag.colors.colorParts[i].part;
                uncoloredParts[i].gameObject.SetActive(true);
                completedParts++;
                Values.AddPoints(5);
            }
        }

        if (isCorrectAnswer)
        {
            if (completedParts == currentFlag.colors.colorParts.Length)
            {

                StartCoroutine(TimerAfterResponse());
                HighlightResponse(index, isCorrectAnswer, true, true, colorButtons);
                countryFlag.sprite = currentFlag.sprite;

                FZSave.Int.Set("colors", ++Stats.colors);
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
        bool isCorrectAnswer = symbolButtons[index].buttonImage.sprite == currentFlag.symbol.symbolSprite;

        HighlightResponse(index, isCorrectAnswer, true, true, symbolButtons);


        if (isCorrectAnswer)
        {
            Values.AddPoints(10);
            countryFlag.sprite = currentFlag.sprite;
        }
        FZSave.Int.Set("symbols", ++Stats.symbols);
        FinalAnswer(isCorrectAnswer);

        StartCoroutine(TimerAfterResponse());
    }

    private void FinalAnswer(bool isCorrectAnswer)
    {
        if (timer != null)
            StopCoroutine(timer);

        if (isCorrectAnswer)
        {
            StartCoroutine(ResolvedFlagsGroupTimer(2));
            Values.AddResolvedFlag(1);
        }
        else
        {
            DecreaseLifes();
        }
    }
    #endregion

    #region Helpers  
    private IEnumerator ResolvedFlagsGroupTimer(float timeToShow)
    {
        resolvedFlagsGroup.SetActive(true);
        resolvedFlagsGroup.GetComponent<Animator>().SetBool("shown", true);
        yield return new WaitForSeconds(timeToShow);
        resolvedFlagsGroup.GetComponent<Animator>().SetBool("shown", false);
    }

    private void HighlightResponse(int index, bool isCorrect, bool withParticles, bool finalResponse, Button[] buttons)
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
                if (gameType != 1)
                {
                    button.GetComponent<Image>().color = colorOthers;
                }
                button.interactable = false;
            }
        }

        if (isCorrect)
        {
            selectedButton.GetComponent<Image>().color = colorTrue;
            psmain.startColor = colorTrue;
        }
        else
        {
            selectedButton.GetComponent<Image>().color = colorFalse;
        }

        if (finalResponse && showCorrectAnswerAfter)
        {
            buttons[currentResponseIndex].GetComponent<Image>().color = colorTrue;
        }

        if (withParticles && isCorrect)
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

        currentFlag = CurrentListOfFlags.RandomUniqueItem(UsedFlags);

        SetGameMode();
        ChangeColors();
    }

    void SetGameMode()
    {
        gameType = mustGameType == -1 ? SelectGameType(currentFlag) : mustGameType;

        switch (gameType)
        {
            case (int)GameTypes.Guess: // "whichCountryFlag"
                ActivateOnly(new GameObject[] { up_flagGroup, down_responseTile }, new GameObject[] { up_uncoloredFlagGroup, countryName.gameObject, down_colorTile, down_colorTile, down_symbolTile });

                //Top
                countryFlag.sprite = currentFlag.sprite;

                //Down
                var usedFlagsForResponses = new List<Flag>
                {
                    currentFlag
                };
                foreach (var button in responseButtons)
                {
                    button.interactable = true;
                    button.buttonText.GetComponent<FZText>().SlowlyWriteText(CurrentListOfFlags.RandomUniqueItem(usedFlagsForResponses).name);
                }
                responseButtons.RandomItem().buttonText.GetComponent<FZText>().SlowlyWriteText(currentFlag.name);

                timer = TimerResponse();
                StartCoroutine(timer);
                break;

            case (int)GameTypes.Color: // "canYouDraw"
                ActivateOnly(new GameObject[] { down_colorTile, countryName.gameObject, up_flagGroup, up_uncoloredFlagGroup, down_colorTile }, new GameObject[] { timeTile, down_responseTile, down_symbolTile });
                countryName.text = currentFlag.name;
                countryFlag.sprite = currentFlag.colors.grayScaleSprite;

                completedParts = 0;

                foreach (var button in colorButtons)
                {
                    button.interactable = true;
                }

                List<Color> colors = new List<Color>();
                foreach (var cPart in currentFlag.colors.colorParts)
                {
                    colors.Add(cPart.color);
                }

                for (int i = 0; i < colorButtons.Length - currentFlag.colors.colorParts.Length; i++)
                {
                    colors.Add(currentFlag.colors.otherColors.RandomItem());
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
                countryFlag.sprite = currentFlag.symbol.noSymbolSprite;

                selectedSymbolIndex = 1;

                foreach (var button in symbolButtons)
                {
                    button.interactable = true;
                }

                List<Sprite> symbols = new List<Sprite>
                {
                    FlagsManager.AllSymbols.RandomItem(),
                    FlagsManager.AllSymbols.RandomItem(),
                    FlagsManager.AllSymbols.RandomItem(),
                };
                symbols[Random.Range(0, 2)] = currentFlag.symbol.symbolSprite;

                for (int i = 0; i < symbolButtons.Length; i++)
                {
                    symbolButtons[i].buttonImage.sprite = symbols[i];
                }

                break;
        }
        titleGameType.SlowlyWriteText(UITexts.selectedLanguage[gameTypes[gameType]]);
    }

    private void ChangeColors()
    {
        Color currentColor = UIColors.RandomItem();
        //Color currentShadowColor = new Color(currentColor.r, currentColor.g, currentColor.b, currentColor.a / 4);
        //foreach (var shadow in shadowsList)
        //{
        //    shadow.color = currentShadowColor;
        //}

        foreach (var resBtn in responseButtons)
        {
            resBtn.image.color = currentColor;
        }
        foreach (var symBtn in symbolButtons)
        {
            symBtn.image.color = currentColor;
        }
    }

    private IEnumerator TimerResponse()
    {
        timerResponseRunning = true;
        float time = 5 + (3 - gameDifficulty);
        float remainedTime = time;
        while (remainedTime > 0 && timerResponseRunning)
        {
            yield return new WaitForSeconds(time / 100);
            remainedTime -= time / 100;
            progressBar.SetProgress(time, remainedTime);
        }

        if (remainedTime <= 0)
        {
            foreach (Button button in responseButtons)
            {
                button.GetComponent<Image>().color = colorOthers;
                button.interactable = false;
            }

            if (showCorrectAnswerAfter)
                responseButtons[currentResponseIndex].GetComponent<Image>().color = colorTrue;

            DecreaseLifes();
            StartCoroutine(TimerAfterResponse(true));
        }
    }

    private IEnumerator TimerAfterResponse(bool fast = false)
    {
        if (AdsManager.shouldShowAd)
        {
            yield return new WaitForSeconds(0.5f);
            AdsManager.Instance.ShowAd();
            LoadNewFlag();
        }
        else
        {
            yield return new WaitForSeconds(fast ? 0.5f : 1.5f);
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
        var availabelGameTypesForFlag = new List<int>
        {
            (int)GameTypes.Guess
        };
        if (flag.hasColors)
            availabelGameTypesForFlag.Add((int)GameTypes.Color);
        if (flag.hasSymbol)
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
