using UnityEngine;
using UnityEngine.UI;

//04.11.22

public class FZProgressBar : MonoBehaviour
{
    public Image fillImage;
    public Text fillText;
    public float fillAmount = 0;

    private void OnEnable()
    {
        fillImage.fillAmount = fillAmount;
    }

    public void SetProgressBackwards(float maxValue, float currentValue)
    {
        fillAmount = 1f - currentValue / maxValue;
        if (fillImage != null)
            fillImage.fillAmount = fillAmount;
        if (fillText != null)
            fillText.text = currentValue.ToString() + "/" + maxValue.ToString();
        //fillImage.color = color;
    }

    public void SetProgress(float maxValue, float currentValue)
    {
        fillAmount = currentValue / maxValue;

        if (fillImage != null)
            fillImage.fillAmount = fillAmount;
        if (fillText != null)
            fillText.text = currentValue.ToString() + "/" + maxValue.ToString();
        //fillImage.color = color;
    }
}
