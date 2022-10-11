using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float _timer;

    void Start()
    {
        DestroyOnTime();
    }

    public void DestroyOnTime()
    {
        Destroy(gameObject, _timer);
    }
}