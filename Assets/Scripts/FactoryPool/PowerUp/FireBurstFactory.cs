using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBurstFactory : MonoBehaviour
{
    public static FireBurstFactory Instance { get { return _instance; } }
    static FireBurstFactory _instance;

    [SerializeField] FireBurst _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<FireBurst> _pool;

    void Awake()
    {
        _instance = this;

        _pool = new ObjectPool<FireBurst>(FireBurstCreator, (b) => { b.gameObject.SetActive(true); }, (b) => { b.gameObject.SetActive(false); }, _initialStock);
    }

    FireBurst FireBurstCreator()
    {
        return Instantiate(_prefab);
    }

    public FireBurst GetFireBurst()
    {
        return _pool.GetObject();
    }

    public void ReturnFireBurst(FireBurst b)
    {
        _pool.ReturnObject(b);
    }
}
