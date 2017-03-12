using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilation : MonoBehaviour {

    public GameObject TrackedObject;
    public static float lightSpeed;
    private Vector3 originalScale;
    private Rigidbody trackedRigidBody;
    private Rigidbody currentRigidBody;
    public static float bigG = 500f;
    public static float characteristicLength = 50.0f;
    public static float virtualRadius = 1.0f;

    private Vector3 lastFramePosition;
    private Vector3 lastThisPosition;
    private Vector3 gamma;
    private Vector3 lastGamma;

	// Use this for initialization
	void Start () {
        // Tracked object's rigid body
		TrackedObject=GameObject.Find("Player");
		trackedRigidBody = TrackedObject.GetComponent<Rigidbody>();
        // Track (this)'s rigid body
        currentRigidBody = this.GetComponent<Rigidbody>();
        // This object's scale
        originalScale = this.transform.localScale;
        // Initialize
        lastGamma = new Vector3(1, 1, 1);
        lastFramePosition = trackedRigidBody.position;
        lastThisPosition = this.transform.position;
    }
	
	// Update is called once per frame
	bool firstTime = true;
	void FixedUpdate () {
        // Grab the tracked reference frame
        Rigidbody playerReferenceFrame = TrackedObject.GetComponent<Rigidbody>();
        // Calculate gamma factor
        Vector3 gamma = SugarController.GammaVector(currentRigidBody.velocity, playerReferenceFrame);

        // Boost the object scale
        this.transform.localScale = SugarController.LorentzBoostScale(originalScale, gamma);

        // Grab frame position
        Vector3 framePosition = TrackedObject.transform.position;
        // Boost
		this.transform.position = SugarController.InverseBoostedPosition (lastFramePosition, lastThisPosition - framePosition, lastGamma);
		
		this.transform.position = SugarController.MapToBoostedPosition (framePosition, this.transform.position - framePosition, gamma);
        // Save relative velocity 
        lastGamma = gamma;
        lastFramePosition = framePosition;
        lastThisPosition = this.transform.position;

        // Apply gravity
        ApplyGravity();
    }

    private void ApplyGravity() {
        // Calculate gravitational force
        // Relative position normalized by characteristic length (smears attraction force)
        Vector3 relativePosition = (trackedRigidBody.position - currentRigidBody.position) / characteristicLength;
        // Calculate ||r||, and add a virtual radius to remove singularity
        float virtualRelativePosition = relativePosition.magnitude + virtualRadius;
        // Calculuates ||r||^3
        float r3 = virtualRelativePosition * virtualRelativePosition * virtualRelativePosition;
        // Calculate gravitational force
        Vector3 force = - bigG * currentRigidBody.mass * trackedRigidBody.mass * relativePosition / r3;
        // Apply force to tracked body
        trackedRigidBody.AddForce(force);
    }
}
