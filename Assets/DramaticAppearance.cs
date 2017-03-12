using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DramaticAppearance : MonoBehaviour {
	public Camera cam;
	public GameObject lamp;
	public Image img;
	private float initializeTime;
	private float decayCoefficient = 0.98f;
	private Light L;
	// Use this for initialization
	void Start () {
		initializeTime = Time.time;
		L = lamp.GetComponent<Light>();
		img.enabled = false;
		//Skybox sb = cam.GetComponent<Skybox>();
		//boxlight = sb.GetComponent<Light>();
	}

	// Update is called once per frame
	float lerp2=0;
	float lerpTime=3f;
	void Update () {
		Debug.Log(Time.time - initializeTime);
		if (Time.time - initializeTime > 4.5f) {
			float omega = 120.0f * Time.deltaTime;
			if (this.transform.eulerAngles[1] < 270)
				this.transform.Rotate(new Vector3(0, omega, 0));
			img.enabled=true;
			lerp2 += Time.deltaTime;
			float perc = lerp2 / lerpTime;
			img.color = Color.Lerp (Color.clear, Color.black,perc);

			//L.intensity *= decayCoefficient;
		}

	
	}
}
