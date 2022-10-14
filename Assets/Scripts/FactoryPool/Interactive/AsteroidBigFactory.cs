using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBigFactory : MonoBehaviour
{
    public AsteroidBigFactory Instance { get { return _instance; } }
    AsteroidBigFactory _instance;

    [SerializeField] AsteroidBig _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<AsteroidBig> _pool;

    void Awake()
    {
        _instance = this;
        GameManager.Instance.asteroidBigFactory = Instance;
        _pool = new ObjectPool<AsteroidBig>(AsteroidBigCreator, (a) => { a.gameObject.SetActive(true); }, (a) => { a.gameObject.SetActive(false); }, _initialStock);
    }

    AsteroidBig AsteroidBigCreator()
    {
        return Instantiate(_prefab);
    }

    public AsteroidBig GetAsteroidBig()
    {
        return _pool.GetObject();
    }

    public void ReturnAsteroidBig(AsteroidBig a)
    {
        _pool.ReturnObject(a);
    }
}