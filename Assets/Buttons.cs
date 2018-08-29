using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    Text t;
    public GameObject NextLvlButton;

    public SingleView[] singleViews;

    public LineController myLRC;

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
        t.text = "Clicked";
        StartCoroutine(NextLvl());
        //myLRC.TimeScaleFactor = 0;
        //Time.timeScale = 0;
        //singleViews = gameObject.GetComponentsInParent<SingleView>();
        myLRC.calculateTimeNeeded(System.DateTime.Now);
    }

    IEnumerator NextLvl()
    {
        yield return new WaitForSeconds(1);
        NextLvlButton.SetActive(true);
        Debug.Log("Cool Stuff");
        myLRC.TimeScaleFactor = 0;
    }
}
