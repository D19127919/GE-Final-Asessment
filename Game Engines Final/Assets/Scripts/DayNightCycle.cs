using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    public float cycleLength = 120; //How many seconds a full 24-hour cycle is.

    [SerializeField] private float currentTime = 0;

    public bool isDay = true; //If false, it's night.
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > cycleLength / 2)
        {
            isDay = false;
        }
        else
        {
            isDay = true;
        }

        if (currentTime > cycleLength)
        {
            currentTime = 0;
        }

        float lightRotation = (currentTime / cycleLength) * 360;
        
        gameObject.transform.rotation = Quaternion.Euler(lightRotation, -30, 0);
    }
}
