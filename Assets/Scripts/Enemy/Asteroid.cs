using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxDistance;
    float _currentDistance;

    [SerializeField] float _damage;

    [SerializeField] int _chance;

    AudioSource _myAudioSource;
    ParticleSystem _myParticleSystem;
    Collider _myCollider;
    MeshRenderer _myMeshRenderer;

    void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponent<ParticleSystem>();
        _myCollider = GetComponent<Collider>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        _currentDistance += _speed * Time.deltaTime;

        if (_currentDistance > _maxDistance)
        {
            AsteroidFactory.Instance.ReturnAsteroid(this);
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

    public static void TurnOn(Asteroid a)
    {
        a.gameObject.SetActive(true);
    }

    public static void TurnOff(Asteroid a)
    {
        a.gameObject.SetActive(false);
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
            entity.OnDamage(_damage);
    }

    void OnDestroy()
    {
        _chance = Random.Range(0,11);

        if (_chance <=3)
        {
            RandomBulletPU r = RandomBulletFactory.Instance.GetRandomBullet();

            r.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            r.transform.forward = Vector3.forward * -1;
        }
        else if (_chance <=5 && _chance > 3)
        {
            SinuousBulletPU s = SinuousFactory.Instance.GetSinuous();

            s.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            s.transform.forward = Vector3.forward * -1;
        }

        _myAudioSource.Play();
        _myParticleSystem.Play();
        _myCollider.enabled = false;
        _myMeshRenderer.enabled = false;

        StartCoroutine("WaitReturn");
    }

    IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        AsteroidFactory.Instance.ReturnAsteroid(this);
    }
}