using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedBullet : BulletDecorator
{
    public ImprovedBullet(Bullet bullet) : base(bullet)
    {
        AddDamage();
    }
}