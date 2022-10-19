using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;

public class Player : PlayerBase
{
    void Awake()
    {
        _maxHealth = _data.maxHealth;
        _currentHealth = _data.maxHealth;
        _maxSpeed = _data.maxSpeed;
        _myAnimator = GetComponentInChildren<Animator>();
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponentInChildren<ParticleSystem>();
        _allObservers = new List<IObserver>();
        _shootTimer = _shootTime;
        _canFire = true;
        _shooting = false;
    }

    private void Start()
    {
        EventManager.TriggerEvent(Contants.EVENT_INICIATEHEALTHBAR, _data.maxHealth, _currentHealth);
        EventManager.TriggerEvent(Contants.EVENT_INICIATECREDITS, GameManager.Instance.GetCredits());
    }

    void Update()
    {
        GameManager.Instance.GetBoundManager().CheckBounds(this);
        UpdateAnimatorVariables();
        Movement(transform);

        if(_shooting)
            Shoot();
    }

    public void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }
}