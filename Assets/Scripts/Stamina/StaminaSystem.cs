using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] int maxStamina = 10;
    [SerializeField] float timeToRecharge = 10f;
    [SerializeField] TextMeshProUGUI staminaText = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Button _playButton;

    DateTime nextStaminaTime;
    DateTime lastStaminaTime;

    int currentStamina;
    bool rechargin;
    public bool HasEnoughStamina(int stamina) => currentStamina - stamina >= 0;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentStamina"))
        {
            PlayerPrefs.SetInt("currentStamina", maxStamina);
        }

        Load();
        StartCoroutine(RechargeStamina());
    }

    IEnumerator RechargeStamina()
    {
        UpdateTimer();
        rechargin = true;
        while (currentStamina < maxStamina)
        {

            DateTime currentTime = DateTime.Now;
            DateTime nextTime = nextStaminaTime;

            bool staminaAdd = false;

            while (currentTime > nextTime)
            {
                if (currentStamina >= maxStamina)
                {
                    break;
                }

                currentStamina += 1;
                staminaAdd = true;
                UpdateStamina();

                DateTime timeToAdd = nextTime;

                if (lastStaminaTime > nextTime)
                {
                    timeToAdd = lastStaminaTime;
                }

                nextTime = AddDuration(timeToAdd, timeToRecharge);
            }
            if (staminaAdd)
            {
                nextStaminaTime = nextTime;
                lastStaminaTime = DateTime.Now;
            }

            UpdateTimer();
            UpdateStamina();
            Save();

            yield return new WaitForEndOfFrame();
        }
        rechargin = false;
    }

    DateTime AddDuration(DateTime date, float duration)
    {
        return date.AddSeconds(duration);
    }

    public void UseStamina(int staminaToUse)
    {
        if (currentStamina - staminaToUse >= 0)
        {
            currentStamina -= staminaToUse;
            UpdateStamina();

            if (!rechargin)
            {
                nextStaminaTime = AddDuration(DateTime.Now, timeToRecharge);
                StartCoroutine(RechargeStamina());
            }
        }
        else
        {
            Debug.Log("No tengo stamina!");
        }
    }

    void UpdateTimer()
    {
        if (currentStamina >= maxStamina)
        {
            timerText.text = "Full Stamina";

            return;
        }

        TimeSpan timer = nextStaminaTime - DateTime.Now;
        timerText.text = timer.Minutes.ToString() + ":" + timer.Seconds.ToString();
    }

    void UpdateStamina()
    {
        staminaText.text = currentStamina.ToString() + " / " + maxStamina.ToString();
    }

    void Save()
    {
        PlayerPrefs.SetInt("currentStamina", currentStamina);
        PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
    }

    void Load()
    {
        currentStamina = PlayerPrefs.GetInt("currentStamina");
        nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));
    }

    DateTime StringToDateTime(string date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return DateTime.Now;
        }
        else
        {
            return DateTime.Parse(date);
        }
    }
}
