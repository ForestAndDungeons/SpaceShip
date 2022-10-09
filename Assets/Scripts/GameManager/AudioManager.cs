using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    AudioSource _audioSource;
    [SerializeField] AudioClip[] _audioClips;

    public AudioManager(AudioSource audioSource, AudioClip[] audioClips)
    {
        _audioSource = audioSource;
        _audioClips = audioClips;
    }

    public void ChangeMusic(AudioClip clip) { _audioSource.clip = clip; PlayMusic(); }
    public void MenuMusic() { _audioSource.clip = _audioClips[0]; PlayMusic(); }
    public void LevelMusic() { _audioSource.clip = _audioClips[1]; PlayMusic(); }
    public void PlayMusic() { _audioSource.Play(); }
}