using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fog : BaseWeather
{
    public Fog()
    {
        name = nameof(Fog);
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
    [RuntimeInitializeOnLoadMethod]
    static void Start()
    {
        icon[3] = GameObject.Find("Fog");
        icon[3].SetActive(false);

        effects[3] = GameObject.Find("").GetComponent<ParticleSystem>();
        effects[3].gameObject.SetActive(false);
    }
}
