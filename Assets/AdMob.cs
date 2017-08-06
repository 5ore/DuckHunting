using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMob : MonoBehaviour
{

    bool hasShownAdOneTime;

    // Use this for initialization
    void Start()
    {
        //Request Ad
        hasShownAdOneTime = false;
        RequestInterstitialAds();
        //Invoke("showInterstitialAd", 2.0f);

    }
    public void LoadAd()
    {
        if (!hasShownAdOneTime)
        {
            hasShownAdOneTime = true;
            StartCoroutine(WaitForTime());
        }
    }

    public void showInterstitialAd()
    {
        Debug.Log("showInterstitialAd is functional.");
        //Show Ad
        if (interstitial.IsLoaded())
        {
            interstitial.Show();

            //Stop Sound
            //
            AudioListener.pause = true;
            Time.timeScale = 0f;

            Debug.Log("SHOW AD XXX");
        }

    }

    InterstitialAd interstitial;
    private void RequestInterstitialAds()
    {
        Debug.Log("RequestInterstitialAds is functional");
        string adID = "ca-app-pub-4364787564620669/4233610012";

#if UNITY_ANDROID
        string adUnitId = adID;
#elif UNITY_IOS
        string adUnitId = adID;
#else
        string adUnitId = adID;
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);

        /* //***Test***
         AdRequest request = new AdRequest.Builder()
        .AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
        .AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
        .Build();
        */

        //***Production***
        AdRequest request = new AdRequest.Builder().Build();

        //Register Ad Close Event
        interstitial.OnAdClosed += Interstitial_OnAdClosed;

        // Load the interstitial with the request.
        interstitial.LoadAd(request);

        Debug.Log("AD LOADED XXX");

    }

    //Ad Close Event
    private void Interstitial_OnAdClosed(object sender, System.EventArgs e)
    {
        hasShownAdOneTime = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
        RequestInterstitialAds();
    }
    IEnumerator WaitForTime()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        showInterstitialAd();
    }
}
