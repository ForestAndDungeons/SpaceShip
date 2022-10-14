using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBuilder
{
    Asteroid _asteroidCreated;

    public AsteroidBuilder(Asteroid prefab)
    {
        _asteroidCreated = GameObject.Instantiate<Asteroid>(prefab);
    }

    public AsteroidBuilder SetColor(Color newColor)
    {
        _asteroidCreated.GetComponent<Renderer>().material.color = newColor;
        return this;
    }

    public AsteroidBuilder SetPosition(float x, float y, float z)
    {
        _asteroidCreated.transform.position = new Vector3(x, y, z);
        return this;
    }

    public AsteroidBuilder SetScale(Vector3 newScale)
    {
        _asteroidCreated.transform.localScale = newScale;
        return this;
    }

    public Asteroid Done()
    {
        return _asteroidCreated;
    }
}
