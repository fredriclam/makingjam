using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyMove : MonoBehaviour {

	Rigidbody rb;
	//bool isMove;
	float zPos;
	float xPos;
	float yPos;
	float radius;
	GameObject[] energies;
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		zPos= Random.Range (0.01f,1);
		xPos= Random.Range (-3,3);
		yPos= Random.Range (-3,3);
		rb = GetComponent<Rigidbody> ();
		radius = this.GetComponent<SphereCollider> ().radius;
	}

	// Update is called once per frame
	void Update () {
		if (isActiveAndEnabled) {
			//zPos= Random.Range (0.01f,0.05f);
			Move();
			energies = GameObject.FindGameObjectsWithTag ("Energy");
			DeleteThis ();
		}

		if(Vector3.Distance(transform.position,player.transform.position)<25)
		{
			transform.position=Vector3.Lerp (transform.position,player.transform.position,0.001f);
			Player.energyLevel++;
		}

	}


	void InRange()
	{
		for(int i =0; i<energies.Length;i++)
		{
			if(energies[i]!=null)
			{
				if(Vector3.Distance(transform.position,energies[i].transform.position)<radius)
				{
					Destroy(this.gameObject);
					ObjectGenerator.enemyNum--;
				}
			}
		}
	}

	void DeleteThis()
	{
		if (Mathf.Abs (transform.position.z - player.transform.position.z) > 1000||Mathf.Abs (transform.position.x - player.transform.position.x) > 1000||Mathf.Abs (transform.position.y - player.transform.position.y) > 1000) {
			ObjectGenerator.energyIndex--;
			Destroy (this.gameObject);
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
		yPos = yPos+10*Mathf.Sin (transform.position.z/100);
		transform.position= new Vector3(transform.position.x+xPos,yPos,transform.position.z-zPos);
	}

}