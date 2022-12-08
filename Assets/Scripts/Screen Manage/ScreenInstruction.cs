using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInstruction : ScreenUI
{
   public void BTN_Back()  { 
        GameManager.Instance.GetScreenManager().PopScreen();
        GameManager.Instance.GetScreenManager().PushScreen("MenuPanel", transform.parent);
    }
}
