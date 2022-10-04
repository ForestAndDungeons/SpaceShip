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

    protected Animator _myAnimator;

    public void onDamage(float damage)
    {
        if (_currentHealth > 0)
        {
            if (!_isShieldUp)
            {
                _currentHealth -= damage;
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
                /*_playerSoundManager.playOnDeath();
                _animationController.onDeath();
                _player.DisableThisObject();*/
            }
        }
    }

    public void SetSpeed(float value) { _maxSpeed = value; }
    public void SetShieldUp(bool value) { _isShieldUp = value; _shield.SetActive(value); }
}
