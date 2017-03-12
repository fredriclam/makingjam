using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static float maxVelocity;
	public static float energyLevel;
	public static bool hasCollision;
	//difficulty changes with number of photons. (If you lose 5, you go back to the previous level

	// Use this for initialization
	void Start () {
		maxVelocity = 10;//might change!
		energyLevel=10;
		hasCollision = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		//MovePlayer ();
		Vector3 pos = transform.position;
		pos.y = Mathf.Clamp(transform.position.y,-700,700);
		pos.x = Mathf.Clamp(transform.position.x,-700,700);
		pos.z = Mathf.Clamp (transform.position.z, -700, 700);
		transform.position=pos;

	}
		

	void MovePlayer()
	{
		float speed = 3f;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.position = new Vector3 (transform.position.x-speed,transform.position.y,transform.position.z);
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position = new Vector3 (transform.position.x+speed,transform.position.y,transform.position.z);
		}
		if (Input.GetKey (KeyCode.Space)) {
			transform.position = new Vector3 (transform.position.x,transform.position.y+speed,transform.position.z);
		}
		if (Input.GetKey (KeyCode.LeftShift)) {
			transform.position = new Vector3 (transform.position.x,transform.position.y-speed,transform.position.z);
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			transform.position = new Vector3 (transform.position.x,transform.position.y,transform.position.z+speed);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			transform.position = new Vector3 (transform.position.x,transform.position.y,transform.position.z-speed);
		}

	}

	void increaseVelocity()
	{
		if (Mathf.Floor(energyLevel/10)>(maxVelocity-10)/5+1) {
			maxVelocity += 5;
		}
	}

	void decreaseVelocity()
	{
		if (Mathf.Floor(energyLevel/10)<(maxVelocity-10)/5+1) {
			maxVelocity -= 5;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		BlinkArrow.Blink ();
		if (other.name == "Enemy(Clone)") {
			energyLevel--;
			hasCollision = true;//DC have to make a script to deal with this
			if (energyLevel < 0) {
				energyLevel = 0;
			}
			} else if (other.name == "DeathSphere") {
		//nothing
		} else {
			energyLevel++;
			increaseVelocity ();
		}

	}


}
