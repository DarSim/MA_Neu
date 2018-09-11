using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    Text t;
    public GameObject NextLvlButton;

    public SingleView[] singleViews;

    public LineController myLRC;

    public bool changedLine;

    string buttonText = "falsch";

    public GameObject endOfGameButton;

    public GameObject exportResultsButton;

	// Use this for initialization
	void Start () {
        t = GetComponentInChildren<Text>();
        t.text = " ";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeTextWhenClicked()
    {
        t.text = buttonText;
        NextLvlButton nLBController = NextLvlButton.GetComponent<NextLvlButton>();
        Debug.Log("lvlCounter in Button: " + nLBController.lvlCounter);
        myLRC.calculateTimeNeeded(System.DateTime.Now, nLBController.lvlCounter, buttonText);
        if (nLBController.lvlCounter < 35)
        {
            NextLvlButton.SetActive(true);
            Debug.Log("Cool Stuff");
            myLRC.TimeScaleFactor = 0;

        } else
        {
            myLRC.clearTheStage();
            endOfGameButton.SetActive(true);
            exportResultsButton.SetActive(true);

        }
    }

    public void changedShownText()
    {
        buttonText = "richtig";
    }
}
