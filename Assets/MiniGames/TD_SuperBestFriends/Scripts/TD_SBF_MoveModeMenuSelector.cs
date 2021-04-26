// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 05/04/2020
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the arrows on the TD SBF Mode Selection Menu
public class TD_SBF_MoveModeMenuSelector : MonoBehaviour
{
    public ControllerSupport contSupp;
    public Button arcadeBtn;
    public Button menuBtn;
    public GameObject modeCanvas;

    public bool bControllerDown;
    public bool bControllerUp;
    public bool bDelayOnSwitch;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        story = 1,
        arcade = 2,
        multiplayer = 3,
        menu = 4
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        InitialSelection();
    }

    void Update()
    {
        if (modeCanvas.activeSelf)
        {
            if (bDelayOnSwitch)
            {
                bDelayOnSwitch = false;
                return;
            }
            
            // Controller Support 
            if (!contSupp.bIsMoving &&
                contSupp.ControllerRightJoystickVertical() == 0)
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() < 0 ||
                      contSupp.ControllerLeftJoystickVertical() < 0))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() > 0 ||
                      contSupp.ControllerLeftJoystickVertical() > 0))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                if (currentPosition == SelectorPosition.arcade)
                {
                    currentPosition = SelectorPosition.menu;
                    menuBtn.Select();
                }
                else if (currentPosition == SelectorPosition.menu)
                {
                    currentPosition = SelectorPosition.arcade;
                    arcadeBtn.Select();
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;
                
                if (currentPosition == SelectorPosition.arcade)
                {
                    currentPosition = SelectorPosition.menu;
                    menuBtn.Select();
                }
                else if (currentPosition == SelectorPosition.menu)
                {
                    currentPosition = SelectorPosition.arcade;
                    arcadeBtn.Select();
                }
            }
            
            else if (Input.GetButtonDown("Action") ||
                     contSupp.ControllerButtonPadBottom("down"))
            {
                if (currentPosition == SelectorPosition.arcade)
                {
                    arcadeBtn.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.menu)
                {
                    menuBtn.onClick.Invoke();
                }
            }
        }
    }

    public void InitialSelection()
    {
        currentPosition = SelectorPosition.arcade;

        //contSupp.FindControllers();
        //if (contSupp.bControllerConnected)
        arcadeBtn.Select();
    }
}
