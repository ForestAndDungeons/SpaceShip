using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxDistance;

    float _currentDistance;

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        _currentDistance += _speed * Time.deltaTime;
        
        if (_currentDistance > _maxDistance)
        {
            BulletFactory.Instance.ReturnBullet(this);
        }
    }

    private void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(Bullet b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        BulletFactory.Instance.ReturnBullet(this);
    }
}