// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  03/03/2018

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
    private TouchControls touches;

    public bool bAcquiring; // UX -- Prevents fast dialogue cycle (see below)
    public bool bAcquired; // Checks & Balances
    public bool bDoneAcquiring; // Checks & Balances
    public bool bHasBud;

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
        bDoneAcquiring = false;


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
        if (bAcquired && dMan.bDialogueActive)
        {
            bAcquired = false;
            bDoneAcquiring = true;
        }
        if (!dMan.bDialogueActive && !bAcquired && bDoneAcquiring)
        {
            HideBud();
            bDoneAcquiring = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if ((!dMan.bPauseDialogue && !bAcquiring && Input.GetButtonUp("Action")) ||
                (!dMan.bPauseDialogue && !bAcquiring && touches.bAction))
            {
                // bAcquiring prevents the dialogue from cycling too quickily, i.e. prevent "Has bud" & "No bud" in one 'click'
                bAcquiring = false;

                if (bHasBud &&
                    inv.items.Count < inv.totalItems)
                {
                    bAcquiring = true;

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
                    
                    dMan.dialogueLines = new string[HasBud.Length];
                    dMan.dialogueLines = HasBud;
                    dMan.ShowDialogue();

                    this.bHasBud = false;
                }
                else if (bHasBud &&
                    inv.items.Count >= inv.totalItems)
                {
                    string[] outOfSpace = { "Rats.. We have no more space for stuff." };
                    dMan.dialogueLines = outOfSpace;
                    dMan.ShowDialogue();
                }
                else
                {
                    dMan.dialogueLines = new string[NoBud.Length];
                    dMan.dialogueLines = NoBud;
                    dMan.ShowDialogue();
                }

                bAcquired = true;
                touches.bAction = false;
            }
        }
    }

    public void HideBud()
    {
        greenBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        orangeBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        purpleBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
        whiteBud.GetComponent<Transform>().localScale = new Vector3(0, 0, 0);
    }
}
