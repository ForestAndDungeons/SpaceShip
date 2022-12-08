using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpBullet : Interactive
{
    //[SerializeField] int _value;

    void Update()
    {
        MovementRight();
    }

    public override void Interact(CharacterBase entity)
    {
        entity.LevelUpBullet();
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

    public static void TurnOn(LevelUpBullet b)
    {
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(LevelUpBullet b)
    {
        b.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.levelUpBulletFactory.ReturnLevelUpBullet(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}