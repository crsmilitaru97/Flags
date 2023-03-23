using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayGamesManager : MonoBehaviour
{
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
        isGooglePlayGames = status == SignInStatus.Success;
        Debug.Log("GooglePlayGames " + status);
    }

    public void ShowLeaderboard()
    {
        if (isGooglePlayGames)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
    }

    public void AddToLeaderboard(long value, int difficulty)
    {
        if (isGooglePlayGames)
        {
            switch (difficulty)
            {
                case 1:
                    Social.ReportScore(value, "CgkI26Tu-vkJEAIQAQ", (bool success) => { });
                    break;
                case 2:
                    Social.ReportScore(value, "CgkI26Tu-vkJEAIQAg", (bool success) => { });
                    break;
                case 3:
                    Social.ReportScore(value, "CgkI26Tu-vkJEAIQAw", (bool success) => { });
                    break;
            }
        }
    }
}
