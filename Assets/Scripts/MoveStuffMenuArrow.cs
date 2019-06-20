// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 06/18/2019
// Last:  06/20/2019

using System.Collections;
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
    public Joystick joystick;
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
        itemMenu = GameObject.Find("ItemMenu").transform;
        joystick = FindObjectOfType<Joystick>();
        stuffMenu = GameObject.Find("StuffMenu").transform;
        touches = FindObjectOfType<TouchControls>();

        item1 = GameObject.Find("InventorySlot").transform.GetChild(0).GetComponent<Button>();
        item2 = GameObject.Find("InventorySlot (1)").transform.GetChild(0).GetComponent<Button>();
        item3 = GameObject.Find("InventorySlot (2)").transform.GetChild(0).GetComponent<Button>();
        item4 = GameObject.Find("InventorySlot (3)").transform.GetChild(0).GetComponent<Button>();
        item5 = GameObject.Find("InventorySlot (4)").transform.GetChild(0).GetComponent<Button>();
        item6 = GameObject.Find("InventorySlot (5)").transform.GetChild(0).GetComponent<Button>();
        item7 = GameObject.Find("InventorySlot (6)").transform.GetChild(0).GetComponent<Button>();
        item8 = GameObject.Find("InventorySlot (7)").transform.GetChild(0).GetComponent<Button>();
        item9 = GameObject.Find("InventorySlot (8)").transform.GetChild(0).GetComponent<Button>();
        item10 = GameObject.Find("InventorySlot (9)").transform.GetChild(0).GetComponent<Button>();
        item11 = GameObject.Find("InventorySlot (10)").transform.GetChild(0).GetComponent<Button>();
        item12 = GameObject.Find("InventorySlot (11)").transform.GetChild(0).GetComponent<Button>();
        item13 = GameObject.Find("InventorySlot (12)").transform.GetChild(0).GetComponent<Button>();
        item14 = GameObject.Find("InventorySlot (13)").transform.GetChild(0).GetComponent<Button>();
        item15 = GameObject.Find("InventorySlot (14)").transform.GetChild(0).GetComponent<Button>();
        item16 = GameObject.Find("InventorySlot (15)").transform.GetChild(0).GetComponent<Button>();
        item17 = GameObject.Find("InventorySlot (16)").transform.GetChild(0).GetComponent<Button>();
        item18 = GameObject.Find("InventorySlot (17)").transform.GetChild(0).GetComponent<Button>();
        item19 = GameObject.Find("InventorySlot (18)").transform.GetChild(0).GetComponent<Button>();
        item20 = GameObject.Find("InventorySlot (19)").transform.GetChild(0).GetComponent<Button>();

        itemSelector1 = GameObject.Find("ItemSelector1");
        itemSelector2 = GameObject.Find("ItemSelector2");
        itemSelector3 = GameObject.Find("ItemSelector3");
        itemSelector4 = GameObject.Find("ItemSelector4");
        itemSelector5 = GameObject.Find("ItemSelector5");
        itemSelector6 = GameObject.Find("ItemSelector6");
        itemSelector7 = GameObject.Find("ItemSelector7");
        itemSelector8 = GameObject.Find("ItemSelector8");
        itemSelector9 = GameObject.Find("ItemSelector9");
        itemSelector10 = GameObject.Find("ItemSelector10");
        itemSelector11 = GameObject.Find("ItemSelector11");
        itemSelector12 = GameObject.Find("ItemSelector12");
        itemSelector13 = GameObject.Find("ItemSelector13");
        itemSelector14 = GameObject.Find("ItemSelector14");
        itemSelector15 = GameObject.Find("ItemSelector15");
        itemSelector16 = GameObject.Find("ItemSelector16");
        itemSelector17 = GameObject.Find("ItemSelector17");
        itemSelector18 = GameObject.Find("ItemSelector18");
        itemSelector19 = GameObject.Find("ItemSelector19");
        itemSelector20 = GameObject.Find("ItemSelector20");

        currentPosition = SelectorPosition.item1;
    }
    
    void Update()
    {
        if (stuffMenu.localScale == Vector3.one &&
            itemMenu.GetComponent<CanvasGroup>().alpha == 0)
        {
            // Controller Support 
            if (Input.GetAxis("Controller DPad Vertical") == 0 &&
                Input.GetAxis("Controller Joystick Vertical") == 0 &&
                Input.GetAxis("Controller DPad Horizontal") == 0 &&
                Input.GetAxis("Controller Joystick Horizontal") == 0 &&
                joystick.Vertical == 0 &&
                joystick.Horizontal == 0 &&
                (!touches.bDown &&
                 !touches.bUp &&
                 !touches.bLeft &&
                 !touches.bRight))
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
            else if (!bFreezeControllerInput &&
                    (Input.GetAxis("Controller DPad Horizontal") > 0 ||
                     Input.GetAxis("Controller Joystick Horizontal") > 0 ||
                     joystick.Horizontal > 0 ||
                     touches.bRight))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                    (Input.GetAxis("Controller DPad Horizontal") < 0 ||
                     Input.GetAxis("Controller Joystick Horizontal") < 0 ||
                     joystick.Horizontal < 0 ||
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
                      Input.GetKeyDown(KeyCode.JoystickButton0) ||
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
                     Input.GetKeyDown(KeyCode.JoystickButton7) ||
                     Input.GetKeyDown(KeyCode.JoystickButton1) ||
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
