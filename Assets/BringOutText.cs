using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringOutText : MonoBehaviour {

    //public GameObject outputTextBoxObject;
    //public GameObject inputBoxObject;

    public Text outputTextBox;
    public Slider inputBox;
    public static int energyVictoryCondition;

	// Use this for initialization
	void Start () {
        //outputTextBox = outputTextBoxObject.GetComponent<Text>();
        //inputBox = inputBoxObject.GetComponent<Slider>();

        energyVictoryCondition = (int) (inputBox.value);
        outputTextBox.text = energyVictoryCondition.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        energyVictoryCondition = (int)(inputBox.value);
        outputTextBox.text = energyVictoryCondition.ToString();
    }
}