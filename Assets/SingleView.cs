using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleView : MonoBehaviour {

    public int counterIndex_lr1, counterIndex_lr2, counterIndex_lr3, counterIndex_lr4, counterIndex_lr5, counterIndex_lr6 = 0;

    public int counterData = 0;

    public LineRenderer lr1, lr2, lr3, lr4, lr5, lr6;

    public List<float> csvfileData = new List<float>();

    public List<float> csvfileLR = new List<float>();

    public GameObject EraserBar;

    float shiftX, shiftY, scalingFactor;

    string csvFileName = "csvTest.csv";




    void ReadLineTest(int line_index, List<string> line)
    {
        csvfileData.Add(float.Parse(line[0]));
        csvfileLR.Add(float.Parse(line[1]));
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

    void DrawNewLine()
    {
        //wenn man am Ende der CSV angekommen ist
        if (counterData == csvfileData.Count - 1)
        {
            //setze Datencounter auf 0 --> BEENDEN
            counterData = 0;

            /* TODO: neue Methode: LevelLost: stoppe dieses level, rufe nextlvl button mit lvl lost schrift auf
             */
        }

        //switch (csvfileLR[counterData])
        {

        }

        if (csvfileLR[counterData] == 1)
        {
            lr1.positionCount++;
            
            lr1.SetPosition(counterIndex_lr1, new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
            EraserBar.transform.position = new Vector3(counterIndex_lr1 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

            //erhöhe Interval-Counter von LR1
            counterIndex_lr1++;
        }
        if (csvfileLR[counterData] == 2)
        {
            lr2.positionCount++;

            lr2.SetPosition(counterIndex_lr2, new Vector3(counterIndex_lr2 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
            EraserBar.transform.position = new Vector3(counterIndex_lr2 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

            //erhöhe Interval-Counter von LR1
            counterIndex_lr2++;
        }
        if (csvfileLR[counterData] == 3)
        {
            lr3.positionCount++;

            lr3.SetPosition(counterIndex_lr3, new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
            EraserBar.transform.position = new Vector3(counterIndex_lr3 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

            //erhöhe Interval-Counter von LR1
            counterIndex_lr3++;
        }
        if (csvfileLR[counterData] == 4)
        {
            lr4.positionCount++;

            lr4.SetPosition(counterIndex_lr4, new Vector3(counterIndex_lr4 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
            EraserBar.transform.position = new Vector3(counterIndex_lr4 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

            //erhöhe Interval-Counter von LR1
            counterIndex_lr4++;
        }
        if (csvfileLR[counterData] == 5)
        {
            lr5.positionCount++;

            lr5.SetPosition(counterIndex_lr5, new Vector3(counterIndex_lr5 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
            EraserBar.transform.position = new Vector3(counterIndex_lr5 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

            //erhöhe Interval-Counter von LR1
            counterIndex_lr5++;
        }
        if (csvfileLR[counterData] == 6)
        {
            lr6.positionCount++;

            lr6.SetPosition(counterIndex_lr6, new Vector3(counterIndex_lr6 * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
            EraserBar.transform.position = new Vector3(counterIndex_lr6 * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.5f) * scalingFactor;

            //erhöhe Interval-Counter von LR1
            counterIndex_lr6++;
        }


        if (counterData > counterIndex_lr1)
        {
            if (lr1.positionCount > 0)
            {
                removePointsFromLR(lr1);
            }
        }
        if (counterData > counterIndex_lr2)
        {
            if (lr2.positionCount > 0)
            {
                removePointsFromLR(lr2);
            }
        }
        if (counterData > counterIndex_lr3)
        {
            if (lr3.positionCount > 0)
            {
                removePointsFromLR(lr3);
            }
        }
        if (counterData > counterIndex_lr4)
        {
            if (lr4.positionCount > 0)
            {
                removePointsFromLR(lr4);
            }
        }
        if (counterData > counterIndex_lr5)
        {
            if (lr5.positionCount > 0)
            {
                removePointsFromLR(lr5);
            }
        }
        if (counterData > counterIndex_lr6)
        {
            if (lr6.positionCount > 0)
            {
                removePointsFromLR(lr6);
            }
        }

    }


    // Use this for initialization
    void Start () {
        fgCSVReader.LoadFromFile(Application.dataPath + "/" + csvFileName, new fgCSVReader.ReadLineDelegate(ReadLineTest));
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
        DrawLine(csvfileData, 1200);
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
