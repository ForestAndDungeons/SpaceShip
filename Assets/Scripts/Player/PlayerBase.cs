using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : CharacterBase
{
    [SerializeField]protected PlayerBaseSO _data;
    [SerializeField] protected Controller _controller;

    [SerializeField] protected float _credits;

    public void Movement(Transform player)
    {
        player.transform.position += _controller.GetMovementInput() * _maxSpeed * Time.deltaTime;
    }

    public void Shoot()
    {
        Bullet b = BulletFactory.Instance.GetBullet();

        b.transform.position = GameManager.Instance.playerReference.transform.position;
        b.transform.forward = Vector3.forward;
    }

    public void UpdateAnimatorVariables()
    {
        _myAnimator.SetFloat("Horizontal", _controller.GetMovementInput().x);
        _myAnimator.SetFloat("Vertical", _controller.GetMovementInput().y);
    }

    public void SetCredits(float value) { _credits = value; }
}