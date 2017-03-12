using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarController : MonoBehaviour {

    public float forceMagnitude;
    public float lateralForceMagnitude;
    public Camera playerCamera;
    private Rigidbody rb;
    public static float lightSpeed = 1;
    private float maxSpeed = 0.99f;
    private Vector3 heading;
    private Vector3 lastHeading;
    private float minimumSpeed = 0.01f;

    // Calculates the vector gamma (different in each direction)
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

    // Maps the position of an arbitrary object to the Lorentz-boosted position
    public static Vector3 MapToBoostedPosition(Vector3 framePosition, Vector3 relativePosition, Vector3 gammaVector)
    {
        return framePosition + new Vector3(relativePosition[0] / gammaVector[0],
            relativePosition[1] / gammaVector[1],
            relativePosition[2] / gammaVector[2]);
    }

    // Maps the Lorentz-boosted position of an arbitrary object back to the "original" Galilean position
    public static Vector3 InverseBoostedPosition(Vector3 framePosition, Vector3 relativePosition, Vector3 gammaVector)
    {
        return framePosition + new Vector3(relativePosition[0] * gammaVector[0],
            relativePosition[1] * gammaVector[1],
            relativePosition[2] * gammaVector[2]);
    }

    // Elementwise product of the scaling vector with elementwise inverse of gamma vector 
    public static Vector3 LorentzBoostScale(Vector3 objectScale, Vector3 gammaVector) {
        return new Vector3(objectScale[0] / gammaVector[0],
            objectScale[1] / gammaVector[1],
            objectScale[2] / gammaVector[2]);
    }

    // Initalize
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        heading = new Vector3(1, 0, 0);
        lastHeading = new Vector3(1, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        //float xMovement = Input.GetAxis("Horizontal");
        //float yMovement = Input.GetAxis("Vertical");

        //Vector3 forceVector = new Vector3(xMovement, 0, yMovement);
        //rb.AddForce(forceMagnitude * forceVector);

        // Calculate forces
        // Update axes of camera-fixed frame
        Vector3 forwardDirection = playerCamera.transform.rotation * Vector3.forward;
        Vector3 upDirection = playerCamera.transform.rotation * Vector3.up;
        Vector3 rightDirection = playerCamera.transform.rotation * Vector3.right;
        float forwardInput;

        // Check the forward input for deceleration
        if (Input.GetAxis("Depth") < 0 && rb.velocity.magnitude < minimumSpeed) {
            forwardInput = Input.GetAxis("Depth");
        }
        else {
            forwardInput = 0.0f;
        }

        // Apply force input, rotated to camera-fixed frame
        Vector3 forceVector = forceMagnitude * forwardInput * forwardDirection +
            lateralForceMagnitude * Input.GetAxis("Horizontal") * rightDirection +
            lateralForceMagnitude * Input.GetAxis("Vertical") * upDirection;

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
