using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : CharacterBase
{
    [SerializeField] protected PlayerBaseSO _data;
    [SerializeField] protected Controller _controller;

    [SerializeField] protected float _credits;
    [SerializeField] float _rateOfFire;
    [SerializeField] int _burstSize;
    [SerializeField] bool _canFire = true;

    public void Movement(Transform player)
    {
        player.transform.position += _controller.GetMovementInput() * _maxSpeed * Time.deltaTime;
    }

    public void Shoot()
    {
        if(_canFire)
            StartCoroutine(FireBurst());
           
    }

    public override void onDeath()
    {
        StartCoroutine("End");
    }

    public void UpdateAnimatorVariables()
    {
        _myAnimator.SetFloat("Horizontal", _controller.GetMovementInput().x);
        _myAnimator.SetFloat("Vertical", _controller.GetMovementInput().y);
    }

    public void SetCredits(float value) { _credits = value; }
    
    public IEnumerator FireBurst()
    {
        _canFire = false;

        float bulletDelay = 60 / _rateOfFire;
        // rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
        for (int i = 0; i < _burstSize; i++)
        {
            Bullet b = BulletFactory.Instance.GetBullet();
            b.transform.position = GameManager.Instance.playerReference.transform.position;
            b.transform.forward = Vector3.forward;
            
            yield return new WaitForSeconds(bulletDelay);// wait till the next round
        }
        _canFire = true;
    }

    public void SetFireRate(int value) { _rateOfFire += value; }
    public void SetFireBurst(int value) { _burstSize += value; }

    public IEnumerator End()
    {
        GameManager.Instance.ChangeScene("Defeat");
        yield return new WaitForSeconds(2f);
    }
}