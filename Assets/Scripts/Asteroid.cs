using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float _timer;
    [SerializeField] float _spawnTime;
    [SerializeField] GameObject _prefab;

    void Update()
    {
        /*_spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f)
        {
            SpawnAsteroid();
            _spawnTime = _timer;
        }*/
    }
    /*void SpawnAsteroid()
    {
        _prefab = Instantiate(_prefab);

        Vector3 pos = new Vector3(Random.Range(-GameManager.Instance._boundWidth, GameManager.Instance._boundWidth), 0, Random.Range(-GameManager.Instance._boundHeight/3, GameManager.Instance._boundHeight/3));

        this.transform.position = pos;
    }*/
}