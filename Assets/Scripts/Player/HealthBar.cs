using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
    [SerializeField] float _maxHeath;
    [SerializeField] float _currentHeath;

    private void Awake()
    {
        EventManager.SubscribeToEvent(Contants.EVENT_INICIATEHEALTHBAR, IniciateHealthBar);
        EventManager.SubscribeToEvent(Contants.EVENT_PLAYERONDAMAGE, PlayedDamaged);
    }

    public void IniciateHealthBar(params object[] param)
    {
        _maxHeath = (float)param[0];
        _currentHeath = (float)param[1];
        _healthBar.value = _maxHeath;
    }

    public void PlayedDamaged(params object[] param)
    {
        _currentHeath = (float)param[0];
        _healthBar.value = _currentHeath;
    }
}