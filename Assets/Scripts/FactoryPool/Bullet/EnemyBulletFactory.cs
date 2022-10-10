using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletFactory : MonoBehaviour
{
    public static EnemyBulletFactory Instance { get { return _instance; } }
    static EnemyBulletFactory _instance;

    [SerializeField] Bullet _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Bullet> _pool;

    void Awake()
    {
        _instance = this;

        _pool = new ObjectPool<Bullet>(BulletCreator, (b) => { b.gameObject.SetActive(true); }, (b) => { b.gameObject.SetActive(false); }, _initialStock);  
    }

    //Funcion que contiene la logica de la creacion de la bala
    Bullet BulletCreator()
    {
        return Instantiate(_prefab);
    }

    //Funcion que va a ser llamada cuando el cliente quiera un objeto
    public Bullet GetBullet()
    {
        return _pool.GetObject();
    }

    //Funcion que va a ser llamada cuando el objeto tenga que ser devuelto al Pool
    public void ReturnBullet(Bullet b)
    {
        _pool.ReturnObject(b);
    }
}