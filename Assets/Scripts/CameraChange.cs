using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 rot = transform.localRotation.eulerAngles;
		rot.x = Mathf.Clamp (transform.localRotation.eulerAngles.x,-80,80);
		rot.y = Mathf.Clamp (transform.localRotation.eulerAngles.y,-80,80);
		transform.localRotation =Quaternion.Euler(rot);
	}
}
