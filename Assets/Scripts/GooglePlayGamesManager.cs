using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class GooglePlayGamesManager : MonoBehaviour
{
    public static bool isGooglePlayGames;
    public static GooglePlayGamesManager Instance;

    public GameObject highscoresGroup;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        DontDestroyOnLoad(gameObject);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        Debug.Log("GooglePlayGames " + status);

        if (highscoresGroup != null)
        {
            highscoresGroup.SetActive(status != SignInStatus.Success);
        }
    }

    public void ShowLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI();
    }

    public void AddToLeaderboard(long value, int difficulty)
    {
        switch (difficulty)
        {
            case 1:
                Stats.easyHighscore = (int)value;
                FZSave.Int.Set("easyHighscore", Stats.easyHighscore);
                PlayGamesPlatform.Instance.ReportScore(value, "CgkI26Tu-vkJEAIQAQ", (bool success) => { });
                break;
            case 2:
                Stats.mediumHighscore = (int)value;
                FZSave.Int.Set("mediumHighscore", Stats.mediumHighscore);
                PlayGamesPlatform.Instance.ReportScore(value, "CgkI26Tu-vkJEAIQAg", (bool success) => { });
                break;
            case 3:
                Stats.hardHighscore = (int)value;
                FZSave.Int.Set("hardHighscore", Stats.hardHighscore);
                PlayGamesPlatform.Instance.ReportScore(value, "CgkI26Tu-vkJEAIQAw", (bool success) => { });
                break;
        }
    }
}