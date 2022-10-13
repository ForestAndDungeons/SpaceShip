using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsFactory : MonoBehaviour
{
    public CreditsFactory Instance { get { return _instance; } }
    CreditsFactory _instance;

    [SerializeField] Credits _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Credits> _pool;

    void Awake()
    {
        _instance = this;
        GameManager.Instance.creditsFactory = Instance;
        _pool = new ObjectPool<Credits>(CreditsCreator, (c) => { c.gameObject.SetActive(true); }, (c) => { c.gameObject.SetActive(false); }, _initialStock);
    }

    Credits CreditsCreator()
    {
        return Instantiate(_prefab);
    }

    public Credits GetCredits()
    {
        return _pool.GetObject();
    }

    public void ReturnCredits(Credits c)
    {
        _pool.ReturnObject(c);
    }
}
