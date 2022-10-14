using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBig : Interactive
{
    [SerializeField] float _damage;
    [SerializeField] Asteroid _asteroidPrefab;

    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        if (entity != null)
            entity.OnDamage(_damage);

        OnInteraction();

        AsteroidBuilder();
        AsteroidBuilder();
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

    public static void TurnOn(AsteroidBig a)
    {
        a.gameObject.SetActive(true);
    }

    public static void TurnOff(AsteroidBig a)
    {
        a.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        

        GameManager.Instance.asteroidBigFactory.ReturnAsteroidBig(this);
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

    void AsteroidBuilder()
    {
        Vector3 offset = new Vector3 (Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

        Asteroid myAsteroid = new AsteroidBuilder(_asteroidPrefab).SetColor(Color.white)
                                                          .SetPosition(transform.position + offset)
                                                          .SetScale(Vector3.one * 2)
                                                          .Done();
    }
}