using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    string newFlag_interstitial = "ca-app-pub-8089995158636506/9674189433";
    string interstitial_test = "ca-app-pub-3940256099942544/1033173712";
    //App ID ca-app-pub-8089995158636506~6664866802
    private InterstitialAd newFlagAd;

    public bool isTest;

    public static AdsManager instance;
    public static bool shouldShowAd = false;

    int steps;
    int stepToShow = 3;

    #region Basic Events
    void Awake()
    {
        instance = this;
        MobileAds.Initialize(initStatus => { });
    }

    void Start()
    {
        if (isTest)
        {
            newFlag_interstitial = interstitial_test;
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void LoadAdIfCase()
    {
        steps++;
        shouldShowAd = steps == stepToShow;

        if (shouldShowAd)
        {
            steps = 0;

            newFlagAd = new InterstitialAd(newFlag_interstitial);

            Debug.Log("Ad loads " + isTest.ToString());
            AdRequest adRequest = new AdRequest.Builder().Build();
            newFlagAd.LoadAd(adRequest);
        }
    }

    public void ShowAd()
    {
        if (shouldShowAd)
        {
            Debug.Log("Ad show " + isTest.ToString());
            newFlagAd.Show();
            newFlagAd.OnAdClosed += HandleOnAdClosed;
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        newFlagAd.Destroy();
    }
}
