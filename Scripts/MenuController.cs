using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public float canvasRotationSpeed;

    // Use this for initialization
    void Awake () {
		
	}
	
	public void StartGame () {
        SceneManager.LoadScene(1);
	}

    void Update() {
        Vector3 rotation = canvasRotationSpeed * new Vector3(0, 1, 0) * Time.deltaTime;
        this.transform.Rotate(rotation);
    }
}
