using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//07.12.22

public class FZText : Text
{
    public float timeInterval = 0.025f;

    public void SlowlyUpdateNumberText(int newValue)
    {
        if (gameObject.activeSelf)
        {
            int oldValue = int.Parse(this.text);
            StartCoroutine(ChangeNumberText(oldValue, newValue - oldValue));
        }
    }

    private IEnumerator ChangeNumberText(int oldValue, int diff)
    {
        int value = oldValue;
        if (diff >= 0)
        {
            while (value < oldValue + diff)
            {
                yield return new WaitForSecondsRealtime(timeInterval);
                value++;
                this.text = value.ToString();
            }
        }
        else
        {
            while (value > oldValue - Mathf.Abs(diff))
            {
                yield return new WaitForSecondsRealtime(timeInterval);
                value--;
                this.text = value.ToString();
            }
        }
    }

    public void SlowlyWriteText(string text)
    {
        if (gameObject.activeSelf)
        {
            StopAllCoroutines();
            StartCoroutine(WriteText(text));
        }
    }

    int i = 0;
    private IEnumerator WriteText(string text)
    {
        this.text = string.Empty;
        foreach (var item in text)
        {
            i++;
            yield return new WaitForSeconds(timeInterval);
            this.text += item.ToString();

            if (i % 2 == 0)
            {
                if (FZAudio.Manager != null)
                    FZAudio.Manager?.PlaySound(FZAudio.Manager.textSound);
            }
        }
    }
}
