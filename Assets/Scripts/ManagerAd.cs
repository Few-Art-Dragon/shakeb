using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class ManagerAd : MonoBehaviour
{
    private RewardedAd _rewardedAd;

    //private AdRequest _request;

    private string _rewardedAdID = "ca-app-pub-7509810502932347/1808042209";

    private void Start()
    {
        _rewardedAd = new RewardedAd(_rewardedAdID);
        //AdRequest _request = new AdRequest.Builder().Build();
        AdRequest _request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("C879AD0917CBB034").Build();
        _rewardedAd.LoadAd(_request);
    }
    public void ShowVideo()
    {
        
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }    

}
