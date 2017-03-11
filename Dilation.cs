using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilation : MonoBehaviour {

    public GameObject TrackedObject;
    public static float lightSpeed;
    private Vector3 originalScale;
    private Rigidbody trackedRigidBody;
    private Rigidbody currentRigidBody;

    private Vector3 gamma;
    private Vector3 lastGamma;

	// Use this for initialization
	void Start () {
        // Tracked object's rigid body
        trackedRigidBody = TrackedObject.GetComponent<Rigidbody>();
        // Track (this)'s rigid body
        currentRigidBody = this.GetComponent<Rigidbody>();
        // This object's scale
        originalScale = this.transform.localScale;
        // Initialize
        lastGamma = new Vector3(1, 1, 1);

    }
	
	// Update is called once per frame
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
        this.transform.position = SugarController.InverseBoostedPosition(framePosition, this.transform.position - framePosition, lastGamma);
        this.transform.position = SugarController.MapToBoostedPosition(framePosition, this.transform.position - framePosition, gamma);
        
        // Save relative velocity 
        lastGamma = gamma;
    }
}
