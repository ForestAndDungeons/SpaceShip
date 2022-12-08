using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    Animator _myAnimator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] GameObject _menuTest;

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

    public void BTN_Menu()
    {
        _menuTest.SetActive(false);
        GameManager.Instance.GetScreenManager().PushScreen("MenuPanel", GameManager.Instance.mainCanvas);
    }
}