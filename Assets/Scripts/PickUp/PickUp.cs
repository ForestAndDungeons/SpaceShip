using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class PickUp : MonoBehaviour
{
    AudioSource _audioSource;
    Renderer _renderer;
    Collider _collider;
    ParticleSystem _particleSystem;

    public abstract void Pick(Player player);

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnPickUp()
    {
        Activate();
    }

    public void Activate()
    {
        _audioSource.Play();
        _particleSystem.Play();
        _renderer.enabled = false;
        _collider.enabled = false;

        Destroy(this.gameObject, 1f);
    }
}