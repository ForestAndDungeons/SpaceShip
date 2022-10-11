using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get { return _instance; } }
    static EnemyFactory _instance;

    [SerializeField] Enemy _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Enemy> _pool;

    void Awake()
    {
        _instance = this;

        _pool = new ObjectPool<Enemy>(AsteroidCreator, (e) => { e.gameObject.SetActive(true); }, (e) => { e.gameObject.SetActive(false); }, _initialStock);
    }

    Enemy AsteroidCreator()
    {
        return Instantiate(_prefab);
    }

    public Enemy GetAsteroid()
    {
        return _pool.GetObject();
    }

    public void ReturnAsteroid(Enemy e)
    {
        _pool.ReturnObject(e);
    }
}
