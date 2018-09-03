using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleView : MonoBehaviour {

    public int counterIndex_lr1, counterIndex_lr2, counterIndex_lr3, counterIndex_lr4, counterIndex_lr5, counterIndex_lr6 = 0;

    public int counterData = 0;

    public LineRenderer lr1, lr2, lr3, lr4, lr5, lr6;

    public List<float> csvfileData = new List<float>();
    public List<int> csvfileLR = new List<int>();
    public List<int> csvfileLR_startingPoints = new List<int>();
    public List<int> csvChange = new List<int>();

    public GameObject EraserBar;

    float shiftX, shiftY, scalingFactor;

    string csvFileName = "csvTest4.csv";

    int lr1_start, lr2_start, lr3_start, lr4_start, lr5_start, lr6_start = 0;

    int intervalInternCounter = 0;

    public GameObject backgroundButton;

    public Canvas buttonCanvas;

    public Shader myShader;

    float multiWidth = 1.0f;
    AnimationCurve curve = new AnimationCurve();


    void ReadLineTest(int line_index, List<string> line)
    {
        csvfileData.Add(float.Parse(line[0]));
        csvfileLR.Add(int.Parse(line[1]));
        csvfileLR_startingPoints.Add(int.Parse(line[2]));
        csvChange.Add(int.Parse(line[3]));
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

                    case 6:
                        drawLinesWithLR(lr6, counterIndex_lr6);

                        counterIndex_lr6++;

                        lr6_start = csvfileLR_startingPoints[counterData];

                        break;
                }


                if (counterData > (lr1_start + 1200))
                {
                    if (lr1.positionCount > 0)
                    {
                        removePointsFromLR(lr1);
                    }
                }
                if (counterData > (lr2_start + 1200))
                {
                    if (lr2.positionCount > 0)
                    {
                        removePointsFromLR(lr2);
                    }
                }
                if (counterData > (lr3_start + 1200))
                {
                    if (lr3.positionCount > 0)
                    {
                        removePointsFromLR(lr3);
                    }
                }
                if (counterData > (lr4_start + 1200))
                {
                    if (lr4.positionCount > 0)
                    {
                        removePointsFromLR(lr4);
                    }
                }
                if (counterData > (lr5_start + 1200))
                {
                    if (lr5.positionCount > 0)
                    {
                        removePointsFromLR(lr5);
                    }
                }
                if (counterData > (lr6_start + 1200))
                {
                    if (lr6.positionCount > 0)
                    {
                        removePointsFromLR(lr6);
                    }
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
        lr1.useWorldSpace = true;
        lr2.useWorldSpace = true;
        lr3.useWorldSpace = true;
        lr4.useWorldSpace = true;
        lr5.useWorldSpace = true;
        lr6.useWorldSpace = true;
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
        if (csvChange[counterData] == 1)
        {
            lr.widthCurve = curve;
            lr.widthMultiplier = multiWidth;
        }

        lr.positionCount++;

        lr.SetPosition(counterIndex_lr, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;
    }
}