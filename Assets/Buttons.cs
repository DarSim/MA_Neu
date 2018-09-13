using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    Text t;

    public GameObject NextLvlButton;
    public GameObject endOfGameButton;

    public LineController myLRC;

    string buttonText = "falsch";


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
        NextLvlButton nLBController = NextLvlButton.GetComponent<NextLvlButton>(); 
        myLRC.calculateTimeNeeded(System.DateTime.Now, nLBController.lvlCounter, buttonText); 
        t.text = buttonText;

        if (nLBController.lvlCounter < 35)
        {
            // aktiviere NextLevel Button und halte Zeit an im Spiel
            NextLvlButton.SetActive(true);
            myLRC.TimeScaleFactor = 0;

        } else
        {
            // am Ende des Spiels: lösche alle Prefabs und zeige Ende Text (ist Button) an
            myLRC.clearTheStage();
            endOfGameButton.SetActive(true);

        }
    }

    // falls es der Button der geänderten Linie ist
    public void changedShownText()
    {
        buttonText = "richtig";
    }
}
