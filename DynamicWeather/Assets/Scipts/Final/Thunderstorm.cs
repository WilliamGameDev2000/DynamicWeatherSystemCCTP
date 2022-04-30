using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Thunderstorm : BaseWeather
{
    public Thunderstorm()
    {
        name = nameof(Thunderstorm);
        //define start probability
        startProbability = 0.1;
        //define transition probabilities
        overcastTransition = 0.2;
        rainTransition = 0.3;
        sunnyTransition = 0.0;
        fogTransition = 0.0;
        snowTransition = 0.1;
        stormTransition = 0.4;
    }
    [RuntimeInitializeOnLoadMethod]
    static void Start()
    {
        icon[5] = GameObject.Find("Thunder");
        icon[5].SetActive(false);
    }
}
