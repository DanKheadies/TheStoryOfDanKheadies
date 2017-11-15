// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  11/08/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To "move" and execute the arrows on the Pause Menu
public class MovePauseMenuArrow : MonoBehaviour
{
    private Button GoOnBtn;
    private Button SaveBtn;
    private Button StuffBtn;
    private Button MapBtn;
    private Button SoundBtn;
    private Button ControlsBtn;
    private Button QuitBtn;

    private GameObject GoOnArw;
    private GameObject SaveArw;
    private GameObject StuffArw;
    private GameObject MapArw;
    private GameObject SoundArw;
    private GameObject ControlsArw;
    private GameObject QuitArw;

    private Scene scene;


    public enum ArrowPos : int
    {
        GoOn = 165,
        Save = 110,
        Stuff = 55,
        Map = 0,
        Sound = -55,
        Controls = -110,
        Quit = -165
    }

    public ArrowPos currentPosition;

    void Start()
    {
        // Initializers
        GoOnBtn = GameObject.Find("GoOn").GetComponent<UnityEngine.UI.Button>();
        SaveBtn = GameObject.Find("Save").GetComponent<UnityEngine.UI.Button>();
        StuffBtn = GameObject.Find("Stuff").GetComponent<UnityEngine.UI.Button>();
        MapBtn = GameObject.Find("Map").GetComponent<UnityEngine.UI.Button>();
        SoundBtn = GameObject.Find("Sound").GetComponent<UnityEngine.UI.Button>();
        ControlsBtn = GameObject.Find("Controls").GetComponent<UnityEngine.UI.Button>();
        QuitBtn = GameObject.Find("Quit").GetComponent<UnityEngine.UI.Button>();

        GoOnArw = GameObject.Find("GoOnArw");
        SaveArw = GameObject.Find("SaveArw");
        StuffArw = GameObject.Find("StuffArw");
        MapArw = GameObject.Find("MapArw");
        SoundArw = GameObject.Find("SoundArw");
        ControlsArw = GameObject.Find("ControlsArw");
        QuitArw = GameObject.Find("QuitArw");

        scene = SceneManager.GetActiveScene();


        currentPosition = ArrowPos.GoOn;
    }
	
	void Update ()
    {
        //Debug.Log((float)currentPosition);
        //var newfloat = (float)currentPosition;
        //Debug.Log(newfloat);
        //PauseArrow.transform.position = new Vector2(
        //    PauseArrow.transform.position.x,
        //    (int)currentPosition
        //    );
        //Debug.Log(newfloat);
        //Debug.Log((int)currentPosition);
        
        if (Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPosition == ArrowPos.GoOn)
            {
                if (scene.name == "Showdown")
                {
                    currentPosition = ArrowPos.Sound;
                    ClearAllArrows();
                    SoundArw.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    currentPosition = ArrowPos.Save;
                    ClearAllArrows();
                    SaveArw.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (currentPosition == ArrowPos.Save)
            {
                //// currentPosition = ArrowPos.Stuff;
                //ClearAllArrows();
                //StuffArw.transform.localScale = new Vector3(1, 1, 1);

                // Temp: Skip the Stuff & Map until built / accessible
                currentPosition = ArrowPos.Sound;
                ClearAllArrows();
                SoundArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Stuff)
            {
                currentPosition = ArrowPos.Map;
                ClearAllArrows();
                MapArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Map)
            {
                currentPosition = ArrowPos.Sound;
                ClearAllArrows();
                SoundArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Sound)
            {
                currentPosition = ArrowPos.Controls;
                ClearAllArrows();
                ControlsArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Controls)
            {
                currentPosition = ArrowPos.Quit;
                ClearAllArrows();
                QuitArw.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) ||
                 Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentPosition == ArrowPos.Quit)
            {
                currentPosition = ArrowPos.Controls;
                ClearAllArrows();
                ControlsArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Controls)
            {
                currentPosition = ArrowPos.Sound;
                ClearAllArrows();
                SoundArw.transform.localScale = new Vector3(1, 1, 1);

            }
            else if (currentPosition == ArrowPos.Sound)
            {
                //currentPosition = ArrowPos.Map;
                //ClearAllArrows();
                //MapArw.transform.localScale = new Vector3(1, 1, 1);

                // Temp: Skip the Stuff & Map until built / accessible
                if (scene.name == "Showdown")
                {
                    currentPosition = ArrowPos.GoOn;
                    ClearAllArrows();
                    GoOnArw.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    currentPosition = ArrowPos.Save;
                    ClearAllArrows();
                    SaveArw.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (currentPosition == ArrowPos.Map)
            {
                currentPosition = ArrowPos.Stuff;
                ClearAllArrows();
                StuffArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Stuff)
            {
                currentPosition = ArrowPos.Save;
                ClearAllArrows();
                SaveArw.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentPosition == ArrowPos.Save)
            {
                currentPosition = ArrowPos.GoOn;
                ClearAllArrows();
                GoOnArw.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) ||
                 Input.GetKeyDown(KeyCode.Return))
        {
            if (currentPosition == ArrowPos.GoOn)
            {
                GoOnBtn.onClick.Invoke();
            }
            else if (currentPosition == ArrowPos.Save)
            {
                SaveBtn.onClick.Invoke();
            }
            else if (currentPosition == ArrowPos.Stuff)
            {
                StuffBtn.onClick.Invoke();
            }
            else if (currentPosition == ArrowPos.Map)
            {
                MapBtn.onClick.Invoke();
            }
            else if (currentPosition == ArrowPos.Sound)
            {
                SoundBtn.onClick.Invoke();
            }
            else if (currentPosition == ArrowPos.Controls)
            {
                ControlsBtn.onClick.Invoke();
            }
            else if (currentPosition == ArrowPos.Quit)
            {
                QuitBtn.onClick.Invoke();
            }
        }
    }

    public void ClearAllArrows()
    {
        GoOnArw.transform.localScale = new Vector3(0, 0, 0);
        SaveArw.transform.localScale = new Vector3(0, 0, 0);
        StuffArw.transform.localScale = new Vector3(0, 0, 0);
        MapArw.transform.localScale = new Vector3(0, 0, 0);
        SoundArw.transform.localScale = new Vector3(0, 0, 0);
        ControlsArw.transform.localScale = new Vector3(0, 0, 0);
        QuitArw.transform.localScale = new Vector3(0, 0, 0);
    }
                
}
