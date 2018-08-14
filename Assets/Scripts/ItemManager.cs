﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  08/13/2018

using UnityEngine;
using UnityEngine.SceneManagement;

// Assigns plants Cannabis Objects
public class ItemManager : MonoBehaviour
{
    private Animator pAnim;
    private DialogueManager dMan;
    public GameObject player;
    public Inventory inv;
    public Item item;
    private Scene scene;
    public Sprite portPic;
    private TouchControls touches;

    private GameObject greenBud;
    private GameObject orangeBud;
    private GameObject purpleBud;
    private GameObject whiteBud;
    private GameObject vrGoggles;

    public bool bAcquired; // Checks & Balances
    public bool bDoneAcquiring; // Checks & Balances
    public bool bHasEntered;
    public bool bHasExited;

    public string[] AcquireItem;

    void Start()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        pAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();

        bAcquired = false;
        bDoneAcquiring = false;
        bHasEntered = false;
        bHasExited = true;

        // 04/07/2018 DC TODO -- Extract to individual scene files
        if (scene.name == "Chp0")
        {
            vrGoggles = GameObject.Find("VR.Goggles");
        }
        else if (scene.name == "Chp1")
        {
            greenBud = GameObject.Find("Cannabis.Bud.Green");
            orangeBud = GameObject.Find("Cannabis.Bud.Orange");
            purpleBud = GameObject.Find("Cannabis.Bud.Purple");
            whiteBud = GameObject.Find("Cannabis.Bud.White");
            vrGoggles = GameObject.Find("VR.Goggles");
        }
    }

    void Update()
    {
        if ((bHasEntered && !bHasExited && !dMan.bDialogueActive && !dMan.bPauseDialogue && Input.GetButtonUp("Action")) ||
            (bHasEntered && !bHasExited && !dMan.bDialogueActive && !dMan.bPauseDialogue && touches.bAaction))
        {
            InteractWithItem();
        }

        if (bAcquired && dMan.bDialogueActive)
        {
            bAcquired = false;
            bDoneAcquiring = true;
        }
        if (!dMan.bDialogueActive && !bAcquired && bDoneAcquiring)
        {
            HideItem();
            bDoneAcquiring = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = true;
            bHasExited = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = false;
            bHasExited = true;
        }
    }

    public void InteractWithItem()
    {
        if (inv.items.Count < inv.totalItems)
        {
            pAnim.Play("Acquire");

            // Display and add item to inventory
            if (this.name == "Cannabis.Bud.Green")
            {
                greenBud.GetComponent<Transform>().localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "Cannabis.Bud.Orange")
            {
                orangeBud.GetComponent<Transform>().localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "Cannabis.Bud.Purple")
            {
                purpleBud.GetComponent<Transform>().localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (this.name == "Cannabis.Bud.White")
            {
                whiteBud.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Inventory.instance.Add(item);
            }
            else if (this.name == "VR.Goggles")
            {
                vrGoggles.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Inventory.instance.Add(item);
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.transform.localScale = Vector2.zero;
            }

            dMan.portPic = portPic;
            dMan.dialogueLines = AcquireItem;
            dMan.ShowDialogue();
        }
        else if (inv.items.Count >= inv.totalItems)
        {
            dMan.portPic = portPic;
            string[] outOfSpace = { "Rats.. We have no more space for stuff." };
            dMan.dialogueLines = outOfSpace;
            dMan.ShowDialogue();
        }

        bAcquired = true;
        touches.bAaction = false;
    }

    public void HideItem()
    {
        if (scene.name == "Chp0")
        {
            vrGoggles.GetComponent<Transform>().localScale = Vector3.zero;
        }
        else if (scene.name == "Chp1")
        {
            greenBud.GetComponent<Transform>().localScale = Vector3.zero;
            orangeBud.GetComponent<Transform>().localScale = Vector3.zero;
            purpleBud.GetComponent<Transform>().localScale = Vector3.zero;
            whiteBud.GetComponent<Transform>().localScale = Vector3.zero;
            vrGoggles.GetComponent<Transform>().localScale = Vector3.zero;
        }
    }
}
