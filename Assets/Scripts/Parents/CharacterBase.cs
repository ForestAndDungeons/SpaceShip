using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour , IObservable
{
    [SerializeField] protected float _maxSpeed;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;

    [SerializeField] protected GameObject _shield;
    [SerializeField] protected bool _isShieldUp;

    [SerializeField] protected float _rateOfFire;
    [SerializeField] protected int _burstSize;
    [SerializeField] protected bool _canFire;
    [SerializeField] protected float _shootTime;
    protected float _shootTimer;
    protected bool _shooting;
    [SerializeField] protected bool _isRandomBullet;
    [SerializeField] protected bool _isSinuousBullet;
    protected List<IObserver> _allObservers;


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
        var interactuable = other.GetComponent<Interactive>();

        if (interactuable != null)
            interactuable.Interact(this);
    }

    public void Subscribe(IObserver obs)
    {
        if (!_allObservers.Contains(obs))
        {
            _allObservers.Add(obs);
        }
    }

    public void NotifyToObservers(string notif)
    {
        for (int i = _allObservers.Count - 1; i >= 0; i--)
        {
            _allObservers[i].Notify(notif);
        }
    }

    public void Unsuscribe(IObserver obs)
    {
        if (_allObservers.Contains(obs))
        {
            _allObservers.Remove(obs);
        }
    }

    public abstract void OnDeath();
    public virtual void OnDamageEvent(){}
    public void SetShieldUp(bool value) { _isShieldUp = value; _shield.SetActive(value); }
    public void SetSpeed(float value) { _maxSpeed = value; }
    public void AddFireRate(int value) { _rateOfFire += value; }
    public void AddFireBurst(int value) { _burstSize += value; }
    public void ReduceShootTime(float value) { _shootTime -= value; }
    public void SetShieldUps(bool value) { _isShieldUp = value; _shield.SetActive(value); }
    public void SetIsRandomBullet(bool value) { _isRandomBullet = value;}
    public void SetIsSinuousBullet(bool value) { _isSinuousBullet = value;}
}