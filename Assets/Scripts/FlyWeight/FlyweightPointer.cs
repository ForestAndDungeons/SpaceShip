using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightPointer : MonoBehaviour
{
    public static readonly Flyweight Bigasteroidred = new Flyweight
    {
        speed = 15,
        damage = 2,
        dir = Vector3.down
    };

    public static readonly Flyweight Smallasteroidred = new Flyweight
    {
        speed = 30,
        damage = 1,
        dir = Vector3.down
    };

    public static readonly Flyweight Enemyship = new Flyweight
    {
        speed = 20,
      
    };

}
