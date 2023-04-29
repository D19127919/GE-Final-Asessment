using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private List<BoidBehaviour> behaviours = new List<BoidBehaviour>(); //Prepare a list for all behaviour scripts.

    public Vector3 velocity = Vector3.zero; //How much your position is changing
    public Vector3 force = Vector3.zero; //How much your acceleration is changing
    public Vector3 acceleration = Vector3.zero; //How much your velocity is changing

    public float mass = 1; //How heavy you are. Makes you accelerate slower.

    [Range(0.0f, 1f)] public float friction = 0.1f;

    [Range(0f, 1f)] public float turnRate = 0.1f;
    public float maxSpeed = 5;
    public float maxForce = 10;
    
    void Start()
    {
        BoidBehaviour[] behaviours = GetComponents<BoidBehaviour>(); //Grab an array of behaviours on this object

        foreach (BoidBehaviour behaviour in behaviours)
        {
            this.behaviours.Add(behaviour);
        } //And dump them into the list.
    }

    // Update is called once per frame
    void Update()
    {
        force = Calculate(); //Calculate the force; the direction you're being pushed and how strong.
        acceleration = force / mass; //Calculate the acceleration; the actual strength of the "push"
        velocity += acceleration * Time.deltaTime; //Update the velocity; what direction you are moving and how fast.
        velocity.y = 0; //No flying allowed.

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed); //Make sure you aren't moving faster than the fastest you are allowed to move.

        if (velocity.magnitude > 0.1f)
        {
            transform.LookAt(transform.position + velocity);

            transform.position += velocity * Time.deltaTime; //Actually apply the movement
            velocity *= 1.0f - (friction * Time.deltaTime); //Apply friction.
        }
    }

    public Vector3 ChaseForce(Vector3 targetPos)
    {
        Vector3 direction = targetPos - gameObject.transform.position; //Get the direction between you and your target
        direction.Normalize(); //Normalize it so it's between 0 and 1 (so the distance doesn't matter)
        direction *= maxSpeed; //Multiply it by speed to get the true desired velocity
        return direction - velocity; //Return the difference between that and your current speed - which is how much you need to accelerate by.
    }

    public Vector3 ArriveForce(Vector3 targetPos, float slowThreshold = 10.0f, float deceleration = 3)
    {
        Vector3 targetDir = targetPos - gameObject.transform.position; //As above, get the direction to target

        float distance = targetDir.magnitude; //Get the distance by just looking at the numbers and not the direction
        Vector3 direction; //initialize
        if (distance < slowThreshold) //If you're within slowing distance...
        {
            direction = maxSpeed * (distance / slowThreshold) * (targetDir / distance); //As you get closer your desired speed is reduced
        }
        else
        {
            direction = maxSpeed * (targetDir / distance); //Just have your desired speed remain unchanged.
            return direction - velocity; //Just move normally towards the target
        }

        return direction - velocity * deceleration; //makes your desired velocity slower by pretending you're going faster than you really are.
    }

    Vector3 Calculate()
    {
        force = Vector3.zero; //Reset force to zero.

        foreach (BoidBehaviour behaviour in behaviours)
        {
            if (behaviour.isActiveAndEnabled) //Checks to be sure the behaviour is enabled
            {
                force += behaviour.Calculate() * behaviour.Multiplier; //Calculates that behavior's force and multiplies it by the multiplier, then adds it to the force
                float forceMagnitude = force.magnitude;
                if (forceMagnitude > maxForce) //if the power of the force is higher than the max...
                {
                    force = Vector3.ClampMagnitude(force, maxForce); //Clamp the power of force to not be above max
                    break; //stop adding force.
                }
            }
        }

        return force;
    }
    
    
}
