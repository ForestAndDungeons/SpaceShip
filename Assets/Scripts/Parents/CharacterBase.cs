using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;

    [SerializeField] protected GameObject _shield;
    [SerializeField] protected bool _isShieldUp;

    [SerializeField] protected float _rateOfFire;
    [SerializeField] protected int _burstSize;
    [SerializeField] protected bool _canFire;

    protected AudioSource _myAudioSource;
    protected ParticleSystem _myParticleSystem;
    protected Animator _myAnimator;

    public void OnDamage(float damage)
    {
        if (_currentHealth > 0)
        {
            if (!_isShieldUp)
            {
                _currentHealth -= damage;
                OnDamageEvent();
            }
            else
                SetShieldUp(false);

            if (_currentHealth <= 0)
            {
                OnDeath();

                _myAudioSource.Play();
                _myParticleSystem.Play();
            }
        }
    }

    public void Interact(Collider other)
    {
        var interactuable = other.GetComponent<Interactive >();

        if (interactuable != null)
            interactuable.Interact(this);
    }

    public abstract void OnDeath();
    public virtual void OnDamageEvent(){}
    public void SetShieldUp(bool value) { _isShieldUp = value; _shield.SetActive(value); }
    public void SetSpeed(float value) { _maxSpeed = value; }
    public void SetFireRate(int value) { _rateOfFire += value; }
    public void SetFireBurst(int value) { _burstSize += value; }
    public void SetShieldUps(bool value) { _isShieldUp = value; _shield.SetActive(value); }
}