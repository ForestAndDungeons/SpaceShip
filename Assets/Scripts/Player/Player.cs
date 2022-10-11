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
        _credits = GameManager.Instance.GetJSONManager()._data.credits;
    }

    private void Start()
    {
        EventManager.TriggerEvent(Contants.EVENT_INICIATEHEALTHBAR, _data.maxHealth, _currentHealth);
    }

    void Update()
    {
        GameManager.Instance.GetJSONManager()._data.credits = _credits;
        GameManager.Instance.GetBoundManager().CheckBounds(this);
        UpdateAnimatorVariables();
        Movement(transform);
        if (isRandomBullet || isSinuousBullet)
        {
            PowerUpTime();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        var pickUp = other.GetComponent<PickUp>();

        if (pickUp != null)
            pickUp.Pick(this);
    }
    public void SetShieldUps(bool value) { _isShieldUp = value; _shield.SetActive(value); }
}