using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTimeFactory : MonoBehaviour
{
    public FireTimeFactory Instance { get { return _instance; } }
    FireTimeFactory _instance;

    [SerializeField] FireTime _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<FireTime> _pool;

    void Awake()
    {
        _instance = this;
        GameManager.Instance.fireTimeFactory = Instance;
        _pool = new ObjectPool<FireTime>(FireTimeCreator, (t) => { t.gameObject.SetActive(true); }, (t) => { t.gameObject.SetActive(false); }, _initialStock);
    }

    FireTime FireTimeCreator()
    {
        return Instantiate(_prefab);
    }

    public FireTime GetFireTime()
    {
        return _pool.GetObject();
    }

    public void ReturnFireTime(FireTime t)
    {
        _pool.ReturnObject(t);
    }
}
