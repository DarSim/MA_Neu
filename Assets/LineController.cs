using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour {

    [Range(0f, 5f)]
    public float TimeScaleFactor = 1f;

    public Transform ViewParent;

    public float scaling = 1.5f;

    public GameObject SubViewPrefab;
    List<SingleView> singleViewList;

    public GameObject nextLvlButton;

    public int dataCounter;
    

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
        singleViewList = new List<SingleView>();

        GameObject sc1 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon1 = sc1.GetComponent<SingleView>();
        scCon1.initWindow(-13f, 2f, scaling);
        singleViewList.Add(scCon1);
        sc1.transform.parent = ViewParent;

        GameObject sc2 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon2 = sc2.GetComponent<SingleView>();
        scCon2.initWindow(-13f, -4f, scaling);
        singleViewList.Add(scCon2);
        sc2.transform.parent = ViewParent;

        GameObject sc3 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon3 = sc3.GetComponent<SingleView>();
        scCon3.initWindow(1f, 2f, scaling);
        singleViewList.Add(scCon3);
        sc3.transform.parent = ViewParent;

        GameObject sc4 = Instantiate(SubViewPrefab) as GameObject;
        SingleView scCon4 = sc4.GetComponent<SingleView>();
        scCon4.initWindow(1f, -4f, scaling);
        singleViewList.Add(scCon4);
        sc4.transform.parent = ViewParent;
        
    }
}
