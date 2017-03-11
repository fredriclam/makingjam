using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarController : MonoBehaviour {

    public float forceMagnitude;
    private Rigidbody rb;
    public static float lightSpeed = 10;
    private float maxSpeed = 0.9f; 

    public static Vector3 GammaVector(Vector3 objectVelocity, Rigidbody playerReferenceFrame) {
        // Grab velocity 
        Vector3 frameVelocity = playerReferenceFrame.velocity;
        Vector3 relativeVelocity = frameVelocity - objectVelocity;
        // Gamma vector
        return new Vector3(
            1.0f / Mathf.Sqrt(1 - (relativeVelocity[0] / lightSpeed) * (relativeVelocity[0] / lightSpeed)),
            1.0f / Mathf.Sqrt(1 - (relativeVelocity[1] / lightSpeed) * (relativeVelocity[1] / lightSpeed)),
            1.0f / Mathf.Sqrt(1 - (relativeVelocity[2] / lightSpeed) * (relativeVelocity[2] / lightSpeed)));
    }

    // Affine transformation
    public static Vector3 MapToBoostedPosition(Vector3 framePosition, Vector3 relativePosition, Vector3 gammaVector)
    {
        return framePosition + new Vector3(relativePosition[0] / gammaVector[0],
            relativePosition[1] / gammaVector[1],
            relativePosition[2] / gammaVector[2]);
    }

    public static Vector3 InverseBoostedPosition(Vector3 framePosition, Vector3 relativePosition, Vector3 gammaVector)
    {
        return framePosition + new Vector3(relativePosition[0] * gammaVector[0],
            relativePosition[1] * gammaVector[1],
            relativePosition[2] * gammaVector[2]);
    }

    public static Vector3 LorentzBoostScale(Vector3 objectScale, Vector3 gammaVector)
    {
        return new Vector3(objectScale[0] / gammaVector[0],
            objectScale[1] / gammaVector[1],
            objectScale[2] / gammaVector[2]);
    }

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        Vector3 forceVector = new Vector3(xMovement, 0, yMovement);
        rb.AddForce(forceMagnitude * forceVector);

        // Speed penalty how much the speed is over by
        float infraction = rb.velocity.magnitude / (maxSpeed * lightSpeed);
        if (infraction > 1.0f) {
            float u = rb.velocity[0] / infraction;
            float v = rb.velocity[1] / infraction;
            float w = rb.velocity[2] / infraction;
            rb.velocity = new Vector3(u, v, w);
        }
	}
}
