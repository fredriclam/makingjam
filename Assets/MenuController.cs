using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public float canvasRotationSpeed;
    public Canvas FrontMenu;
    public Canvas SettingsMenu;
    //public static int energyVictoryRequirement;

    // Use this for initialization
    void Awake () {
        //energyVictoryRequirement = 42;
        SettingsMenu.enabled = false;
    }
	
	public void StartGame () {
        SceneManager.LoadScene(1);
	}

    public void OpenSettingsMenu() {
        FrontMenu.enabled = false;
        SettingsMenu.enabled = true;
    }

    public void CloseSettingsMenu() {
        FrontMenu.enabled = true;
        SettingsMenu.enabled = false;
    }

    void Update() {
        Vector3 rotation = canvasRotationSpeed * new Vector3(0, 1, 0) * Time.deltaTime;
        this.transform.Rotate(rotation);

        // Support escape-key-quitting
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
