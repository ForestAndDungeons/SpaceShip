using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyweightPointer : MonoBehaviour
{
    public static readonly Flyweight InteractiveForward = new Flyweight
    {
        speed = 30,
        dir = Vector3.forward
    };

    public static readonly Flyweight InteractiveRight = new Flyweight
    {
        speed = 25,
        dir = Vector3.forward
    };
}