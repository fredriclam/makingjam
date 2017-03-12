using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour {

    private float initializeTime;
    public Image screen;
    private float percent;

    // Use this for initialization
    void Start () {
        initializeTime = Time.time;
        percent = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time - initializeTime > 7.2f && percent <= 100) {
            percent += 0.007f;
            screen.color = Color.Lerp(Color.clear, Color.black, percent);
        }
    }
}
