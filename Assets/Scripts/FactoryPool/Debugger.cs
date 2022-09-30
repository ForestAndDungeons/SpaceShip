using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet b = BulletFactory.Instance.GetBullet();

            b.transform.position = Vector3.zero;
            b.transform.forward = Vector3.forward;
        }
    }
}
