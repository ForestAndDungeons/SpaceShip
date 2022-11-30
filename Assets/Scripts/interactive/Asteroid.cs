using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Interactive 
{
    [SerializeField] float _damage;
    int _chance;
    
    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        if(entity != null)
            entity.OnDamage(_damage);

        _chance = Random.Range(0, 10);

        if (_chance <= 1)
        {
            Shield s = GameManager.Instance.shieldFactory.GetShield();

            s.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            s.transform.forward = Vector3.forward;
        }
        else if (_chance > 1&&_chance <= 3)
        {
            Credits c = GameManager.Instance.creditsFactory.GetCredits();

            c.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            c.transform.forward = Vector3.forward;
        }
        else if (_chance >= 9)
        {
            Heal h = GameManager.Instance.healFactory.GetHeal();

            h.transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            h.transform.forward = Vector3.forward;
        }

        OnInteraction();
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

    public static void TurnOn(Asteroid a)
    {
        a.gameObject.SetActive(true);
    }

    public static void TurnOff(Asteroid a)
    {
        a.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.asteroidFactory.ReturnAsteroid(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }

    void OnTriggerEnter(Collider other)
    {
        Interact(null);
    }
}