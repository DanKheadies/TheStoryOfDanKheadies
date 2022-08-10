// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/30/2020
// Last:  04/26/2021

using UnityEngine;

// To "move" and execute the arrows on the Sound Menu
public class MoveSoundMenuArrow : MonoBehaviour
{
    public ControllerSupport contSupp;
    public GameObject backSelector;
    public GameObject volumeSelector;
    public FixedJoystick fixedJoystick;
    public MovePauseMenuArrow movePMA;
    public PauseGame pause;
    public TouchControls touches;
    public Transform soundMenu;
    public VolumeManager vMan;

    public bool bControllerDown;
    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bControllerUp;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        volumeSlider = 1,
        back = 2
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        currentPosition = SelectorPosition.volumeSlider;
    }
    
    void Update()
    {
        if (soundMenu.localScale == Vector3.one)
        {
            // Controller Support 
            if (!contSupp.bIsMoving &&
                fixedJoystick.Vertical == 0 &&
                fixedJoystick.Horizontal == 0 &&
                (!touches.bDown &&
                 !touches.bUp &&
                 !touches.bLeft &&
                 !touches.bRight))
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
                     (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() > 0 ||
                      fixedJoystick.Horizontal > 0 ||
                      touches.bRight))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadHorizontal() < 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() < 0 ||
                      fixedJoystick.Horizontal < 0 ||
                      touches.bLeft))
            {
                bControllerLeft = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                if (currentPosition == SelectorPosition.volumeSlider)
                {
                    currentPosition = SelectorPosition.back;
                    ClearAllSelectors();
                    backSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    currentPosition = SelectorPosition.volumeSlider;
                    ClearAllSelectors();
                    volumeSelector.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;

                if (currentPosition == SelectorPosition.volumeSlider)
                {
                    currentPosition = SelectorPosition.back;
                    ClearAllSelectors();
                    backSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    currentPosition = SelectorPosition.volumeSlider;
                    ClearAllSelectors();
                    volumeSelector.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.LeftArrow) ||
                     bControllerLeft)
            {
                bControllerLeft = false;

                if (currentPosition == SelectorPosition.volumeSlider)
                    vMan.LowerVolume();
            }
            else if (Input.GetKeyDown(KeyCode.D) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     bControllerRight)
            {
                bControllerRight = false;

                if (currentPosition == SelectorPosition.volumeSlider)
                    vMan.RaiseVolume();
            }
            else if ((Input.GetButtonDown("Action") ||
                      contSupp.ControllerButtonPadBottom("down") ||
                      touches.bAaction))
            {
                if (currentPosition == SelectorPosition.back)
                {
                    ResetSelectors();
                    movePMA.bDelayAction = true;
                    pause.Sound(false);
                }
                
                touches.bAaction = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) ||
                     contSupp.ControllerMenuRight("down") ||
                     contSupp.ControllerButtonPadRight("down") ||
                     Input.GetButton("BAction") ||
                     touches.bBaction)
            {
                ResetSelectors();
            }
        }
    }

    public void HideSelectors()
    {
        volumeSelector.transform.localScale = Vector3.zero;
        backSelector.transform.localScale = Vector3.zero;
    }

    public void ClearAllSelectors()
    {
        if (soundMenu.localScale == Vector3.one)
            HideSelectors();
    }

    public void ResetSelectors()
    {
        HideSelectors();

        volumeSelector.transform.localScale = Vector3.one;
        currentPosition = SelectorPosition.volumeSlider;
    }
}
