using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject TrackedObject;
    private Vector3 followDistance;

	// Use this for initialization
	void Start () {
        // Save the follow distance
        followDistance = this.transform.position - TrackedObject.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        this.transform.position = TrackedObject.transform.position + followDistance;
    }
}
