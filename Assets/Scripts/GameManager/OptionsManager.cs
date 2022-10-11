using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager
{
    public bool _onMuted = false;

    //[SerializeField] AudioMixer _audioMixer;
    //Resolution[] _resolutions;
    //[SerializeField] Dropdown _resoulutionDropdown;

    public OptionsManager()
    {
        //_audioMixer = audioMixer;
    }

    /* void Start()
    {
        _resolutions = Screen.resolutions;
        _resoulutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resoulutionDropdown.AddOptions(options);
        _resoulutionDropdown.value = currentResolutionIndex;
        _resoulutionDropdown.RefreshShownValue();
    }*/

    public void Mute(Image soundOn, Image soundOff)
    {
        _onMuted = !_onMuted;
        AudioListener.pause = _onMuted;
        Save();
        UpdateButtonIncon(soundOn, soundOff);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("_onMuted", _onMuted ? 1 : 0);
    }

    public void Load()
    {
        _onMuted = PlayerPrefs.GetInt("_onMuted") == 1;
    }

    public void UpdateButtonIncon(Image soundOn, Image soundOff)
    {
        if (_onMuted == false)
        {
            soundOn.enabled = true;
            soundOff.enabled = false;
        }
        else if (_onMuted == true)
        {
            soundOn.enabled = false;
            soundOff.enabled = true;
        }
    }

    public void SetVolume(float volume)
    {
        //_audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    
    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }*/
}