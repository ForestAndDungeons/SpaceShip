using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    Animator _myAnimator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    void Awake()
    {
        _myAnimator = GetComponent<Animator>();
    }

    public void OnTouch()
    {
        _myAnimator.SetBool("onTouch", true);
    }

    void OnLevelWasLoaded()
    {
        _audioSource.clip = _audioClip;
        _audioSource.Play();
        Destroy(gameObject);
    }
}