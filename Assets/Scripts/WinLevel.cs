﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if (BringOutText.energyVictoryCondition != 0) {
			if (Player.energyLevel >= BringOutText.energyVictoryCondition) {
				SceneManager.LoadScene (3);
			}
		} else {
			if (Player.energyLevel >= 30) {
				SceneManager.LoadScene (3);
			}
		}
		
	}
}
