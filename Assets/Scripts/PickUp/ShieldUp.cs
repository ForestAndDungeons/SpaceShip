using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUp : PickUp
{
    public override void Pick(Player player)
    {
        player.SetShieldUp(true);
    }
}