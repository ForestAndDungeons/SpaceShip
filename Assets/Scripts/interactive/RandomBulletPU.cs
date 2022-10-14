using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBulletPU : Interactive 
{
    //Temporal
    //Player player;

    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        entity.SetIsSinuousBullet(false);
        entity.SetIsRandomBullet(true);
        OnInteraction();

        //Temporal
        /*player = entity.GetComponent<Player>();
        TemporalInteract(player);*/
    }

 /*   public void TemporalInteract(Player player)
    {
        player.isSinuousBullet = false;
        player.isRandomBullet = true;
        OnInteraction();
    }*/

    void OnEnable()
    {
        _collider.enabled = true;
        _renderer.enabled = true;
    }

    void OnDisable()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(RandomBulletPU s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(RandomBulletPU s)
    {
        s.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.randomBulletFactory.ReturnRandomBullet(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}