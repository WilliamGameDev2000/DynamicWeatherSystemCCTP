using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherClass : MonoBehaviour
{
    private weatherList currentWeather;
    public weatherList CurrentWeather => currentWeather;

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
        switch (currentWeather)
        {
            case weatherList.CLEAR:
                
                break;
            case weatherList.OVERCAST:

                break;
            case weatherList.RAINING:

                break;
            case weatherList.THUNDERSTORM:

                break;
            case weatherList.SNOWING:

                break;
            case weatherList.FOG:

                break;
        }
        Debug.Log(weather);
        Debug.Log(currentWeather);
    }
}

public enum weatherList
{
    CLEAR,
    OVERCAST,
    RAINING,
    THUNDERSTORM,
    SNOWING,
    FOG,
    WEATHERLIST
}
