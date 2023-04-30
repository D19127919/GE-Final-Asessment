using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAI : MonoBehaviour
{
    public string preyTag; //The tag that prey boids have.
    public float sightDistance = 30; //How far away you can see prey.
    public bool isDiurnal = true; //Whether the creature is active during the day;
    

    private Boid myBoid;
    private Arrive myArrive;
    private Chase myChase;
    private GameObject myTarget;
    private DayNightCycle cycle;
    private float starterMaxVelocity;
    

    // Start is called before the first frame update
    void Start()
    {
        cycle = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayNightCycle>();
        myBoid = gameObject.GetComponent<Boid>();
        myChase = gameObject.GetComponent<Chase>();
        myArrive = gameObject.GetComponent<Arrive>(); //Set all the components for easier access.

        starterMaxVelocity = myBoid.maxSpeed;

        
        myTarget = FindTarget(sightDistance, false); //Find a target without using LOS rules.
        
        myChase.target = myTarget;
        myArrive.target = myTarget; //Set the starting targets.
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget == null) //If the target doesn't exist...
        {
            myTarget = FindTarget(sightDistance, true); //Get a new one.

            if (myTarget != null) //If you now have a target...
            {
                myChase.target = myTarget;
                myArrive.target = myTarget; //Set the targets.
            }
        }

        if (cycle.isDay != isDiurnal) //If you should be sleeping...
        {
            myBoid.maxSpeed = Mathf.Lerp(myBoid.maxSpeed, 0, 0.3f * Time.deltaTime);
        }
        else
        {
            myBoid.maxSpeed = Mathf.Lerp(myBoid.maxSpeed, starterMaxVelocity, 0.3f * Time.deltaTime);
        }
        
        
        
        
    }

    public GameObject FindTarget(float maxDistance, bool needsLOS)
    {
        List<GameObject> potentialPrey = new List<GameObject>(); //Initialize a blank array...
        GameObject[] allPrey = GameObject.FindGameObjectsWithTag(preyTag); //Find all the prey in the scene...
        if (allPrey.Length == 0) //If there isn't any...
        {
            return null; //Stop doing stuff.
        }

        foreach (GameObject prey in allPrey) //For each prey in the scene...
        {
            if (Vector3.Distance(prey.transform.position, gameObject.transform.position) < maxDistance) //If it's within maximum distance...
            {
                if (needsLOS) //If you need LOS...
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, prey.transform.position - gameObject.transform.position, out hit, maxDistance) && hit.collider.tag == preyTag) //If the raycast hits the prey...
                    {
                        potentialPrey.Add(prey); //Add it to the list of potential targets.
                    }
                }
                else //If you don't need LOS...
                {
                    potentialPrey.Add(prey); //Add it to the list of potential targets.
                }
            }
        }

        if (potentialPrey.Count == 0)
        {
            return null;
        }


        int closest = 0;
        for (int i = 1; i < potentialPrey.Count; i++)
        {
            if (Vector3.Distance(gameObject.transform.position, potentialPrey[i].transform.position) <
                Vector3.Distance(gameObject.transform.position, potentialPrey[closest].transform.position)) //If the one you are on is closer than the previous closest...
            {
                closest = i; //Set that to the closest.
            }
        }

        return potentialPrey[closest]; //Return the closest prey.
    }

    private void OnTriggerStay(Collider other) //If something enters your trigger...
    {
        if (other.CompareTag(preyTag))
        {
            other.SendMessage("Caught"); //Start damaging it.
        }
    }
}
