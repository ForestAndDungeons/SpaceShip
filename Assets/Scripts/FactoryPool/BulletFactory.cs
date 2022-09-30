using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    public static BulletFactory Instance
    {
        get
        {
            return _instance;
        }
    }
    static BulletFactory _instance;

    [SerializeField] Bullet _prefab;
    [SerializeField] int _initialStock;

    ObjectPool<Bullet> _pool;

    void Awake()
    {
        _instance = this;

        #region SINGLETON, NO SE USA EN FACTORY
        /*if (Instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }*/
        #endregion

        //Creo un nuevo pool pasandole:
        //1.- La funcion que contiene la logica de instanciar el objeto (factoryMethod)
        //2.- La funcion que contiene la logica de que hacer al pedir el objeto (turnOnCallback)
        //3.- La funcion que contiene la logica de que hacer al devolver el objeto (turnOffCallback)
        //4.- La cantidad de objetos que se crearan en un principio [Opcional]
        //5.- Si es dinamico o no [Opcional]


        //Con LAMBDA evitando las funciones estaticas en la clase Bullet
        //pool = new ObjectPool<Bullet>(BulletCreator, (b) => { b.gameObject.SetActive(true); }, (b) => { b.gameObject.SetActive(false); }, _initialStock);

        _pool = new ObjectPool<Bullet>(BulletCreator, 
                                       Bullet.TurnOn,
                                       Bullet.TurnOff, 
                                       _initialStock);
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
