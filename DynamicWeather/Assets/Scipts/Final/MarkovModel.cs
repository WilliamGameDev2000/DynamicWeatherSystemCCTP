using System;
using UnityEngine;

public class MarkovModel : MonoBehaviour
{
    BaseWeather currentWeather = null;
    BaseWeather previousWeather = null;
    //define states here
    public struct States
    {
        public Overcast overcast;
        public Rain rain;
        public Sunny sunny;
        public Fog fog;
        public Snow snow;
        public Thunderstorm thunder;
    }


    States states = new States
    {
        overcast = new Overcast(),
        rain = new Rain(),
        sunny = new Sunny(),
        fog = new Fog(),
        snow = new Snow(),
        thunder = new Thunderstorm(),
    };

    double[,] transitionProbabilityMatrix;

    MarkovModel()
    {
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
                break;
            case 1:
                currentWeather = states.rain;
                break;
            case 2:
                currentWeather = states.sunny;
                break;
            case 3:
                currentWeather = states.fog;
                break;
            case 4:
                currentWeather = states.snow;
                break;
            case 5:
                currentWeather = states.thunder;
                break;

            default:
                currentWeather = states.overcast;
                break;
        }
        //Debug.Log(currentWeather);
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Debug.Log($"Transition of weather with {transitionProbabilityMatrix[i, j] * 100}% probability");
            }
        }
        
        //PlayWeather(currentWeather);
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
    public void PlayWeather(BaseWeather newWeather)
    {
        if (previousWeather != null)
        {
            previousWeather = currentWeather;
        }
        
        System.Random rand = new System.Random();
        //Basic system
        //take weather and activate it
        Mathf.Lerp(currentWeather.Intensity, newWeather.Intensity, Time.deltaTime * 200);
        currentWeather = newWeather;
        Mathf.Lerp(currentWeather.Intensity, (float)rand.NextDouble(), Time.deltaTime * 200);
        
        //Additions to system

    }

    public void Transition()
    {
        ///check if there should be a transition based off probabilities from states
        ///
        ///if the weather is different pass that weather into the play weather function

        
    }
}
