using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateFactory : MonoBehaviour
{
    public FireRateFactory Instance { get { return _instance; } }
    FireRateFactory _instance;

    [SerializeField] FireRate _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<FireRate> _pool;

    void Awake()
    {
        _instance = this;
        GameManager.Instance.fireRateFactory = Instance;
        _pool = new ObjectPool<FireRate>(FireRateCreator, (r) => { r.gameObject.SetActive(true); }, (r) => { r.gameObject.SetActive(false); }, _initialStock);
    }

    FireRate FireRateCreator()
    {
        return Instantiate(_prefab);
    }

    public FireRate GetFireRate()
    {
        return _pool.GetObject();
    }

    public void ReturnFireRate(FireRate r)
    {
        _pool.ReturnObject(r);
    }
}
