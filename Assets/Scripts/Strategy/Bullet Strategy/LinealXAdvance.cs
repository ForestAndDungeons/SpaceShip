using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LinealXAdvance : IAdvance
{
    float _speed;
    float _boundWidth;
    float _boundHeight;
    Transform _transform;

    public LinealXAdvance(float speed,Transform transform)
    {
        _speed = speed;
        _transform = transform;
        _boundWidth = 85;
        _boundHeight = 40;
    }

    public void Advance()
    {
        _transform.position += new Vector3(_speed * Time.deltaTime, 0, 0);
        ApplyBounds();
    }

    public void ApplyBounds()
    {
        if (_transform.position.x > _boundWidth)
            _speed = -_speed;
        else if (_transform.position.x < -_boundWidth)
            _speed = -_speed;

        if (_transform.position.z > _boundHeight)
            _speed = -_speed;
        else if (_transform.position.z < -_boundHeight)
            _speed = -_speed;
    }
}