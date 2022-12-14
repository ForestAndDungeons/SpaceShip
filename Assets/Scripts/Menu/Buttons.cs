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

    [SerializeField] StaminaSystem _staminaSystem;

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
    public void ChangeSceneAsync(string sceneToLoad) {
        if (_staminaSystem.GetCurrentStamina() > 0)
            GameManager.Instance.ChangeSceneAsync(sceneToLoad);
    }

    public void ChangeMusic(AudioClip clip) { GameManager.Instance.ChangeMusic(clip); }
    public void ChangeMusicToLevel(AudioClip clip)
    {
        if (_staminaSystem.GetCurrentStamina() > 0)
            GameManager.Instance.ChangeMusic(clip);
    }
    public void GyroOption() { GameManager.Instance.InvertIsGyro(); }
    public void InvertObj(GameObject obj)
    {
        if (obj.activeSelf)
            obj.SetActive(false);
        else
            obj.SetActive(true); 
    }

    public void Pause() { GameManager.Instance.Pause(); }
    public void UnPause() { GameManager.Instance.UnPause(); }
    public void QuitGame() { GameManager.Instance.QuitGame(); }
    public void SaveGame() { GameManager.Instance.SaveGame(); }
    public void LoadGame() { GameManager.Instance.LoadGame(); GameManager.Instance.SetCredits(GameManager.Instance.GetJSONManager()._data.credits); }
    public void DeleteSave() { GameManager.Instance.DeleteSave(); }
    public void SaveCredits() { GameManager.Instance.SaveCredits(); }
    public void Mute() { GameManager.Instance.Mute(_soundOnIcon, _soundOffIcon); }
    public void UpdateButtonIcon() { GameManager.Instance.UpdateButtonIcon(_soundOnIcon, _soundOffIcon); }


}