using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeather
{
    protected string Name { get; set; }
    [Range(0, 1)] protected float Intensity;
    //public Material effectMat;
    protected bool HasClouds { get; set; }
    protected bool HasThunder {get; set; }

    public BaseWeather()
    {
        Name = "DefaultName";
        Intensity = 0;
        HasClouds = false;
        HasThunder = false;
    }

    //transition probabilities
    protected float overcastTransition { get; set; }
    protected float rainTransition { get; set; }
    protected float sunnyTransition { get; set; }
    protected float fogTransition { get; set; }
    protected float snowTransition { get; set; }
    protected float stormTransition { get; set; }
}
