using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDolly : MonoBehaviour {

    private Rigidbody rb;
    Vector3 omega;

    // Use this for initialization
    void Start () {
        rb = this.GetComponent<Rigidbody>();
        float theta = 15.0f;
        float speed = 4.2f;
        rb.velocity = speed * new Vector3(-speed, 0, 0);
        // Angular speed
        float w = 5.0f;
        omega = new Vector3(0, -w, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Angular acceleration
        float alph = 0.36f;
        // Linear acceleration
        float acc = 16.0f;
        float decayCoefficient = 0.98f;

        // Angular accelerate
        float error = Mathf.Abs(90 - this.transform.rotation.eulerAngles[1]);
        Debug.Log(this.transform.rotation.eulerAngles[1]);
        if (this.transform.rotation.eulerAngles[1] > 300 || this.transform.rotation.eulerAngles[1] < 40) {
            omega = alph * error * new Vector3(0, 1, 0) * Time.deltaTime;
            this.transform.Rotate(omega);
        }
        else if (this.transform.rotation.eulerAngles[1] > 300 || this.transform.rotation.eulerAngles[1] < 80) {
            omega *= decayCoefficient;
            this.transform.Rotate(omega);
        }
        // Linear accelerate
        rb.velocity += new Vector3(-acc * Time.deltaTime, 0, 0);

    }
}
