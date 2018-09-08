﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLvlButton : MonoBehaviour {

    public int lvlCounter = 1;
    public LineController myLineController;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {

    }

    public void nextLvlClicked()
    {
        Debug.Log("nextlvl clicked");
        lvlCounter++;
        Debug.Log("lvlcounter: " + lvlCounter);
        myLineController.TimeScaleFactor = 2;
        myLineController.startThoseLines(lvlCounter);
        this.gameObject.SetActive(false);

        
    }
}
