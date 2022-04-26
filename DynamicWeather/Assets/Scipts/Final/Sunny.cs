﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : BaseWeather
{
    public Sunny()
    {
        //define start probability
        startProbability = 0.2;
        //define transition probabilities
        overcastTransition = 0.2;
        rainTransition = 0.2;
        sunnyTransition = 0.3;
        fogTransition = 0.2;
        snowTransition = 0.1;
        stormTransition = 0.0;

        HasClouds = false;
        HasThunder = false;
    }
}
