﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    Text t;
    public GameObject NextLvlButton;

    public Component[] singleViews;

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

        singleViews = (SingleView[])gameObject.GetComponentsInParent(typeof(SingleView));
    }

    IEnumerator NextLvl()
    {
        yield return new WaitForSeconds(2);
        NextLvlButton.SetActive(true);
    }
}
