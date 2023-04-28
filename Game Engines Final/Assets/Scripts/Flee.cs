using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : BoidBehaviour
{
    public GameObject chaser = null;
    public Vector3 chaserPos = Vector3.zero;
    public string fleeTag = "Predator";
    
    // Start is called before the first frame update
    public override Vector3 Calculate() //This behaviour...
    {
        return -myBoid.ChaseForce(chaserPos); //... makes the boid mirror the chase movement - I.E. move directly away from the "target"
    }

    // Update is called once per frame
    void Update()
    {
        if (chaser != null) //If something is chasing you...
        {
            chaserPos = chaser.transform.position; //Find where they are.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(fleeTag))
        {
            chaser = other.gameObject;
        }
    }
}
