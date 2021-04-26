// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/18/2017
// Last:  02/24/2020

using UnityEngine;

// Assigns plants Cannabis Objects
public class CannabisPlant : MonoBehaviour
{
    public Animator playerAnim;
    public ControllerSupport contSupp;
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
        bHasExited = true;

        if (Random.Range(0.0f, 1.0f) > 0.66f)
            bHasBud = true;
        else
            bHasBud = false;
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
             contSupp.ControllerButtonPadBottom("down") ||
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

        if (bDoneAcquiring &&
            !dMan.bDialogueActive && 
            !bAcquired)
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
        if (greenBud)
        {
            greenBud.transform.localScale = Vector3.zero;
        }
        else if (orangeBud)
        {
            orangeBud.transform.localScale = Vector3.zero;
        }
        else if (purpleBud)
        {
            purpleBud.transform.localScale = Vector3.zero;
        }
        else if (whiteBud)
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


