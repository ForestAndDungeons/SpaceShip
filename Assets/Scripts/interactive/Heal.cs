using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Interactive 
{
    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        entity.AddCurrentLife(2);
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

    public static void TurnOn(Heal h)
    {
        h.gameObject.SetActive(true);
    }

    public static void TurnOff(Heal h)
    {
        h.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.healFactory.ReturnHeal(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}