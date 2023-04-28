using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : BoidBehaviour
{
    public float destinationDistance = 10; //The distance away from the boid the target position is

    public float randomizeRadius = 10; //The radius of the sphere the target position is constrained in

    public float destinationShift = 100; //How much the destination shifts every second.

    public Vector3 destination;
    public Vector3 worldDestination;
    
    // Start is called before the first frame update
    public override Vector3 Calculate() //This behaviour...
    {
        Vector2 shift = destinationShift * Random.insideUnitCircle * Time.deltaTime; 
        destination += new Vector3(shift.x, 0, shift.y); //Apply a random offset to the target position

        destination = Vector3.ClampMagnitude(destination, randomizeRadius); //Make sure the target position doesn't shift outside of a certain range

        Vector3 relativeDestination = Vector3.forward * destinationDistance + destination; //Displace it forward
        worldDestination = transform.TransformPoint(relativeDestination); //Transform it to world space
        worldDestination.y = 0; //No flying

        return worldDestination - transform.position; //return the distance.
    }
    void Start()
    {
        destination = Random.insideUnitCircle * randomizeRadius;
    }
}
