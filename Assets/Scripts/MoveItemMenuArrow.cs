// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 01/29/2018
// Last:  04/11/2019

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 10/04/2018 DC TODO -- Add a square around items for selection w/ arrow keys

// To "move" and execute the arrows on the Item Menu
public class MoveItemMenuArrow : MonoBehaviour
{
    private Button UseBtn;
    private Button DropBtn;
    private Button BackBtn;

    private GameObject UseArw;
    private GameObject DropArw;
    private GameObject BackArw;

    private Scene scene;

    private TouchControls touches;

    private Transform itemMenu;

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
        UseBtn = GameObject.Find("UseBtn").GetComponent<UnityEngine.UI.Button>();
        DropBtn = GameObject.Find("DropBtn").GetComponent<UnityEngine.UI.Button>();
        BackBtn = GameObject.Find("BackBtn").GetComponent<UnityEngine.UI.Button>();

        UseArw = GameObject.Find("UseArw");
        DropArw = GameObject.Find("DropArw");
        BackArw = GameObject.Find("BackArw");

        itemMenu = GameObject.Find("ItemMenu").transform;
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();
        
        currentPosition = ItemArrowPos.Use;
    }

    void Update()
    {
        if (itemMenu.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            // Controller Support 
            // DC TODO 01/10/2019 -- temp bug where sub-pause menus not closing as expected
            if (Input.GetAxis("Controller DPad Horizontal") == 0 &&
               (!touches.bLeft &&
                !touches.bRight))
            {
                bFreezeControllerInput = false;
            }
            else if (!bFreezeControllerInput &&
                    (Input.GetAxis("Controller DPad Horizontal") > 0 ||
                    touches.bDown))
            {
                bControllerRight = true;
                bFreezeControllerInput = true;
            }
            else if (!bFreezeControllerInput &&
                    (Input.GetAxis("Controller DPad Horizontal") < 0 ||
                    touches.bUp))
            {
                bControllerLeft = true;
                bFreezeControllerInput = true;
            }

            // DC TODO 01/10/2019 -- Finish for items
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
                     Input.GetKeyDown(KeyCode.JoystickButton0) ||
                     touches.bAaction)
            {
                if (currentPosition == ItemArrowPos.Use)
                {
                    UseBtn.onClick.Invoke();
                }
                else if (currentPosition == ItemArrowPos.Drop)
                {
                    DropBtn.onClick.Invoke();
                }
                else if (currentPosition == ItemArrowPos.Back)
                {
                    BackBtn.onClick.Invoke();
                }

                touches.bAaction = false;
            }
        }
    }

    public void ClearAllArrows()
    {
        if (itemMenu.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            UseArw.transform.localScale = Vector3.zero;
            DropArw.transform.localScale = Vector3.zero;
            BackArw.transform.localScale = Vector3.zero;
        }
    }

    public void ResetArrowPos()
    {
        DropArw.transform.localScale = Vector3.zero;
        BackArw.transform.localScale = Vector3.zero;

        UseArw.transform.localScale = Vector3.one;
        currentPosition = ItemArrowPos.Use;
    } 
}
