using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkArrow : MonoBehaviour {
    public GameObject arrow;
    private float blinkDelay = 0.2f;
	float pastEnergy;
	// Use this for initialization
	void Start () {
        arrow.SetActive(false);
        StartCoroutine(Blink());
    }
	
	// Update is called once per frame
	void Update () {
		if (Player.energyLevel > pastEnergy) {
			StartCoroutine (Blink ());
		}
		pastEnergy = Player.energyLevel;
	}

     IEnumerator Blink() {
        //Renderer renderer = arrow.GetComponent<Renderer>();
        //Debug.Log("P1");
        arrow.SetActive(true);
        yield return new WaitForSeconds(blinkDelay);
        arrow.SetActive(false);
        yield return new WaitForSeconds(blinkDelay);
        arrow.SetActive(true);
        yield return new WaitForSeconds(blinkDelay);
        arrow.SetActive(false);
        yield return new WaitForSeconds(blinkDelay);
        arrow.SetActive(true);
        yield return new WaitForSeconds(blinkDelay);
        arrow.SetActive(false);
        //Debug.Log("P2");        
        
    }
}
