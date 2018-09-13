using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLvlButton : MonoBehaviour {

    public int lvlCounter = 0;
    public LineController myLineController;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextLvlClicked()
    {
        lvlCounter++;
        Debug.Log("lvlcounter: " + lvlCounter);
        // sobald man auf next level klickt geht die zeit wieder weiter
        myLineController.TimeScaleFactor = 2;
        // nächstes level wird gestartet
        myLineController.startThoseLines(lvlCounter);
        // dieser button wird wieder deaktiviert
        this.gameObject.SetActive(false);

        
    }
}
