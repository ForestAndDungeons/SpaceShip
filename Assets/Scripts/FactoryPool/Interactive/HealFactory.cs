using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFactory : MonoBehaviour
{
    public HealFactory Instance { get { return _instance; } }
    HealFactory _instance;

    [SerializeField] Heal _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Heal> _pool;

    void Start()
    {
        _instance = this;
        GameManager.Instance.healFactory = Instance;
        _pool = new ObjectPool<Heal>(HealCreator, (h) => { h.gameObject.SetActive(true); }, (h) => { h.gameObject.SetActive(false); }, _initialStock);
    }

    Heal HealCreator()
    {
        return Instantiate(_prefab);
    }

    public Heal GetHeal()
    {
        return _pool.GetObject();
    }

    public void ReturnHeal(Heal h)
    {
        _pool.ReturnObject(h);
    }
}