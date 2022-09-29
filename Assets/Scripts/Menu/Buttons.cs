using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    Button _button;
    [SerializeField] Sprite _downSprite;
    [SerializeField] Sprite _upSprite;

    void Awake()
    {
        _button = this.GetComponent<Button>();
    }

    public void DownSprite()
    {
        _button.image.overrideSprite = _downSprite;
    }

    public void UpSprite()
    {
        _button.image.overrideSprite = _upSprite;
    }
}
