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




    void ReadLineTest(int line_index, List<string> line)
    {
        csvfileData.Add(float.Parse(line[0]));
        csvfileLR.Add(int.Parse(line[1]));
        csvfileLR_startingPoints.Add(int.Parse(line[2]));
        csvChange.Add(int.Parse(line[3]));
    }

    void DrawNewLine2(int data_limit)
    {
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 1.0f);
        float multiWidth = 1.0f;

        if (intervalInternCounter < data_limit)
        {
            intervalInternCounter++;

            //wenn man am Ende der CSV angekommen ist
            if (counterData == csvfileData.Count - 1)
            {
                //setze Datencounter auf 0 --> BEENDEN
                counterData = 0;
            }
            else
            {
                switch (csvfileLR[counterData])
                {
                    case 1:
                        if (csvChange[counterData] == 1)
                        {
                            lr1.widthCurve = curve;
                            lr1.widthMultiplier = multiWidth;
                        }

                        lr1.positionCount++;

                        lr1.SetPosition(counterIndex_lr1, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                        //erhöhe Interval-Counter von LR1
                        counterIndex_lr1++;

                        lr1_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 2:
                        if (csvChange[counterData] == 1)
                        {
                            lr2.widthCurve = curve;
                            lr2.widthMultiplier = multiWidth;
                        }

                        lr2.positionCount++;

                        lr2.SetPosition(counterIndex_lr2, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                        //erhöhe Interval-Counter von LR1
                        counterIndex_lr2++;

                        lr2_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 3:
                        if (csvChange[counterData] == 1)
                        {
                            lr3.widthCurve = curve;
                            lr3.widthMultiplier = multiWidth;
                        }

                        lr3.positionCount++;

                        lr3.SetPosition(counterIndex_lr3, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                        //erhöhe Interval-Counter von LR1
                        counterIndex_lr3++;

                        lr3_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 4:
                        if (csvChange[counterData] == 1)
                        {
                            lr4.widthCurve = curve;
                            lr4.widthMultiplier = multiWidth;
                        }

                        lr4.positionCount++;

                        lr4.SetPosition(counterIndex_lr4, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                        //erhöhe Interval-Counter von LR1
                        counterIndex_lr4++;

                        lr4_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 5:
                        if (csvChange[counterData] == 1)
                        {
                            lr5.widthCurve = curve;
                            lr5.widthMultiplier = multiWidth;
                        }

                        lr5.positionCount++;

                        lr5.SetPosition(counterIndex_lr5, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                        //erhöhe Interval-Counter von LR1
                        counterIndex_lr5++;

                        lr5_start = csvfileLR_startingPoints[counterData];

                        break;

                    case 6:
                        if (csvChange[counterData] == 1)
                        {
                            lr6.widthCurve = curve;
                            lr6.widthMultiplier = multiWidth;
                        }

                        lr6.positionCount++;

                        lr6.SetPosition(counterIndex_lr6, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                        //erhöhe Interval-Counter von LR1
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

    void DrawLine(List<float> data, int data_limit)
    {
        //wenn man am Ende der CSV angekommen ist
        if (counterData == csvfileData.Count - 1)
        {
            //setze Datencounter auf 0 --> BEENDEN
            counterData = 0;

        }
        // solange noch innerhalb eines Intervals
        else if ((counterIndex_lr1 < data_limit) && ((counterIndex_lr1 + counterIndex_lr2) < data_limit) && (counterIndex_lr3 < data_limit))
        {
            //wenn im csv noch 1 für Normal drin steht
            if (csvfileLR[counterData] == 1)
            {
                //setze positionCount höher solange noch innerhalb eines Intervals
                if (lr1.positionCount <= counterIndex_lr1)
                {
                    lr1.positionCount++;
                }
                lr1.SetPosition(counterIndex_lr1, new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                EraserBar.transform.position = new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                //erhöhe Interval-Counter von LR1
                counterIndex_lr1++;
            }
            //wenn im csv 2 für Veränderung drin steht
            if (csvfileLR[counterData] == 2)
            {
                if (counterData < 1200)
                {
                    //setze positionCount höher solange noch innerhalb eines Intervals
                    if (lr2.positionCount <= counterIndex_lr2)
                    {
                        lr2.positionCount++;
                    }
                    lr2.SetPosition(counterIndex_lr2, new Vector3((counterIndex_lr1 + counterIndex_lr2) * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3((counterIndex_lr1 + counterIndex_lr2) * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR2
                    counterIndex_lr2++;
                } else
                {
                    if (lr3.positionCount <= counterIndex_lr3)
                    {
                        lr3.positionCount++;
                    }
                    lr3.SetPosition(counterIndex_lr3, new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR2
                    counterIndex_lr3++;
                }
                

                //sobald nächstes Interval beginnt entferne kontinuierlich die Punkte von LR1
                if (counterData > 1200)
                {
                    if(lr1.positionCount > 0)
                    {
                        removePointsFromLR(lr1);
                    }
                    
                }
                if (counterData > 1500)
                {
                    if (lr2.positionCount > 0)
                    {
                        removePointsFromLR(lr2);
                    }
                }

                /* TODO:
                 * - allgemeiner machen (Veränderung kann auch im 2. Interval erst geschehen, nicht auf counterData 
                 * zurückgreifen sondern Boolean oder nochmal counter einbauen)
                 * - counter für wann fettigkeit einsetzt, dass man ursprüngliches posCount von lr1 hat, damit removePoints 
                 * bei counterData+posCount oder 2. Boolean
                 */
                
            }

            counterData++;
        } else
        {
            counterIndex_lr1 = 0;
            counterIndex_lr2 = 0;
            counterIndex_lr3 = 0;
        }
        
    }

    IEnumerator RemovePointsFromL1()
    {
        if (lr1.positionCount != 0)
        {
            removePointsFromLR(lr1);
        }
        yield return null;
    }

    void DrawNewLine1()
    {
        //wenn man am Ende der CSV angekommen ist
        if (counterData == csvfileData.Count - 1)
        {
            //setze Datencounter auf 0 --> BEENDEN
            counterData = 0;

            /* TODO: neue Methode: LevelLost: stoppe dieses level, rufe nextlvl button mit lvl lost schrift auf
             */
        } else
        {
            switch (csvfileLR[counterData])
            {
                case 1:
                    lr1.positionCount++;

                    lr1.SetPosition(counterIndex_lr1, new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR1
                    counterIndex_lr1++;

                    lr1_start = csvfileLR_startingPoints[counterData];

                    break;

                case 2:
                    lr2.positionCount++;

                    lr2.SetPosition(counterIndex_lr2, new Vector3(counterIndex_lr2 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr2 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR1
                    counterIndex_lr2++;

                    lr2_start = csvfileLR_startingPoints[counterData];

                    break;

                case 3:
                    lr3.positionCount++;

                    lr3.SetPosition(counterIndex_lr3, new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR1
                    counterIndex_lr3++;

                    lr3_start = csvfileLR_startingPoints[counterData];

                    break;

                case 4:
                    lr4.positionCount++;

                    lr4.SetPosition(counterIndex_lr4, new Vector3(counterIndex_lr4 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr4 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR1
                    counterIndex_lr4++;

                    lr4_start = csvfileLR_startingPoints[counterData];

                    break;

                case 5:
                    lr5.positionCount++;

                    lr5.SetPosition(counterIndex_lr5, new Vector3(counterIndex_lr5 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr5 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR1
                    counterIndex_lr5++;

                    lr5_start = csvfileLR_startingPoints[counterData];

                    break;

                case 6:
                    lr6.positionCount++;

                    lr6.SetPosition(counterIndex_lr6, new Vector3(counterIndex_lr6 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
                    EraserBar.transform.position = new Vector3(counterIndex_lr6 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

                    //erhöhe Interval-Counter von LR1
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

       

        //if (csvfileLR[counterData] == 1)
        //{
        //    lr1.positionCount++;
            
        //    lr1.SetPosition(counterIndex_lr1, new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        //    EraserBar.transform.position = new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

        //    //erhöhe Interval-Counter von LR1
        //    counterIndex_lr1++;
        //    counterIndex_lr2 = counterIndex_lr1;
        //    counterIndex_lr3 = counterIndex_lr1;
        //    counterIndex_lr4 = counterIndex_lr1;
        //    counterIndex_lr5 = counterIndex_lr1;
        //    counterIndex_lr6 = counterIndex_lr1;
        //}
        //if (csvfileLR[counterData] == 2)
        //{
        //    lr2.positionCount++;

        //    lr2.SetPosition(counterIndex_lr2, new Vector3(counterIndex_lr2 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        //    EraserBar.transform.position = new Vector3(counterIndex_lr2 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

        //    //erhöhe Interval-Counter von LR1
        //    counterIndex_lr2++;
        //    counterIndex_lr3 = counterIndex_lr2;
        //    counterIndex_lr4 = counterIndex_lr2;
        //    counterIndex_lr5 = counterIndex_lr2;
        //    counterIndex_lr6 = counterIndex_lr2;
        //}
        //if (csvfileLR[counterData] == 3)
        //{
        //    lr3.positionCount++;

        //    lr3.SetPosition(counterIndex_lr3, new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        //    EraserBar.transform.position = new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

        //    //erhöhe Interval-Counter von LR1
        //    counterIndex_lr3++;
        //    counterIndex_lr4 = counterIndex_lr3;
        //    counterIndex_lr5 = counterIndex_lr3;
        //    counterIndex_lr6 = counterIndex_lr3;
        //}
        //if (csvfileLR[counterData] == 4)
        //{
        //    lr4.positionCount++;

        //    lr4.SetPosition(counterIndex_lr4, new Vector3(counterIndex_lr4 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        //    EraserBar.transform.position = new Vector3(counterIndex_lr4 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

        //    //erhöhe Interval-Counter von LR1
        //    counterIndex_lr4++;
        //    counterIndex_lr5 = counterIndex_lr4;
        //    counterIndex_lr6 = counterIndex_lr4;
        //}
        //if (csvfileLR[counterData] == 5)
        //{
        //    lr5.positionCount++;

        //    lr5.SetPosition(counterIndex_lr5, new Vector3(counterIndex_lr5 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        //    EraserBar.transform.position = new Vector3(counterIndex_lr5 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

        //    //erhöhe Interval-Counter von LR1
        //    counterIndex_lr5++;
        //    counterIndex_lr6 = counterIndex_lr5;
        //}
        //if (csvfileLR[counterData] == 6)
        //{
        //    lr6.positionCount++;

        //    lr6.SetPosition(counterIndex_lr6, new Vector3(counterIndex_lr6 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        //    EraserBar.transform.position = new Vector3(counterIndex_lr6 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

        //    //erhöhe Interval-Counter von LR1
        //    counterIndex_lr6++;
        //}




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
        //DrawLine(csvfileData, 1200);
        //DrawNewLine();
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
}
