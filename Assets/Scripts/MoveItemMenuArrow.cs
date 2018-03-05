// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 01/29/2018
// Last:  01/29/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            if (Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                touches.bRight)
            {
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
                     touches.bLeft)
            {
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
            else if (Input.GetKeyDown(KeyCode.Space) ||
                     Input.GetKeyDown(KeyCode.Return) ||
                     touches.bAction)
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
            }
        }
    }

    public void ClearAllArrows()
    {
        if (itemMenu.gameObject.GetComponent<CanvasGroup>().alpha == 1)
        {
            UseArw.transform.localScale = new Vector3(0, 0, 0);
            DropArw.transform.localScale = new Vector3(0, 0, 0);
            BackArw.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
