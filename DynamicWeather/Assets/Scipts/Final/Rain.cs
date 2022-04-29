using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rain : BaseWeather
{
    public Rain()
    {
        name = nameof(Rain);
        //define start probability
        startProbability = 0.1;
        //define transition probabilities
        overcastTransition = 0.1;
        rainTransition = 0.4;
        sunnyTransition = 0.1;
        fogTransition = 0.0;
        snowTransition = 0.1;
        stormTransition = 0.3;

        HasClouds = true;
        HasThunder = false;

    }
}
