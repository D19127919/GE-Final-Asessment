using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : BoidBehaviour
{
    public float senseLength = 3;
    public float sideSenseAngle = 30; //In degrees

    private LayerMask mask = 1 << 3;

    private RaycastHit hit;
    // Start is called before the first frame update
    public override Vector3 Calculate()
    {
        force = Vector3.zero;
        //Forward
        if (DetectCollisions(transform.forward))
        {
            //Debug.Log("Something ahead");
            force += Vector3.Normalize((hit.transform.position - gameObject.transform.position));
        }
        
        //Left
        if (DetectCollisions(-transform.right))
        {
            //Debug.Log("Something left");
            force -= Vector3.Normalize((hit.transform.position - gameObject.transform.position));
        }
        
        //Right
        if (DetectCollisions(transform.right))
        {
            //Debug.Log("Something right");
            force -= Vector3.Normalize((hit.transform.position - gameObject.transform.position));
        }

        force.y = 0;

        return force;
    }

    // Update is called once per frame
    void Start()
    {
        mask = ~mask;
    }

    public bool DetectCollisions(Vector3 direction)
    {
        Debug.DrawRay(transform.position, direction);

        return Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, senseLength, mask);
    }
}
