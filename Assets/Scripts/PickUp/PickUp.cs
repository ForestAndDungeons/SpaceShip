using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public abstract class PickUp : MonoBehaviour
{
    AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;

    protected Renderer _renderer;
    protected Collider _collider;
    protected ParticleSystem _particleSystem;

    public abstract void Pick(Player player);

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnPickUp(Player player)
    {
        _audioSource.PlayOneShot(_audioClip);
        Activate();
    }

    public void Activate()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _particleSystem = GetComponent<ParticleSystem>();

        _particleSystem.Play();

        _renderer.enabled = false;
        _collider.enabled = false;

        Destroy(this.gameObject, 1f);
    }
}