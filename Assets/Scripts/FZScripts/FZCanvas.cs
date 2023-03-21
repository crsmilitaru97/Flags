using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//21.03.23

public class FZCanvas : MonoBehaviour
{
    public Color fadeColor = Color.black;
    public float introTime = 0.05f;
    public static FZCanvas Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Hide(introTime, FadeImage(fadeColor)));
    }

    public void FadeLoadScene(int sceneIndex, Color color, float time = 1)
    {
        StartCoroutine(Show(time, FadeImage(color), sceneIndex, color));
    }

    #region Helpers
    private Image FadeImage(Color color)
    {
        GameObject fade = new GameObject
        {
            name = "Fade Screen"
        };
        fade.transform.SetParent(transform);
        fade.transform.localScale = Vector3.one;

        Image fadeImage = fade.AddComponent<Image>();
        fadeImage.sprite = null;
        fadeImage.color = color;
        fadeImage.raycastTarget = false;

        RectTransform rectTransform = fadeImage.rectTransform;
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);

        fadeImage.GetComponent<RectTransform>().localPosition = Vector3.zero;

        return fadeImage;
    }

    private IEnumerator Hide(float time, Image fadeImage)
    {
        float percent = time / 10;
        float colorPercent = fadeColor.a / 10;
        float a = fadeColor.a;

        yield return new WaitForSecondsRealtime(percent);

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(percent);
            a -= colorPercent;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
        }
    }

    public IEnumerator Show(float time, Image fadeImage, int sceneIndex, Color color)
    {
        fadeImage.color = new Color(color.r, color.g, color.b, 0);
        float percent = 1f / time / 10f;
        float a = 0;

        while (a < 1)
        {
            yield return new WaitForSecondsRealtime(time / 100);
            a += percent;
            fadeImage.color = new Color(color.r, color.g, color.b, a);
        }
        yield return new WaitForSecondsRealtime(time / 100);

        SceneManager.LoadScene(sceneIndex);
    }
    #endregion
}