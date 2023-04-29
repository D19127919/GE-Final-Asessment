using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BoidBehaviour
{
    public GameObject target; //What you're chasing.

    public Vector3 targetPos; //Where it is.
    public bool isSmart = true; //Whether you predict your target's movements or not.
    
    // Start is called before the first frame update
    public override Vector3 Calculate() //This behaviour...
    {
        if (target != null) //If you have a target...
        {
            targetPos = target.transform.position; //Find its position.
            if (isSmart) //If you predict movements...
            {
                float targetDist = Vector3.Distance(target.transform.position, gameObject.transform.position); //Get the distance between you and the target.
                float timeToReach = targetDist / myBoid.maxSpeed; //Find how long it will take you to reach the target...

                targetPos += target.GetComponent<Boid>().velocity * timeToReach; //Set the desired position to where the target will be when you reach them based on their velocity.
            }
        
            return myBoid.ChaseForce(targetPos); //Return the desired position to calculate the chase force in the Boid script.
        }

        return Vector3.zero; //Don't return anything (if you don't have a target).

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
