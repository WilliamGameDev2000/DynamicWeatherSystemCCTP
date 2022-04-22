using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : BaseWeather
{
    Fog()
    {
        //define start probability
        startProbability = 0.1;
        //define transition probabilities
        overcastTransition = 0.3;
        rainTransition = 0.1;
        sunnyTransition = 0.1;
        fogTransition = 0.3;
        snowTransition = 0.1;
        stormTransition = 0.1;
    }
}
