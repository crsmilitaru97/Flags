using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//27.10.22

public class FZAFadeShow : MonoBehaviour
{
    public float speed = 0.01f;
    public float timeToDisable = 0;
    public float maxAlpha = 1;
    public bool destroyAfter = false;

    private List<Image> images = new List<Image>();
    private List<Text> texts = new List<Text>();

    void OnEnable()
    {
        images.Clear();
        texts.Clear();

        CheckIfTextImages(transform);
        for (int i = 0; i < transform.childCount; i++)
        {
            CheckIfTextImages(transform.GetChild(i));
        }

        StartCoroutine(FadingIn());
        if (timeToDisable > 0)
            StartCoroutine(Timer());
    }

    void CheckIfTextImages(Transform tr)
    {
        Image img = tr.GetComponent<Image>();
        if (img != null)
        {
            img.color = FZColors.ChangeAlpha(img.color, 0);
            images.Add(img);
        }
        Text txt = tr.GetComponent<Text>();
        if (txt != null)
        {
            txt.color = FZColors.ChangeAlpha(txt.color, 0);
            texts.Add(txt);
        }
    }

    private IEnumerator FadingIn()
    {
        while (images[0].color.a < maxAlpha)
        {
            yield return new WaitForSecondsRealtime(speed);
            foreach (var image in images)
            {
                image.color = FZColors.ChangeAlpha(image.color, image.color.a + 0.1f);
            }
            foreach (var text in texts)
            {
                text.color = FZColors.ChangeAlpha(text.color, text.color.a + 0.1f);
            }
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(timeToDisable);
        StartCoroutine(FadingOut());
    }

    private IEnumerator FadingOut()
    {
        while (images[0].color.a > 0)
        {
            yield return new WaitForSecondsRealtime(speed);
            foreach (var image in images)
            {
                image.color = FZColors.ChangeAlpha(image.color, image.color.a - 0.1f);
            }
            foreach (var text in texts)
            {
                text.color = FZColors.ChangeAlpha(text.color, text.color.a - 0.1f);
            }
        }

        if (destroyAfter)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }
}
