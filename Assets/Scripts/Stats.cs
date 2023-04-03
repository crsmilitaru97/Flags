using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text symbolsText, namesText, colorsText;
    public Text easyHighscoreText, mediumHighscoreText, hardHighscoreText;
    public GameObject highscoresGroup;
    public static int easyHighscore, mediumHighscore, hardHighscore;
    public static int symbols, names, colors;

    void Start()
    {
        easyHighscore = FZSave.Int.GetToText("easyHighscore", easyHighscoreText);
        mediumHighscore = FZSave.Int.GetToText("mediumHighscore", mediumHighscoreText);
        hardHighscore = FZSave.Int.GetToText("hardHighscore", hardHighscoreText);

        symbols = FZSave.Int.GetToText("symbols", symbolsText);
        names = FZSave.Int.GetToText("names", namesText);
        colors = FZSave.Int.GetToText("colors", colorsText);
    }
}
