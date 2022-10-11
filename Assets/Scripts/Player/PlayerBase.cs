using System.Collections;
using UnityEngine;

public abstract class PlayerBase : CharacterBase
{
    [SerializeField] protected PlayerBaseSO _data;
    [SerializeField] protected Controller _controller;

    [SerializeField] protected float _credits;
    [SerializeField] float _startTimeResetBulletAdvance;
    float _timeResetBulletAdvance;
    
    protected float _amplitude;
    protected float _period;
    protected float _displacement;
    protected float _vertical;
    public bool isRandomBullet;
    public bool isSinuousBullet;
    IAdvance _linealBullet;
    IAdvance _sinuousBullet;
    IAdvance _randomBullet;
    IAdvance _currentBullet;

    public void Movement(Transform player)
    {
        player.transform.position += _controller.GetMovementInput() * _maxSpeed * Time.deltaTime;
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
            StartCoroutine(FireBurst());
    }
    public IEnumerator FireBurst()
    {
        _canFire = false;

        float bulletDelay = 60 / _rateOfFire;
        //Rate of fire in weapons is in rounds per minute (RPM), therefore we should calculate how much time passes before firing a new round in the same burst.
        
        for (int i = 0; i < _burstSize; i++)
        {
            Bullet b = BulletFactory.Instance.GetBullet();
            
            //////////
            _linealBullet = new LinealZAdvance(b.speed, b.transform);
            _currentBullet = _linealBullet;
            if (isSinuousBullet)
            {
                _sinuousBullet = new SinuousAdvance(b.transform, b.speed,_amplitude, _period, _displacement, _vertical);
                _currentBullet = _sinuousBullet;
            }
            else if (isRandomBullet)
            {
                _randomBullet = new SinuousAdvance(b.transform, Random.Range(50, 61), Random.Range(10, 71), Random.Range(0, 9), Random.Range(0, 11), Random.Range(0, 11));
                _currentBullet = _randomBullet;
            }
            b.currentAdvance = _currentBullet;
            //////////
            
            b.transform.position = GameManager.Instance.playerReference.transform.position;
            b.transform.forward = Vector3.forward;

            yield return new WaitForSeconds(bulletDelay);// wait till the next round
        }
        _canFire = true;
    }

    public void PowerUpTime()
    {
        if (_timeResetBulletAdvance <= 0)
        {
            _timeResetBulletAdvance = _startTimeResetBulletAdvance;
            isRandomBullet = false;
            isSinuousBullet = false;
            _currentBullet = _linealBullet;
            Debug.Log(_timeResetBulletAdvance);
        }
        else
        {
            _timeResetBulletAdvance -= Time.deltaTime;
        }
    }

    public void SetFireRate(int value) { _rateOfFire += value; }
    public void SetFireBurst(int value) { _burstSize += value; }
    public void SetAmplitude(float value) { _amplitude = value; }
    public void SetPeriod(float value) { _period = value; }
    public void SetDisplacement(float value) { _displacement = value; }
    public void SetVertical(float value) { _vertical = value; }
    public void SetCredits(float value) { _credits = value; }
    public float GetCredits() { return _credits; }

    public IEnumerator End()
    {

        EventManager.TriggerEvent(Contants.EVENT_LOSEGAME, "Defeat");
        yield return new WaitForSeconds(2f);
    }
}