using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive  : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected float _maxDistance;
    protected float _currentDistance;
    protected AudioSource _audioSource;
    protected Renderer _renderer;
    protected Collider _collider;
    protected ParticleSystem _particleSystem;

    public abstract void Interact(CharacterBase entity);

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnInteraction()
    {
        Activate();
    }

    public void Activate()
    {
        _audioSource.Play();
        _particleSystem.Play();
        _renderer.enabled = false;
        _collider.enabled = false;

        StartCoroutine(WaitReturn());
    }

    public virtual void Movement()
    {
        transform.forward = Vector3.forward * -1;
        transform.position += transform.forward * _speed * Time.deltaTime;

        _currentDistance += _speed * Time.deltaTime;

        if (_currentDistance > _maxDistance)
        {
            ReturnToPool();
        }
    }
    public virtual void MovementRight()
    {
        transform.position += transform.right * _speed * Time.deltaTime;

        _currentDistance += _speed * Time.deltaTime;

        if (_currentDistance > _maxDistance)
        {
            ReturnToPool();
        }
    }

    public abstract void ReturnToPool();
    public abstract IEnumerator WaitReturn();

    public float GetSpeed() { return _speed; }
}