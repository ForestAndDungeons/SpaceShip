using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : PickUp
{
    [Header("Shield Parameters")]
    [SerializeField] float _speed;
    [SerializeField] float _maxDistance;
    float _currentDistance;

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
            ShieldFactory.Instance.ReturnShield(this);
        }
    }

    public override void Pick(Player player)
    {
        player.SetShieldUps(true);
        //OnPickUp();
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

    public static void TurnOn(Shield s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(Shield s)
    {
        s.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        OnDestroy();
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
        yield return new WaitForSeconds(1f);
        ShieldFactory.Instance.ReturnShield(this);
    }
}
