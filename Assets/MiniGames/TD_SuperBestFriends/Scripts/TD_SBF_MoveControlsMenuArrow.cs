// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 05/06/2020
// Last:  05/06/2020

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the arrows on the TD_SBF_Controls Menu
public class TD_SBF_MoveControlsMenuArrow : MonoBehaviour
{
    public ControllerSupport contSupp;
    public FixedJoystick fixedJoystickLeft;
    public FixedJoystick fixedJoystickRight;
    public GameObject backSelector;
    public GameObject buttonOpacitySelector;
    public GameObject controlsDescriptionSelector;
    public GameObject vibrateSelector;
    public ScriptManager scriptMan;
    public TD_SBF_MovePauseMenuSelector movePMA;
    public TD_SBF_PauseMenu pause;
    public TD_SBF_TouchControls touches;
    public Toggle vibrateToggle;
    public Transform controlsMenu;

    public bool bControllerDown;
    public bool bControllerDownSecondary;
    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bControllerUp;
    public bool bControllerUpSecondary;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        opacitySlider = 1,
        vibrate = 2,
        description = 3,
        back = 4
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        currentPosition = SelectorPosition.opacitySlider;
    }

    // Update is called once per frame
    void Update()
    {
        if (controlsMenu.localScale == Vector3.one)
        {
            // Controller Support 
            if (!contSupp.bIsMoving &&
                fixedJoystickLeft.Vertical == 0 &&
                fixedJoystickLeft.Horizontal == 0)
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() < 0 ||
                      contSupp.ControllerLeftJoystickVertical() < 0 ||
                      (Mathf.Abs(fixedJoystickLeft.Vertical) > Mathf.Abs(fixedJoystickLeft.Horizontal) &&
                       fixedJoystickLeft.Vertical < 0)))
            {
                bControllerDown = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadVertical() > 0 ||
                      contSupp.ControllerLeftJoystickVertical() > 0 ||
                      (Mathf.Abs(fixedJoystickRight.Vertical) > Mathf.Abs(fixedJoystickRight.Horizontal) &&
                       fixedJoystickRight.Vertical > 0)))
            {
                bControllerUp = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() > 0 ||
                      fixedJoystickLeft.Horizontal > 0))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadHorizontal() < 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() < 0 ||
                      fixedJoystickLeft.Horizontal < 0))
            {
                bControllerLeft = true;
                bFreezeControllerInput = true;
            }

            if (Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.DownArrow) ||
                bControllerDown)
            {
                bControllerDown = false;

                if (currentPosition == SelectorPosition.opacitySlider)
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
                    currentPosition = SelectorPosition.opacitySlider;
                    ClearAllSelectors();
                    buttonOpacitySelector.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;

                if (currentPosition == SelectorPosition.opacitySlider)
                {
                    currentPosition = SelectorPosition.back;
                    ClearAllSelectors();
                    backSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.vibrate)
                {
                    currentPosition = SelectorPosition.opacitySlider;
                    ClearAllSelectors();
                    buttonOpacitySelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.description)
                {
                    currentPosition = SelectorPosition.vibrate;
                    ClearAllSelectors();
                    vibrateSelector.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    currentPosition = SelectorPosition.description;
                    ClearAllSelectors();
                    controlsDescriptionSelector.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.LeftArrow) ||
                     bControllerLeft)
            {
                bControllerLeft = false;

                if (currentPosition == SelectorPosition.opacitySlider)
                    touches.DecreaseOpacity();
            }
            else if (Input.GetKeyDown(KeyCode.D) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     bControllerRight)
            {
                bControllerRight = false;

                if (currentPosition == SelectorPosition.opacitySlider)
                    touches.IncreaseOpacity();
            }
            else if (Input.GetButtonDown("Action") ||
                     contSupp.ControllerButtonPadBottom("down"))
            {
                if (currentPosition == SelectorPosition.vibrate)
                {
                    vibrateToggle.isOn = !vibrateToggle.isOn;
                    touches.ToggleVibrate();
                    touches.Vibrate();
                }
                else if (currentPosition == SelectorPosition.back)
                {
                    ResetSelectors();
                    //movePMA.bDelayAction = true; // DC TODO
                    pause.ToggleControls();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape) ||
                     contSupp.ControllerMenuRight("down") ||
                     contSupp.ControllerButtonPadRight("down") ||
                     Input.GetButton("BAction"))
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
        }
    }

    public void HideSelectors()
    {
        buttonOpacitySelector.transform.localScale = Vector3.zero;
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

        buttonOpacitySelector.transform.localScale = Vector3.one;
        currentPosition = SelectorPosition.opacitySlider;
    }
}
