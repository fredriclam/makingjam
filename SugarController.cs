using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarController : MonoBehaviour {

    public float forceMagnitude;
    private Rigidbody rb;
    public static float lightSpeed = 10;
    private float maxSpeed = 0.9f; 

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
