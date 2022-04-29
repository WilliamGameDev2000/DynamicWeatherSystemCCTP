using System;
using System.Linq;
using System.Reflection;
using UnityEngine;


public struct States
{
    public Overcast overcast;
    public Rain rain;
    public Sunny sunny;
    public Fog fog;
    public Snow snow;
    public Thunderstorm thunder;
}

public class MarkovModel : MonoBehaviour
{
    BaseWeather currentWeather = null;
    BaseWeather previousWeather = null;


    States states = new States
    {
        overcast = new Overcast(),
        rain = new Rain(),
        sunny = new Sunny(),
        fog = new Fog(),
        snow = new Snow(),
        thunder = new Thunderstorm(),
    };

    string[] names;
    GameObject[] icons;
    double[,] transitionProbabilityMatrix;

    private void OnEnable()
    {
        WeatherChanger.OnWeatherTransition += PickWeather;
    }

    private void OnDisable()
    {
        WeatherChanger.OnWeatherTransition -= PickWeather;
    }

    void Start()
    {
        names = new string[6] { states.overcast.Name, states.rain.Name,
            states.sunny.Name,states.fog.Name, states.snow.Name, states.thunder.Name };
        icons = new GameObject[6] { GameObject.Find("Overcast"), GameObject.Find("Rain"), GameObject.Find("Sun"),
            GameObject.Find("Fog"), GameObject.Find("Snow"), GameObject.Find("Thunder") };

        foreach(GameObject icon in icons)
        {
            icon.SetActive(false);
        }

        double[] startProbabilityMatrix = new double[6] { states.overcast.StartProbability, states.rain.StartProbability, states.sunny.StartProbability,
            states.fog.StartProbability,  states.snow.StartProbability, states.thunder.StartProbability };

        transitionProbabilityMatrix = new double[,]
        {
            //Overcast
            {
                states.overcast.OvercastTransition,
                states.overcast.RainTransition,
                states.overcast.SunnyTransition,
                states.overcast.FogTransition,
                states.overcast.SnowTransition,
                states.overcast.StormTransition,
            },
            //Raining
            {
                states.rain.OvercastTransition,
                states.rain.RainTransition,
                states.rain.SunnyTransition,
                states.rain.FogTransition,
                states.rain.SnowTransition,
                states.rain.StormTransition,
            },
            //Sunny
            {
                states.sunny.OvercastTransition,
                states.sunny.RainTransition,
                states.sunny.SunnyTransition,
                states.sunny.FogTransition,
                states.sunny.SnowTransition,
                states.sunny.StormTransition,
            },
            //Foggy
            {
                states.fog.OvercastTransition,
                states.fog.RainTransition,
                states.fog.SunnyTransition,
                states.fog.FogTransition,
                states.fog.SnowTransition,
                states.fog.StormTransition,
            },
            //Snowing
            {
                states.snow.OvercastTransition,
                states.snow.RainTransition,
                states.snow.SunnyTransition,
                states.snow.FogTransition,
                states.snow.SnowTransition,
                states.snow.StormTransition,
            },
            //Storming
            {
                states.thunder.OvercastTransition,
                states.thunder.RainTransition,
                states.thunder.SunnyTransition,
                states.thunder.FogTransition,
                states.thunder.SnowTransition,
                states.thunder.StormTransition,
            },
        };

        switch (GetStartProbability(startProbabilityMatrix))
        {
            case 0:
                currentWeather = states.overcast;
                icons[0].SetActive(true);
                break;
            case 1:
                currentWeather = states.rain;
                icons[1].SetActive(true);
                break;
            case 2:
                currentWeather = states.sunny;
                icons[2].SetActive(true);
                break;
            case 3:
                currentWeather = states.fog;
                icons[3].SetActive(true);
                break;
            case 4:
                currentWeather = states.snow;
                icons[4].SetActive(true);
                break;
            case 5:
                currentWeather = states.thunder;
                icons[5].SetActive(true);
                break;

            default:
                currentWeather = states.overcast;
                break;
        }
        //Debug.Log(currentWeather);
        
        ChangeEffects(currentWeather);
    }

    private double GetStartProbability(double[] startProb)
    {
        if (startProb == null || startProb.Length == 0) return -1;

        double w, t = 0;
        int i;

        for(i = 0; i < startProb.Length; i++)
        {
            w = startProb[i];
            if (double.IsPositiveInfinity(w)) return i;
            else if (w >= 0f && !double.IsNaN(w)) t += startProb[i];
        }
        System.Random rand = new System.Random();

        double r = rand.NextDouble();
        double s = 0f;

        for(i = 0; i < startProb.Length; i++)
        {
            w = startProb[i];
            if (double.IsNaN(w) || w <= 0f) continue;

            s += w / t;
            if (s >= r) return i;
        }

        return -1;
    }

    private double TransitionChance(double[,] changeProb, int changeFrom)
    {

        if (changeProb == null || changeProb.Length == 0) return -1;
        double w, t = 0;
        int i = 0;

        for (i = 0; i < 6; i++)
        {
            w = changeProb[changeFrom, i];
            if (double.IsPositiveInfinity(w)) return i;
            else if (w >= 0f && !double.IsNaN(w)) t += changeProb[changeFrom, i];
        }
        System.Random rand = new System.Random();

        double r = rand.NextDouble();
        double s = 0f;

        for (i = 0; i < 6; i++)
        {
            w = changeProb[changeFrom, i];
            if (double.IsNaN(w) || w <= 0f) continue;

            s += w / t;
            if (s >= r) return i;
        }

        return -1;
    }
    private void ChangeEffects(BaseWeather newWeather)
    {
        if (newWeather == previousWeather)
        {
            previousWeather = currentWeather;
            return;
        }
        System.Random rand = new System.Random();
        //Basic system
        //take weather and activate it
        Mathf.Lerp(currentWeather.Intensity, newWeather.Intensity, Time.deltaTime * 200);
        previousWeather = currentWeather;
        currentWeather = newWeather;
        Mathf.Lerp(currentWeather.Intensity, (float)rand.NextDouble(), Time.deltaTime * 200);
        
        //Additions to system

    }

    private void PickWeather()
    {
        ///check if there should be a transition based off probabilities from states
        ///
        ///if the weather is different pass that weather into the play weather function
        ///
        int weather = -1;
        for (int j = 0; j < names.Length; j++)
        {
            if (names[j] == currentWeather.ToString())
            {
                icons[j].SetActive(false);
                weather = j;
            }
        }
        switch (TransitionChance(transitionProbabilityMatrix, weather))
        {
            case 0:
                {
                    ChangeEffects(states.overcast);
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weather, 0] * 100}% probability");
                    icons[0].SetActive(true);
                    break;
                }
            case 1:
                {
                    ChangeEffects(states.rain);
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weather, 1] * 100}% probability");
                    icons[1].SetActive(true);
                    break;
                }
            case 2:
                {
                    ChangeEffects(states.sunny);
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weather, 2] * 100}% probability");
                    icons[2].SetActive(true);
                    break;
                }
            case 3:
                {
                    ChangeEffects(states.fog);
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weather, 3] * 100}% probability");
                    icons[3].SetActive(true);
                    break;
                }
            case 4:
                {
                    ChangeEffects(states.snow);
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weather, 4] * 100}% probability");
                    icons[4].SetActive(true);
                    break;
                }
            case 5:
                {
                    ChangeEffects(states.thunder);
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weather, 5] * 100}% probability");
                    icons[5].SetActive(true);
                    break;
                }
            default:
                {
                    ChangeEffects(states.overcast);
                    icons[0].SetActive(true);
                    break;
                }
        }
    }
}
