using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] Sprite _downSprite;
    [SerializeField] Sprite _upSprite;

    [SerializeField] Image _soundOnIcon;
    [SerializeField] Image _soundOffIcon;

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

    public void ChangeScene(string sceneToLoad) { GameManager.Instance.ChangeScene(sceneToLoad); }
    public void ChangeMusic(AudioClip clip) { GameManager.Instance.ChangeMusic(clip); }
    public void Pause() { GameManager.Instance.Pause(); }
    public void UnPause() { GameManager.Instance.UnPause(); }
    public void QuitGame() { GameManager.Instance.QuitGame(); }
    public void SaveGame() { GameManager.Instance.SaveGame(); }
    public void LoadGame() { GameManager.Instance.LoadGame(); GameManager.Instance.SetCredits(GameManager.Instance.GetJSONManager()._data.credits); }
    public void Mute() { GameManager.Instance.Mute(_soundOnIcon, _soundOffIcon); }
    public void UpdateButtonIcon() { GameManager.Instance.UpdateButtonIcon(_soundOnIcon, _soundOffIcon); }
}