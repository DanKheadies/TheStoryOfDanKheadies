// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 01/29/2018
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the arrows on the Item Menu
public class MoveItemMenuArrow : MonoBehaviour
{
    public Button UseBtn;
    public Button DropBtn;
    public Button BackBtn;
    public ControllerSupport contSupp;
    public GameObject UseArw;
    public GameObject DropArw;
    public GameObject BackArw;
    public Joystick joystick;
    public MoveStuffMenuArrow moveSMA;
    public TouchControls touches;
    public Transform itemMenu;

    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bFreezeControllerInput;


    public enum ItemArrowPos : int
    {
        Use = -160,
        Drop = -50,
        Back = 65
    }

    public ItemArrowPos currentPosition;

    void Start()
    {
        // Initializers
        currentPosition = ItemArrowPos.Use;
    }

    void Update()
    {
        if (itemMenu.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            // Controller Support 
            if (contSupp.ControllerDirectionalPadHorizontal() == 0 &&
                contSupp.ControllerLeftJoystickHorizontal() == 0 &&
                joystick.Horizontal == 0 &&
                (!touches.bLeft &&
                 !touches.bRight))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() > 0 ||
                      joystick.Horizontal > 0 ||
                      touches.bRight))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     (contSupp.ControllerDirectionalPadHorizontal() < 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() < 0 ||
                      joystick.Horizontal < 0 ||
                      touches.bLeft))
            {
                bControllerLeft = true;
                bFreezeControllerInput = true;
            }
            
            if (Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                bControllerRight)
            {
                bControllerRight = false;

                if (currentPosition == ItemArrowPos.Use)
                {
                    currentPosition = ItemArrowPos.Drop;
                    ClearAllArrows();
                    DropArw.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
                else if (currentPosition == ItemArrowPos.Drop)
                {
                    currentPosition = ItemArrowPos.Back;
                    ClearAllArrows();
                    BackArw.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.LeftArrow) ||
                     bControllerLeft)
            {
                bControllerLeft = false;

                if (currentPosition == ItemArrowPos.Back)
                {
                    currentPosition = ItemArrowPos.Drop;
                    ClearAllArrows();
                    DropArw.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
                else if (currentPosition == ItemArrowPos.Drop)
                {
                    currentPosition = ItemArrowPos.Use;
                    ClearAllArrows();
                    UseArw.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
            }
            else if (Input.GetButtonDown("Action") ||
                     contSupp.ControllerButtonPadBottom("down") ||
                     touches.bAaction)
            {
                if (currentPosition == ItemArrowPos.Use)
                    UseBtn.onClick.Invoke();
                else if (currentPosition == ItemArrowPos.Drop)
                    DropBtn.onClick.Invoke();
                else if (currentPosition == ItemArrowPos.Back)
                    BackBtn.onClick.Invoke();

                moveSMA.bAllowSelection = false;
                moveSMA.bAvoidAllower = false;
                touches.bAaction = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) ||
                     contSupp.ControllerMenuRight("down") ||
                     contSupp.ControllerButtonPadRight("down") ||
                     Input.GetButton("BAction") ||
                     touches.bBaction)
            {
                ResetArrowPos();
            }
        }
    }

    public void HideSelectors()
    {
        UseArw.transform.localScale = Vector3.zero;
        DropArw.transform.localScale = Vector3.zero;
        BackArw.transform.localScale = Vector3.zero;
    }

    public void ClearAllArrows()
    {
        if (itemMenu.gameObject.GetComponent<CanvasGroup>().alpha == 1)
            HideSelectors();
    }

    public void ResetArrowPos()
    {
        HideSelectors();

        UseArw.transform.localScale = Vector3.one;
        currentPosition = ItemArrowPos.Use;
    } 
}
