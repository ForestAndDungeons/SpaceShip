using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager
{
    bool _isPaused = false;

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        _isPaused = false;
        Time.timeScale = 1f;
    }
}
