using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingleView : MonoBehaviour {

    public int counterIndex_lr1, counterIndex_lr2, counterIndex_lr3, counterIndex_lr4, counterIndex_lr5 = 0;

    public int counterData = 0;

    public LineRenderer lr1, lr2, lr3, lr4, lr5;

    public List<float> csvfileData = new List<float>();
    public List<int> csvfileLR = new List<int>();
    public List<int> csvfileLR_startingPoints = new List<int>();
    public List<int> csvChangeThickness = new List<int>();
    public List<int> csvChangeColor = new List<int>();
    public List<int> csvChangeStart = new List<int>();

    public GameObject EraserBar;

    float shiftX, shiftY, scalingFactor;

    string csvFileName = "csvNormal2.csv";

    int lr1_start, lr2_start, lr3_start, lr4_start, lr5_start = 0;

    int intervalInternCounter = 0;

    public GameObject backgroundButton;

    public Canvas buttonCanvas;

    public Shader myShader;

    public Material redMaterial;

    float multiWidth = 1.0f;
    AnimationCurve curve = new AnimationCurve();

    public LineController myLC;


    void ReadLineTest(int line_index, List<string> line)
    {
        csvfileData.Add(float.Parse(line[0]));
        csvfileLR.Add(int.Parse(line[1]));
        csvfileLR_startingPoints.Add(int.Parse(line[2]));
        csvChangeThickness.Add(int.Parse(line[3]));
        csvChangeColor.Add(int.Parse(line[4]));
        csvChangeStart.Add(int.Parse(line[5]));
    }

    void DrawNewLine2(int data_limit)
    {
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 1.0f);

        if (intervalInternCounter < data_limit)
        {
            intervalInternCounter++;

            //wenn man am Ende der CSV angekommen ist
            if (counterData == csvfileData.Count - 1)
            {
                //setze Datencounter auf 0 --> BEENDEN
                counterData = 0;
                backgroundButton.GetComponent<Buttons>().changeTextWhenClicked();
            }
            else
            {
                switch (csvfileLR[counterData])
                {
                    case 1:
                        drawLinesWithLR(lr1, counterIndex_lr1);

                        counterIndex_lr1++;

                        lr1_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 2:
                        drawLinesWithLR(lr2, counterIndex_lr2);
                        
                        counterIndex_lr2++;

                        lr2_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 3:
                        drawLinesWithLR(lr3, counterIndex_lr3);
                        
                        counterIndex_lr3++;

                        lr3_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 4:
                        drawLinesWithLR(lr4, counterIndex_lr4);

                        counterIndex_lr4++;

                        lr4_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 5:
                        drawLinesWithLR(lr5, counterIndex_lr5);

                        counterIndex_lr5++;

                        lr5_start = csvfileLR_startingPoints[counterData];

                        break;
                }


                checkRemoval(lr1_start, lr1);
                checkRemoval(lr2_start, lr2);
                checkRemoval(lr3_start, lr3);
                checkRemoval(lr4_start, lr4);
                checkRemoval(lr5_start, lr5);

                if (counterData == csvChangeStart[counterData])
                {
                    myLC.setTimeOfChangestart(DateTime.Now);
                }

                counterData++;
            }

        } else
        {
            intervalInternCounter = 0;
        }
    }


    // Use this for initialization
    void Start () {
        fgCSVReader.LoadFromFile(Application.dataPath + "/CSVs/" + csvFileName, new fgCSVReader.ReadLineDelegate(ReadLineTest));
        Debug.Log(csvFileName);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        DrawNewLine2(1200);
    }

    public void removePointsFromLR(LineRenderer lr)
    {
        Vector3[] toBeMovedPos = new Vector3[lr.positionCount];
        lr.GetPositions(toBeMovedPos);

        for (int i = 0; i < lr.positionCount - 1; i++)
        {
            lr.SetPosition(i, toBeMovedPos[i + 1]);
        }
        lr.positionCount--;
    }

    public void initWindow(float _shiftX, float _shiftY, float _scalingFactor)
    {
        shiftX = _shiftX;
        shiftY = _shiftY;
        scalingFactor = _scalingFactor;
    }

    public void changeCsvFilename(string newFileName)
    {
        csvFileName = newFileName;
    }

    public void initButton(float xPos, float yPos, float zPos)
    {
        buttonCanvas.transform.position = new Vector3(xPos, yPos, zPos);
    }

    public void setButtonConnections(GameObject nLB, LineController lineController, bool changed, GameObject endButton)
    {
        Buttons buttonController = backgroundButton.GetComponent<Buttons>();
        buttonController.NextLvlButton = nLB;
        buttonController.myLRC = lineController;
        buttonController.endOfGameButton = endButton;

        if (changed)
        {
            buttonController.changedShownText();
        }
    }

    public void setCameraForCanvas(Camera mainCam)
    {
        buttonCanvas.worldCamera = mainCam;
    }

    public void drawLinesWithLR(LineRenderer lr, int counterIndex_lr)
    {
        if (csvChangeThickness[counterData] == 1)
        {
            lr.widthCurve = curve;
            lr.widthMultiplier = multiWidth;
        }
        if (csvChangeColor[counterData] == 1)
        {
            lr.material = redMaterial;
        }

        lr.positionCount++;

        lr.SetPosition(counterIndex_lr, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.001f) * scalingFactor;
    }

    public void checkRemoval(int lr_start, LineRenderer lr)
    {
        if (counterData > (lr_start + 1200))
        {
            if (lr.positionCount > 0)
            {
                removePointsFromLR(lr);
            }
        }
    }

    public void setLC(LineController lineController)
    {
        myLC = lineController;
    }
}