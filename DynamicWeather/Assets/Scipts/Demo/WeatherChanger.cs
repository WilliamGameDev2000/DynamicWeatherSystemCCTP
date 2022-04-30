using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherChanger : MonoBehaviour
{
    WeatherClass weather;
    [SerializeField] int ticks_before_change = 15;
/*    [SerializeField] private Slider weather_slider;
    [SerializeField] private bool slider_active = false;*/

    private int currentWeatherTick = 0;

    public static Action<weatherList> OnWeatherChange;

    public static Action OnWeatherTransition;

    /*private void Start()
    {
        weather_slider.gameObject.SetActive(slider_active);
        if (!slider_active)
        {
            OnWeatherChange?.Invoke((weatherList)UnityEngine.Random.Range(0, (int)weatherList.WEATHERLIST));
        }
        else
        {
            OnWeatherChange?.Invoke(weatherList.CLEAR);
        }
        
    }*/

    /*private void Update()
    {
        if (slider_active)
        {
            weather_slider.onValueChanged.AddListener(v =>
            {
                OnWeatherChange?.Invoke((weatherList)v - 1);
            });
        }
    }*/

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

        if(currentWeatherTick >= ticks_before_change/* && slider_active == false*/)
        {
            currentWeatherTick = 0;
            //OnWeatherChange?.Invoke((weatherList)UnityEngine.Random.Range(0, (int)weatherList.WEATHERLIST));
            OnWeatherTransition?.Invoke();
        }
    }
}
