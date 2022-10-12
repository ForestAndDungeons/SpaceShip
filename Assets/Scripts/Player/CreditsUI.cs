using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUI : MonoBehaviour
{
    Text _creditsText;
    [SerializeField] int _credits;

    private void Awake()
    {
        _creditsText = GetComponent<Text>();
        EventManager.SubscribeToEvent(Contants.EVENT_INICIATECREDITS, CreditsAwake);
        EventManager.SubscribeToEvent(Contants.EVENT_ADDCREDITUI, AddCredits);
    }

    public void CreditsAwake(params object[] param)
    {
        if (_creditsText!=null)
        {
            _credits = (int)param[0];
            _creditsText.text = ""+_credits.ToString();    
        }
    }


    public void AddCredits(params object[] param)
    {
        if (_creditsText!=null)
        {
            _credits = (int)param[0];
            _creditsText.text =""+ _credits.ToString();
        }
      
    }

}
