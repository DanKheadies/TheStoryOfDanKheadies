// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/23/2018
// Last:  02/25/2020

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the arrows on the Options Menu
public class MoveOptionsMenuArrow : MonoBehaviour
{
    public Button Opt1Btn;
    public Button Opt2Btn;
    public Button Opt3Btn;
    public Button Opt4Btn;
    public ControllerSupport contSupp;
    public GameObject Opt1Arw;
    public GameObject Opt2Arw;
    public GameObject Opt3Arw;
    public GameObject Opt4Arw;
    public GameObject pauseScreen;
    public Joystick joystick;
    public OptionsManager oMan;
    public TouchControls touches;

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
        currentPosition = ArrowPos.Opt1;
    }

    void Update()
    {
        if (oMan.bOptionsActive &&
            !oMan.bPauseOptions &&
            pauseScreen.transform.localScale == Vector3.zero)
        {
            //// Controller Support 
            //if (Input.GetAxis("Controller DPad Vertical") == 0 &&
            //    Input.GetAxis("Controller Joystick Vertical") == 0 &&
            if (contSupp.ControllerDirectionalPadVertical() == 0 &&
                contSupp.ControllerLeftJoystickVertical() == 0 &&
                joystick.Vertical == 0 &&
                (!touches.bDown &&
                 !touches.bUp))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     //(Input.GetAxis("Controller DPad Vertical") > 0 ||
                     // Input.GetAxis("Controller Joystick Vertical") < 0 ||
                     (contSupp.ControllerDirectionalPadVertical() < 0 ||
                      contSupp.ControllerLeftJoystickVertical() < 0 ||
                      touches.bDown ||
                      (Mathf.Abs(joystick.Vertical) > Mathf.Abs(joystick.Horizontal) &&
                       joystick.Vertical < 0)))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     //(Input.GetAxis("Controller DPad Vertical") < 0 ||
                     // Input.GetAxis("Controller Joystick Vertical") > 0 ||
                     (contSupp.ControllerDirectionalPadVertical() > 0 ||
                      contSupp.ControllerLeftJoystickVertical() > 0 ||
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
                     //Input.GetKeyDown(KeyCode.JoystickButton0) ||
                     contSupp.ControllerButtonPadBottom("down") ||
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
