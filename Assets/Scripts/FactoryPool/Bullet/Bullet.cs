using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Interactive, IObserver
{
    [SerializeField] protected float _damage = 1;

    [Header("SINUOUS BULLET")]
    [SerializeField] float _amplitud;
    [SerializeField] float _period;
    [SerializeField] float _displacement;
    [SerializeField] float _vertical;
    IObservable _player;
    IAdvance _linealAdvance;
    IAdvance _sinuousAdvance;
    IAdvance _randomAdvance;
    IAdvance _currentAdvance;
    Dictionary<string, System.Action> _bulletActions;

    void Start()
    {
        _bulletActions = new Dictionary<string, System.Action>();
        _bulletActions.Add(Contants.OBS_LINEALADVANCE, LinealAdvance);
        _bulletActions.Add(Contants.OBS_SINUOUSADVANCE, SinuousAdvance);
        _bulletActions.Add(Contants.OBS_RANDOMADVANCE, RandomAdvance);

        _player = GameManager.Instance.playerReference;
        _player.Subscribe(this);
    }

    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        if (entity != null)
        {
            entity.OnDamage(_damage);
            OnInteraction();
        }
    }

    public override void Movement()
    {
        if (_currentAdvance != null) _currentAdvance.Advance();
        _currentDistance += _speed * Time.deltaTime;
        if (_currentDistance > _maxDistance) ReturnToPool();
    }

    void OnEnable()
    {
         _collider.enabled = true;
         _renderer.enabled = true;
    }

    void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(Bullet b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.bulletFactory.Instance.ReturnBullet(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.bulletFactory.Instance.ReturnBullet(this);
    }

    public void Notify(string notif)
    {
        if (_bulletActions.ContainsKey(notif))
        {
            _bulletActions[notif]();
        }
    }

    void LinealAdvance()
    {
        _linealAdvance = new LinealZAdvance(_speed,transform);
        _currentAdvance = _linealAdvance;
    }

    void SinuousAdvance()
    {
        _sinuousAdvance = new SinuousAdvance(transform, _speed, _amplitud, _period, _displacement, _vertical);
        _currentAdvance = _sinuousAdvance;
    }

    void RandomAdvance()
    {
        _randomAdvance = new SinuousAdvance(transform, Random.Range(50, 61), Random.Range(10, 71), Random.Range(0, 9), Random.Range(0, 11), Random.Range(0, 11));
        _currentAdvance = _randomAdvance;
    }

    public float GetDamage() { return _damage; }
    public void SetDamage(Bullet bullet)
    {
        Debug.Log(_damage);
        Debug.Log(bullet._damage);
        _damage = bullet._damage;
    }
    public void AddDamage()
    {
        _damage++;
    }
}