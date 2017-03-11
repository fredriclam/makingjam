using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dilation : MonoBehaviour {

    public GameObject TrackedObject;
    public static float lightSpeed;
    private Vector3 originalScale;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        // Tracked object's rigid body
        rb = TrackedObject.GetComponent<Rigidbody>();
        // This object's scale
        originalScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float lightSpeed = SugarController.lightSpeed;
        Vector3 velocity = -rb.velocity;
        Vector3 gammaVector = new Vector3(originalScale[0] * Mathf.Sqrt(1 - (velocity[0] / lightSpeed) * (velocity[0] / lightSpeed)),
            originalScale[1] * Mathf.Sqrt(1 - (velocity[1] / lightSpeed) * (velocity[1] / lightSpeed)),
            originalScale[2] * Mathf.Sqrt(1 - (velocity[2] / lightSpeed) * (velocity[2] / lightSpeed)));
        this.transform.localScale = gammaVector;
    }
}
