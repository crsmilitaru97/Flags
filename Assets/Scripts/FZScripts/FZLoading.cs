using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FZLoading : MonoBehaviour
{
    public bool enabledOnStart = false;

    public float animSpeed = 0.4f;
    public string dotStyle = ".";
    public int sceneIndex = 1;
    public Text tipsText;
    public string[] tips;

    public Text loadingProcent;
    public Text loadingDots;
    public Image loadingCircle;

    private int i;

    private void Start()
    {
        if (tips.Length > 0 && tipsText != null)
            tipsText.text = tips[Random.Range(0, tips.Length)];

        if (enabledOnStart)
            StartLoadScene();
    }

    public void StartLoadScene()
    {
        StartLoadingAnimation();
        StartCoroutine(LoadScene());
    }

    #region Helpers
    private void StartLoadingAnimation()
    {
        if (loadingDots != null)
            StartCoroutine(TimerDots());
        if (loadingCircle != null)
            StartCoroutine(TimerCircle());
    }

    private IEnumerator TimerCircle()
    {
        loadingCircle.fillAmount = 0;
        bool clockwise = loadingCircle.fillClockwise = true;

        while (true)
        {
            if (clockwise && loadingCircle.fillAmount < 1 || !clockwise && loadingCircle.fillAmount > 0)
            {
                loadingCircle.fillAmount += clockwise ? 0.05f : -0.05f;
                yield return new WaitForSecondsRealtime(animSpeed / 10);
            }
            else
            {
                clockwise = !clockwise;
                loadingCircle.fillClockwise = clockwise;
            }
        }
    }

    private IEnumerator TimerDots()
    {
        InitializeText();

        while (true)
        {
            if (i < 4)
            {
                loadingDots.text += dotStyle;
                yield return new WaitForSecondsRealtime(animSpeed);
                i++;
            }
            else
            {
                InitializeText();
            }
        }
    }

    private void InitializeText()
    {
        i = 0;
        loadingDots.text = string.Empty;
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncOperation.isDone)
        {
            if (loadingProcent != null)
                loadingProcent.text = (asyncOperation.progress * 100) + "%";

            yield return null;
        }
    }
    #endregion
}
