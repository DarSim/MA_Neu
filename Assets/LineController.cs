using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LineController : MonoBehaviour {

    // Geschwindigkeitsänderung
    [Range(0f, 5f)]
    public float TimeScaleFactor = 2f;

    // Bildschirmgrößen-Einstellungen
    public Transform ViewParent;
    public float scaling = 7.5f;

    // zur Erstellung der Prefabs
    public GameObject SubViewPrefab;
    public List<SingleView> singleViewList;

    // Buttons auf die Zugegriffen werden muss
    public GameObject nextLvlButton;
    public GameObject endButton;

    // welche der 4 Linien zeigt die Änderung an
    public int whichLineToChange = 1;

    // Zeitmessung: Beginn des Levels
    public DateTime startOfLvl;
    // Beginn der Änderung am Anfang auf MinValue von Zeit gestellt
    public DateTime startOfChange = DateTime.MinValue;

    // Für die Kameraeinstellung der Prefabs muss wegen der Position der Buttons eine Kamera an SingleView übergeben werden
    public Camera mainCam;

    // "random" Reihenfolge, in der die CSV-Dateien angezeigt werden
    int[] orderOfCSVs = new int[] {13, 27, 8, 12, 5, 2, 15, 10, 29, 32, 6, 21, 16, 31, 7, 4, 28, 20, 24,
                                   30, 26, 25, 18, 14, 0, 35, 22, 1, 3, 17, 23, 34, 33, 19, 9, 11};

    // solange Änderung noch nicht angezeigt worden ist ist tooEarly auf true
    bool tooEarly = true;


    // Use this for initialization
    void Start () {
        // Initialaufruf für Level 0
        startThoseLines(0);
    }
	
	// Update is called once per frame
	void Update () {
        // Falls man innerhalb des Inspectors während des Spiels die Geschwindigkeit ändert
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

    // wird vom Buttons-Skript aufgerufen und berechnet die Zeitmessungsvariablen und speichert alles in die Datei testFile.txt
    public void calculateTimeNeeded(DateTime endOfLvl, int lvlCounter, string buttonText)
    {
        // Berechnung vergangene Zeit zwischen Beginn des Levels und Klick
        long elapsedTicks = endOfLvl.Ticks - startOfLvl.Ticks;
        TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);

        // Berechnung vergangene Zeit zwischen Beginn der Änderung und Klick
        long elapsedTicksChange = endOfLvl.Ticks - startOfChange.Ticks;
        TimeSpan elapsedSpanChange = new TimeSpan(elapsedTicksChange);

        string path = Application.persistentDataPath + "/testFile.txt";
        //string path = Application.dataPath + "/testFile.txt";

        string toBeSaved = DateTime.Now + ", Level: " + lvlCounter + ", start of level until click[s]: " + elapsedSpan.TotalSeconds.ToString("F2") + ", too early: " + tooEarly 
            + ", start of change until click[s]: " + elapsedSpanChange.TotalSeconds.ToString("F2") + ", Changed Linie: " + whichLineToChange + ", Button clicked: " + buttonText + Environment.NewLine;
        if (lvlCounter == 35)
        {
            toBeSaved = toBeSaved + "-------------" + Environment.NewLine;
        }

        // mit AppendAll wird die Text Datei um jede Linie erweitert und nicht überschrieben
        File.AppendAllText(path, toBeSaved);
    }

    // instanziiert Linien und übergibt Informationen an SubView-Skript
    public void instanciateLines(float xCoord, float yCoord, float scaling, bool change, int lvlCounter, float buttonPosX, float buttonPosY, float buttonPosZ)
    {
        GameObject subViewGO = Instantiate(SubViewPrefab) as GameObject;
        SingleView subViewController = subViewGO.GetComponent<SingleView>();
        if (change)
        {
            //Änderung des Datei-Pfades, falls es sich um eine geänderte Linie handelt
            subViewController.changeCsvFilename("newAll/csvChange" + orderOfCSVs[lvlCounter] + ".csv");
            // setzt bool variable in singleview, dient der Berechnung der Zeit zwischen Änderungsanzeige und Klick
            subViewController.setChangedLine(change);
        } 
        //Setzte Koordinaten innerhalb des Bildschirms
        subViewController.initWindow(xCoord, yCoord, scaling);
        // Kameraübergabe
        subViewController.setCameraForCanvas(mainCam);
        // Koordinaten für Button
        subViewController.initButton(buttonPosX, buttonPosY, buttonPosZ);
        // stellt Verbindung zwischen SingleView und diesem LineController Skript her, damit SingleView auch auf Methoden von LC zugreifen kann
        subViewController.setLC(this);
        // einige Variablen für das Buttons Skript
        subViewController.setButtonConnections(nextLvlButton, change, endButton);

        // alle Instanzen der SingleViews werden in eine Liste geschrieben, damit sie nach jedem Level gelöscht werden können
        singleViewList.Add(subViewController);
        subViewController.transform.parent = ViewParent;
    }

    // löscht alle Instanzen des Prefabs nach Beenden des Levels
    public void clearTheStage()
    {
        foreach (SingleView sv in singleViewList)
        {
            Destroy(sv.gameObject);
        }
        singleViewList.Clear();
        singleViewList = new List<SingleView>();
    }

    // setzt Zeitpunkt des Starts der Veränderung
    // wird vom SingleView Skript, welches die veränderte Line anzeigt aufgerufen
    public void setTimeOfChangestart(DateTime changestart)
    {
        startOfChange = changestart;
    }

    // falls zum Zeitpunkt des Klickens die Änderung noch nicht aufgetreten ist wird die Variable tooEarly auf true gesetzt 
    public void setClickedTooEarly(bool _tooEarly)
    {
        tooEarly = _tooEarly;
    }
}
