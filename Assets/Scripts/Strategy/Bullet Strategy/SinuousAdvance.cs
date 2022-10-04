using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinuousAdvance : IBulletAdvance
{
    Transform _transform;
    float _speed;

    float _amplitude;
    float _period;
    float _displacement;
    float _vertical;

    public SinuousAdvance(Transform transform, float speed, float amplitude, float period, float displacement, float vertical)
    {
        _transform = transform;
        _speed = speed;
        _amplitude = amplitude;
        _period = period;
        _displacement = displacement;
        _vertical = vertical;
    }
    public void BulletAdvance()
    {
        _transform.position = _transform.position + new Vector3(_amplitude * Mathf.Sin(Time.time * _period + _displacement) + _vertical, 0, _speed) * Time.deltaTime;
    }
}
