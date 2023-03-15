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
        points = FZSave.Int.GetToText(FZSave.Constants.Points, pointsText);
    }

    public static void AddPoints(int value)
    {
        Instance.pointsText.SlowlyUpdateNumberText(value);
        FZSave.Int.Set(FZSave.Constants.Points, points);
    }

    public static void AddResolvedFlag(int value)
    {
        Instance.responsesText.SlowlyUpdateNumberText(value);
    }
}
