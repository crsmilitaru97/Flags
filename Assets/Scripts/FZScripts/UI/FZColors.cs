using UnityEngine;
using UnityEngine.UI;

//25.10.22

public static class FZColors
{
    public static void ChangeButtonColors(Button button, Color normalColor, Color disabledColor)
    {
        ColorBlock colorBlock;
        colorBlock = button.colors;

        colorBlock.disabledColor = disabledColor;
        colorBlock.normalColor = normalColor;

        button.colors = colorBlock;
    }

    public static Color ChangeAlpha(this Color color, float newValue)
    {
        color.a = newValue;
        return color;
    }
}
