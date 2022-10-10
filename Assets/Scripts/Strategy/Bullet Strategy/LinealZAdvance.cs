using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinealZAdvance : IAdvance
{
    float _speed;
    Transform _transform;

    public LinealZAdvance(float speed,Transform transform)
    {
        _speed = speed;
        _transform = transform;
    }

    public void Advance()
    {
        _transform.position += _transform.forward * _speed * Time.deltaTime;
    }
}