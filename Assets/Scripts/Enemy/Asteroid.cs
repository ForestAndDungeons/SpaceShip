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

        _chance = Random.Range(0, 11);

        if (_chance <= 3)
        {
            RandomBulletPU r = GameManager.Instance.randomBulletFactory.GetRandomBullet();

            r.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            r.transform.forward = Vector3.forward * -1;
        }
        else if (_chance <= 5 && _chance > 3)
        {
            SinuousBulletPU s = GameManager.Instance.sinuousBulletFactory.GetSinuous();

            s.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            s.transform.forward = Vector3.forward * -1;
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

    public static void TurnOn(Shield s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(Shield s)
    {
        s.gameObject.SetActive(false);
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