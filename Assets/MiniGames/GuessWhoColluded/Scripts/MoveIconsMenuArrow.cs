// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/30/2020
// Last:  05/04/2020

using UnityEngine;

// To "move" and execute the arrows on the Icons Menu
public class MoveIconsMenuArrow : MonoBehaviour
{
    public ControllerSupport contSupp;
    public FixedJoystick fixedJoystick;
    public MovePauseMenuArrow movePMA;
    public PauseGame pause;
    public TouchControls touches;
    public Transform iconsMenu;

    public bool bControllerDown;
    public bool bControllerDownSecondary;
    public bool bControllerUp;
    public bool bControllerUpSecondary;
    public bool bDelayAction;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        back = 1
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        bDelayAction = true;
        currentPosition = SelectorPosition.back;
    }

    void Update()
    {
        if (iconsMenu.localScale == Vector3.one)
        {
            // Controller Support
            if (bDelayAction)
            {
                bDelayAction = false;
                return;
            }

            // Controller Support 
            if (!contSupp.bIsMoving &&
                fixedJoystick.Vertical == 0 &&
                fixedJoystick.Horizontal == 0 &&
                contSupp.ControllerRightJoystickVertical() == 0 &&
                (!touches.bDown &&
                 !touches.bUp))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() < 0 ||
                      contSupp.ControllerLeftJoystickVertical() < 0 ||
                      touches.bDown ||
                      (Mathf.Abs(fixedJoystick.Vertical) > Mathf.Abs(fixedJoystick.Horizontal) &&
                       fixedJoystick.Vertical < 0)))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerRightJoystickVertical() > 0))
            {
                bControllerDownSecondary = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() > 0 ||
                      contSupp.ControllerLeftJoystickVertical() > 0 ||
                      touches.bUp ||
                      (Mathf.Abs(fixedJoystick.Vertical) > Mathf.Abs(fixedJoystick.Horizontal) &&
                       fixedJoystick.Vertical > 0)))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerRightJoystickVertical() < 0))
            {
                bControllerUpSecondary = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown ||
                bControllerDownSecondary)
            {
                bControllerDown = false;
                bControllerDownSecondary = false;

                iconsMenu.GetChild(1).GetChild(0).localPosition =
                    new Vector3(
                        iconsMenu.GetChild(1).GetChild(0).localPosition.x,
                        iconsMenu.GetChild(1).GetChild(0).localPosition.y + 50,
                        iconsMenu.GetChild(1).GetChild(0).localPosition.z);
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;
                bControllerDownSecondary = false;

                iconsMenu.GetChild(1).GetChild(0).localPosition =
                    new Vector3(
                        iconsMenu.GetChild(1).GetChild(0).localPosition.x,
                        iconsMenu.GetChild(1).GetChild(0).localPosition.y - 50,
                        iconsMenu.GetChild(1).GetChild(0).localPosition.z);
            }
            else if ((Input.GetButtonDown("Action") ||
                      contSupp.ControllerButtonPadBottom("down") ||
                      touches.bAaction))
            {
                if (currentPosition == SelectorPosition.back)
                {
                    movePMA.bDelayAction = true;
                    bDelayAction = true;
                    pause.Icons(false);
                }

                touches.bAaction = false;
            }
        }
    }
}
