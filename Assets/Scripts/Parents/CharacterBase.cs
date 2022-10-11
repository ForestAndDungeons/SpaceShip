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

            /*_animationController.onHit();
            _playerSoundManager.playOnCollision(_audioSource, _audioClip[0]);
            _particleSystem.Play();
            _player._uiPlayer.UIArtificialUpdate(_maxHealth, _currentHealth, _armor);
            SetIsImmune(true);
            _playerSoundManager.playOnHit();
            _player.StartCoroutine(_player.TimeOfImmune());*/

            if (_currentHealth <= 0)
            {
                OnDeath();

                _myAudioSource.Play();
                _myParticleSystem.Play();

                /*_myAudioSource.playOnDeath();
                _animationController.onDeath();
                _player.DisableThisObject();*/
            }
        }
    }

    public abstract void OnDeath();
    public virtual void OnDamageEvent(){}
    public void SetShieldUp(bool value) { _isShieldUp = value; _shield.SetActive(value); }
    public void SetSpeed(float value) { _maxSpeed = value; }
}