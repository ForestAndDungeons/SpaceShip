using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public EnemyFactory Instance { get { return _instance; } }
    EnemyFactory _instance;

    [SerializeField] Enemy _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Enemy> _pool;

    void Start()
    {
        _instance = this;
        GameManager.Instance.enemyFactory = Instance;
        _pool = new ObjectPool<Enemy>(EnemyCreator, (e) => { e.gameObject.SetActive(true); }, (e) => { e.gameObject.SetActive(false); }, _initialStock);
    }

    Enemy EnemyCreator()
    {
        return Instantiate(_prefab);
    }

    public Enemy GetEnemy()
    {
        return _pool.GetObject();
    }

    public void ReturnEnemy(Enemy e)
    {
        _pool.ReturnObject(e);
    }
}
