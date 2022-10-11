using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    int _chance;
    protected IAdvance _linealXAdvance;
    IAdvance _sinuousAdvance;
    IAdvance _currentAdvance;
    public bool isRandomAdvance;
    public bool isSinuousAdvance;
    IAdvance _linealBullet;

    public void Movement()
    {

        _linealXAdvance.Advance(); 
    }

    void SinMovement()
    {
        //_sinuousAdvance = new SinuousAdvance(b.transform, b.speed, _amplitude, _period, _displacement, _vertical);
        //_currentBullet = _sinuousAdvance;
    }

    void LinearMovement()
    {
        _linealXAdvance = new LinealXAdvance(_maxSpeed, transform);
    }

    public void Shoot()
    {
        StartCoroutine(FireBurst());
    }

    public  IEnumerator FireBurst()
    {
        _canFire = false;

        float bulletDelay = 60 / _rateOfFire;
        //Rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.

        for (int i = 0; i < _burstSize; i++)
        {
            EnemyBullet b = EnemyBulletFactory.Instance.GetBullet();
            _linealBullet = new LinealZAdvance(b.speed, b.transform);
            b.currentAdvance = _linealBullet;

            b.transform.forward = Vector3.forward;
            b.transform.position = transform.position;
            b.transform.forward = Vector3.forward * -1;

            yield return new WaitForSeconds(bulletDelay);// wait till the next round
        }
        _canFire = true;
    }

    public override void OnDeath()
    {
        _chance = Random.Range(0, 11);

        if (_chance <= 3)
        {
            Shield s = ShieldFactory.Instance.GetShield();

            s.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            s.transform.forward = Vector3.forward * -1;
        }
        else if (_chance == 4)
        {
            FireRate r = FireRateFactory.Instance.GetFireRate();

            r.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            r.transform.forward = Vector3.forward * -1;
        }
        else if (_chance == 5)
        {
            FireBurst b = FireBurstFactory.Instance.GetFireBurst();

            b.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            b.transform.forward = Vector3.forward * -1;
        }

        GameManager.Instance.SetCountDeadEnemies(1);

        Destroy(gameObject, 0.5f);
    }
}