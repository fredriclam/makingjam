using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CutScreen : MonoBehaviour {


	public Transform initial;
	public Transform final;
	public Transform last;
	public GameObject player;
	public GameObject anom;
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
		texts [5] = "Curiosity made me approach the strange anomaly.";
		texts [6] = "Its pull was stronger than that of a black hole";
		texts [7] = "I knew I had made a mistake";
		texts [8] = "Something had gone wrong with the laws of physics";
		texts [9] = "";
		texts [10] = "I don't have enough energy to escape. I'll need to collect energy.";
		texts [11] = "The laws of physics are already getting skewed. I have to hurry.";
		beginning.text = texts [index];
		index++;
		currentLerpTime = 0;
		begin = true;
		diag = false;
		lerp2 = 0;
	}
	
	// Update is called once per frame
	int index=0;
	float lastTime;
	bool begin;
	float lerp2;
	bool diag;
	float currentLerpTime;
	float lerpTime;
	void Update () {

		lerpTime = 5f;
		if (begin&&Input.GetKey (KeyCode.Space)&&Time.time>lastTime+1f) {
			beginning.text = texts [index];
			index++;
			lastTime = Time.time;
			player.transform.position = initial.position;
		}
		if (index >= 5) {
			begin = false;
			black1.enabled = false;
			black2.enabled = false;
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}
			float perc = currentLerpTime / lerpTime;
			player.transform.position=Vector3.Lerp (initial.position, final.position, perc);
			expand.canMove = true;
			diag = true;
		}
		if (diag == true&&Time.time>(lastTime+0.5f)&&Input.GetKey (KeyCode.Space)) {
			dialogue.enabled = true;
			dialogue.text=texts[index];
			index++;
			lastTime = Time.time;
		}
		if (index > 9) {
			anom.GetComponent<MeshRenderer> ().enabled = false;
			player.transform.position = last.position;
		}

	
		if (index > 12) {
			lerp2 += Time.deltaTime;
			float perc = lerp2 / lerpTime;
			black1.enabled=true;
			black2.enabled=true;
			black1.color = Color.Lerp (Color.clear, Color.black,perc);
			black2.color = Color.Lerp (Color.clear, Color.black,perc);
			if (black1.color == Color.black) {
				SceneManager.LoadScene ("GenerateEnemies");
			}
		}
	}
		


}
