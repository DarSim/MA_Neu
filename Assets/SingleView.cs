using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingleView : MonoBehaviour {

    // FUNNY FAKT, den ich herausfinden musste: sobald eine Variable public deklariert wird
    // ist der Initialwert, welchen man ihr im Skript gibt egal, da dieser Wert durch die
    // eingabe im Inspektor überschrieben wird @ __ @

    // ein paar counter
    public int counterIndex_lr1, counterIndex_lr2, counterIndex_lr3, counterIndex_lr4, counterIndex_lr5 = 0;
    public int counterData = 0;

    int lr1_start, lr2_start, lr3_start, lr4_start, lr5_start = 0;
    int intervalInternCounter = 0;

    float shiftX, shiftY, scalingFactor;
    // multiplikationsfaktor für die breite eines linerenderers
    float multiWidth = 1.0f;

    // meine 10000 listen für jede spalte aus den csv dateien
    // Spalten: x-koordinate / Linerenderer-Nr / startpunkt des LR / ist Linie dick? / ist linie farbig / start der änderung
    public List<float> csvfileData = new List<float>();
    public List<int> csvfileLR = new List<int>();
    public List<int> csvfileLR_startingPoints = new List<int>();
    public List<int> csvChangeThickness = new List<int>();
    public List<int> csvChangeColor = new List<int>();
    public List<int> csvChangeStart = new List<int>();

    public GameObject EraserBar;
    public GameObject backgroundButton;

    public LineRenderer lr1, lr2, lr3, lr4, lr5;

    string csvFileName = "csvNormal2.csv";

    public Canvas buttonCanvas;

    public Material redMaterial;

    // seit neuestem muss die Dicke eines LR mithilfe einer Animationskurve geändert werden
    AnimationCurve curve = new AnimationCurve();

    public LineController myLC;

    public bool changedLine = false;

    // lies alle spalten der csv datei
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
        if (intervalInternCounter < data_limit)
        {
            intervalInternCounter++;

            //wenn man am Ende der CSV angekommen ist
            if (counterData == csvfileData.Count - 1)
            {
                //setze Datencounter auf 0 --> BEENDEN
                counterData = 0;
                // Klicke alle Buttons --> Auflösung welche die veränderte Kurve war und dieses Level
                // wird 4 mal in die text-datei abgespeichert, somit weiß man, dass die Versuchsperson
                // dieses level "nicht geschafft" hat
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

                clickedTooEarly();

                counterData++;
            }

        } else
        {
            intervalInternCounter = 0;
        }
    }


    // Use this for initialization
    void Start () {
        fgCSVReader.LoadFromFile(Application.streamingAssetsPath + "/" + csvFileName, new fgCSVReader.ReadLineDelegate(ReadLineTest));
        // setzten der Punkte für die strichdicke
        curve.AddKey(0.0f, 1.0f);
        curve.AddKey(1.0f, 1.0f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        DrawNewLine2(1200);
    }

    // löscht die Punnkte aus dem Linerenderer sobald das nächste Intervall beginnt
    // indem alle Punkte in der Reihenfolge um eins nach oben verschoben werden und der 
    // unterste Position-Punkt gelöscht wird
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

    // initialisiert das Fenster innerhalb des Gesamtbildschirms
    // wird vom LineController Skript für jede Instanz des SingleView Prefabs aufgerufen
    public void initWindow(float _shiftX, float _shiftY, float _scalingFactor)
    {
        shiftX = _shiftX;
        shiftY = _shiftY;
        scalingFactor = _scalingFactor;
    }

    // ändert die Variable csvFileName, wenn die Linie eine der Veränderungen anzeigen soll
    public void changeCsvFilename(string newFileName)
    {
        csvFileName = newFileName;
    }

    // Übergabe der Position des Canvases das den Button enthält
    // von LineController aufgerufen
    public void initButton(float xPos, float yPos, float zPos)
    {
        buttonCanvas.transform.position = new Vector3(xPos, yPos, zPos);
    }

    // Übergabe der Variablen vom LineController durch SingleView an das Buttons Skript
    // dieser ein wenig umständliche Weg musste gegangen werden, da Objekte eines Prefabs
    // nicht so einfach auf Objekte die nicht Teil des Prefabs sind zugreifen können
    public void setButtonConnections(GameObject nLB, bool changed, GameObject endButton)
    {
        Buttons buttonController = backgroundButton.GetComponent<Buttons>();
        buttonController.NextLvlButton = nLB;
        buttonController.myLRC = myLC;
        buttonController.endOfGameButton = endButton;

        if (changed)
        {
            buttonController.changedShownText();
        }
    }

    // Kamera wird an Button übergeben
    // dient der Darstellungsart des Buttons, da der Canvas ansonsten überproportional groß gewesen ist (Bug von Unity?)
    public void setCameraForCanvas(Camera mainCam)
    {
        buttonCanvas.worldCamera = mainCam;
    }

    // Linien werden mit dieser Methode gezeichnet
    // wird von der DrawNewLine2 für jeden LineRenderer aufgerufen
    public void drawLinesWithLR(LineRenderer lr, int counterIndex_lr)
    {
        // wenn geänderte Line mit Strichdickenänderung
        if (csvChangeThickness[counterData] == 1)
        {
            lr.widthCurve = curve;
            lr.widthMultiplier = multiWidth;
        }
        // wenn geänderte Linie mit Farbe
        if (csvChangeColor[counterData] == 1)
        {
            lr.material = redMaterial;
        }

        lr.positionCount++;

        lr.SetPosition(counterIndex_lr, new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, csvfileData[counterData] + shiftY, 0.0f) * scalingFactor);
        EraserBar.transform.position = new Vector3(intervalInternCounter * 0.01f - 6.0f + shiftX, 0.0f + shiftY, -0.001f) * scalingFactor;
    }

    // schaut nach, ob wir uns bereit im nächsten intervall bewegen und es zeit ist die alte linie zu löschen
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

    // Verbindung zum Linecontroller
    public void setLC(LineController lineController)
    {
        myLC = lineController;
    }

    // setzt variable, ob diese linie "the chosen one" ist
    public void setChangedLine(bool changed)
    {
        changedLine = changed;
    }

    // sobald die veränderung eintritt werden die beiden methoden des linecontroller skripts aufgerufen
    // dient der zeiterfassung
    public void clickedTooEarly()
    {
        if (changedLine)
        {
            if (counterData == csvChangeStart[counterData])
            {
                myLC.setTimeOfChangestart(DateTime.Now);
            }

            if (counterData > csvChangeStart[counterData])
            {
                myLC.setClickedTooEarly(false);
            }
        }
        
    }
}