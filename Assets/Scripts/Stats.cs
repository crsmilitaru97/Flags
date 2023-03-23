using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public Text symbolsText, namesText, colorsText;
    public static int easyHighscore, mediumHighscore, hardHighscore;
    public static int symbols, names, colors;

    void Start()
    {
        easyHighscore = FZSave.Int.Get("easyHighscore", 0);
        mediumHighscore = FZSave.Int.Get("mediumHighscore", 0);
        hardHighscore = FZSave.Int.Get("hardHighscore", 0);

        symbols = FZSave.Int.GetToText("symbols", symbolsText);
        names = FZSave.Int.GetToText("names", namesText);
        colors = FZSave.Int.GetToText("colors", colorsText);
    }
}
