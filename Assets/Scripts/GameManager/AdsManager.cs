using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    string gameId = "5057479";
    string adID;
    [SerializeField] StaminaSystem _staminaSystem;

    public void Start()
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
                _staminaSystem.AddCurrentStamina(5);
                Debug.Log("Da la recompenza");
            }
            else
                Debug.Log("No da la recompenza");
        }
    }
}