// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  08/17/2019

using UnityEngine;

// Assigns plants Cannabis Objects
public class CannabisPlant : MonoBehaviour
{
    public Animator playerAnim;
    public DialogueManager dMan;
    public GameObject greenBud;
    public GameObject orangeBud;
    public PauseGame pause;
    public GameObject player;
    public GameObject purpleBud;
    public GameObject whiteBud;
    public Inventory inv;
    public Item item;
    public Sprite portPic;
    public TouchControls touches;
    public UIManager uMan;

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
        //anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        //dMan = FindObjectOfType<DialogueManager>();
        //greenBud = GameObject.Find("Cannabis.Bud.Green");
        //inv = FindObjectOfType<Inventory>();
        //orangeBud = GameObject.Find("Cannabis.Bud.Orange");
        //pause = FindObjectOfType<PauseGame>();
        //player = GameObject.FindGameObjectWithTag("Player");
        //purpleBud = GameObject.Find("Cannabis.Bud.Purple");
        //touches = FindObjectOfType<TouchControls>();
        //uMan = FindObjectOfType<UIManager>();
        //whiteBud = GameObject.Find("Cannabis.Bud.White");

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
        if (bHasEntered && 
            !bHasExited &&
            !bFreeze &&
            !dMan.bDialogueActive && 
            !dMan.bPauseDialogue && 
            !pause.bPausing &&
            !pause.bPauseActive &&
            (touches.bAaction ||
             Input.GetButtonDown("Action") ||
             (Input.GetButtonDown("DialogueAction") &&
              !uMan.bControlsActive)))
        {
            InteractWithPlant();
        }

        if (bAcquired && 
            dMan.bDialogueActive)
        {
            bAcquired = false;
            bDoneAcquiring = true;
        }

        if (!dMan.bDialogueActive && 
            !bAcquired && 
            bDoneAcquiring)
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
        if (greenBud != null)
        {
            greenBud.transform.localScale = Vector3.zero;
        }
        else if (orangeBud != null)
        {
            orangeBud.transform.localScale = Vector3.zero;
        }
        else if (purpleBud != null)
        {
            purpleBud.transform.localScale = Vector3.zero;
        }
        else if (whiteBud != null)
        {
            whiteBud.transform.localScale = Vector3.zero;
        }
    }

    public void InteractWithPlant()
    {
        // bFreeze prevents the dialogue from cycling too quickily, i.e. prevent "Has bud" & "No bud" in one 'click'
        bFreeze = false;

        if (bHasBud &&
            inv.items.Count < inv.totalItems)
        {
            bFreeze = true;

            playerAnim.Play("Acquire");

            // Display and add bud to inventory
            if (bGreen)
            {
                greenBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (bOrange)
            {
                orangeBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);

            }
            else if (bPurple)
            {
                purpleBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }
            else if (bWhite)
            {
                whiteBud.transform.localScale = Vector3.one;
                Inventory.instance.Add(item);
            }

            dMan.portPic = portPic;
            dMan.dialogueLines = HasBud;
            dMan.ShowDialogue();

            bHasBud = false;
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
            dMan.dialogueLines = NoBud;
            dMan.ShowDialogue();
        }

        bAcquired = true;
        dMan.PauseDialogue();
        touches.bAaction = false;
    }
}


