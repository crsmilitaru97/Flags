using System;
using UnityEngine;

public static class Constants
{
    public static class Scenes
    {
        public const int Menu = 0;
        public const int Game = 1;
    }

    [Serializable]
    public class Flag
    {
        public string abbrev;
        [HideInInspector]
        public int ID;
        public string name;
        public Sprite sprite;

        [Header("Color")]
        public Sprite grayScaleSprite;
        public FlagPart[] colorParts;
        [Header("Symbol")]
        public Sprite noSymbolSprite;
        public Sprite symbol;
    }

    [Serializable]
    public class FlagPart
    {
        public Sprite part;
        public Color color;
    }
}
