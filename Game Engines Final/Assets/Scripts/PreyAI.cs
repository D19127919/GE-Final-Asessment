using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PreyAI : MonoBehaviour
{
    public float health = 5;
    public float decayTime = 10; //How long it takes for the prey to vanish once killed.
    public string foodTag = "Food";
    public float eatDistance = 2; //From how far away the prey can consume food.
    [Range(0, 2)] public float hunger = 0.5f;
    public float hungerRate = 0.01f;
    public bool isDiurnal = true; //Whether the creature is active during the day;

    private float startingSeekWeight;

    private Boid myBoid;
    private GameObject myTarget;
    private Chase myChase;
    private Arrive myArrive;
    private DayNightCycle cycle;
    private float starterMaxVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        myBoid = gameObject.GetComponent<Boid>();
        myChase = gameObject.GetComponent<Chase>();
        myArrive = gameObject.GetComponent<Arrive>(); //Set components for easier access.
        cycle = GameObject.FindGameObjectWithTag("Sun").GetComponent<DayNightCycle>();
        starterMaxVelocity = myBoid.maxSpeed;

        startingSeekWeight = myChase.Multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (hunger < 2)
        {
            hunger += hungerRate * Time.deltaTime;
        }

        myChase.Multiplier = startingSeekWeight * hunger;
        
        if (cycle.isDay != isDiurnal) //If you should be sleeping...
        {
            myBoid.maxSpeed = Mathf.Lerp(myBoid.maxSpeed, 0, 0.3f * Time.deltaTime);
        }
        else
        {
            myBoid.maxSpeed = Mathf.Lerp(myBoid.maxSpeed, starterMaxVelocity, 0.3f * Time.deltaTime);
        }
        
        
        
        if (health <= 0) //When you die...
        {
            gameObject.GetComponent<Rigidbody>().freezeRotation = false; //Stop freezing the rotation (this causes the boid to fall over).
            gameObject.GetComponent<Boid>().enabled = false; //Disable the Boid so you stop moving.
            decayTime -= Time.deltaTime; //Start decaying.
        }

        if (decayTime <= 0) //Once fully decayed...
        {
            Destroy(gameObject); //Delete yourself.
        }

        if (myTarget == null) //If you don't have a target...
        {
            GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag(foodTag); //Find all the food in the scene.
            myTarget = potentialTargets[Random.Range(0, potentialTargets.Length +1)]; //Get a random one.
        }

        if (myTarget != null) //If you now have a target...
        {
            myChase.target = myTarget;
            myArrive.target = myTarget; //Set the targets.
            if (Vector3.Distance(myTarget.transform.position, gameObject.transform.position) <= eatDistance) //If you're within eating distance...
            {
                health++; //Increase health.
                Destroy(myTarget); //Destroy the food.
            }
        }
    }

    public void Caught() //If you're caught by predators...
    {
        health -= Time.deltaTime; //Tick down health while you're caught.
    }
}
