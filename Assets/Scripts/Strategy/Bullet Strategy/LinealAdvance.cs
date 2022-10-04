using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealAdvance : IBulletAdvance
{
    float _speed;
    Transform _transform;
    public LinealAdvance(float speed,Transform transform)
    {
        _speed = speed;
        _transform = transform;
    }

    public void BulletAdvance()
    {
        _transform.position += _transform.forward * _speed * Time.deltaTime;
    }
}
