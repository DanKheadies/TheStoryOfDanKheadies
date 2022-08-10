// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/30/2020
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the arrows on the Controls Menu
public class MoveControlsMenuArrow : MonoBehaviour
{
    public ControllerSupport contSupp;
    public FixedJoystick fixedJoystick;
    public GameObject backSelector;
    public GameObject buttonOpacitySelector;
    public GameObject controlsDescriptionSelector;
    public GameObject devSupport;
    public GameObject dPadSelector;
    public GameObject showButtonsSelector;
    public GameObject vibrateSelector;
    public MovePauseMenuArrow movePMA;
    public PauseGame pause;
    public ScriptManager scriptMan;
    public Toggle dPadToggle;
    public Toggle showButtonsToggle;
    public Toggle vibrateToggle;
    public TouchControls touches;
    public Transform controlsMenu;
    public UIManager uMan;

    public bool bControllerDown;
    public bool bControllerDownSecondary;
    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bControllerUp;
    public bool bControllerUpSecondary;
    public bool bDevSupportActive;
    public bool bFreezeControllerInput;

    public float devSupportTimer;
    public float devSupportTimerInit;

    public enum SelectorPosition : int
    {
        showButtons = 1,
        opacitySlider = 2,
        dPad = 3,
        vibrate = 4,
        description = 5,
        back = 6
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        currentPosition = SelectorPosition.showButtons;
        devSupportTimerInit = 2f;
        devSupportTimer = devSupportTimerInit;
    }
    
    void Update()
    {
        if (controlsMenu.localScale == Vector3.one)
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

                if (currentPosition == SelectorPosition.showButtons)
                {
                    currentPosition = SelectorPosition.opacitySlider;
                    ClearAllSelectors();
                    buttonOpacitySelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.opacitySlider)
                {
                    currentPosition = SelectorPosition.dPad;
                    ClearAllSelectors();
                    dPadSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.dPad)
                {
                    currentPosition = SelectorPosition.vibrate;
                    ClearAllSelectors();
                    vibrateSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.vibrate)
                {
                    currentPosition = SelectorPosition.description;
                    ClearAllSelectors();
                    controlsDescriptionSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.description)
                {
                    currentPosition = SelectorPosition.back;
                    ClearAllSelectors();
                    backSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    currentPosition = SelectorPosition.showButtons;
                    ClearAllSelectors();
                    showButtonsSelector.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;

                if (currentPosition == SelectorPosition.back)
                {
                    currentPosition = SelectorPosition.description;
                    ClearAllSelectors();
                    controlsDescriptionSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.description)
                {
                    currentPosition = SelectorPosition.vibrate;
                    ClearAllSelectors();
                    vibrateSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.vibrate)
                {
                    currentPosition = SelectorPosition.dPad;
                    ClearAllSelectors();
                    dPadSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.dPad)
                {
                    currentPosition = SelectorPosition.opacitySlider;
                    ClearAllSelectors();
                    buttonOpacitySelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.opacitySlider)
                {
                    currentPosition = SelectorPosition.showButtons;
                    ClearAllSelectors();
                    showButtonsSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.showButtons)
                {
                    currentPosition = SelectorPosition.back;
                    ClearAllSelectors();
                    backSelector.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.LeftArrow) ||
                     bControllerLeft)
            {
                bControllerLeft = false;

                if (currentPosition == SelectorPosition.opacitySlider)
                    uMan.DecreaseOpacity();
            }
            else if (Input.GetKeyDown(KeyCode.D) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     bControllerRight)
            {
                bControllerRight = false;

                if (currentPosition == SelectorPosition.opacitySlider)
                    uMan.IncreaseOpacity();
            }
            else if ((Input.GetButtonDown("Action") ||
                      contSupp.ControllerButtonPadBottom("down") ||
                      touches.bAaction))
            {
                if (currentPosition == SelectorPosition.showButtons)
                {
                    showButtonsToggle.isOn = !showButtonsToggle.isOn;
                    uMan.ToggleControls();
                    touches.Vibrate();
                }
                else if (currentPosition == SelectorPosition.dPad &&
                         !uMan.bOnlyDPad)
                {
                    dPadToggle.isOn = !dPadToggle.isOn;
                    uMan.ToggleDPadControl();
                    touches.Vibrate();
                }
                else if (currentPosition == SelectorPosition.vibrate)
                {
                    vibrateToggle.isOn = !vibrateToggle.isOn;
                    touches.ToggleVibrate();
                    touches.Vibrate();
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    ResetSelectors();
                    movePMA.bDelayAction = true;
                    pause.Controls(false);
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

            if (contSupp.ControllerRightJoystickVertical() > 0)
            {
                if (controlsMenu.GetChild(0).GetChild(0).GetComponent<RectTransform>().
                        offsetMin.y < 0)
                    controlsMenu.GetChild(0).GetChild(0).localPosition = new Vector3(
                        controlsMenu.GetChild(0).GetChild(0).localPosition.x,
                        controlsMenu.GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        controlsMenu.GetChild(0).GetChild(0).localPosition.z);
                else
                    controlsMenu.GetChild(0).GetChild(0).GetComponent<RectTransform>().
                        offsetMin = new Vector2(controlsMenu.GetChild(0).
                            GetChild(0).GetComponent<RectTransform>().offsetMin.x,
                            0);
            }
            else if (contSupp.ControllerRightJoystickVertical() < 0)
            {
                if (controlsMenu.GetChild(0).GetChild(0).GetComponent<RectTransform>().
                        offsetMax.y * -1f < 0)
                    controlsMenu.GetChild(0).GetChild(0).localPosition = new Vector3(
                        controlsMenu.GetChild(0).GetChild(0).localPosition.x,
                        controlsMenu.GetChild(0).GetChild(0).localPosition.y + 10 * contSupp.ControllerRightJoystickVertical(),
                        controlsMenu.GetChild(0).GetChild(0).localPosition.z);
                else
                    controlsMenu.GetChild(0).GetChild(0).GetComponent<RectTransform>().
                        offsetMax = new Vector2(controlsMenu.GetChild(0).
                            GetChild(0).GetComponent<RectTransform>().offsetMax.x,
                            0);
            }

            // Dev Support
            if (currentPosition == SelectorPosition.opacitySlider &&
                (Input.GetButton("Action") ||
                 contSupp.ControllerButtonPadBottom("hold") ||
                 (touches.bAvoidSubUIElements &&
                  touches.bUIactive)))
            {
                devSupportTimer -= 0.01f;

                if (devSupportTimer <= 0)
                {
                    bDevSupportActive = !bDevSupportActive;
                    ToggleDevSupport();
                }
            }
        }
    }

    public void ToggleDevSupport()
    {
        if (devSupport)
        {
            if (bDevSupportActive)
            {
                devSupport.transform.localScale = Vector3.one;
                controlsMenu.GetChild(1).GetComponent<Text>().color =
                    new Color(255f / 255f, 0 / 255f, 0 / 255f);
            }
            else
            {
                devSupport.transform.localScale = Vector3.zero;
                controlsMenu.GetChild(1).GetComponent<Text>().color =
                    new Color(255f / 255f, 255f / 255f, 255f / 255f);
            }

            scriptMan.ToggleDevSupport();
        }

        devSupportTimer = devSupportTimerInit;
    }

    public void HideSelectors()
    {
        showButtonsSelector.transform.localScale = Vector3.zero;
        buttonOpacitySelector.transform.localScale = Vector3.zero;
        dPadSelector.transform.localScale = Vector3.zero;
        vibrateSelector.transform.localScale = Vector3.zero;
        controlsDescriptionSelector.transform.localScale = Vector3.zero;
        backSelector.transform.localScale = Vector3.zero;
    }

    public void ClearAllSelectors()
    {
        if (controlsMenu.localScale == Vector3.one)
            HideSelectors();
    }

    public void ResetSelectors()
    {
        HideSelectors();

        showButtonsSelector.transform.localScale = Vector3.one;
        currentPosition = SelectorPosition.showButtons;
    }
}
