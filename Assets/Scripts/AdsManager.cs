using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    string banner_ID = "ca-app-pub-8089995158636506/1190694487";
    string newFlag_interstitial_ID = "ca-app-pub-8089995158636506/9674189433";
    string getPoints_reward_ID = "ca-app-pub-8089995158636506/5520653863";

    //App ID ca-app-pub-8089995158636506~6664866802

    public static AdsManager Instance;
    public static bool shouldShowAd = false;

    private InterstitialAd newFlagAd;
    private BannerView bannerView;
    private RewardedAd getPoints;

    [HideInInspector]
    public FZButton rewardButton;

    public static bool isAdMob;

    int steps;
    readonly int stepToShow = 4;

    #region Basic Events
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            MobileAds.Initialize((InitializationStatus initStatus) => { });

            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        LoadRewardedAd();
    }
    #endregion

    #region Interstitial
    public void LoadAdIfCase()
    {
        steps++;
        shouldShowAd = steps == stepToShow;

        if (shouldShowAd)
        {
            steps = 0;
            LoadAd();
        }
    }

    public void LoadAd()
    {
        if (newFlagAd != null)
        {
            newFlagAd.Destroy();
            newFlagAd = null;
        }

        var adRequest = new AdRequest.Builder()
             .AddKeyword("unity-admob-sample")
             .Build();

        InterstitialAd.Load(newFlag_interstitial_ID, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
              {
                  return;
              }
              newFlagAd = ad;
          });
    }

    public void ShowAd()
    {
        if (newFlagAd != null && newFlagAd.CanShowAd())
        {
            newFlagAd.Show();
        }
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        newFlagAd.Destroy();
    }
    #endregion

    #region Banner
    public void CreateBannerView()
    {
        if (bannerView != null)
        {
            DestroyBannerAd();
        }

        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        bannerView = new BannerView(banner_ID, adaptiveSize, AdPosition.Bottom);
    }

    public void LoadBannerAd()
    {
        if (bannerView == null)
        {
            CreateBannerView();
        }
        var adRequest = new AdRequest.Builder()
            .AddKeyword("unity-admob-sample")
            .Build();

        bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
    }
    #endregion

    #region Reward
    public void LoadRewardedAd()
    {
        if (rewardButton != null)
            rewardButton.gameObject.SetActive(false);

        if (getPoints != null)
        {
            getPoints.Destroy();
            getPoints = null;
        }

        var adRequest = new AdRequest.Builder().Build();

        RewardedAd.Load(getPoints_reward_ID, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    return;
                }

                if (rewardButton != null)
                    rewardButton.gameObject.SetActive(true);
                getPoints = ad;
            });
    }

    public void ShowRewardedAd()
    {
        if (getPoints != null && getPoints.CanShowAd())
        {
            getPoints.Show((Reward reward) =>
            {
                RegisterReloadHandler(getPoints);
            });
        }
    }

    private void RegisterReloadHandler(RewardedAd ad)
    {
        Values.AddPoints(25);

        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }
    #endregion
}
