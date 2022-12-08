using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMenuPanel : ScreenUI
{
    public void BTN_Shop()
    {
        GameManager.Instance.GetScreenManager().PushScreen("Shop", transform.parent);
    }

    public void BTN_Options()
    {
        GameManager.Instance.GetScreenManager().PushScreen("MenuOptions", transform.parent);
    }

    public void BTN_Instructions()
    {
        GameManager.Instance.GetScreenManager().PushScreen("Instructions", transform.parent);
    }
}
