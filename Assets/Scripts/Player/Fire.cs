using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public void Shoot()
    {
        Bullet b = BulletFactory.Instance.GetBullet();

        b.transform.position = GameManager.Instance.playerReference.transform.position;
        b.transform.forward = Vector3.forward;
    }
}