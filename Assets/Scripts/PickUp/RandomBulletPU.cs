using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBulletPU : PickUp
{
    AudioSource _myAudioSource;
    ParticleSystem _myParticleSystem;
    Collider _myCollider;
    MeshRenderer _myMeshRenderer;
    SpriteRenderer _mySpriteChildren;

    void Awake()
    {
        _myAudioSource = GetComponent<AudioSource>();
        _myParticleSystem = GetComponent<ParticleSystem>();
        _myCollider = GetComponent<Collider>();
        _myMeshRenderer = GetComponent<MeshRenderer>();
        _mySpriteChildren = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        _currentDistance += _speed * Time.deltaTime;

        if (_currentDistance > _maxDistance)
        {
            RandomBulletFactory.Instance.ReturnRandomBullet(this);
        }
    }

    public override void Pick(Player player)
    {
        if (player != null)
        {
            player.isSinuousBullet = false;
            player.isRandomBullet = true;
        }

    }

    void OnEnable()
    {
        _myCollider.enabled = true;
        _myMeshRenderer.enabled = true;
        _mySpriteChildren.enabled = true;
    }

    void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(FireBurst b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(FireBurst b)
    {
        b.gameObject.SetActive(false);
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
        _mySpriteChildren.enabled = false;
        _myMeshRenderer.enabled = false;

        StartCoroutine("WaitReturn");
    }

    IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        RandomBulletFactory.Instance.ReturnRandomBullet(this);
    }
}
