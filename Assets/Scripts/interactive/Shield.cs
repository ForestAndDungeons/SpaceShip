using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Interactive 
{
    void Update()
    {
        MovementRight();
    }

    public override void Interact(CharacterBase entity)
    {
        entity.SetShieldUp(true);
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

    public static void TurnOn(Shield s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(Shield s)
    {
        s.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.shieldFactory.ReturnShield(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}