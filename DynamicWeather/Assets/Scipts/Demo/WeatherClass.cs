using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeatherClass : MonoBehaviour
{
    [SerializeField] private weatherList currentWeather;
    public  weatherList CurrentWeather => currentWeather;

    private weatherProperties currentProperties;

    public List<weatherProperties> properties = new List<weatherProperties>();

    [SerializeField] private RawImage[] images;

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
        images[(int)currentWeather].gameObject.SetActive(false);

        currentWeather = weather;
        currentProperties = properties[(int)currentWeather];

        switch (currentWeather)
        {
            case weatherList.CLEAR:
                images[0].gameObject.SetActive(true);
                break;
            case weatherList.OVERCAST:
                images[1].gameObject.SetActive(true);
                break;
            case weatherList.RAINING:
                images[2].gameObject.SetActive(true);
                break;
            case weatherList.THUNDERSTORM:
                images[3].gameObject.SetActive(true);
                break;
            case weatherList.SNOWING:
                images[4].gameObject.SetActive(true);
                break;
            case weatherList.FOG:
                images[5].gameObject.SetActive(true);
                break;
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
    FOG,
    WEATHERLIST
}
