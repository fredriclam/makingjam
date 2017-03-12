using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemorizedBehaviour : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        float theta = 15.0f;
        float speed = 5.0f;
        rb.velocity = speed * new Vector3(speed, 0, 0);
        Debug.Log(1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
