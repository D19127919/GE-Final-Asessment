using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyAI : MonoBehaviour
{
    public float health = 5;
    public string foodTag = "Food";
    public float eatDistance = 2;

    private GameObject myTarget;
    private Chase myChase;
    private Arrive myArrive;
    
    // Start is called before the first frame update
    void Start()
    {
        myChase = gameObject.GetComponent<Chase>();
        myArrive = gameObject.GetComponent<Arrive>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (myTarget == null)
        {
            myTarget = GameObject.FindWithTag(foodTag);
        }

        if (myTarget != null) //If you now have a target...
        {
            myChase.target = myTarget;
            myArrive.target = myTarget; //Set the targets.
        }

        if (Vector3.Distance(myTarget.transform.position, gameObject.transform.position) <= eatDistance)
        {
            health++;
            Destroy(myTarget);
        }
    }

    public void Caught()
    {
        health -= Time.deltaTime;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(foodTag))
        {
            myTarget = other.gameObject;
        }
    }*/
}
