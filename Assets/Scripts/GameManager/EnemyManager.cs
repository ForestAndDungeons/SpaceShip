using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    float _timer;
    float _spawnTime;
    float _boundWidth;
    float _boundHeight;
    float _boundOffset;
    float _count;

    public EnemyManager(float timer, float boundWidth, float boundHeight, float boundOffset)
    {
        _timer = timer;
        _spawnTime = timer;
        _boundWidth = boundWidth;
        _boundHeight = boundHeight;
        _boundOffset = boundOffset;
        _count = 1;
    }

    public void ArtificialUpdate()
    {
        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f)
        {
            _spawnTime = _timer;
            _count++;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Enemy e = EnemyFactory.Instance.GetAsteroid();

        e.transform.position = new Vector3(Random.Range(-_boundWidth, _boundWidth), 3, Random.Range(_boundHeight + _boundOffset / 2, _boundHeight + _boundOffset));
        e.transform.forward = Vector3.forward * -1;
    }

    public float GetCounter() { return _count; }
    public void SetCounter(float value) { _count = value; }
}
