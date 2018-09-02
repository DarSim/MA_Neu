using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LineController : MonoBehaviour {

    [Range(0f, 5f)]
    public float TimeScaleFactor = 1f;

    public Transform ViewParent;

    public float scaling = 1.5f;

    public GameObject SubViewPrefab;
    public List<SingleView> singleViewList;

    public GameObject nextLvlButton;

    public int dataCounter;

    public DateTime startOfLvl;

    public int whichLineToChange, whichNormalCSV = 1;

    public Camera mainCam;


    // Use this for initialization
    void Start () {
        startThoseLines(1);
        
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = TimeScaleFactor;
    }

    public void startThoseLines(int lvlCounter)
    {
        clearTheStage();

        // gibt random int zw. 1 und 4 zurück
        // ermittelt, welche der 4 Linien die Veränderung anzeigen soll
        whichLineToChange = UnityEngine.Random.Range(1, 5);
        
        // ermittelt, welche der 4 Grundlinien für dieses Level benutzt wird
        whichNormalCSV = UnityEngine.Random.Range(1, 5);

        // instanziiere die Prefabs
        if (whichLineToChange == 1)
        {
            instanciateLines(-13f, 2f, scaling, true, whichNormalCSV, lvlCounter, -120, 27, -6);
        } else
        {
            instanciateLines(-13f, 2f, scaling, false, whichNormalCSV, lvlCounter, -120, 27, -6);
        }

        if (whichLineToChange == 2)
        {
            instanciateLines(-13f, -4f, scaling, true, whichNormalCSV, lvlCounter, -120, -40, -6);
        } else
        {
            instanciateLines(-13f, -4f, scaling, false, whichNormalCSV, lvlCounter, -120, -40, -6);
        }

        if (whichLineToChange == 3)
        {
            instanciateLines(1f, 2f, scaling, true, whichNormalCSV, lvlCounter, 5, 27, -6);
        } else
        {
            instanciateLines(1f, 2f, scaling, false, whichNormalCSV, lvlCounter, 5, 27, -6);
        }

        if (whichLineToChange == 4)
        {
            instanciateLines(1f, -4f, scaling, true, whichNormalCSV, lvlCounter, 5, -40, -6);
        } else
        {
            instanciateLines(1f, -4f, scaling, false, whichNormalCSV, lvlCounter, 5, -40, -6);
        }

        startOfLvl = DateTime.Now;
    }



    public void calculateTimeNeeded(DateTime endOfLvl)
    {
        long elapsedTicks = endOfLvl.Ticks - startOfLvl.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

        Debug.Log(elapsedSpan.TotalSeconds);

        string path = Application.dataPath + "/testFile.txt";
        string toBeSaved = elapsedSpan.TotalSeconds.ToString() + Environment.NewLine;
        File.AppendAllText(path, toBeSaved);
    }



    public void instanciateLines(float xCoord, float yCoord, float scaling, bool change, int whichNormalLine, int lvlCounter, float buttonPosX, float buttonPosY, float buttonPosZ)
    {
        GameObject subViewGO = Instantiate(SubViewPrefab) as GameObject;
        SingleView subViewController = subViewGO.GetComponent<SingleView>();
        if (change)
        {
            subViewController.changeCsvFilename("Changed/" + whichNormalLine + "/csvChange" + lvlCounter + ".csv");
            //TODO: hier methode aufrufen für button text = richtig
        } else
        {
            subViewController.changeCsvFilename("Normal/csvNormal" + whichNormalLine + ".csv");
            //TODO: hier methode aufrufen für button text = falsch
        }
        subViewController.initWindow(xCoord, yCoord, scaling);
        subViewController.setCameraForCanvas(mainCam);
        subViewController.initButton(buttonPosX, buttonPosY, buttonPosZ);
        subViewController.setButtonConnections(nextLvlButton, this);
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
}
