using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    public AsteroidFactory Instance { get { return _instance; } }
    AsteroidFactory _instance;

    [SerializeField] Asteroid _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Asteroid> _pool;

    void Start()
    {
        _instance = this;
        GameManager.Instance.asteroidFactory = Instance;
        _pool = new ObjectPool<Asteroid>(AsteroidCreator, (a) => { a.gameObject.SetActive(true); }, (a) => { a.gameObject.SetActive(false); }, _initialStock);
    }

    Asteroid AsteroidCreator()
    {
        return Instantiate(_prefab);
    }

    public Asteroid GetAsteroid()
    {
        return _pool.GetObject();
    }

    public void ReturnAsteroid(Asteroid a)
    {
        _pool.ReturnObject(a);
    }
}