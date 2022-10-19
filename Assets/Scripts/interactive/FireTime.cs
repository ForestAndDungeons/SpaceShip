using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTime : Interactive
{
    [SerializeField] int _value;

    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        entity.ReduceShootTime(_value);
        OnInteraction();
    }

    void OnEnable()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(FireRate s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(FireRate s)
    {
        s.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.fireTimeFactory.ReturnFireTime(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}
