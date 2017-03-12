using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

	public float xBound;
	public float yBound;
	public float zBound;
	public static float difficulty; //Proportional to time
	public int maxEnemyNum; //Initial # of enemies
	public float energyNum;
	public GameObject enemy;
	public GameObject energy;
	List<GameObject> energies;
	public static int enemyNum;
	float lastTime;
	//int index;
	GameObject player;
	public GameObject deathSphere;
	// Use this for initialization
	void Start () {
		//transform.position = player.transform.position;
		energyIndex = 0;
		enemyNum = 0;
		lastTime = 0;
		//index = 0;
		difficulty = 1;
		player = GameObject.Find ("Player");
		energies = new List<GameObject> ();
		for (int i = 0; i < 200; i++) {
			xPos= Random.Range (-1* xBound, xBound);
			yPos= Random.Range (-1* yBound, yBound);
			zPos= Random.Range (-1* zBound, zBound);
			GameObject thing = Instantiate(energy, new Vector3(xPos,yPos,zPos), Quaternion.identity) as GameObject;
		}
		GameObject s = Instantiate(deathSphere, deathSphere.transform.position, Quaternion.identity) as GameObject;

		for (int i = 0; i < enemyNum; i++) {
			xPos= Random.Range (-1*xBound,xBound);
			yPos= Random.Range (-1*yBound,yBound);
			zPos= Random.Range (-1*zBound,zBound);
			GameObject thing = Instantiate(enemy, new Vector3(xPos,yPos,zPos), Quaternion.identity) as GameObject;
		}

	}



	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position;
		SpawnEnergy();
		Spawn ();
	}
		
	public static int energyIndex;
	float eLastTime;
	float numOfEnergy;
	void SpawnEnergy()
	{
		xPos= Random.Range (-1* xBound, xBound);
		yPos= Random.Range (-1* yBound, yBound);
		zPos= Random.Range (-1* zBound, zBound);
		if (Time.time > eLastTime + 0.01f&& energyIndex<energyNum) {
			GameObject thing = Instantiate(energy, new Vector3(xPos,yPos,zPos), Quaternion.identity) as GameObject;
			energyIndex++;
			eLastTime = Time.time;
		}
	}

	float xPos;
	float yPos;
	float zPos;
	GameObject[] enemies;
	void Spawn()
	{
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		xPos= Random.Range (-1* xBound, xBound);
		yPos= Random.Range (-1* yBound, yBound);
		zPos= Random.Range (-1* zBound, zBound);
		if (Time.time > lastTime + 0.01f&&enemies.Length<maxEnemyNum) {
			Debug.Log (enemies.Length);
			GameObject thing = Instantiate(enemy, new Vector3(xPos,yPos,zPos), Quaternion.identity) as GameObject;
			enemyNum++;
			lastTime = Time.time;
			}
			
		}
		


	void CheckBounds()
	{
		float xPos;
		float yPos;
		//deletes and respawns the enemy
		foreach(var p in energies)
		{
			if (p.transform.position.z < player.transform.position.z - zBound/2) {
				xPos= Random.Range (player.transform.position.x - xBound, player.transform.position.x + xBound);
				yPos= Random.Range (player.transform.position.y - yBound, player.transform.position.y + yBound);
				p.transform.position = new Vector3 (xPos,yPos,zBound/10+player.transform.position.z);
			}
		}
	}

}
