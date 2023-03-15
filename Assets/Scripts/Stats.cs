using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text highscoreText, symbolsText, namesText, colorsText;
    public static int highscore, symbols, names, colors;

    void Start()
    {
        highscore = FZSave.Int.GetToText(FZSave.Constants.Highscore, highscoreText);
        symbols = FZSave.Int.GetToText("symbols", symbolsText);
        names = FZSave.Int.GetToText("names", namesText);
        colors = FZSave.Int.GetToText("colors", colorsText);
    }
}
