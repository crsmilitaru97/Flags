using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    public FZButton buttonSplit;
    public GameObject finishedAllFlags;

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
    public RectTransform symbolsListContainer;

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


    private List<Flag> UsedFlags = new List<Flag>();
    private List<Flag> CurrentFlags = new List<Flag>();

    enum GameTypes { Guess, Color, Symbol };
    public static int selectedSymbolIndex;
    public static int mustGameType = -1;
    int completedParts = 0;
    IEnumerator timer;


    int gameType;
    public int gameDifficulty;

    public static GameManager Manager;

    #region Basic Events
    private void Awake()
    {
        Manager = this;
    }

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
                CurrentFlags.Add(flag);
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
            buttonSplit.transform.parent.gameObject.SetActive(false);
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

            Values.AddPoints(gameDifficulty == 1 ? 10 : gameDifficulty == 2 ? 15 : 20);
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
                Values.AddPoints(gameDifficulty == 1 ? 5 : gameDifficulty == 2 ? 10 : 15);
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
                HighlightResponse(index, isCorrectAnswer, true, false, colorButtons);
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
            Values.AddPoints(gameDifficulty == 1 ? 10 : gameDifficulty == 2 ? 15 : 20);
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
            StartCoroutine(ResolvedFlagsGroupTimer(1));
            Values.AddResolvedFlag(1);
        }
        else
        {
            DecreaseOneLife();
        }
    }
    #endregion

    #region Helpers  
    private void SplitFlags()
    {
        List<FZButton> wrongButtons = new List<FZButton>();
        int count = 0;
        switch (gameType)
        {
            case (int)GameTypes.Guess:
                foreach (var item in responseButtons)
                {
                    if (item.buttonText.text != currentFlag.name)
                        wrongButtons.Add(item);
                }
                count = responseButtons.Count();
                break;
            case (int)GameTypes.Color:
                foreach (var item in colorButtons)
                {
                    if (currentFlag.colors.colorParts.FirstOrDefault(e => e.color == item.buttonImage.color) == null)
                        wrongButtons.Add(item);
                }
                count = colorButtons.Count();
                break;
            case (int)GameTypes.Symbol:
                foreach (var item in symbolButtons)
                {
                    if (item.buttonImage.sprite != currentFlag.symbol.symbolSprite)
                        wrongButtons.Add(item);
                }
                count = symbolButtons.Count();
                break;
        }

        for (int i = 0; i < count / 2; i++)
        {
            var btn = wrongButtons.RandomItem();
            wrongButtons.Remove(btn);
            btn.interactable = false;
            btn.image.color = colorOthers;
        }
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
            buttonSplit.interactable = false;

            foreach (Button button in buttons)
            {
                if (gameType != 1)
                {
                    button.GetComponent<Image>().color = colorOthers;
                }
                else
                {
                    foreach (var item in colorButtons)
                    {
                        if (currentFlag.colors.colorParts.FirstOrDefault(e => e.color == item.buttonImage.color) == null)
                        {
                            item.image.color = colorOthers;
                        }
                        else
                        {
                            if (item.interactable)
                                item.image.color = colorOthers;
                        }

                    }
                }
                button.interactable = false;
            }
        }

        if (isCorrect)
        {
            selectedButton.GetComponent<Image>().color = colorTrue;
            psmain.startColor = colorTrue;

            FZAudio.Manager.PlaySound(Sounds.Instance.correct);
        }
        else
        {
            selectedButton.GetComponent<Image>().color = colorFalse;
            FZAudio.Manager.PlaySound(Sounds.Instance.wrong);
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
        if (CurrentFlags.Count != UsedFlags.Count)
        {
            currentFlag = CurrentFlags.RandomUniqueItem(UsedFlags);
        }
        else
        {
            DecreaseOneLife();
            DecreaseOneLife();
            DecreaseOneLife();
            finishedAllFlags.SetActive(true);
        }

        SetGameMode();
        ChangeColors();

        buttonSplit.interactable = Values.points >= (gameDifficulty == 1 ? 25 : gameDifficulty == 2 ? 35 : 50);
        buttonSplit.buttonText.text = gameDifficulty == 1 ? "-25" : gameDifficulty == 2 ? "-35" : "-50";
    }

    private void SetGameMode()
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
                    button.buttonText.GetComponent<FZText>().SlowlyWriteText(CurrentFlags.RandomUniqueItem(usedFlagsForResponses).name);

                }
                responseButtons.RandomItem().buttonText.GetComponent<FZText>().SlowlyWriteText(currentFlag.name);

                timer = TimerResponse();
                StartCoroutine(timer);
                break;

            case (int)GameTypes.Color: // "canYouDraw"
                ActivateOnly(new GameObject[] { down_colorTile, countryName.gameObject, up_flagGroup, up_uncoloredFlagGroup, down_colorTile }, new GameObject[] { timeTile, down_responseTile, down_symbolTile });
                countryName.text = currentFlag.name;
                countryFlag.sprite = currentFlag.colors.graySprite;

                completedParts = 0;

                //Get colors
                List<Color> colors = new List<Color>();
                foreach (var cPart in currentFlag.colors.colorParts)
                {
                    colors.Add(cPart.color);
                }

                currentFlag.colors.otherColors.Shuffle();
                for (int i = 0; i < colorButtons.Length - currentFlag.colors.colorParts.Length; i++)
                {
                    colors.Add(currentFlag.colors.otherColors[i]);
                }

                colors.Shuffle();

                //Apply colors
                for (int i = 0; i < colorButtons.Length; i++)
                {
                    colorButtons[i].image.color = Color.white;
                    colorButtons[i].interactable = true;
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
                symbolsListContainer.anchoredPosition = new Vector2(-445, 0);

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

    private void DecreaseOneLife()
    {
        hearts[hearts.Length - heartsUsed - 1].interactable = false;
        heartsUsed++;

        if (heartsUsed == hearts.Length)
        {
            switch (gameDifficulty)
            {
                case 1:
                    if (Stats.easyHighscore < Values.resolvedFlags)
                    {
                        Stats.easyHighscore = Values.resolvedFlags;
                        FZSave.Int.Set("easyHighscore", Stats.easyHighscore);
                        highscoreMessage.SetActive(true);
                        GooglePlayGamesManager.Instance.AddToLeaderboard(Stats.easyHighscore, gameDifficulty);
                    }
                    break;
                case 2:
                    if (Stats.mediumHighscore < Values.resolvedFlags)
                    {
                        Stats.mediumHighscore = Values.resolvedFlags;
                        FZSave.Int.Set("mediumHighscore", Stats.mediumHighscore);
                        highscoreMessage.SetActive(true);
                        GooglePlayGamesManager.Instance.AddToLeaderboard(Stats.mediumHighscore, gameDifficulty);
                    }
                    break;
                case 3:
                    if (Stats.hardHighscore < Values.resolvedFlags)
                    {
                        Stats.hardHighscore = Values.resolvedFlags;
                        FZSave.Int.Set("hardHighscore", Stats.hardHighscore);
                        highscoreMessage.SetActive(true);
                        GooglePlayGamesManager.Instance.AddToLeaderboard(Stats.hardHighscore, gameDifficulty);
                    }
                    break;
            }

            main.GetComponent<Animator>().SetBool("shown", false);
            gameoverTile.SetActive(true);
            buttonSplit.transform.parent.gameObject.SetActive(false);
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

    private IEnumerator ResolvedFlagsGroupTimer(float timeToShow)
    {
        resolvedFlagsGroup.SetActive(true);
        resolvedFlagsGroup.GetComponent<Animator>().SetBool("shown", true);
        yield return new WaitForSeconds(timeToShow);
        resolvedFlagsGroup.GetComponent<Animator>().SetBool("shown", false);
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

            DecreaseOneLife();
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
    #endregion

    #region Buttons 
    public void SplitInHalf()
    {
        Values.AddPoints(gameDifficulty == 1 ? -25 : gameDifficulty == 2 ? -35 : -50);
        buttonSplit.interactable = false;

        SplitFlags();
    }

    public void GetPointsReward()
    {
        AdsManager.Instance.ShowRewardedAd();
    }

    public void BackToMenu()
    {
        AdsManager.Instance.DestroyBannerAd();
        FZCanvas.Instance.FadeLoadScene(Scenes.Menu, Color.white);
    }

    public void ContinueGame()
    {
        main.GetComponent<Animator>().SetBool("shown", true);
        Time.timeScale = 1;
        pauseTile.SetActive(false);
        buttonSplit.transform.parent.gameObject.SetActive(true);
    }

    public void Replay()
    {
        FZCanvas.Instance.FadeLoadScene(Scenes.Game, Color.white);
    }
    #endregion
}
