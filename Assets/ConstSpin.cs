using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstSpin : MonoBehaviour {

    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        float tau = 10;
        rb.AddTorque(tau * new Vector3(0, 1, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
