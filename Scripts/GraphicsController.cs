using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GraphicsController : MonoBehaviour {

	public Text energyLvl;
	public Text pressSpace;
	public Text maxV;
	public Text deadtext;
	public Text timeText;
	GameObject player;
	public Image img;
	public Image black;
	public static bool death;
	bool wait;
	Component arrow;

	// Use this for initialization
	void Start () {
		death = false;
		wait = false;
		arrow =GetComponent<BlinkArrow> ();
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

	publid static void Arrow()
	{
		
	}

	void resetGame()
	{
		if(Time.time>lastTime+1f)
		{
			pressSpace.enabled = true;
			if (Input.GetKey (KeyCode.Space)) {
				death = false;
				black.enabled = false;
				pressSpace.enabled = false;
				death = false;
				deadtext.enabled = false;
				DeathExpand.timeLimit = 5 * 60;
				SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
			}
		}


	}


	// Update is called once per frame
	bool fade;
	void Update () {

		if (death) {
			Death ();
			resetGame ();
		}

		if (fade) {
			Fade ();
		}
		transform.position=player.transform.position;
		energyLvl.text = "Energy Level: " + (Player.energyLevel).ToString();
		maxV.text = "Max Velocity: " + (Player.maxVelocity).ToString();
		float timeLeft = DeathExpand.timeLimit- Time.time;
		if (timeLeft < 0) {
			timeLeft = 0;
		}
		timeText.text="Time: " + timeLeft.ToString();

		if (Player.hasCollision && Time.time > lastTime + 2f) {
			Color c = Color.white;
			Color f = Color.clear;
			img.color = Color.white;
			Player.hasCollision = false;
			fade = true;
			lastTime = Time.time;
		} else {
			Player.hasCollision = false;
		}
	}


	void Death()
	{
		deadtext.enabled = true;
		player.GetComponent<SugarController> ().enabled=false;
		black.enabled = true;
		if (!wait) {
			lastTime = Time.time;
			wait = true;
		}
	}
}
