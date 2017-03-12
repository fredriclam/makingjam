using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScreen : MonoBehaviour {
	public Transform initial;
	public Transform final;
	public GameObject player;
	public Image black1;
	public Image black2;
	public Text beginning;
	public Text dialogue;
	string[] texts;
	// Use this for initialization
	void Start () {
		player.transform.position = initial.position;
		texts=new string[100];
		texts [0] = "Nothing can move faster than the speed of light.";
		texts [1] = "So life as a photon isn't bad.";
		texts [2] = "Stay away from black holes. Absorb energy.";
		texts [3] = "Physics was in our favour.";
		texts [4] = "";
		texts [5] = "Curiosity made me approahc the strange anomaly.";
		texts [6] = "Its pull was stronger than that of a black hole";
		texts [7] = "I knew I had made a mistake";
		beginning.text = texts [index];
		index++;
	}
	
	// Update is called once per frame
	int index=0;
	float lastTime;
	bool begin=true;
	bool diag=true;
	void Update () {
		if (begin&&Input.GetKey (KeyCode.Space)&&Time.time>lastTime+1f) {
			beginning.text = texts [index];
			index++;
			lastTime = Time.time;
		}
		if (index == 5) {
			begin = false;
			black1.enabled = false;
			black2.enabled = false;
			player.transform.position=Vector3.Lerp (player.transform.position, final.position, 0.001f*Time.time);
			expand.canMove = true;
			lastTime = Time.time;
			index++;
		}
		if (diag == true&&Time.time>lastTime+0.5f&&Input.GetKey (KeyCode.Space)) {
			dialogue.text=texts[index];
			index++;
			lastTime = Time.time;
		}

	}


}
