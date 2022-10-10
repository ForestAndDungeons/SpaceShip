using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : CharacterBase
{
    [SerializeField] GameObject[] _loot;

    public override void onDeath()
    {
        if (Random.Range(0, 11) <= 3)
        {
            Shield s = ShieldFactory.Instance.GetShield();

            s.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            s.transform.forward = Vector3.forward * -1;
        }
        Destroy(gameObject, 0.5f);
    }
}