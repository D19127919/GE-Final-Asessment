using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrive : BoidBehaviour
{
    public Vector3 targetPos = Vector3.zero; //The position of the target
    public float slowThreshold = 10; //The distance at which you start to slow
    public float deceleration = 2; //How rapid the slowing is

    public GameObject target = null;


    // Start is called before the first frame update
    public override Vector3 Calculate() //This behaviour...
    {
        Vector3 force = myBoid.ArriveForce(targetPos, slowThreshold, deceleration); //... calls arrive force.
        return force;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) //If you have a target...
        {
            targetPos = target.transform.position; //Get its position.
        }
    }
}
