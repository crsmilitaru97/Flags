using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//07.12.22

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
        GameObject fade = new GameObject();
        fade.transform.SetParent(this.transform);
        fade.name = "Fade Screen";
        fade.transform.localScale = new Vector3(1, 1, 1);

        Image fadeImage = fade.AddComponent<Image>();
        fadeImage.sprite = null;
        fadeImage.color = fadeColor;
        fadeImage.raycastTarget = false;

        RectTransform rectTransform = fadeImage.rectTransform;
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);

        StartCoroutine(Hide(introTime, fadeImage));
    }

    private IEnumerator Hide(float time, Image fadeImage)
    {
        float percent = time / 10;
        float colorPercent = fadeColor.a / 10;
        float a = fadeColor.a;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(percent);
            a -= colorPercent;
            fadeImage.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, a);
        }
    }

    public void FadeLoadSceneAsync(int sceneIndex, int time = 0)
    {
        GameObject fade = new GameObject();
        fade.transform.SetParent(transform);
        fade.name = "Fade Screen";
        fade.transform.localScale = new Vector3(1, 1, 1);

        Image fadeImage = fade.AddComponent<Image>();
        fadeImage.sprite = null;
        fadeImage.color = Color.black;
        fadeImage.raycastTarget = false;

        RectTransform rectTransform = fadeImage.rectTransform;
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        rectTransform.offsetMin = new Vector2(0, 0);
        rectTransform.offsetMax = new Vector2(0, 0);

        StartCoroutine(Show(time, fadeImage, sceneIndex));
    }

    public IEnumerator Show(float time, Image fadeImage, int sceneIndex)
    {
        fadeImage.color = new Color(Color.black.r, Color.black.g, Color.black.b, 0);
        float percent = time / 10;
        float colorPercent = Color.black.a / 10;
        float a = 0;

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(percent);
            a += colorPercent;
            fadeImage.color = new Color(Color.black.r, Color.black.g, Color.black.b, a);
        }
        SceneManager.LoadScene(sceneIndex);
    }
}