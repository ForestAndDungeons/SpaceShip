using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contants 
{
    // Observers
    public const string OBS_LINEALADVANCE = "Active when bullet is Lineal Advance";
    public const string OBS_SINUOUSADVANCE = "Active when bullet is Sinuous Advance";
    public const string OBS_RANDOMADVANCE = "Active when bullet is Random Advance";

    // EventManager
    public const string EVENT_INICIATEHEALTHBAR = "Subscribe to EventManager when start the game and iniciate the player health";
    public const string EVENT_INICIATECREDITS = "Subscribe to EventManager when start the game and iniciate the player credits";
    public const string EVENT_PLAYERONDAMAGE = "Triggered when played damaged";
    public const string EVENT_ADDCREDITUI = "Triggered when played pickUP credits";
    public const string EVENT_LOSEGAME = "Triggered when lose game";
}
