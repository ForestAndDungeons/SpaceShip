using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Sinuous Bullet")]
    [SerializeField]float _amplitude;
    [SerializeField] float _period;
    [SerializeField] float _displacement;
    [SerializeField] float _vertical;

    [Header("Bullet Parameters")]
    [SerializeField] float _speed;
    [SerializeField] float _maxDistance;
    float _currentDistance;

    [SerializeField] float _damage;

    AudioSource _myAudioSource;
    ParticleSystem _myParticleSystem;
    Collider _myCollider;
    MeshRenderer _myMeshRenderer;
    IBulletAdvance _linealAdvance;
    IBulletAdvance _sinuousAdvance;
    IBulletAdvance _randomAdvance;
    IBulletAdvance _currentAdvance;

    void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponent<ParticleSystem>();
        _myCollider = GetComponent<Collider>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
        _linealAdvance = new LinealAdvance(_speed,this.transform);
        _sinuousAdvance = new SinuousAdvance(this.transform, _speed, _amplitude, _period, _displacement, _vertical);
        _randomAdvance = new SinuousAdvance(this.transform, Random.Range(50,61), Random.Range(10, 71), Random.Range(0, 9), Random.Range(0, 11), Random.Range(0, 11));
        _currentAdvance = _linealAdvance;
    }

    void Update()
    {
        if(_currentAdvance!=null) _currentAdvance.BulletAdvance();

        _currentDistance += _speed * Time.deltaTime;
        
        if (_currentDistance > _maxDistance)
        {
            BulletFactory.Instance.ReturnBullet(this);
        }
    }

    void OnEnable()
    {
        _myCollider.enabled = true;
        _myMeshRenderer.enabled = true;
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

    void OnTriggerEnter(Collider other)
    {
        Damage(other);
        OnDestroy();
    }

    void Damage(Collider other)
    {
        var entity = other.GetComponent<CharacterBase>();

        if (entity != null)
            entity.onDamage(_damage);
    }

    void OnDestroy()
    {
        _myAudioSource.Play();
        _myParticleSystem.Play();
        _myCollider.enabled = false;
        _myMeshRenderer.enabled = false;

        StartCoroutine("WaitReturn");
    }

    IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1.5f);
        BulletFactory.Instance.ReturnBullet(this);
    }
}