using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager
{
    float _timer;
    float _spawnTime;
    float _spawnBigTime;
    float _boundWidth;
    float _boundHeight;
    float _boundOffset;

    public AsteroidManager(float timer, float boundWidth, float boundHeight, float boundOffset)
    {
        _timer = timer;
        _spawnTime = timer;
        _spawnBigTime = timer * 3;
        _boundWidth = boundWidth;
        _boundHeight = boundHeight;
        _boundOffset = boundOffset;
    }

    public void ArtificialUpdate()
    {
        _spawnTime -= Time.deltaTime;
        _spawnBigTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f)
        {
            _spawnTime = _timer;
            SpawnAsteroid();
        }

        if (_spawnBigTime <= 0.0f)
        {
            _spawnBigTime = _timer;
            SpawnAsteroidBig();
        }
    }

    void SpawnAsteroid()
    {
        Asteroid a = GameManager.Instance.asteroidFactory.GetAsteroid();

        a.transform.position = new Vector3(Random.Range(-_boundWidth, _boundWidth), 0, Random.Range(_boundHeight + _boundOffset / 2, _boundHeight + _boundOffset));
        a.transform.forward = Vector3.forward;
    }

    void SpawnAsteroidBig()
    {
        AsteroidBig a = GameManager.Instance.asteroidBigFactory.GetAsteroidBig();

        a.transform.position = new Vector3(Random.Range(-_boundWidth, _boundWidth), 0, Random.Range(_boundHeight + _boundOffset / 2, _boundHeight + _boundOffset));
        a.transform.forward = Vector3.forward;
    }
}