using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	Rigidbody rb;
	float difficulty;
	bool isMove;
	float zPos;
	float xPos;
	float yPos;
	float radius;
	// Use this for initialization
	void Start () {
		zPos= Random.Range (1,findSpeed()/3);
		xPos= Random.Range (-1*findSpeed(),1*findSpeed()/3);
		yPos= Random.Range (-1*findSpeed(),1*findSpeed()/3);
		rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (xPos,yPos,zPos);
		radius = this.GetComponent<SphereCollider> ().radius;
		
	}
	
	// Update is called once per frame
	void Update () {
		difficulty = ObjectGenerator.difficulty;
		if (isActiveAndEnabled && !isMove) {
			//zPos= Random.Range (1,findSpeed());
			//xPos= Random.Range (-findSpeed()*10,findSpeed()*10);
			//yPos= Random.Range (-findSpeed()*10,findSpeed()*10);
			//Move1();
		}

	}
		
	void OnTriggerEnter(Collider other)
	{
		if (other.name != "Player") {
			//transform.position = new Vector3 (transform.position.x + xPos, transform.position.y + yPos, transform.position.z - zPos - radius);
		} else {
			//transform.position= new Vector3(transform.position.x+xPos,yPos,transform.position.z-40);
		}

	}

	float findSpeed()//finds max speed based on difficulty
	{
		return 0.1f;
	}
		
	void Move1()
	{
		rb.velocity = new Vector3 (xPos,yPos,zPos);
		//transform.position= new Vector3(transform.position.x+xPos,transform.position.y+yPos,transform.position.z-zPos);
	}

	void Move3()//if there's time, there will be several ways the enemies can move
	{
		

	}

	void Move2()//Static
	{

	}
	

}
