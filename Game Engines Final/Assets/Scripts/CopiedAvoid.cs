 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/*
I need to disclaim this, because it's not easy to understand just by looking...

Yes. This looks familiar. Yes, this is your code from the example repository.

I've been transcribing that code in some places to help create my behaviours, but I was always transcribing and changing variable names and adding comments. Not to escape claims of plagiarism, but rather to prove I
understood how the code works - because that's what's really important in my eyes; not knowing how to code from scratch with no resources, but understanding what the stuff you're copying does.

And then we got to this script. Unlike all the others I couldn't make heads or tails out of it. I still don't understand half the functions or even half the terms used here and my attempts to create my own
failed miserably, so I copy-pasted this directly and synced all the variable names just so I could have a functional avoid behaviour. This is in direct contrast to my state-machine scripts which I was able to make
work albeit messily.

So when I'm all done with the rest of this project - provided I both have time and don't forget - I'll revisit this and see if I can either figure out what the heck is going on here or make my own, but given my schedule
and how much trouble I had the last time I tried this I doubt either of those will happen.

I'd rather have a complete project with one plagiarized behaviour than 50% of a project with an original, functional one.
*/


public class CopiedAvoid : BoidBehaviour
{
    public float scale = 4.0f;
    public float forwardFeelerDepth = 30;
    public float sideFeelerDepth = 15;
    FeelerInfo[] feelers = new FeelerInfo[5];

    public float frontFeelerUpdatesPerSecond = 10.0f;
    public float sideFeelerUpdatesPerSecond = 5.0f;

    public float feelerRadius = 2.0f;

    public enum ForceType
    {
        normal,
        incident,
        up,
        braking
    };

    public ForceType forceType = ForceType.normal;

    public LayerMask mask = -1;

    public void OnEnable()
    {
        StartCoroutine(UpdateFrontFeelers());
        StartCoroutine(UpdateSideFeelers());
    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            foreach (FeelerInfo feeler in feelers)
            {
                Gizmos.color = Color.gray;
                if (Application.isPlaying)
                {
                    Gizmos.DrawLine(transform.position, feeler.point);
                }
                if (feeler.collided)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawLine(feeler.point, feeler.point + (feeler.normal * 5));
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(feeler.point, feeler.point + force);
                }
            }
        }
    }

    Vector3 lerpedForce = Vector3.zero;
    public override Vector3 Calculate()
    {
        force = Vector3.zero;

        for (int i = 0; i < feelers.Length; i++)
        {
            FeelerInfo info = feelers[i];
            if (info.collided)
            {
                force += CalculateSceneAvoidanceForce(info);
            }
        }
        lerpedForce = Vector3.Lerp(lerpedForce,force, Time.deltaTime);
        return lerpedForce;
    }

    void UpdateFeeler(int feelerNum, Quaternion localRotation, float baseDepth, FeelerInfo.FeeelerType feelerType)
    {
        Vector3 direction = localRotation * transform.rotation * Vector3.forward;
        float depth = baseDepth + ((myBoid.velocity.magnitude / myBoid.maxSpeed) * baseDepth);

        RaycastHit info;
        bool collided = Physics.SphereCast(transform.position, feelerRadius, direction, out info, depth, mask.value);
        Vector3 feelerEnd = collided ? info.point : (transform.position + direction * depth);
        feelers[feelerNum] = new FeelerInfo(feelerEnd, info.normal
            , collided, feelerType);
    }

    System.Collections.IEnumerator UpdateFrontFeelers()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        while (true)
        {
            UpdateFeeler(0, Quaternion.identity, this.forwardFeelerDepth, FeelerInfo.FeeelerType.front);
            yield return new WaitForSeconds(1.0f / frontFeelerUpdatesPerSecond);
        }
    }

    System.Collections.IEnumerator UpdateSideFeelers()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 0.5f));
        float angle = 45;
        while (true)
        {
            // Left feeler
            UpdateFeeler(1, Quaternion.AngleAxis(angle, Vector3.up), sideFeelerDepth, FeelerInfo.FeeelerType.side);
            // Right feeler
            UpdateFeeler(2, Quaternion.AngleAxis(-angle, Vector3.up), sideFeelerDepth, FeelerInfo.FeeelerType.side);
            // Up feeler
            UpdateFeeler(3, Quaternion.AngleAxis(angle, Vector3.right), sideFeelerDepth, FeelerInfo.FeeelerType.side);
            // Down feeler
            UpdateFeeler(4, Quaternion.AngleAxis(-angle, Vector3.right), sideFeelerDepth, FeelerInfo.FeeelerType.side);

            yield return new WaitForSeconds(1.0f / sideFeelerUpdatesPerSecond);
        }
    }
    

    Vector3 CalculateSceneAvoidanceForce(FeelerInfo info)
    {
        Vector3 force = Vector3.zero;

        Vector3 fromTarget = fromTarget = transform.position - info.point;
        float dist = Vector3.Distance(transform.position, info.point);

        switch (forceType)
        {
            case ForceType.normal:
                force = info.normal * (forwardFeelerDepth * scale / dist);
                break;
            case ForceType.incident:
                fromTarget.Normalize();
                force -= Vector3.Reflect(fromTarget, info.normal) * (forwardFeelerDepth / dist);
                break;
            case ForceType.up:
                force += Vector3.up * (forwardFeelerDepth * scale / dist);
                break;
            case ForceType.braking:
                force += fromTarget * (forwardFeelerDepth / dist);
                break;
        }
        return force;
    }
    
    struct FeelerInfo
    {
        public Vector3 point;
        public Vector3 normal;
        public bool collided;
        public FeeelerType feelerType;

        public enum FeeelerType
        {
            front,
            side
        };

        public FeelerInfo(Vector3 point, Vector3 normal, bool collided, FeeelerType feelerType)
        {
            this.point = point;
            this.normal = normal;
            this.collided = collided;
            this.feelerType = feelerType;
        }
    }
}