using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class AdManager : MonoBehaviour
{
    [SerializeField] GameObject FishOut;
    [SerializeField] GameObject FishIn;
    [SerializeField] GameManager gameManager;
    [SerializeField] Fish fish;
    [SerializeField] Animator fishAnimator;
    [SerializeField] GameObject rewardButton;
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string bannerId = "ca-app-pub-2149732207719747/6097649579";  //     ca-app-pub-3940256099942544/6300978111 þuan yorum satýrýndakiler test reklamlarý
    private string interId = "ca-app-pub-2149732207719747/9681671369";  //       ca-app-pub-3940256099942544/1033173712
    private string rewardedId = "ca-app-pub-2149732207719747/3105155733";   //       ca-app-pub-3940256099942544/5224354917
#elif UNITY_IPHONE
    private string bannerId = "ca-app-pub-3940256099942544/2934735716";
    private string interId = "ca-app-pub-3940256099942544/4411468910";
    private string rewardedId = "ca-app-pub-3940256099942544/1712485313";
#else
    private string bannerId = "unused";
    private string interId = "unused";
    private string rewardedId = "unused";
#endif
    BannerView _bannerView;
    InterstitialAd _interstitialAd;
    RewardedAd _rewardedAd;


    
    void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
        LoadInterstitialAd();
        LoadRewardedAd();
        //LoadBannerAd();
    }

    #region BannerAd

    public void LoadBannerAd()
    {
        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        //Listen to Banner events
        ListenToBannerEvents();

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        //Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest); //Show the banner on screen
    }

    public void CreateBannerView()
    {
        //Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerAd();
        }

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Top);
    }

    public void ListenToBannerEvents()
    {
        // Raised when an ad is loaded into the banner view.
        _bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + _bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        _bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };
        // Raised when the ad is estimated to have earned money.
        _bannerView.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Banner view paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        _bannerView.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Banner view recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        _bannerView.OnAdClicked += () =>
        {
            Debug.Log("Banner view was clicked.");
        };
        // Raised when an ad opened full screen content.
        _bannerView.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Banner view full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        _bannerView.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Banner view full screen content closed.");
        };
    }

    public void DestroyBannerAd()
    {
        if (_bannerView != null)
        {
            //Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    #endregion

    #region InterstitialAd

    public void LoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(interId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                _interstitialAd = ad;
                InterstitialEvent(_interstitialAd);
            });
    }

    public void ShowInterstitialAd()
    {
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
        }
    }

    public void InterstitialEvent(InterstitialAd interstitialAd)
    {
        // Raised when the ad is estimated to have earned money.
        interstitialAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        interstitialAd.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        interstitialAd.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        interstitialAd.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            interstitialAd.Destroy();
        };
        // Raised when the ad failed to open full screen content.
        interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            
        };
    }

    #endregion

    #region RewardedAd

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(rewardedId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                _rewardedAd = ad;
                RewardedAdEvents(_rewardedAd);
            });
    }

    public void ShowRewardedAd()
    {

        if (_rewardedAd != null)     //************* if (_rewardedAd != null && _rewardedAd.CanShowAd())   eski hail böyleydi, rewarded reklam her týkladýðýmýzda gösterilmiyor diye böyle yaptým belki bundandýr
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                gameManager.Rewarded();
                fish.Rewarded();
                FishIn.transform.position = new Vector3(FishIn.transform.position.x, 4f,  FishIn.transform.position.z);
                FishOut.transform.position = this.transform.position;
                FishIn.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                fishAnimator.SetTrigger("SuperFishTrigger");
                

            });
        }
    }

    public void RewardedAdEvents(RewardedAd rewardedAd)
    {
        // Raised when the ad is estimated to have earned money.
        rewardedAd.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
            LoadRewardedAd();
        };
        // Raised when an impression is recorded for an ad.
        rewardedAd.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        rewardedAd.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        rewardedAd.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        rewardedAd.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Rewarded ad full screen content closed.");
            rewardButton.SetActive(false);
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        rewardedAd.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);
        };
    }
    
    #endregion
}

