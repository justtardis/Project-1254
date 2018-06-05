using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{

   

    InterstitialAd interstitial;
    public int counter = 1;
    public bool videoCounter = false;
    /*public void VideoAds()
    {
        if (!videoCounter)
        {
            if (Advertisement.IsReady("video"))
            {
                Advertisement.Show("video");
            }
            videoCounter = true;
        }
        else
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo");
            }
            videoCounter = false;
        }
    }*/

    /*void Start()
    {
        /*RequestInterstitial();
        //RequestBanner();
    }*/

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-2967267784683496/4633296693";
#elif UNITY_IPHONE
        string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

    //// Initialize an InterstitialAd.
    /*interstitial = new InterstitialAd(adUnitId);
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
    // Load the interstitial with the request.
    interstitial.LoadAd(request);
}

public void ReqInter()
{
    RequestInterstitial();
}
// Update is called once per frame
public void showInterstital()
{
    if (interstitial.IsLoaded())
    {
        interstitial.Show();
        print("show");
    }*/
    }
}
