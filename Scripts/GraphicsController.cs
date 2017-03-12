using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GraphicsController : MonoBehaviour {

	public Text energyLvl;
	public Text maxV;
	GameObject player;
	public Image img;

	// Use this for initialization
	void Start () {
		//img.color = Color.clear;
		player = GameObject.Find ("Player");
		transform.position=player.transform.position;
		img.color = Color.clear;

		
	}

	float lastTime;
	void Fade()
	{
		Color c = Color.white;
		Color f = Color.clear;
		img.material.color = Color.Lerp (c,f,Mathf.PingPong(2f*Time.time,0.15f));
		if (img.color == Color.clear||Time.time>lastTime+0.15f) {
			img.color = Color.clear;
			fade = false;
		}
	}

	// Update is called once per frame
	bool fade;
	void Update () {

		if (fade) {
			Fade ();
		}
		transform.position=player.transform.position;
		energyLvl.text = "Energy Level: " + (Player.energyLevel).ToString();
		maxV.text = "Max Velocity: " + (Player.maxVelocity).ToString();

		if (false&&Player.hasCollision&&Time.time>lastTime+1f) {
			Color c = Color.white;
			Color f = Color.clear;
			img.color=Color.white;
			Player.hasCollision = false;
			fade = true;
			lastTime = Time.time;
		}
	}
}
