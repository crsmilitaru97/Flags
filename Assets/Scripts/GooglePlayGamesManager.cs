using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class GooglePlayGamesManager : MonoBehaviour
{
    public Text text;
    public static bool isGooglePlayGames;
    public static GooglePlayGamesManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        DontDestroyOnLoad(gameObject);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        text.text = status.ToString();
        isGooglePlayGames = status == SignInStatus.Success;
        Debug.Log("GooglePlayGames " + status);
    }

    public void ShowLeaderboard()
    {
        if (isGooglePlayGames)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI26Tu-vkJEAIQAQ");
        }
    }

    public void AddToLeaderboard(long value, string leaderboardID)
    {
        if (isGooglePlayGames)
        {
            Social.ReportScore(value, leaderboardID, (bool success) => { });
        }
    }
}
