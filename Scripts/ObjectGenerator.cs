using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour {

	public float xBound;
	public float yBound;
	public float zBound;
	public static float difficulty; //Proportional to time
	public float enemyNum; //Initial # of enemies
	public float energyNum;
	public GameObject enemy;
	public GameObject energy;
	List<GameObject> enemies;
	List<GameObject> energies;
	float lastTime;
	int index;
	GameObject player;

	// Use this for initialization
	void Start () {
		//transform.position = player.transform.position;
		energyIndex = 0;
		lastTime = 0;
		index = 0;
		difficulty = 1;
		player = GameObject.Find ("Player");
		enemies = new List<GameObject> ();
		energies = new List<GameObject> ();
		for (int i = 0; i < enemyNum; i++) {
			GameObject thing = Instantiate(enemy, new Vector3(i*0.01f-15f,0f,0f), Quaternion.identity) as GameObject;
			//thing.transform.parent=transform.parent;
			thing.SetActive(false);
			enemies.Add (thing);
		}
		for (int i = 0; i < energyNum; i++) {
			GameObject thing = Instantiate(energy, new Vector3(i*0.01f-15f,0f,0f), Quaternion.identity) as GameObject;
			thing.SetActive(false);
			//thing.transform.parent=transform.parent;//DC
			energies.Add (thing);
		}

	}



	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position;
		CheckBounds ();
		if (index < enemies.Count) {
			Spawn ();
		}

		if (energyIndex < energies.Count) {
			SpawnEnergy ();
		}

		if (false&&enemyNum!=difficulty/2) {//some relationship between difficulty and enemyNum DC
			float compare= difficulty/2-enemyNum;
			//index = Mathf.RoundToInt(difficulty/2);
			if (compare > 0) {
				for (int i = 0; i < compare; i++) {
					GameObject thing = Instantiate (enemy, new Vector3 (i * 0.01f - 15f, 0f, 0f), Quaternion.identity) as GameObject;
					enemies.Add (thing);
					thing.SetActive(false);
				}
			} else {
				for (int i = 0; i < compare; i++) {
					enemies.RemoveAt (i);
			}
		}
	}
}
		
	int energyIndex;
	float eLastTime;
	void SpawnEnergy()
	{
		float xPos= Random.Range (player.transform.position.x - xBound, player.transform.position.x + xBound);
		float yPos= Random.Range (player.transform.position.y - yBound, player.transform.position.y + yBound);
		if (Time.time > eLastTime + 3f&& energyIndex<energies.Count) {
			GameObject p = energies [energyIndex];
			p.SetActive(true);
			p.transform.position = new Vector3 (xPos,yPos,zBound/10+player.transform.position.z);
			energyIndex++;
			eLastTime = Time.time;
		}
	}

	void Spawn()
	{
		float xPos= Random.Range (player.transform.position.x - xBound, player.transform.position.x + xBound);
		float yPos= Random.Range (player.transform.position.y - yBound, player.transform.position.y + yBound);
		if (Time.time > lastTime + 3f&& index<enemies.Count) {
			GameObject e = enemies [index];
			e.SetActive(true);
			e.transform.position = new Vector3 (xPos,yPos,zBound+player.transform.position.z);
			index++;
			lastTime = Time.time;
			}
			
		}
		


	void CheckBounds()
	{
		float xPos;
		float yPos;
		foreach(var e in enemies)
		{
			if (e.transform.position.z < player.transform.position.z - zBound/2) {
				xPos= Random.Range (player.transform.position.x - xBound, player.transform.position.x + xBound);
				yPos= Random.Range (player.transform.position.y - yBound, player.transform.position.y + yBound);
				e.transform.position = new Vector3 (xPos,yPos,zBound+player.transform.position.z);
			}
		}
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
