using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : PickUp
{
    [SerializeField] float _value;
    public override void Pick(Player player)
    {
        player.SetCredits(_value);
    }
}