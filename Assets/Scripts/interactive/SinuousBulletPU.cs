using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;


public class SinuousBulletPU : Interactive 
{
    [SerializeField] float _amplitude;
    [SerializeField] float _period;
    [SerializeField] float _displacement;
    [SerializeField] float _vertical;

    //Temporal
    Player player;

    void Update()
    {
        Movement();
    }

    public override void Interact(CharacterBase entity)
    {
        //OnInteraction();

        //Temporal
        player = entity.GetComponent<Player>();
        TemporalInteract(player);
    }

    public void TemporalInteract(Player player)
    {
        player.isRandomBullet = false;
        player.SetAmplitude(_amplitude);
        player.SetPeriod(_period);
        player.SetDisplacement(_displacement);
        player.SetVertical(_vertical);
        player.isSinuousBullet = true;
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

    public static void TurnOn(SinuousBulletPU s)
    {
        s.gameObject.SetActive(true);
    }

    public static void TurnOff(SinuousBulletPU s)
    {
        s.gameObject.SetActive(false);
    }

    public override void ReturnToPool()
    {
        GameManager.Instance.sinuousBulletFactory.ReturnSinuous(this);
    }

    public override IEnumerator WaitReturn()
    {
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }
}