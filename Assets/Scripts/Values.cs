using UnityEngine;

public class Values : MonoBehaviour
{
    public FZText pointsText, responsesText;
    public static int points;
    public static int resolvedFlags;

    public static Values Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        resolvedFlags = 0;
        points = FZSave.Int.Get(FZSave.Constants.Points, 0);

        if (pointsText != null)
        {
            pointsText.text = points.ToString();
        }
    }

    public static void AddPoints(int value)
    {
        points += value;

        if (GameManager.Manager != null)
        {
            GameManager.Manager.buttonSplit.interactable = points >= (GameManager.Manager.gameDifficulty == 1 ? 25 : GameManager.Manager.gameDifficulty == 2 ? 35 : 50);
        }

        if (Instance.pointsText != null)
        {
            Instance.pointsText.SlowlyUpdateNumberText(points);
        }
        FZSave.Int.Set(FZSave.Constants.Points, points);
    }

    public static void AddResolvedFlag(int value)
    {
        resolvedFlags += value;
        Instance.responsesText.SlowlyUpdateNumberText(resolvedFlags);
    }
}
