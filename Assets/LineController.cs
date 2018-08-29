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
        foreach (SingleView sv in singleViewList)
        {
            Destroy(sv.gameObject);
        }
        singleViewList.Clear();
        singleViewList = new List<SingleView>();

        // gibt random int zw. 1 und 4 zurück
        whichLineToChange = UnityEngine.Random.Range(1, 5);
        // random int zw. 1 und 4
        whichNormalCSV = UnityEngine.Random.Range(1, 5);

        GameObject sc1 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon1 = sc1.GetComponent<SingleView>();
        if (whichLineToChange == 1)
        {
            scCon1.changeCsvFilename("Changed/" + whichNormalCSV + "/csvChange" + lvlCounter + ".csv");
        }
        else
        {
            scCon1.changeCsvFilename("Normal/csvNormal" + whichNormalCSV + ".csv");
        }
        scCon1.initWindow(-13f, 2f, scaling);
        singleViewList.Add(scCon1);
        sc1.transform.parent = ViewParent;
        Debug.Log("dumm");

        GameObject sc2 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon2 = sc2.GetComponent<SingleView>();
        if (whichLineToChange == 2)
        {
            scCon2.changeCsvFilename("Changed/" + whichNormalCSV + "/csvChange" + lvlCounter + ".csv");
        }
        else
        {
            scCon2.changeCsvFilename("Normal/csvNormal" + whichNormalCSV + ".csv");
        }
        scCon2.initWindow(-13f, -4f, scaling);
        singleViewList.Add(scCon2);
        sc2.transform.parent = ViewParent;

        GameObject sc3 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon3 = sc3.GetComponent<SingleView>();
        if (whichLineToChange == 3)
        {
            scCon3.changeCsvFilename("Changed/" + whichNormalCSV + "/csvChange" + lvlCounter + ".csv");
        }
        else
        {
            scCon3.changeCsvFilename("Normal/csvNormal" + whichNormalCSV + ".csv");
        }
        scCon3.initWindow(1f, 2f, scaling);
        singleViewList.Add(scCon3);
        sc3.transform.parent = ViewParent;

        GameObject sc4 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon4 = sc4.GetComponent<SingleView>();
        if (whichLineToChange == 4)
        {
            scCon4.changeCsvFilename("Changed/" + whichNormalCSV + "/csvChange" + lvlCounter + ".csv");
        }
        else
        {
            scCon4.changeCsvFilename("Normal/csvNormal" + whichNormalCSV + ".csv");
        }
        scCon4.initWindow(1f, -4f, scaling);
        singleViewList.Add(scCon4);
        sc4.transform.parent = ViewParent;

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
}
