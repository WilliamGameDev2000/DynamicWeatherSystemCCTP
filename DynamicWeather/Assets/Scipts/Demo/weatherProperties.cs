using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class weatherProperties
{
    public string name;
    [Range(0,1)]public float intesnsity;
    public Material effectMat;
    public bool haveClouds;
    public bool Thunder;
}
