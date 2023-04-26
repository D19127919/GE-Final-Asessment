using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorAI : MonoBehaviour
{
    public string preyTag; //The tag that prey boids have.
    public float sightDistance = 30; //How far away you can see prey.

    private Boid myBoid;
    private Arrive myArrive;
    private Chase myChase;
    private GameObject myTarget;
    

    // Start is called before the first frame update
    void Start()
    {
        myBoid = gameObject.GetComponent<Boid>();
        myChase = gameObject.GetComponent<Chase>();
        myArrive = gameObject.GetComponent<Arrive>();

        
        myTarget = FindTarget(sightDistance, false); //Find a target without using LOS rules.
        
        myChase.target = myTarget.GetComponent<Boid>();
        myArrive.target = myTarget; //Set the starting targets.
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget == null) //If the target doesn't exist...
        {
            myTarget = FindTarget(sightDistance, true); //Get a new one.
            
            myChase.target = myTarget.GetComponent<Boid>();
            myArrive.target = myTarget; //Set the targets.
        }
    }

    public GameObject FindTarget(float maxDistance, bool needsLOS)
    {
        List<GameObject> potentialPrey = new List<GameObject>();
        GameObject[] allPrey = GameObject.FindGameObjectsWithTag(preyTag);

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
                else
                {
                    potentialPrey.Add(prey); //Add it to the list of potential targets.
                }
            }
        }


        int closest = 0;
        for (int i = 1; i < potentialPrey.Count; i++)
        {
            if (Vector3.Distance(gameObject.transform.position, potentialPrey[i].transform.position) <
                Vector3.Distance(gameObject.transform.position, potentialPrey[closest].transform.position)) //If the one you are on is closer than the previous closest...
            {
                closest = i;
            }
        }

        return potentialPrey[closest];
    }
}
