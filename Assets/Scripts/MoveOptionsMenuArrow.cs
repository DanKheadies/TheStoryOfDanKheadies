﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/23/2018
// Last:  06/20/2019

using UnityEngine;
using UnityEngine.UI;

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
    private Joystick joystick;
    private OptionsManager oMan;
    private TouchControls touches;

    public bool bControllerDown;
    public bool bControllerUp;
    public bool bFreezeControllerInput;
    public bool bFreezeVirtualInput;


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
        Opt1Btn = GameObject.Find("Opt1").GetComponent<Button>();
        Opt2Btn = GameObject.Find("Opt2").GetComponent<Button>();
        Opt3Btn = GameObject.Find("Opt3").GetComponent<Button>();
        Opt4Btn = GameObject.Find("Opt4").GetComponent<Button>();

        Opt1Arw = GameObject.Find("Opt1Arw");
        Opt2Arw = GameObject.Find("Opt2Arw");
        Opt3Arw = GameObject.Find("Opt3Arw");
        Opt4Arw = GameObject.Find("Opt4Arw");
        
        joystick = FindObjectOfType<Joystick>();
        oMan = FindObjectOfType<OptionsManager>();
        pauseScreen = GameObject.Find("PauseScreen");
        touches = FindObjectOfType<TouchControls>();
        
        currentPosition = ArrowPos.Opt1;
    }

    void Update()
    {
        if (oMan.bOptionsActive &&
            !oMan.bPauseOptions &&
            pauseScreen.transform.localScale == Vector3.zero)
        {
            // Controller Support 
            if (Input.GetAxis("Controller DPad Vertical") == 0 &&
                Input.GetAxis("Controller Joystick Vertical") == 0 &&
                joystick.Vertical == 0 &&
                (!touches.bDown &&
                 !touches.bUp))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (Input.GetAxis("Controller DPad Vertical") > 0 ||
                      Input.GetAxis("Controller Joystick Vertical") < 0 ||
                      touches.bDown ||
                      (Mathf.Abs(joystick.Vertical) > Mathf.Abs(joystick.Horizontal) &&
                       joystick.Vertical < 0)))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (Input.GetAxis("Controller DPad Vertical") < 0 ||
                      Input.GetAxis("Controller Joystick Vertical") > 0 ||
                      touches.bUp ||
                      (Mathf.Abs(joystick.Vertical) > Mathf.Abs(joystick.Horizontal) &&
                       joystick.Vertical > 0)))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                if (currentPosition == ArrowPos.Opt1 && 
                    oMan.tempOptsCount > 1)
                {
                    currentPosition = ArrowPos.Opt2;
                    ClearAllArrows();
                    Opt2Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt2 && 
                         oMan.tempOptsCount > 2)
                {
                    currentPosition = ArrowPos.Opt3;
                    ClearAllArrows();
                    Opt3Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt3 && 
                         oMan.tempOptsCount > 3)
                {
                    currentPosition = ArrowPos.Opt4;
                    ClearAllArrows();
                    Opt4Arw.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;
                
                if (currentPosition == ArrowPos.Opt4) 
                {
                    currentPosition = ArrowPos.Opt3;
                    ClearAllArrows();
                    Opt3Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt3)
                {
                    currentPosition = ArrowPos.Opt2;
                    ClearAllArrows();
                    Opt2Arw.transform.localScale = Vector3.one;
                }
                else if (currentPosition == ArrowPos.Opt2)
                {
                    currentPosition = ArrowPos.Opt1;
                    ClearAllArrows();
                    Opt1Arw.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetButtonDown("Action") ||
                     Input.GetKeyDown(KeyCode.JoystickButton0) ||
                     touches.bAaction)
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

                touches.bAaction = false;

                ResetArrows();
            }
        }
    }

    public void ClearAllArrows()
    {
        if (oMan.bOptionsActive)
        {
            Opt1Arw.transform.localScale = Vector3.zero;
            Opt2Arw.transform.localScale = Vector3.zero;
            Opt3Arw.transform.localScale = Vector3.zero;
            Opt4Arw.transform.localScale = Vector3.zero;
        }
    }

    public void ResetArrows()
    {
        Opt1Arw.transform.localScale = Vector3.one;
        Opt2Arw.transform.localScale = Vector3.zero;
        Opt3Arw.transform.localScale = Vector3.zero;
        Opt4Arw.transform.localScale = Vector3.zero;

        currentPosition = ArrowPos.Opt1;
    }
}
