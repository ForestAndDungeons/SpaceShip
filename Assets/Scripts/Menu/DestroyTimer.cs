using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float _timer;
    [SerializeField] GameObject _joystick;
    [SerializeField] GameObject _gyro;

    void Start()
    {
        DestroyOnTime();
        
        if(GameManager.Instance.GetIsGyro())
        { 
            _joystick.SetActive(false);
            _gyro.SetActive(true);
        }
        else
        {
            _joystick.SetActive(true);
            _gyro.SetActive(false);
        }
    }

    public void DestroyOnTime()
    {
        Destroy(gameObject, _timer);
    }
}