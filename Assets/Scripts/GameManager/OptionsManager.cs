using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager
{
    public bool _onMuted = false;
    Image _soundOnIcon;
    Image _soundOffIcon;

    public void Mute(Image soundOnIcon, Image soundOffIcon)
    {
        _onMuted = !_onMuted;
        AudioListener.pause = _onMuted;
        Save();
        UpdateButtonIcon(soundOnIcon, soundOffIcon);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("_onMuted", _onMuted ? 1 : 0);
    }

    public void Load()
    {
        _onMuted = PlayerPrefs.GetInt("_onMuted") == 1;
    }

    public void UpdateButtonIcon(Image soundOnIcon, Image soundOffIcon)
    {
        if (_onMuted == false)
        {
            soundOnIcon.enabled = true;
            soundOffIcon.enabled = false;
        }
        else if (_onMuted == true)
        {
            soundOnIcon.enabled = false;
            soundOffIcon.enabled = true;
        }
    }

    public void SetVolume(float volume)
    {
        //_audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
}