using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    public static AsteroidFactory Instance { get { return _instance; } }
    static AsteroidFactory _instance;

    [SerializeField] Asteroid _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Asteroid> _pool;

    void Awake()
    {
        _instance = this;

        _pool = new ObjectPool<Asteroid>(AsteroidCreator, (b) => { b.gameObject.SetActive(true); }, (b) => { b.gameObject.SetActive(false); }, _initialStock);
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