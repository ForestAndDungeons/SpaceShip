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
    }

    void Update()
    {
        
    }
}