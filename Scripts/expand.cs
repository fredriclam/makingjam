using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expand : MonoBehaviour {

	public static bool canMove=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canMove) {
			transform.localScale += Vector3.Lerp (transform.localScale, new Vector3 (0.01f, 0.01f, 0.01f), 50f);
		}
	}

	void exp()
	{
		//transform.localScale += Vector3.Lerp (transform.localScale, new Vector3 (scale, scale, scale), 10f);
	}
}
