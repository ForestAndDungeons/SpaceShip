using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBulletFactory : MonoBehaviour
{
    public LevelUpBulletFactory Instance { get { return _instance; } }
    LevelUpBulletFactory _instance;

    [SerializeField] LevelUpBullet _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<LevelUpBullet> _pool;

    void Start()
    {
        _instance = this;
        GameManager.Instance.levelUpBulletFactory = Instance;
        _pool = new ObjectPool<LevelUpBullet>(LevelUpBulletCreator, (b) => { b.gameObject.SetActive(true); }, (b) => { b.gameObject.SetActive(false); }, _initialStock);
    }

    LevelUpBullet LevelUpBulletCreator()
    {
        return Instantiate(_prefab);
    }

    public LevelUpBullet GetLevelUpBullet()
    {
        return _pool.GetObject();
    }

    public void ReturnLevelUpBullet(LevelUpBullet b)
    {
        _pool.ReturnObject(b);
    }
}
