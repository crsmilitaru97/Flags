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

    public FZButton[] responseButtons;
    public FZButton[] colorButtons;
    public FZButton[] symbolButtons;

    public Button[] hearts;
    public Text numberText;
    public GameObject gameoverTile;
    public GameObject pauseTile;
    public Text finalScore;
    public GameObject highscoreMessage;
    public Button arrowLeftSymbol, arrowRightSymbol;
    public GameObject vertical3, horizontal3;
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

    int corectResponsesNumber;
    int heartsUsed;
    public static bool showCorrectAnswer;
    public static string category = "Flags/world/";

    public GameObject down_colorTile, down_responseTile, down_symbolTile;

    public static Flag currentFlag;
    public static int currentResponseIndex;
    List<int> usedIndexes = new List<int>();
    enum GameTypes { Guess, Color, Symbol };


    #region Basic Events
    private void Start()
    {
        heartsUsed = 0;
        Time.timeScale = 1;

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

        HighlightResponse(index, isCorrectAnswer, responseButtons);

        if (isCorrectAnswer)
        {
            corectResponsesNumber++;
            numberText.text = corectResponsesNumber.ToString();
        }
        else
        {
            DecreaseLifes();
        }
    }

    public void SelectColor(int index)
    {
        colorButtons[index].interactable = false;

        StartCoroutine(TimerAfterResponse());
    }

    public void SelectSymbol(int index)
    {
        bool isTrue = symbolButtons[index].transform.GetChild(1).name == currentFlag.name;

        HighlightResponse(index, isTrue, symbolButtons);

        if (isTrue)
        {
            corectResponsesNumber++;
            numberText.text = corectResponsesNumber.ToString();
            countryFlag.sprite = Resources.Load<Sprite>(category + currentFlag.abbrev);
        }
        else
        {
            DecreaseLifes();
        }

        StartCoroutine(TimerAfterResponse());
    }
    #endregion

    #region Helpers  
    private void HighlightResponse(int index, bool isTrue, Button[] buttons)
    {
        Button selectedButton = buttons[index];

        //Particles
        responseParticles.transform.SetParent(selectedButton.transform);
        responseParticles.transform.localPosition = Vector3.zero;
        responseParticles.transform.localScale = Vector3.one;
        ParticleSystem.MainModule psmain = responseParticles.main;

        foreach (Button button in buttons)
        {
            button.GetComponent<Image>().color = colorOthers;
            button.interactable = false;
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

        if (showCorrectAnswer)
        {
            buttons[currentResponseIndex].GetComponent<Image>().color = colorTrue;
        }

        responseParticles.Play();

        StartCoroutine(TimerAfterResponse());
    }

    private void LoadNewFlag()
    {
        AdsManager.instance.LoadAdIfCase();

        upTile.ReEnable();
        downTile.ReEnable();
        timeTile.ReEnable();

        currentFlag = FlagsManager.Manager.Flags.RandomUniqueItem(usedIndexes);

        SetGameMode();
        ChangeColors();
    }

    void SetGameMode()
    {
        int gameTypeIndex = SelectGameType(currentFlag);

        switch (gameTypeIndex)
        {
            case (int)GameTypes.Guess: // "whichCountryFlag"
                ActivateOnly(new GameObject[] { up_flagGroup, down_responseTile }, new GameObject[] { countryName.gameObject, down_colorTile, down_colorTile, down_symbolTile });

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
                    button.buttonText.SlowlyWriteText(FlagsManager.Manager.Flags.RandomUniqueItem(usedIndexesForResponses).name);
                }
                responseButtons.RandomItem().buttonText.SlowlyWriteText(currentFlag.name);

                StartCoroutine(TimerResponse());
                break;

            case (int)GameTypes.Color: // "canYouDraw"
                ActivateOnly(new GameObject[] { down_colorTile, countryName.gameObject, up_uncoloredFlagGroup, down_colorTile }, new GameObject[] { up_flagGroup, timeTile, down_responseTile, down_symbolTile });
                countryName.text = currentFlag.name;



                break;

            case (int)GameTypes.Symbol: // "missingPart"
                ActivateOnly(new GameObject[] { down_symbolTile, countryName.gameObject, up_flagGroup }, new GameObject[] { timeTile, down_responseTile, down_colorTile });
                countryName.text = currentFlag.name;
                symbolNumber = 1;
                countryFlag.sprite = Resources.Load<Sprite>(category + currentFlag.abbrev + "_noSymbol");
                down_symbolTile.GetComponent<Animator>().SetInteger("symbolNumber", symbolNumber);
                //foreach (var item in symbolButtons)
                //{
                //    int randVal = Random.Range(0, FlagsManager.flagsWithSymbols.Count());
                //    item.transform.GetChild(1).GetComponent<Image>().sprite =
                //        Resources.Load<Sprite>(category + FlagsManager.flagsWithSymbols[randVal].abbrev + "_symbol");
                //    item.transform.GetChild(1).name = FlagsManager.flagsWithSymbols[randVal].abbrev;
                //}
                int randVal2 = Random.Range(0, symbolButtons.Length);
                symbolButtons[randVal2].transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(category + currentFlag.abbrev + "_symbol");
                symbolButtons[randVal2].transform.GetChild(1).name = currentFlag.abbrev;
                break;
        }
        titleGameType.text = UITexts.selectedLanguage[gameTypes[gameTypeIndex]];
    }


    public static int symbolNumber;
    public void ChangeSymbolTileNumber(int step)
    {
        symbolNumber += step;
        down_symbolTile.GetComponent<Animator>().SetInteger("symbolNumber", symbolNumber);

        if (symbolNumber == 0)
            arrowLeftSymbol.gameObject.SetActive(false);
        else if (symbolNumber == 2)
            arrowRightSymbol.gameObject.SetActive(false);
        else
        {
            arrowLeftSymbol.gameObject.SetActive(true);
            arrowRightSymbol.gameObject.SetActive(true);
        }
        StartCoroutine(WaitForAnimation(0.2f));
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

        foreach (var button in symbolButtons)
        {
            button.transform.GetComponent<Image>().color = currentColor;
        }
    }

    public static bool timerResponseRunning;
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

            if (showCorrectAnswer)
                responseButtons[currentResponseIndex].GetComponent<Image>().color = colorTrue;

            DecreaseLifes();
        }
    }

    private IEnumerator TimerAfterResponse()
    {
        if (AdsManager.shouldShowAd)
        {
            yield return new WaitForSeconds(0.5f);
            AdsManager.instance.ShowAd();
            LoadNewFlag();
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            LoadNewFlag();
        }
    }

    private IEnumerator WaitForAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        symbolButtons[symbolNumber].transform.parent.SetParent(down_symbolTile.transform.parent);
        symbolButtons[symbolNumber].transform.parent.SetParent(down_symbolTile.transform);
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
            if (Stats.highscore < corectResponsesNumber)
            {
                PlayerPrefs.SetInt("highscore", corectResponsesNumber);
                highscoreMessage.SetActive(true);
            }
            main.GetComponent<Animator>().SetBool("shown", false);
            gameoverTile.SetActive(true);
            finalScore.text = corectResponsesNumber.ToString();
            Time.timeScale = 0;
        }
    }
    #endregion

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueGame(GameObject tileToHide)
    {
        main.GetComponent<Animator>().SetBool("shown", true);
        Time.timeScale = 1;
        tileToHide.SetActive(false);
    }

    public void Replay()
    {
        SceneManager.LoadScene(1);
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
}
