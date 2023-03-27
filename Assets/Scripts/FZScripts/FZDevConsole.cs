using UnityEngine;
using UnityEngine.UI;

public class FZDevConsole : MonoBehaviour
{
    public Text logText, fpsMeterText;

    private float deltaTime;

    #region Debug Log
    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }

    void LogCallback(string logString, string stackTrace, LogType type)
    {
        logText.text += logString + "\r\n";
    }
    #endregion

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsMeterText.text = string.Format("{0:0.} fps", fps);
    }
}
