using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFactory : MonoBehaviour
{
    public ShieldFactory Instance { get { return _instance; } }
    ShieldFactory _instance;

    [SerializeField] Shield _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Shield> _pool;

    void Start()
    {
        _instance = this;
        GameManager.Instance.shieldFactory = Instance;
        _pool = new ObjectPool<Shield>(ShieldCreator, (s) => { s.gameObject.SetActive(true); }, (s) => { s.gameObject.SetActive(false); }, _initialStock);
    }

    Shield ShieldCreator()
    {
        return Instantiate(_prefab);
    }

    public Shield GetShield()
    {
        return _pool.GetObject();
    }

    public void ReturnShield(Shield s)
    {
        _pool.ReturnObject(s);
    }
}