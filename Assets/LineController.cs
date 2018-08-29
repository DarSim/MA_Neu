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
    public GameObject[] sv_go_list;

    public GameObject nextLvlButton;

    public int dataCounter;

    public DateTime startOfLvl;

    public int whichLineToChange, whichNormalCSV = 1;


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
            instanciateLines(-13f, 2f, scaling, true, whichNormalCSV, lvlCounter);
        } else
        {
            instanciateLines(-13f, 2f, scaling, false, whichNormalCSV, lvlCounter);
        }

        if (whichLineToChange == 2)
        {
            instanciateLines(-13f, -4f, scaling, true, whichNormalCSV, lvlCounter);
        } else
        {
            instanciateLines(-13f, -4f, scaling, false, whichNormalCSV, lvlCounter);
        }

        if (whichLineToChange == 3)
        {
            instanciateLines(1f, 2f, scaling, true, whichNormalCSV, lvlCounter);
        } else
        {
            instanciateLines(1f, 2f, scaling, false, whichNormalCSV, lvlCounter);
        }

        if (whichLineToChange == 4)
        {
            instanciateLines(1f, -4f, scaling, true, whichNormalCSV, lvlCounter);
        } else
        {
            instanciateLines(1f, -4f, scaling, false, whichNormalCSV, lvlCounter);
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



    public void instanciateLines(float xCoord, float yCoord, float scaling, bool change, int whichNormalLine, int lvlCounter)
    {
        GameObject subViewGO = Instantiate(SubViewPrefab) as GameObject;
        SingleView subViewController = subViewGO.GetComponent<SingleView>();
        if (change)
        {
            subViewController.changeCsvFilename("Changed/" + whichNormalLine + "/csvChange" + lvlCounter + ".csv");
        } else
        {
            subViewController.changeCsvFilename("Normal/csvNormal" + whichNormalLine + ".csv");
        }
        subViewController.initWindow(xCoord, yCoord, scaling);
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
