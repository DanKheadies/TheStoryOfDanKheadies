﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  04/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assigns plants Cannabis Objects
public class CannabisPlant : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    private GameObject greenBud;
    private GameObject orangeBud;
    public GameObject player;
    private GameObject purpleBud;
    private GameObject whiteBud;
    public Inventory inv;
    public Item item;
    public Sprite portPic;
    private TouchControls touches;

    public bool bAcquired; // Checks & Balances
    public bool bDoneAcquiring; // Checks & Balances
    public bool bFreeze; // UX -- Prevents fast dialogue cycle (see below)
    public bool bHasBud;
    public bool bHasEntered;
    public bool bHasExited;

    public bool bGreen;
    public bool bOrange;
    public bool bPurple;
    public bool bWhite;

    public string[] HasBud;
    public string[] NoBud;
    public string[] outOfSpace;

    void Start()
    {
        // Initializers
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        greenBud = GameObject.Find("Cannabis.Bud.Green");
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        orangeBud = GameObject.Find("Cannabis.Bud.Orange");
        player = GameObject.FindGameObjectWithTag("Player");
        purpleBud = GameObject.Find("Cannabis.Bud.Purple");
        touches = FindObjectOfType<TouchControls>();
        whiteBud = GameObject.Find("Cannabis.Bud.White");

        bAcquired = false;
        bFreeze = false;
        bDoneAcquiring = false;
        bHasEntered = false;
        bHasExited = true;

        if (Random.Range(0.0f, 1.0f) > 0.66f)
        {
            bHasBud = true;
        }
        else
        {
            bHasBud = false;
        }
    }

    void Update()
    {
        if ((bHasEntered && !bHasExited && !dMan.bDialogueActive && !dMan.bPauseDialogue && !bFreeze && Input.GetButtonUp("Action")) ||
            (bHasEntered && !bHasExited && !dMan.bDialogueActive && !dMan.bPauseDialogue && !bFreeze && touches.bAction))
        {
            InteractWithPlant();
        }

        if (bAcquired && dMan.bDialogueActive)
        {
            bAcquired = false;
            bDoneAcquiring = true;
        }

        if (!dMan.bDialogueActive && !bAcquired && bDoneAcquiring)
        {
            HideBud();
            bDoneAcquiring = false;
            bFreeze = false;
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

    public void HideBud()
    {
        greenBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        orangeBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        purpleBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        whiteBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }

    public void InteractWithPlant()
    {
        // bFreeze prevents the dialogue from cycling too quickily, i.e. prevent "Has bud" & "No bud" in one 'click'
        bFreeze = false;

        if (bHasBud &&
            inv.items.Count < inv.totalItems)
        {
            bFreeze = true;

            anim.Play("Acquire");

            // Display and add bud to inventory
            if (this.bGreen)
            {
                greenBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                Inventory.instance.Add(item);
            }
            else if (this.bOrange)
            {
                orangeBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                Inventory.instance.Add(item);

            }
            else if (this.bPurple)
            {
                purpleBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
                Inventory.instance.Add(item);
            }
            else if (this.bWhite)
            {
                whiteBud.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Inventory.instance.Add(item);
            }

            dMan.portPic = portPic;
            dMan.dialogueLines = new string[HasBud.Length];
            dMan.dialogueLines = HasBud;
            dMan.ShowDialogue();

            this.bHasBud = false;
        }
        else if (bHasBud &&
            inv.items.Count >= inv.totalItems)
        {
            dMan.portPic = portPic;
            string[] outOfSpace = { "Rats.. We have no more space for stuff." };
            dMan.dialogueLines = outOfSpace;
            dMan.ShowDialogue();
        }
        else
        {
            dMan.portPic = portPic;
            dMan.dialogueLines = new string[NoBud.Length];
            dMan.dialogueLines = NoBud;
            dMan.ShowDialogue();
        }

        bAcquired = true;
        touches.bAction = false;
    }
}


