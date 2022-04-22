using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeather
{
    protected string Name { get; set; }
    [Range(0, 1)] protected float Intensity;
    //public Material effectMat;
    protected bool HasClouds { get; set; }
    protected bool HasThunder { get; set; }

    public BaseWeather()
    {
        Name = "DefaultName";
        Intensity = 0;
        HasClouds = false;
        HasThunder = false;
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
    public double RainTransition => overcastTransition;
    public double SunnyTransition => overcastTransition;
    public double FogTransition => overcastTransition;
    public double SnowTransition => overcastTransition;
    public double StormTransition => overcastTransition;
}
