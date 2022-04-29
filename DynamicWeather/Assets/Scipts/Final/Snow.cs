using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snow : BaseWeather
{
    public Snow()
    {
        name = nameof(Snow);
        //define start probability
        startProbability = 0.2;
        //define transition probabilities
        overcastTransition = 0.3;
        rainTransition = 0.2;
        sunnyTransition = 0.1;
        fogTransition = 0.1;
        snowTransition = 0.3;
        stormTransition = 0.0;

        HasClouds = true;
        HasThunder = false;
    }
}
