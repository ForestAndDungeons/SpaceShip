using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Enemy : EnemyBase
{
    void Awake()
    {
        _currentHealth = _maxHealth;
        _myAnimator = GetComponent<Animator>();
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponent<ParticleSystem>();
        
        _linealXAdvance = new LinealXAdvance(_maxSpeed, transform);
        _canFire = true;
    }

    void Update()
    {
        //GameManager.Instance.GetBoundManager().CheckBounds(this);
        Movement();
        if(_canFire)
            Shoot();
    }

    public void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }
}