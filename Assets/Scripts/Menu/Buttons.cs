using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] Sprite _upSprite;
    AudioSource _myAudioSource;

    void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
    }

    public void DownSprite(Image button)
    {
        _myAudioSource.Play();
        button.overrideSprite = _downSprite;
    }

    public void UpSprite(Image button)
    {
        button.overrideSprite = _upSprite;
    }
}