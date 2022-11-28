using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Enemy : EnemyBase
{
    bool flag = false;
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

        if (!flag && GameManager.Instance.GetCountDeadEnemies() == GameManager.Instance.GetEnemyManager().GetCounter())
        {
            flag = true;
            Instantiate(GameManager.Instance.GetPrefabBoss(), new Vector3(50, 3, 30), Quaternion.identity);
        }
        if (GameManager.Instance.GetCountDeadEnemies() > GameManager.Instance.GetEnemyManager().GetCounter())
            GameManager.Instance.ChangeScene("Victory");
    }

    public void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }
}