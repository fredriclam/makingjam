using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyMove : MonoBehaviour {

	Rigidbody rb;
	float difficulty;
	bool isMove;
	float zPos;
	float xPos;
	float yPos;
	float radius;
	// Use this for initialization
	void Start () {
		zPos= Random.Range (0.01f,1);
		xPos= Random.Range (-3,3);
		yPos= Random.Range (-3,3);
		rb = GetComponent<Rigidbody> ();
		radius = this.GetComponent<SphereCollider> ().radius;
	}

	// Update is called once per frame
	void Update () {
		difficulty = ObjectGenerator.difficulty;
		if (isActiveAndEnabled && !isMove) {
			zPos= Random.Range (0.01f,0.05f);
			Move();
		}

	}

	void OnTriggerEnter(Collider other)
	{

		if (other.name != "Player") {
			transform.position = new Vector3 (transform.position.x + xPos, transform.position.y + yPos, transform.position.z - zPos - 0.1f);
		} else {
			transform.position= new Vector3(transform.position.x+xPos,yPos,transform.position.z-20);
		}
	}
		
	void Move()
	{
		yPos = 10*Mathf.Sin (transform.position.z/10);
		transform.position= new Vector3(transform.position.x+xPos,yPos,transform.position.z-zPos);
	}

}

//DC-FIX THE INDEX FOR THE energies!!!!!!!!!!!!!!!!!!!!