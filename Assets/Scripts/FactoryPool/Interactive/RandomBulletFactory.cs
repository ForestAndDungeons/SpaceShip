using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBulletFactory : MonoBehaviour
{
    public RandomBulletFactory Instance { get { return _instance; } }
    RandomBulletFactory _instance;

    [SerializeField] RandomBulletPU _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<RandomBulletPU> _pool;

    void Start()
    {
        _instance = this;
        GameManager.Instance.randomBulletFactory = Instance;
        _pool = new ObjectPool<RandomBulletPU>(RandomBulletCreator, (r) => { r.gameObject.SetActive(true); }, (r) => { r.gameObject.SetActive(false); }, _initialStock);
    }

    RandomBulletPU RandomBulletCreator()
    {
        return Instantiate(_prefab);
    }

    public RandomBulletPU GetRandomBullet()
    {
        return _pool.GetObject();
    }

    public void ReturnRandomBullet(RandomBulletPU r)
    {
        _pool.ReturnObject(r);
    }
}
