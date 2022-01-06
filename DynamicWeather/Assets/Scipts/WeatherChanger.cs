using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChanger : MonoBehaviour
{
    public weatherList currentWeather;
    private weatherProperties currentProperties;

    public List<weatherProperties> properties = new List<weatherProperties>();

    void Start()
    {
        currentProperties = properties[(int)currentWeather];
        StartWeather(currentProperties);
    }

    void Update()
    {
        
    }

    void StartWeather(weatherProperties weather)
    {
        if(weather.Thunder)
        {
            Debug.Log("Weather played has thunder");
        }
        else
        {
            Debug.Log("Weather played has no thunder");
        }

        if(weather.haveClouds)
        {
            Debug.Log("Weather played has clouds");
        }
        else
        {
            Debug.Log("Weather played has no clouds");
        }
        if(weather.intesnsity > 0)
        {
            Debug.Log("Weather is in effect");
        }
        else 
        {
            Debug.Log("Weather is clear");
        }
    }
}

public enum weatherList
{
    CLEAR,
    OVERCAST,
    RAINING,
    THUNDERSTORM,
    SNOWING,
    FOG
}
