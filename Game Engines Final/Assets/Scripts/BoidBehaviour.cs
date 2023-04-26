using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Boid))]

public abstract class BoidBehaviour : MonoBehaviour
{
    public float Multiplier = 1.0f;
    public Vector3 force;

    [HideInInspector] public Boid myBoid;
    
    public void Awake()
    {
        myBoid = GetComponent<Boid>();
    }

    public abstract Vector3 Calculate();
}
