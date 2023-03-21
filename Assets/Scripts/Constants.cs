using System;
using UnityEngine;

public static class Constants
{
    public static class Scenes
    {
        public const int Menu = 0;
        public const int Game = 1;
        public const int Learn = 2;
    }

    [Serializable]
    public class Flag
    {
        public string abbrev;
        public string name;
        public Sprite sprite;
        public int difLevel = 1;
        public FlagCountry country = new FlagCountry();

        public bool hasColors;
        public FlagColor colors;

        public bool hasSymbol;
        public FlagSymbol symbol;
    }

    [Serializable]
    public class FlagTranslate
    {
        public string ID;
        public string name;
    }

    public class FlagCountry
    {
        public bool isEU;
        public bool isAfrica;
        public bool isAsia;
        public bool isOceania;
        public bool isNorthAmerica;
        public bool isSouthAmerica;
    }

    [Serializable]
    public class FlagPart
    {
        public Sprite part;
        public Color color;
    }

    [Serializable]
    public class FlagColor
    {
        public string ID;
        [HideInInspector]
        public Sprite graySprite;
        public FlagPart[] colorParts;
        public Color[] otherColors;
    }

    public class FlagSymbol
    {
        public Sprite symbolSprite;
        public Sprite noSymbolSprite;

        public FlagSymbol(Sprite symbol, Sprite noSymbol)
        {
            symbolSprite = symbol;
            noSymbolSprite = noSymbol;
        }
    }
}
