using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedBullet : BulletDecorator
{
    ImprovedBullet(Bullet bullet) : base(bullet)
    {
        
    }

    public override void Interact(CharacterBase entity)
    {
        if (entity != null)
        {
            entity.OnDamage(_damage + 1);
            OnInteraction();
        }
    }
}