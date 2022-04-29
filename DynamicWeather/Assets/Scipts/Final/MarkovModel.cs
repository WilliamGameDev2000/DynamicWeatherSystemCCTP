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
    double[,] transitionProbabilityMatrix;
    int weatherNumber = -1;

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
                weatherNumber = 0;
                break;
            case 1:
                currentWeather = states.rain;
                weatherNumber = 1;
                break;
            case 2:
                currentWeather = states.sunny;
                weatherNumber = 2;
                break;
            case 3:
                currentWeather = states.fog;
                weatherNumber = 3;
                break;
            case 4:
                currentWeather = states.snow;
                weatherNumber = 4;
                break;
            case 5:
                currentWeather = states.thunder;
                weatherNumber = 5;
                break;

            default:
                currentWeather = states.overcast;
                weatherNumber = 0;
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
            currentWeather.Icon[weatherNumber].SetActive(true);
            previousWeather = currentWeather;
            return;
        }
        System.Random rand = new System.Random();
        //Basic system
        //take weather and activate it
        Mathf.Lerp(currentWeather.Intensity, newWeather.Intensity, Time.deltaTime * 200);
        previousWeather = currentWeather;
        currentWeather = newWeather;
        currentWeather.Icon[weatherNumber].SetActive(true);
        currentWeather.SetIntensity(Mathf.Lerp(currentWeather.Intensity, (float)rand.NextDouble(), Time.deltaTime * 200));
        Debug.Log(currentWeather.Intensity);
        //Additions to system

    }

    private void PickWeather()
    {
        ///check if there should be a transition based off probabilities from states
        ///
        ///if the weather is different pass that weather into the play weather function
        ///
        currentWeather.Icon[weatherNumber].SetActive(false);
        for (int j = 0; j < names.Length; j++)
        {
            if (names[j] == currentWeather.ToString())
            {
                weatherNumber = j;
            }
        }
        switch (TransitionChance(transitionProbabilityMatrix, weatherNumber))
        {
            case 0:
                {
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weatherNumber, 0] * 100}% probability");
                    ChangeEffects(states.overcast);
                    break;
                }
            case 1:
                {
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weatherNumber, 1] * 100}% probability");
                    ChangeEffects(states.rain);
                    break;
                }
            case 2:
                {
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weatherNumber, 2] * 100}% probability");
                    ChangeEffects(states.sunny);
                    break;
                }
            case 3:
                {
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weatherNumber, 3] * 100}% probability");
                    ChangeEffects(states.fog);
                    break;
                }
            case 4:
                {
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weatherNumber, 4] * 100}% probability");
                    ChangeEffects(states.snow);
                    break;
                }
            case 5:
                {
                    Debug.Log($"Transition from {previousWeather.Name} to {currentWeather.Name} with {transitionProbabilityMatrix[weatherNumber, 5] * 100}% probability");
                    ChangeEffects(states.thunder);
                    break;
                }
            default:
                {
                    ChangeEffects(states.overcast);
                    break;
                }
        }
    }
}
