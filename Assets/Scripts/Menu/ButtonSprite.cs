using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSprite : MonoBehaviour
{
    Image _button;
    Buttons _father;

    void Awake()
    {
        _button = GetComponent<Image>();
        _father = GetComponentInParent<Buttons>();
    }

    public void DownSprite()
    {
        _father.DownSprite(_button);
    }

    public void UpSprite()
    {
        _father.UpSprite(_button);
    }
}