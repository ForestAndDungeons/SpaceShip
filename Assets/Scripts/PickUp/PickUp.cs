using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] protected float _currentDistance;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxDistance;
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

    public void Movement()
    {
        transform.position += -transform.forward * _speed * Time.deltaTime;

        _currentDistance += _speed * Time.deltaTime;

        if (_currentDistance > _maxDistance)
        {
            Destroy(gameObject);
        }
    }
}