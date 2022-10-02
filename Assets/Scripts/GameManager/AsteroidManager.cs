using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager
{
    float _timer;
    float _spawnTime;
    float _boundWidth;
    float _boundHeight;
    float _boundOffset;

    public AsteroidManager(float timer, float boundWidth, float boundHeight, float boundOffset)
    {
        _timer = timer;
        _spawnTime = timer;
        _boundWidth = boundWidth;
        _boundHeight = boundHeight;
        _boundOffset = boundOffset;
    }

    public void ArtificialUpdate()
    {
        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f)
        {
            _spawnTime = _timer;
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        Asteroid a = AsteroidFactory.Instance.GetAsteroid();

        a.transform.position = new Vector3(Random.Range(-_boundWidth, _boundWidth), 0, Random.Range(_boundHeight + _boundOffset / 2, _boundHeight + _boundOffset));
        a.transform.forward = Vector3.forward * -1;
    }
}