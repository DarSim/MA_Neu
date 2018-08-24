using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLvlButton : MonoBehaviour {

    public int lvlCounter;

	// Use this for initialization
	void Start () {
        lvlCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextLvlClicked()
    {
        lvlCounter++;
    }
}
