using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string gameId = "5042885";
    string adID = "Rewarded_Android";

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId);
    }

    public void ShowAD()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show(adID);
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == "Rewarded_Android")
        {
            if (showResult == ShowResult.Finished)
            {
                Debug.Log("Da la recompenza");
            }
            else
                Debug.Log("No da la recompenza");
        }
    }
}
