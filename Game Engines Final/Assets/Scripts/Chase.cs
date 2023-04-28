using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BoidBehaviour
{
    public GameObject target;

    public Vector3 targetPos;
    public bool isSmart = true;
    
    // Start is called before the first frame update
    public override Vector3 Calculate() //This behaviour...
    {
        if (target != null)
        {
            targetPos = target.transform.position;
            if (isSmart)
            {
                float targetDist = Vector3.Distance(target.transform.position, gameObject.transform.position);
                float timeToReach = targetDist / myBoid.maxSpeed;

                targetPos += (target.GetComponent<Boid>().velocity * timeToReach);
            }
        
            return myBoid.ChaseForce(targetPos);
        }

        return Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
