using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LineController : MonoBehaviour {

    [Range(0f, 5f)]
    public float TimeScaleFactor = 2f;

    public Transform ViewParent;

    public float scaling = 1.5f;

    public GameObject SubViewPrefab;
    public List<SingleView> singleViewList;

    public GameObject nextLvlButton;

    public int dataCounter;

    public DateTime startOfLvl;

    public int whichLineToChange = 1;

    public Camera mainCam;

    public GameObject endButton;

    public GameObject exportButton;

    public DateTime startOfChange = DateTime.MinValue;

    int[] orderOfCSVs = new int[] {13, 27, 8, 12, 5, 2, 15, 10, 29, 32, 6, 21, 16, 31, 7, 4, 28, 20, 24,
                                   30, 26, 25, 18, 14, 0, 35, 22, 1, 3, 17, 23, 34, 33, 19, 9, 11};

    bool tooEarly = true;


    // Use this for initialization
    void Start () {
        startThoseLines(0);
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = TimeScaleFactor;
    }

    public void startThoseLines(int lvlCounter)
    {
        // zerstöre die alten Prefabs
        clearTheStage();

        // ermittelt, welche der 4 Linien die Veränderung anzeigen soll
        whichLineToChange = UnityEngine.Random.Range(1, 5);

        // instanziiere die Prefabs
        if (whichLineToChange == 1)
        {
            instanciateLines(-13f, 2f, scaling, true, lvlCounter, -120, 34, -1f);
        } else
        {
            instanciateLines(-13f, 2f, scaling, false, lvlCounter, -120, 34, -1f);
        }

        if (whichLineToChange == 2)
        {
            instanciateLines(-13f, -4f, scaling, true, lvlCounter, -120, -32, -1f);
        } else
        {
            instanciateLines(-13f, -4f, scaling, false, lvlCounter, -120, -32, -1f);
        }

        if (whichLineToChange == 3)
        {
            instanciateLines(1f, 2f, scaling, true, lvlCounter, 5, 34, -1f);
        } else
        {
            instanciateLines(1f, 2f, scaling, false, lvlCounter, 5, 34, -1f);
        }

        if (whichLineToChange == 4)
        {
            instanciateLines(1f, -4f, scaling, true, lvlCounter, 5, -32, -1f);
        } else
        {
            instanciateLines(1f, -4f, scaling, false, lvlCounter, 5, -32, -1f);
        }

        startOfLvl = DateTime.Now;
    }

    // wird vom Buttons-Skript aufgerufen und berechnet die Zeit die vergangen ist zwischen dem Start des Levels und dem Klicken auf eine der Linien
    // Abgespeicherte Zeile sieht z.b. folgendermaßen aus:
    // 9/3/2018 5:06:02 PM, Level: 0, time needed: 1.54, Button clicked: richtig
    public void calculateTimeNeeded(DateTime endOfLvl, int lvlCounter, string buttonText)
    {
        long elapsedTicks = endOfLvl.Ticks - startOfLvl.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

        long elapsedTicksChange = endOfLvl.Ticks - startOfChange.Ticks;
        TimeSpan elapsedSpanChange = new TimeSpan(elapsedTicksChange);

        Debug.Log("gesamtes level: " + elapsedSpan.TotalSeconds + ", änderung: " + elapsedSpanChange.TotalSeconds);

        string path = Application.dataPath + "/testFile.txt";
        string toBeSaved = DateTime.Now + ", Level: " + lvlCounter + ", start of level until click[s]: " + elapsedSpan.TotalSeconds.ToString("F2") + ", too early: " + tooEarly 
            + ", start of change until click[s]: " + elapsedSpanChange.TotalSeconds.ToString("F2") + ", Changed Linie: " + whichLineToChange + ", Button clicked: " + buttonText + Environment.NewLine;
        File.AppendAllText(path, toBeSaved);
    }

    public void instanciateLines(float xCoord, float yCoord, float scaling, bool change, int lvlCounter, float buttonPosX, float buttonPosY, float buttonPosZ)
    {
        GameObject subViewGO = Instantiate(SubViewPrefab) as GameObject;
        SingleView subViewController = subViewGO.GetComponent<SingleView>();
        if (change)
        {
            //subViewController.changeCsvFilename("Changed/" + whichNormalLine + "/csvChange" + lvlCounter + ".csv");
            subViewController.changeCsvFilename("change/newAll/csvChange" + orderOfCSVs[lvlCounter] + ".csv");
        } else
        {
            subViewController.changeCsvFilename("csvNormal2.csv");
        }
        subViewController.initWindow(xCoord, yCoord, scaling);
        subViewController.setCameraForCanvas(mainCam);
        subViewController.initButton(buttonPosX, buttonPosY, buttonPosZ);
        subViewController.setLC(this);
        subViewController.setButtonConnections(nextLvlButton, change, endButton, exportButton);
        singleViewList.Add(subViewController);
        subViewController.transform.parent = ViewParent;
    }

    public void clearTheStage()
    {
        foreach (SingleView sv in singleViewList)
        {
            Destroy(sv.gameObject);
        }
        singleViewList.Clear();
        singleViewList = new List<SingleView>();
    }

    public void setTimeOfChangestart(DateTime changestart)
    {
        startOfChange = changestart;
    }

    public void setClickedTooEarly(bool _tooEarly)
    {
        tooEarly = _tooEarly;
    }
}
