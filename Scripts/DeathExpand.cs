using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathExpand : MonoBehaviour {

	GameObject player;
	float scale;
	bool firstTime;
	public static float timeLimit;
	//The sphere expands until it absorbs the photon or the photon has enough energy to escape
	void Start () {
		player = GameObject.Find ("Player");
		scale = 2f;
		timeLimit = Time.time+5*60;
		a = true;
		Debug.Log (transform.localScale);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScale ();
		Accelerate ();
		if (Time.time > timeLimit) {
			Death ();
		}
	}


	void Death()
	{
		if (!(transform.localScale.magnitude > 10000000)) {
			transform.localScale += Vector3.Lerp (new Vector3 (300, 300, 300), transform.localScale, 1f);
			GraphicsController.death = true;
		}
	}

	void UpdateScale()
	{
		//scale = Time.time;
	}

	bool a=true;
	void Accelerate()
	{
		if (a) {
			transform.localScale += Vector3.Lerp (transform.localScale, new Vector3 (scale, scale, scale), 10f);
		}
	}
		
	void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player" && Time.time > 5f) {
			GraphicsController.death = true;
			Debug.Log("D:");
		} else if(other.name!="Player") {
			Destroy (other.gameObject);
		}
	}

}
