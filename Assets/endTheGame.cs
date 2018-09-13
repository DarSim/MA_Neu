using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endTheGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Beendet die App
    public void endGameWhenClicked()
    {
        Application.Quit();
    }
}
