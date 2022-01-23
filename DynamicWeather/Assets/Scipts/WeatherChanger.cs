using System;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChanger : MonoBehaviour
{
    WeatherClass weather;
    [SerializeField] int ticks_before_change = 15;

    private int currentWeatherTick = 0;

    public static Action<weatherList> OnWeatherChange;

    private void OnEnable()
    {
        GameManager.OnTick += Tick;
    }

    private void OnDisable()
    {
        GameManager.OnTick -= Tick;
    }


    private void Tick()
    {
        currentWeatherTick++;

        if(currentWeatherTick >= ticks_before_change)
        {
            currentWeatherTick = 0;
            OnWeatherChange?.Invoke((weatherList)UnityEngine.Random.Range(0, (int)weatherList.WEATHERLIST));
        }
    }
}
