using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsManager
{
    [SerializeField] AudioMixer _audioMixer;
    //Resolution[] _resolutions;
    //[SerializeField] Dropdown _resoulutionDropdown;

    public OptionsManager(AudioMixer audioMixer)
    {
        _audioMixer = audioMixer;
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

    public void SetVolume(float volume)
    {
        _audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    
    /*public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }*/
}