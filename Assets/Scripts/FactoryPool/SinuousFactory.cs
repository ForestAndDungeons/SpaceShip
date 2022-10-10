using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinuousFactory : MonoBehaviour
{
    public static SinuousFactory Instance { get { return _instance; } }
    static SinuousFactory _instance;

    [SerializeField] SinuousBulletPU _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<SinuousBulletPU> _pool;

    void Awake()
    {
        _instance = this;

        _pool = new ObjectPool<SinuousBulletPU>(SinuousCreator, (s) => { s.gameObject.SetActive(true); }, (s) => { s.gameObject.SetActive(false); }, _initialStock);
    }

    SinuousBulletPU SinuousCreator()
    {
        return Instantiate(_prefab);
    }

    public SinuousBulletPU GetSinuous()
    {
        return _pool.GetObject();
    }

    public void ReturnSinuous(SinuousBulletPU s)
    {
        _pool.ReturnObject(s);
    }
}
