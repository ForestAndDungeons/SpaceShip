using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Interactive 
{
    [SerializeField] float _damage;
    public IAdvance currentAdvance;

    void Update()
    {
        if (currentAdvance != null) currentAdvance.Advance();
    }

    public override void Interact(CharacterBase entity)
    {
        entity.OnDamage(_damage);
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

    public static void TurnOn(Bullet b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.bulletFactory.Instance.ReturnBullet(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.bulletFactory.Instance.ReturnBullet(this);
    }
}