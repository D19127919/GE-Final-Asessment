using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyAI : MonoBehaviour
{
    public float decayTime = 5;

    private float timer = 0;

    private bool isCaught = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timer -= Time.deltaTime;
        

        if (timer <= 0 && isCaught)
        {
            Destroy(gameObject);
        }
    }

    public void Caught()
    {
        timer = decayTime;
        isCaught = true;
    }
}
