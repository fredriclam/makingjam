using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarController : MonoBehaviour {

    public float forceMagnitude;
    public float lateralForceMagnitude;
    public Camera playerCamera;
    private Rigidbody rb;
    public static float lightSpeed = 50;
    private float maxSpeed;
    private Vector3 heading;
    private Vector3 lastHeading;
    private float minimumSpeed = 0.05f;
    public static int maxEnergyLevel = 500;
    private float minPolarAngle = 30;
    private float energyForceAmplification = 10;
    private float kp = 0.0f;

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
		Vector3 temp = framePosition + new Vector3(relativePosition[0] / gammaVector[0],
            relativePosition[1] / gammaVector[1],
            relativePosition[2] / gammaVector[2]);
		if (temp.magnitude >= 0 || temp.magnitude <= 0) {
			return temp;
		} else {
			return framePosition+relativePosition;
		}
    }

    // Maps the Lorentz-boosted position of an arbitrary object back to the "original" Galilean position
    public static Vector3 InverseBoostedPosition(Vector3 framePosition, Vector3 relativePosition, Vector3 gammaVector)
    {
        Vector3 temp= framePosition + new Vector3(relativePosition[0] * gammaVector[0],
            relativePosition[1] * gammaVector[1],
            relativePosition[2] * gammaVector[2]);

		if (temp.magnitude >= 0 || temp.magnitude <= 0) {
			return temp;
		} else {
			return framePosition+relativePosition;
		}
    }

    // Elementwise product of the scaling vector with elementwise inverse of gamma vector 
    public static Vector3 LorentzBoostScale(Vector3 objectScale, Vector3 gammaVector) {
		Vector3 temp = new Vector3(objectScale[0] / gammaVector[0],
            objectScale[1] / gammaVector[1],
            objectScale[2] / gammaVector[2]);

		if (temp.magnitude >= 0 || temp.magnitude <= 0) {
			return temp;
		} else {
			return objectScale;
		}
    }

    // Initalize
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        heading = new Vector3(1, 0, 0);
        lastHeading = new Vector3(1, 0, 0);
        ComputeSpeedLimit();
        kp *= lateralForceMagnitude;
    }

    void ComputeSpeedLimit() {
        float energyRatio = 10 * Mathf.Floor(Player.energyLevel / 10) / maxEnergyLevel;
        // Scaling function
        maxSpeed = Mathf.Pow(0.7f*energyRatio + 0.3f,0.333f);
        energyForceAmplification = Mathf.Pow(1.0f * energyRatio + 1.0f, 0.5f);
    }
	
	// Update is called once per frame
	void FixedUpdate() {

        // Compute speed limit as function of energy level
        ComputeSpeedLimit();

        // Calculate forces
        // Update axes of camera-fixed frame
        Vector3 forwardDirection = playerCamera.transform.rotation * Vector3.forward;
        Vector3 upDirection = playerCamera.transform.rotation * Vector3.up;
        Vector3 rightDirection = playerCamera.transform.rotation * Vector3.right;
        float forwardInput;

        // Check the forward input for deceleration
		forwardInput = Input.GetAxis("Depth");
        // Don't allow moving back from deceleration
        if (forwardInput < 0 && rb.velocity.magnitude < minimumSpeed * lightSpeed) {
            forwardInput = 0;
        }

        float upInput = Input.GetAxis("Vertical");
        if (upInput > 0 && Vector3.Angle(Vector3.up, forwardDirection) < minPolarAngle)
            upInput = 0;
        else if (upInput < 0 && Vector3.Angle(Vector3.down, forwardDirection) < minPolarAngle)
            upInput = 0;

        // Apply force input, rotated to camera-fixed frame
        Vector3 forceVector = energyForceAmplification * forceMagnitude * forwardInput * forwardDirection +
			rb.velocity.magnitude*lateralForceMagnitude * Input.GetAxis("Horizontal") * rightDirection +
			rb.velocity.magnitude*lateralForceMagnitude * upInput * upDirection;

        // Avoid singularity at when facing directly upward
        
        float angularError = Mathf.Abs(Vector3.Angle(Vector3.up, forwardDirection) - minPolarAngle);
        float originalForceMagnitude;
        if (forceVector.magnitude != 0)
            originalForceMagnitude = forceVector.magnitude;
        else
            originalForceMagnitude = 1.0f;
        //float angularMargin = Mathf.Abs(Vector3.Angle(Vector3.up, forwardDirection));
        if (Vector3.Angle(Vector3.up, forwardDirection) < 0.5 * minPolarAngle || Vector3.Angle(Vector3.down, forwardDirection) < 0.5 * minPolarAngle)
            forceVector[1] -= Mathf.Sign(forwardDirection[1]) * kp * angularError * Time.deltaTime / (rb.velocity.magnitude + 0.2f);
        else if (Vector3.Angle(Vector3.up, forwardDirection) < 2 * minPolarAngle || Vector3.Angle(Vector3.down, forwardDirection) < 2 * minPolarAngle)
            forceVector[1] -= Mathf.Sign(forwardDirection[1]) * kp * angularError * Time.deltaTime / (rb.velocity.magnitude + 0.2f);
        // Rescale force
        if (forceVector.magnitude != 0)
            forceVector *= originalForceMagnitude / forceVector.magnitude;

        rb.AddForce(forceMagnitude * forceVector);

        // Speed penalty how much the speed is over by
        float infraction = rb.velocity.magnitude / (maxSpeed * lightSpeed);
        float u = rb.velocity[0];
        float v = rb.velocity[1];
        float w = rb.velocity[2];
        if (infraction > 1.0f) {
            u /= infraction;
            v /= infraction;
            w /= infraction;
        }
        else if (rb.velocity.magnitude != 0 && rb.velocity.magnitude < minimumSpeed * lightSpeed) {
            u *= (minimumSpeed * lightSpeed) / rb.velocity.magnitude;
            v *= (minimumSpeed * lightSpeed) / rb.velocity.magnitude;
            w *= (minimumSpeed * lightSpeed) / rb.velocity.magnitude;
        }
        // Check also the velocity angle
        //if (Vector3.Angle(Vector3.up, rb.velocity) < minPolarAngle) {
        //    float originalNorm = Mathf.Sqrt(u * u + v * v + w * w);
        //    v *= 0 *Mathf.Cos(minPolarAngle) / Mathf.Cos(Vector3.Angle(Vector3.up, rb.velocity));
        //    float newNorm = Mathf.Sqrt(u * u + v * v + w * w);
        //    u *= originalNorm / newNorm;
        //    w *= originalNorm / newNorm;
        //}
        //rb.velocity = new Vector3(u, v, w);
    }
}
