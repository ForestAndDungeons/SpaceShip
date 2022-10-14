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
        Asteroid myAsteroid = new AsteroidBuilder(_asteroidPrefab).SetColor(Color.green)
                                                          .SetPosition(0, 0, 0)
                                                          .SetScale(Vector3.one * 5)
                                                          .Done();

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
