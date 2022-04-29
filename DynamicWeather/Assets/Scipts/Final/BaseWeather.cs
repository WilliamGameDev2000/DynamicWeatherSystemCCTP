using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BaseWeather
{
    protected string name;
    public string Name => name;
    [Range(0, 1)] protected float intensity;
    public float Intensity => intensity;

    static protected GameObject[] icon;
    public GameObject[] Icon => icon;

    static protected ParticleSystem[] effects;
    public ParticleSystem[] Effects => effects;



    public BaseWeather()
    {
        name = "DefaultName";
        intensity = 0;
        icon = new GameObject[6];
    }

    //start probability
    protected double startProbability;
    public double StartProbability => startProbability;

    //transition probabilities
    protected double overcastTransition;
    protected double rainTransition;
    protected double sunnyTransition;
    protected double fogTransition;
    protected double snowTransition;
    protected double stormTransition;

    public double OvercastTransition => overcastTransition;
    public double RainTransition => rainTransition;
    public double SunnyTransition => sunnyTransition;
    public double FogTransition => fogTransition;
    public double SnowTransition => snowTransition;
    public double StormTransition => stormTransition;

    public void SetIntensity(float new_intesnsity)
    {
        intensity = new_intesnsity;
    }
}
