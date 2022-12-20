using System.Collections;
using UnityEngine;

public abstract class PlayerBase : CharacterBase
{
    [SerializeField] protected PlayerBaseSO _data;
    [SerializeField] protected Controller _controller;
    [SerializeField] protected GameObject _joystickBase;
    [SerializeField] float _startTimeResetBulletAdvance;
    [SerializeField] protected Mesh _playerMesh;
    [SerializeField] protected Material _playerMaterial;
    [SerializeField] protected float _speedGyroX;
    [SerializeField] protected float _speedGyroZ;
    protected Gyroscope _gyro;
    float _timeResetBulletAdvance;

    public void Movement(Transform player)
    {
        player.transform.position += _controller.GetMovementInput() * _maxSpeed * Time.deltaTime;
    }

    public void GyroMovement(Transform player)
    {
        player.transform.position += new Vector3(_gyro.gravity.x* _speedGyroX  ,0f,-_gyro.gravity.z* _speedGyroZ) * _maxSpeed * Time.deltaTime;
    }

    public override void OnDeath()
    {
        StartCoroutine("End");
    }

    public void UpdateAnimatorVariables()
    {
        _myAnimator.SetFloat("Horizontal", _controller.GetMovementInput().x);
        _myAnimator.SetFloat("Vertical", _controller.GetMovementInput().y);
    }

    public void Shoot()
    {
        if (_canFire)
        {
            _shootTimer -= Time.deltaTime;

            if (_shootTimer <= 0.0f)
            {
                _shootTimer = _shootTime;
                _myAudioSource.PlayOneShot(_audioClips[0]);
                StartCoroutine(FireBurst());
            }
        }
    }

    public void SetShooting() { _shooting = !_shooting; }

    public IEnumerator FireBurst()
    {
        _canFire = false;

        float bulletDelay = 60 / _rateOfFire;
        //Rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
        
        for (int i = 0; i < _burstSize; i++)
        {
            Bullet b = GameManager.Instance.bulletFactory.GetBullet();

            b.SetDamage(_bulletStats);

            //Temporal//
            if (_isSinuousBullet)
            {
                NotifyToObservers(Contants.OBS_SINUOUSADVANCE);
            }
            else if (_isRandomBullet)
            {
                NotifyToObservers(Contants.OBS_RANDOMADVANCE);
            }
            else
            {
                NotifyToObservers(Contants.OBS_LINEALADVANCE);
            }

            
            b.transform.position = GameManager.Instance.playerReference.transform.position;
            b.transform.forward = Vector3.forward;

            yield return new WaitForSeconds(bulletDelay); //Wait till the next round
        }
        _canFire = true;
    }

    public override void OnDamageEvent() { EventManager.TriggerEvent(Contants.EVENT_PLAYERONDAMAGE, _currentHealth); }

    public IEnumerator End()
    {
        EventManager.TriggerEvent(Contants.EVENT_LOSEGAME, "Defeat");
        yield return new WaitForSeconds(2f);
    }
}