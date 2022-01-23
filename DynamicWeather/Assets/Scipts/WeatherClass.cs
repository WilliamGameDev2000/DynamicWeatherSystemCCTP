using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherClass : MonoBehaviour
{
    [SerializeField] private weatherList currentWeather;
    public  weatherList CurrentWeather => currentWeather;

    private weatherProperties currentProperties;

    public List<weatherProperties> properties = new List<weatherProperties>();

    void Start()
    {
        //StartWeather(currentProperties);
    }

    private void OnEnable()
    {
        WeatherChanger.OnWeatherChange += SetWeather;
    }

    private void OnDisable()
    {
        WeatherChanger.OnWeatherChange -= SetWeather;
    }


    public void SetWeather(weatherList weather)
    {
        currentWeather = weather;

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

        currentProperties = properties[(int)currentWeather];
        Debug.Log(currentWeather);
        Debug.Log(currentProperties);
    }

   /* public void StartWeather(weatherProperties weather)
    {
        
    }*/
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
