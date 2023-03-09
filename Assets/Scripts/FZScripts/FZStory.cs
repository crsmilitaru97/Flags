using System;
using System.Collections;
using UnityEngine;

//07.11.22

public class FZStory : MonoBehaviour
{
    public FZText text;

    public static FZStory Instance;
    private void Awake() => Instance = this;

    #region Classes  
    [Serializable]
    public class Message
    {
        [TextArea]
        public string text;
        public float timeToShow;
        public bool shown;
    }
    #endregion

    public void ShowMessage(string messageText)
    {
        text.transform.parent.gameObject.SetActive(true);
        text.SlowlyWriteText(messageText);
        StopAllCoroutines();
        StartCoroutine(DurationTimer(Duration(messageText)));
    }

    public static void ShowMessageOnce(Message message)
    {
        if (!message.shown)
        {
            Instance.ShowMessage(message.text);
            message.shown = true;
        }
    }

    public static void TryShowMessagesWithDelay(int probability, float delay, params string[] messageTexts)
    {
        if (FZRandom.Should(probability) && messageTexts.Length > 0)
        {
            string messageText = messageTexts.RandomItem();
            Instance.StartCoroutine(Instance.DelayTimer(messageText, delay));
        }
    }

    public static float Duration(string messageText)
    {
        int duration = Mathf.RoundToInt(messageText.Length / 10);
        duration += duration < 3 ? 2 : 1;
        return duration;
    }

    #region Timers
    public IEnumerator DurationTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        text.transform.parent.gameObject.SetActive(false);
    }

    public IEnumerator DelayTimer(string messageText, float delay)
    {
        yield return new WaitForSeconds(delay);
        Instance.ShowMessage(messageText);
    }
    #endregion
}
