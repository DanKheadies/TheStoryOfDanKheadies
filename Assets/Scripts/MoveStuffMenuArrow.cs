// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 06/18/2019
// Last:  02/25/2020

using UnityEngine;
using UnityEngine.UI;

// To "move" and execute the arrows on the Pause Menu
public class MoveStuffMenuArrow : MonoBehaviour
{
    public Button item1;
    public Button item2;
    public Button item3;
    public Button item4;
    public Button item5;
    public Button item6;
    public Button item7;
    public Button item8;
    public Button item9;
    public Button item10;
    public Button item11;
    public Button item12;
    public Button item13;
    public Button item14;
    public Button item15;
    public Button item16;
    public Button item17;
    public Button item18;
    public Button item19;
    public Button item20;
    public ControllerSupport contSupp;
    public GameObject itemSelector1;
    public GameObject itemSelector2;
    public GameObject itemSelector3;
    public GameObject itemSelector4;
    public GameObject itemSelector5;
    public GameObject itemSelector6;
    public GameObject itemSelector7;
    public GameObject itemSelector8;
    public GameObject itemSelector9;
    public GameObject itemSelector10;
    public GameObject itemSelector11;
    public GameObject itemSelector12;
    public GameObject itemSelector13;
    public GameObject itemSelector14;
    public GameObject itemSelector15;
    public GameObject itemSelector16;
    public GameObject itemSelector17;
    public GameObject itemSelector18;
    public GameObject itemSelector19;
    public GameObject itemSelector20;
    public FixedJoystick fixedJoystick;
    public TouchControls touches;
    public Transform itemMenu;
    public Transform stuffMenu; 

    public bool bAllowSelection;
    public bool bAvoidAllower;
    public bool bControllerDown;
    public bool bControllerLeft;
    public bool bControllerRight;
    public bool bControllerUp;
    public bool bFreezeControllerInput;

    public enum SelectorPosition : int
    {
        item1 = 1,
        item2 = 2,
        item3 = 3,
        item4 = 4,
        item5 = 5,
        item6 = 6,
        item7 = 7,
        item8 = 8,
        item9 = 9,
        item10 = 10,
        item11 = 11,
        item12 = 12,
        item13 = 13,
        item14 = 14,
        item15 = 15,
        item16 = 16,
        item17 = 17,
        item18 = 18,
        item19 = 19,
        item20 = 20
    }

    public SelectorPosition currentPosition;

    void Start()
    {
        // Initializers
        currentPosition = SelectorPosition.item1;
    }
    
    void Update()
    {
        if (stuffMenu.localScale == Vector3.one &&
            itemMenu.GetComponent<CanvasGroup>().alpha == 0)
        {
            // Controller Support 
            //if (Input.GetAxis("Controller DPad Vertical") == 0 &&
            //    Input.GetAxis("Controller Joystick Vertical") == 0 &&
            //    Input.GetAxis("Controller DPad Horizontal") == 0 &&
            //    Input.GetAxis("Controller Joystick Horizontal") == 0 &&
            //if (contSupp.ControllerLeftJoystickHorizontal() == 0 &&
            //    contSupp.ControllerLeftJoystickVertical() == 0 &&
            //    contSupp.ControllerDirectionalPadHorizontal() == 0 &&
            //    contSupp.ControllerDirectionalPadVertical() == 0 &&
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
                     //(Input.GetAxis("Controller DPad Vertical") > 0 ||
                     // Input.GetAxis("Controller Joystick Vertical") < 0 ||
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
                     //(Input.GetAxis("Controller DPad Vertical") < 0 ||
                     // Input.GetAxis("Controller Joystick Vertical") > 0 ||
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
                     //(Input.GetAxis("Controller DPad Horizontal") > 0 ||
                     // Input.GetAxis("Controller Joystick Horizontal") > 0 ||
                     (contSupp.ControllerDirectionalPadHorizontal() > 0 ||
                      contSupp.ControllerLeftJoystickHorizontal() > 0 ||
                      fixedJoystick.Horizontal > 0 ||
                      touches.bRight))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                     //(Input.GetAxis("Controller DPad Horizontal") < 0 ||
                     // Input.GetAxis("Controller Joystick Horizontal") < 0 ||
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
                
                if (currentPosition == SelectorPosition.item1)
                {
                    currentPosition = SelectorPosition.item6;
                    ClearAllSelectors();
                    itemSelector6.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item2)
                {
                    currentPosition = SelectorPosition.item7;
                    ClearAllSelectors();
                    itemSelector7.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item3)
                {
                    currentPosition = SelectorPosition.item8;
                    ClearAllSelectors();
                    itemSelector8.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item4)
                {
                    currentPosition = SelectorPosition.item9;
                    ClearAllSelectors();
                    itemSelector9.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item5)
                {
                    currentPosition = SelectorPosition.item10;
                    ClearAllSelectors();
                    itemSelector10.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item6)
                {
                    currentPosition = SelectorPosition.item11;
                    ClearAllSelectors();
                    itemSelector11.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item7)
                {
                    currentPosition = SelectorPosition.item12;
                    ClearAllSelectors();
                    itemSelector12.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item8)
                {
                    currentPosition = SelectorPosition.item13;
                    ClearAllSelectors();
                    itemSelector13.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item9)
                {
                    currentPosition = SelectorPosition.item14;
                    ClearAllSelectors();
                    itemSelector14.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item10)
                {
                    currentPosition = SelectorPosition.item15;
                    ClearAllSelectors();
                    itemSelector15.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item11)
                {
                    currentPosition = SelectorPosition.item16;
                    ClearAllSelectors();
                    itemSelector16.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item12)
                {
                    currentPosition = SelectorPosition.item17;
                    ClearAllSelectors();
                    itemSelector17.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item13)
                {
                    currentPosition = SelectorPosition.item18;
                    ClearAllSelectors();
                    itemSelector18.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item14)
                {
                    currentPosition = SelectorPosition.item19;
                    ClearAllSelectors();
                    itemSelector19.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item15)
                {
                    currentPosition = SelectorPosition.item20;
                    ClearAllSelectors();
                    itemSelector20.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item16)
                {
                    currentPosition = SelectorPosition.item1;
                    ClearAllSelectors();
                    itemSelector1.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item17)
                {
                    currentPosition = SelectorPosition.item2;
                    ClearAllSelectors();
                    itemSelector2.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item18)
                {
                    currentPosition = SelectorPosition.item3;
                    ClearAllSelectors();
                    itemSelector3.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item19)
                {
                    currentPosition = SelectorPosition.item4;
                    ClearAllSelectors();
                    itemSelector4.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item20)
                {
                    currentPosition = SelectorPosition.item5;
                    ClearAllSelectors();
                    itemSelector5.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) ||
                     Input.GetKeyDown(KeyCode.UpArrow) ||
                     bControllerUp)
            {
                bControllerUp = false;
                
                if (currentPosition == SelectorPosition.item1)
                {
                    currentPosition = SelectorPosition.item16;
                    ClearAllSelectors();
                    itemSelector16.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item2)
                {
                    currentPosition = SelectorPosition.item17;
                    ClearAllSelectors();
                    itemSelector17.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item3)
                {
                    currentPosition = SelectorPosition.item18;
                    ClearAllSelectors();
                    itemSelector18.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item4)
                {
                    currentPosition = SelectorPosition.item19;
                    ClearAllSelectors();
                    itemSelector19.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item5)
                {
                    currentPosition = SelectorPosition.item20;
                    ClearAllSelectors();
                    itemSelector20.transform.localScale = Vector3.one;
                }


                else if (currentPosition == SelectorPosition.item6)
                {
                    currentPosition = SelectorPosition.item1;
                    ClearAllSelectors();
                    itemSelector1.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item7)
                {
                    currentPosition = SelectorPosition.item2;
                    ClearAllSelectors();
                    itemSelector2.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item8)
                {
                    currentPosition = SelectorPosition.item3;
                    ClearAllSelectors();
                    itemSelector3.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item9)
                {
                    currentPosition = SelectorPosition.item4;
                    ClearAllSelectors();
                    itemSelector4.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item10)
                {
                    currentPosition = SelectorPosition.item5;
                    ClearAllSelectors();
                    itemSelector5.transform.localScale = Vector3.one;
                }


                else if (currentPosition == SelectorPosition.item11)
                {
                    currentPosition = SelectorPosition.item6;
                    ClearAllSelectors();
                    itemSelector6.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item12)
                {
                    currentPosition = SelectorPosition.item7;
                    ClearAllSelectors();
                    itemSelector7.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item13)
                {
                    currentPosition = SelectorPosition.item8;
                    ClearAllSelectors();
                    itemSelector8.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item14)
                {
                    currentPosition = SelectorPosition.item9;
                    ClearAllSelectors();
                    itemSelector9.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item15)
                {
                    currentPosition = SelectorPosition.item10;
                    ClearAllSelectors();
                    itemSelector10.transform.localScale = Vector3.one;
                }


                else if (currentPosition == SelectorPosition.item16)
                {
                    currentPosition = SelectorPosition.item11;
                    ClearAllSelectors();
                    itemSelector11.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item17)
                {
                    currentPosition = SelectorPosition.item12;
                    ClearAllSelectors();
                    itemSelector12.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item18)
                {
                    currentPosition = SelectorPosition.item13;
                    ClearAllSelectors();
                    itemSelector13.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item19)
                {
                    currentPosition = SelectorPosition.item14;
                    ClearAllSelectors();
                    itemSelector14.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item20)
                {
                    currentPosition = SelectorPosition.item15;
                    ClearAllSelectors();
                    itemSelector15.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A) ||
                     Input.GetKeyDown(KeyCode.LeftArrow) ||
                     bControllerLeft)
            {
                bControllerLeft = false;

                if (currentPosition == SelectorPosition.item1)
                {
                    currentPosition = SelectorPosition.item20;
                    ClearAllSelectors();
                    itemSelector20.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item2)
                {
                    currentPosition = SelectorPosition.item1;
                    ClearAllSelectors();
                    itemSelector1.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item3)
                {
                    currentPosition = SelectorPosition.item2;
                    ClearAllSelectors();
                    itemSelector2.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item4)
                {
                    currentPosition = SelectorPosition.item3;
                    ClearAllSelectors();
                    itemSelector3.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item5)
                {
                    currentPosition = SelectorPosition.item4;
                    ClearAllSelectors();
                    itemSelector4.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item6)
                {
                    currentPosition = SelectorPosition.item5;
                    ClearAllSelectors();
                    itemSelector5.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item7)
                {
                    currentPosition = SelectorPosition.item6;
                    ClearAllSelectors();
                    itemSelector6.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item8)
                {
                    currentPosition = SelectorPosition.item7;
                    ClearAllSelectors();
                    itemSelector7.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item9)
                {
                    currentPosition = SelectorPosition.item8;
                    ClearAllSelectors();
                    itemSelector8.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item10)
                {
                    currentPosition = SelectorPosition.item9;
                    ClearAllSelectors();
                    itemSelector9.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item11)
                {
                    currentPosition = SelectorPosition.item10;
                    ClearAllSelectors();
                    itemSelector10.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item12)
                {
                    currentPosition = SelectorPosition.item11;
                    ClearAllSelectors();
                    itemSelector11.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item13)
                {
                    currentPosition = SelectorPosition.item12;
                    ClearAllSelectors();
                    itemSelector12.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item14)
                {
                    currentPosition = SelectorPosition.item13;
                    ClearAllSelectors();
                    itemSelector13.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item15)
                {
                    currentPosition = SelectorPosition.item14;
                    ClearAllSelectors();
                    itemSelector14.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item16)
                {
                    currentPosition = SelectorPosition.item15;
                    ClearAllSelectors();
                    itemSelector15.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item17)
                {
                    currentPosition = SelectorPosition.item16;
                    ClearAllSelectors();
                    itemSelector16.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item18)
                {
                    currentPosition = SelectorPosition.item17;
                    ClearAllSelectors();
                    itemSelector17.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item19)
                {
                    currentPosition = SelectorPosition.item18;
                    ClearAllSelectors();
                    itemSelector18.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item20)
                {
                    currentPosition = SelectorPosition.item19;
                    ClearAllSelectors();
                    itemSelector19.transform.localScale = Vector3.one;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D) ||
                     Input.GetKeyDown(KeyCode.RightArrow) ||
                     bControllerRight)
            {
                bControllerRight = false;

                if (currentPosition == SelectorPosition.item1)
                {
                    currentPosition = SelectorPosition.item2;
                    ClearAllSelectors();
                    itemSelector2.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item2)
                {
                    currentPosition = SelectorPosition.item3;
                    ClearAllSelectors();
                    itemSelector3.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item3)
                {
                    currentPosition = SelectorPosition.item4;
                    ClearAllSelectors();
                    itemSelector4.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item4)
                {
                    currentPosition = SelectorPosition.item5;
                    ClearAllSelectors();
                    itemSelector5.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item5)
                {
                    currentPosition = SelectorPosition.item6;
                    ClearAllSelectors();
                    itemSelector6.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item6)
                {
                    currentPosition = SelectorPosition.item7;
                    ClearAllSelectors();
                    itemSelector7.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item7)
                {
                    currentPosition = SelectorPosition.item8;
                    ClearAllSelectors();
                    itemSelector8.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item8)
                {
                    currentPosition = SelectorPosition.item9;
                    ClearAllSelectors();
                    itemSelector9.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item9)
                {
                    currentPosition = SelectorPosition.item10;
                    ClearAllSelectors();
                    itemSelector10.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item10)
                {
                    currentPosition = SelectorPosition.item11;
                    ClearAllSelectors();
                    itemSelector11.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item11)
                {
                    currentPosition = SelectorPosition.item12;
                    ClearAllSelectors();
                    itemSelector12.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item12)
                {
                    currentPosition = SelectorPosition.item13;
                    ClearAllSelectors();
                    itemSelector13.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item13)
                {
                    currentPosition = SelectorPosition.item14;
                    ClearAllSelectors();
                    itemSelector14.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item14)
                {
                    currentPosition = SelectorPosition.item15;
                    ClearAllSelectors();
                    itemSelector15.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item15)
                {
                    currentPosition = SelectorPosition.item16;
                    ClearAllSelectors();
                    itemSelector16.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item16)
                {
                    currentPosition = SelectorPosition.item17;
                    ClearAllSelectors();
                    itemSelector17.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item17)
                {
                    currentPosition = SelectorPosition.item18;
                    ClearAllSelectors();
                    itemSelector18.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item18)
                {
                    currentPosition = SelectorPosition.item19;
                    ClearAllSelectors();
                    itemSelector19.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item19)
                {
                    currentPosition = SelectorPosition.item20;
                    ClearAllSelectors();
                    itemSelector20.transform.localScale = Vector3.one;
                }
                else if (currentPosition == SelectorPosition.item20)
                {
                    currentPosition = SelectorPosition.item1;
                    ClearAllSelectors();
                    itemSelector1.transform.localScale = Vector3.one;
                }
            }
            else if (bAllowSelection &&
                     (Input.GetButtonDown("Action") ||
                      //Input.GetKeyDown(KeyCode.JoystickButton0) ||
                      contSupp.ControllerButtonPadBottom("down") ||
                      touches.bAaction))
            {
                if (currentPosition == SelectorPosition.item1)
                {
                    item1.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item2)
                {
                    item2.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item3)
                {
                    item3.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item4)
                {
                    item4.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item5)
                {
                    item5.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item6)
                {
                    item6.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item7)
                {
                    item7.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item8)
                {
                    item8.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item9)
                {
                    item9.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item10)
                {
                    item10.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item11)
                {
                    item11.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item12)
                {
                    item12.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item13)
                {
                    item13.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item14)
                {
                    item14.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item15)
                {
                    item15.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item16)
                {
                    item16.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item17)
                {
                    item17.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item18)
                {
                    item18.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item19)
                {
                    item19.onClick.Invoke();
                }
                else if (currentPosition == SelectorPosition.item20)
                {
                    item20.onClick.Invoke();
                }

                //bAvoidAllower = false;
                touches.bAaction = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) ||
                     //Input.GetKeyDown(KeyCode.JoystickButton7) ||
                     //Input.GetKeyDown(KeyCode.JoystickButton1) ||
                     contSupp.ControllerMenuRight("down") ||
                     contSupp.ControllerButtonPadRight("down") ||
                     Input.GetButton("BAction") ||
                     touches.bBaction)
            {
                ResetSelectors();
            }

            if (!bAvoidAllower)
            {
                bAvoidAllower = true;
                bAllowSelection = true;
            }
        }
    }

    public void StuffMenuReset()
    {
        bAvoidAllower = false;
        bAllowSelection = false;
    }

    public void ClearAllSelectors()
    {
        if (stuffMenu.localScale == Vector3.one)
        {
            itemSelector1.transform.localScale = Vector3.zero;
            itemSelector2.transform.localScale = Vector3.zero;
            itemSelector3.transform.localScale = Vector3.zero;
            itemSelector4.transform.localScale = Vector3.zero;
            itemSelector5.transform.localScale = Vector3.zero;
            itemSelector6.transform.localScale = Vector3.zero;
            itemSelector7.transform.localScale = Vector3.zero;
            itemSelector8.transform.localScale = Vector3.zero;
            itemSelector9.transform.localScale = Vector3.zero;
            itemSelector10.transform.localScale = Vector3.zero;
            itemSelector11.transform.localScale = Vector3.zero;
            itemSelector12.transform.localScale = Vector3.zero;
            itemSelector13.transform.localScale = Vector3.zero;
            itemSelector14.transform.localScale = Vector3.zero;
            itemSelector15.transform.localScale = Vector3.zero;
            itemSelector16.transform.localScale = Vector3.zero;
            itemSelector17.transform.localScale = Vector3.zero;
            itemSelector18.transform.localScale = Vector3.zero;
            itemSelector19.transform.localScale = Vector3.zero;
            itemSelector20.transform.localScale = Vector3.zero;
        }
    }

    public void ResetSelectors()
    {
        itemSelector2.transform.localScale = Vector3.zero;
        itemSelector3.transform.localScale = Vector3.zero;
        itemSelector4.transform.localScale = Vector3.zero;
        itemSelector5.transform.localScale = Vector3.zero;
        itemSelector6.transform.localScale = Vector3.zero;
        itemSelector7.transform.localScale = Vector3.zero;
        itemSelector8.transform.localScale = Vector3.zero;
        itemSelector9.transform.localScale = Vector3.zero;
        itemSelector10.transform.localScale = Vector3.zero;
        itemSelector11.transform.localScale = Vector3.zero;
        itemSelector12.transform.localScale = Vector3.zero;
        itemSelector13.transform.localScale = Vector3.zero;
        itemSelector14.transform.localScale = Vector3.zero;
        itemSelector15.transform.localScale = Vector3.zero;
        itemSelector16.transform.localScale = Vector3.zero;
        itemSelector17.transform.localScale = Vector3.zero;
        itemSelector18.transform.localScale = Vector3.zero;
        itemSelector19.transform.localScale = Vector3.zero;
        itemSelector20.transform.localScale = Vector3.zero;

        itemSelector1.transform.localScale = Vector3.one;
        currentPosition = SelectorPosition.item1;

        bAvoidAllower = false;
        bAllowSelection = false;
    }
}
