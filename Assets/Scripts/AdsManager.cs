using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    string banner_ID = "ca-app-pub-8089995158636506/1190694487";
    string newFlag_interstitial_ID = "ca-app-pub-8089995158636506/9674189433";
    //App ID ca-app-pub-8089995158636506~6664866802

    public bool isTest;

    public static AdsManager instance;
    public static bool shouldShowAd = false;

    private InterstitialAd newFlagAd;
    private BannerView bannerView;

    int steps;
    readonly int stepToShow = 4;

    #region Basic Events
    void Awake()
    {
        instance = this;

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("Ads: AdMob fully initialized");
        });
    }

    void Start()
    {
        if (isTest)
        {
            newFlag_interstitial_ID = "ca-app-pub-3940256099942544/1033173712";
            banner_ID = "ca-app-pub-3940256099942544/6300978111";
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Interstitial
    public void LoadAdIfCase()
    {
        steps++;
        shouldShowAd = steps == stepToShow;

        Debug.Log("Ads: Steps: " + steps);

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

        Debug.Log("Ads: Loading the interstitial ad.");
        Debug.Log("Ads: Is test: " + isTest.ToString());

        var adRequest = new AdRequest.Builder()
             .AddKeyword("unity-admob-sample")
             .Build();

        InterstitialAd.Load(newFlag_interstitial_ID, adRequest,
          (InterstitialAd ad, LoadAdError error) =>
          {
              if (error != null || ad == null)
              {
                  Debug.LogError("Ads: Interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Ads: Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              newFlagAd = ad;
          });
    }

    public void ShowAd()
    {
        if (newFlagAd != null && newFlagAd.CanShowAd())
        {
            Debug.Log("Ads: Showing interstitial ad.");
            newFlagAd.Show();
        }
        else
        {
            Debug.LogError("Ads: Interstitial ad is not ready yet.");
        }

        newFlagAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Ads: Interstitial ad full screen content closed.");
        };
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        newFlagAd.Destroy();
    }
    #endregion

    #region Banner
    public void CreateBannerView()
    {
        Debug.Log("Ads: Creating banner view");

        if (bannerView != null)
        {
            DestroyBannerAd();
        }

        AdSize adaptiveSize =
              AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

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

        Debug.Log("Ads: Loading banner ad.");
        bannerView.LoadAd(adRequest);

        bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void DestroyBannerAd()
    {
        if (bannerView != null)
        {
            Debug.Log("Ads: Destroying banner ad.");
            bannerView.Destroy();
            bannerView = null;
        }
    }
    #endregion
}
