using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenOptions : ScreenUI
{
    [SerializeField] GameObject _supportGyroscope;
    [SerializeField] GameObject _noSupportGyroscope;
    private void Start()
    {
        if (GameManager.Instance.GetSupportGyro())
        {
            _noSupportGyroscope.SetActive(false);
            _supportGyroscope.SetActive(true);
        }
        else
        {
            _noSupportGyroscope.SetActive(true);
            _supportGyroscope.SetActive(false);
        }
    }
    public void BTN_Back() { 
        GameManager.Instance.GetScreenManager().PopScreen();
        GameManager.Instance.GetScreenManager().PushScreen("MenuPanel", transform.parent);
    }
}
