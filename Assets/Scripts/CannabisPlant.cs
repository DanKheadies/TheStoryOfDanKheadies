// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  01/21/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Assigns plants Cannabis Objects
public class CannabisPlant : MonoBehaviour
{
    private Animator anim;
    private DialogueManager theDM;
    private GameObject greenBud;
    private GameObject orangeBud;
    public GameObject player;
    private GameObject purpleBud;
    private GameObject whiteBud;
    public Inventory inv;
    public Item item;
    private TouchControls touches;

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
        greenBud = GameObject.Find("Cannabis.Bud.Green");
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        orangeBud = GameObject.Find("Cannabis.Bud.Orange");
        player = GameObject.FindGameObjectWithTag("Player");
        purpleBud = GameObject.Find("Cannabis.Bud.Purple");
        theDM = FindObjectOfType<DialogueManager>();
        touches = FindObjectOfType<TouchControls>();
        whiteBud = GameObject.Find("Cannabis.Bud.White");


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

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.Space) ||
                touches.bAction)
            {
                if (bHasBud &&
                    inv.items.Count < inv.totalItems)
                {
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
                    
                    theDM.dialogueLines = new string[HasBud.Length];
                    theDM.dialogueLines = HasBud;
                    theDM.ShowDialogue();

                    this.bHasBud = false;
                }
                else if (bHasBud &&
                    inv.items.Count >= inv.totalItems)
                {
                    string[] outOfSpace = { "Rats.. We have no more space for stuff." };
                    theDM.dialogueLines = outOfSpace;
                    theDM.ShowDialogue();
                }
                else
                {
                    theDM.dialogueLines = new string[NoBud.Length];
                    theDM.dialogueLines = NoBud;
                    theDM.ShowDialogue();
                }

                touches.bAction = false;
                
            }
        }
    }

    void DisplayBud()
    {
        if (this.bGreen)
        {
            greenBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (this.bOrange)
        {
            orangeBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (this.bPurple)
        {
            purpleBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (this.bWhite)
        {
            whiteBud.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
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
