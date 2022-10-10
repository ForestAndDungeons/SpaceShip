using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [SerializeField] int _chance;

    public override void onDeath()
    {
        _chance = Random.Range(0, 11);

        if (_chance <= 3)
        {
            Shield s = ShieldFactory.Instance.GetShield();

            s.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            s.transform.forward = Vector3.forward * -1;
        }
        else if (_chance == 4)
        {
            FireRate r = FireRateFactory.Instance.GetFireRate();

            r.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            r.transform.forward = Vector3.forward * -1;
        }
        else if (_chance == 5)
        {
            FireBurst b = FireBurstFactory.Instance.GetFireBurst();

            b.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            b.transform.forward = Vector3.forward * -1;
        }

        Destroy(gameObject, 0.5f);
    }
}