using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
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
            AsteroidFactory.Instance.ReturnAsteroid(this);
        }
    }
    void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(Bullet a)
    {
        a.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet a)
    {
        a.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        AsteroidFactory.Instance.ReturnAsteroid(this);
    }
}