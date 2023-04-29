using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawning : MonoBehaviour
{
    [Tooltip("The X or Z dimension of the scene plane")] public float sceneSize = 100;

    public float spawnDelay = 10;
    public float additionalDelayRandomizer = 5;
    public float spawnHeight = 10;
    public GameObject prefab;

    private float spawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneSize /= 2;
        spawnTimer = spawnDelay + Random.Range(0, additionalDelayRandomizer);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            Instantiate(prefab, new Vector3(Random.Range(-sceneSize, sceneSize), spawnHeight, Random.Range(-sceneSize, sceneSize)), Quaternion.identity);
            spawnTimer = spawnDelay + Random.Range(0, additionalDelayRandomizer);
        }
    }
}
