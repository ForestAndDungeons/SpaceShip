using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   /* [Header("Sinuous Bullet")]
    public float amplitude;
    public float period;
    public float displacement;
    public float vertical;*/

    [Header("Bullet Parameters")]
    public float speed;
    [SerializeField] float _maxDistance;
    float _currentDistance;

    [SerializeField] float _damage;

    AudioSource _myAudioSource;
    ParticleSystem _myParticleSystem;
    Collider _myCollider;
    MeshRenderer _myMeshRenderer;
    public IAdvance currentAdvance;

    void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponent<ParticleSystem>();
        _myCollider = GetComponent<Collider>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(currentAdvance!=null) currentAdvance.Advance();

        _currentDistance += speed * Time.deltaTime;
        
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