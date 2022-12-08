using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager 
{
    Stack<IScreen> _screenStack;

    public ScreenManager()
    {
        _screenStack = new Stack<IScreen>();
    }
    public void PopScreen()
    {
        if (_screenStack.Count <= 1) return;

        _screenStack.Pop().Free();
    }

    public void PushScreen(IScreen newScreen)
    {
        _screenStack.Push(newScreen);    
    }

    public void PushScreen(string resourceName, Transform parent = null)
    {
        var newScreen = GameObject.Instantiate(Resources.Load<GameObject>(resourceName), parent);

        PushScreen(newScreen.GetComponent<IScreen>());
    }
}
