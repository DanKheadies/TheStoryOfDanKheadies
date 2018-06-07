// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/23/2018
// Last:  05/11/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To "move" and execute the arrows on the Options Menu
public class MoveOptionsMenuArrow : MonoBehaviour
{
    private Button Opt1Btn;
    private Button Opt2Btn;
    private Button Opt3Btn;
    private Button Opt4Btn;
    private GameObject Opt1Arw;
    private GameObject Opt2Arw;
    private GameObject Opt3Arw;
    private GameObject Opt4Arw;
    private GameObject pauseScreen;
    private OptionsManager oMan;
    private Scene scene;
    private TouchControls touches;
    private Transform optionsBox;


    public enum ArrowPos : int
    {
        Opt1 = 1,
        Opt2 = 2,
        Opt3 = 3,
        Opt4 = 4
    }

    public ArrowPos currentPosition;

    void Start()
    {
        // Initializers
        Opt1Btn = GameObject.Find("Opt1").GetComponent<UnityEngine.UI.Button>();
        Opt2Btn = GameObject.Find("Opt2").GetComponent<UnityEngine.UI.Button>();
        Opt3Btn = GameObject.Find("Opt3").GetComponent<UnityEngine.UI.Button>();
        Opt4Btn = GameObject.Find("Opt4").GetComponent<UnityEngine.UI.Button>();

        Opt1Arw = GameObject.Find("Opt1Arw");
        Opt2Arw = GameObject.Find("Opt2Arw");
        Opt3Arw = GameObject.Find("Opt3Arw");
        Opt4Arw = GameObject.Find("Opt4Arw");

        oMan = GameObject.FindObjectOfType<OptionsManager>();
        optionsBox = GameObject.Find("Options_Box").transform;
        pauseScreen = GameObject.Find("PauseScreen");
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();
        
        currentPosition = ArrowPos.Opt1;
    }

    void Update()
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

        if (oMan.bOptionsActive &&
            !oMan.bPauseOptions &&
            pauseScreen.transform.localScale == Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow)) //||
                //touches.bDown) DC TODO -- Fix touches w/ arrow movement (also, see PauseMenu)
            {
                if (currentPosition == ArrowPos.Opt1 && oMan.tempOptsCount > 1)
                {
                    currentPosition = ArrowPos.Opt2;
                    ClearAllArrows();
                    Opt2Arw.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (currentPosition == ArrowPos.Opt2 && oMan.tempOptsCount > 2)
                {
                    currentPosition = ArrowPos.Opt3;
                    ClearAllArrows();
                    Opt3Arw.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (currentPosition == ArrowPos.Opt3 && oMan.tempOptsCount > 3)
                {
                    currentPosition = ArrowPos.Opt4;
                    ClearAllArrows();
                    Opt4Arw.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow)) //||
                     //touches.bUp) DC TODO -- Fix touches w/ arrow movement (also, see PauseMenu)
            {
                if (currentPosition == ArrowPos.Opt4) 
                {
                    currentPosition = ArrowPos.Opt3;
                    ClearAllArrows();
                    Opt3Arw.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (currentPosition == ArrowPos.Opt3)
                {
                    currentPosition = ArrowPos.Opt2;
                    ClearAllArrows();
                    Opt2Arw.transform.localScale = new Vector3(1, 1, 1);
                }
                else if (currentPosition == ArrowPos.Opt2)
                {
                    currentPosition = ArrowPos.Opt1;
                    ClearAllArrows();
                    Opt1Arw.transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space) ||
                     Input.GetKeyDown(KeyCode.Return) ||
                     touches.bAction)
            {
                if (currentPosition == ArrowPos.Opt1)
                {
                    Opt1Btn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Opt2)
                {
                    Opt2Btn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Opt3)
                {
                    Opt3Btn.onClick.Invoke();
                }
                else if (currentPosition == ArrowPos.Opt4)
                {
                    Opt4Btn.onClick.Invoke();
                }

                ResetArrows();
            }
        }
    }

    public void ClearAllArrows()
    {
        if (oMan.bOptionsActive)
        {
            Opt1Arw.transform.localScale = new Vector3(0, 0, 0);
            Opt2Arw.transform.localScale = new Vector3(0, 0, 0);
            Opt3Arw.transform.localScale = new Vector3(0, 0, 0);
            Opt4Arw.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void ResetArrows()
    {
        Opt1Arw.transform.localScale = new Vector3(1, 1, 1);
        Opt2Arw.transform.localScale = new Vector3(0, 0, 0);
        Opt3Arw.transform.localScale = new Vector3(0, 0, 0);
        Opt4Arw.transform.localScale = new Vector3(0, 0, 0);

        currentPosition = ArrowPos.Opt1;
    }
}
