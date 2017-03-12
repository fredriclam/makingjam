using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	Rigidbody rb;
	float zPos;
	float xPos;
	float yPos;
	public float yBounds;
	public float xBounds;
	public float zBounds;
	public GameObject[] enemies;
	GameObject player;
	float radius;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		zPos= Random.Range (1,findSpeed()/3);
		xPos= Random.Range (-1*findSpeed(),1*findSpeed()/3);
		yPos= Random.Range (-1*findSpeed(),1*findSpeed()/3);
		rb = GetComponent<Rigidbody> ();
		rb.velocity = new Vector3 (xPos,yPos,zPos);
		radius = this.GetComponent<SphereCollider> ().radius;
		
	}
	
	// Update is called once per frame
	void Update () {
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (isActiveAndEnabled) {
		}
		DeleteThis ();
	}

	void InRange()
	{
		for(int i =0; i<enemies.Length;i++)
		{
			if(enemies[i]!=null)
			{
				if(Vector3.Distance(transform.position,enemies[i].transform.position)<radius)
				{
					Destroy(this.gameObject);
					ObjectGenerator.enemyNum--;
				}
			}
		}
	}

	void DeleteThis()
	{
		if (Mathf.Abs (transform.position.z - player.transform.position.z) > zBounds||Mathf.Abs (transform.position.x - player.transform.position.x) > xBounds||Mathf.Abs (transform.position.y - player.transform.position.y) > yBounds) {
			ObjectGenerator.enemyNum--;
			Destroy (this.gameObject);
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
		return 10f;
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
