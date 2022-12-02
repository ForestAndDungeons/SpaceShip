using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletDecorator : Bullet
{
    protected Bullet _bullet;

    public BulletDecorator(Bullet bullet)
    {
        _bullet = bullet;
    }
}