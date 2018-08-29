using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLvlButton : MonoBehaviour {

    public int lvlCounter;
    public LineController myLineController;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        lvlCounter = 1;
    }

    public void nextLvlClicked()
    {
        Debug.Log("nextlvl clicked");
        lvlCounter++;
        Debug.Log("lvlcounter: " + lvlCounter);
        myLineController.TimeScaleFactor = 1;
        myLineController.startThoseLines(lvlCounter);
        this.gameObject.SetActive(false);
    }
}
