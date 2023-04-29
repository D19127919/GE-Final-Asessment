using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawning : MonoBehaviour
{
    [Tooltip("The X or Z dimension of the scene plane")] public float sceneSize = 100;

    public float spawnDelay = 10; //How many seconds minimum are between spawns.
    public float additionalDelayRandomizer = 5; //Up to how many extra seconds could be between spawns.
    public float spawnHeight = 10; //How high above the ground the items spawn.
    public GameObject prefab; //What is being spawned.

    private float spawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneSize /= 2; //Set the scene size properly for the calculations.
        spawnTimer = spawnDelay + Random.Range(0, additionalDelayRandomizer); //Set the spawn timer to a random value between the base delay and the base delay plus the extra delay.
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime; //Reduce the timer.
        if (spawnTimer <= 0) //If it's zero or less...
        {
            Instantiate(prefab, new Vector3(Random.Range(-sceneSize, sceneSize), spawnHeight, Random.Range(-sceneSize, sceneSize)), Quaternion.identity); //Create a new item within the bounds.
            spawnTimer = spawnDelay + Random.Range(0, additionalDelayRandomizer); //Reset the timer.
        }
    }
}
