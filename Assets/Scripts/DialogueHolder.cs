// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/23/2019

using UnityEngine;

// Holds NPC text (in Unity)
public class DialogueHolder : MonoBehaviour
{
    public Animator anim;
    public Collider2D colliEnter;
    public DialogueManager dMan;
    public PauseGame pause;
    public ScriptManager scriptMan;
    public Sprite portPic;
    public TouchControls touches;
    public UIManager uMan;

    public bool bActionOnClose;
    public bool bContinueDialogue;
    public bool bHasEntered;
    public bool bHasExited;

    public string action;
    public string[] dialogueLines;

	void Start ()
    {
        // Initializers
        bHasEntered = false;
        bHasExited = true;
    }

    void Update ()
    {
        if (bHasEntered && 
            !bHasExited && 
            !dMan.bDialogueActive && 
            !dMan.bPauseDialogue &&
            !pause.bPausing &&
            !pause.bPauseActive &&
            (touches.bAaction ||
             Input.GetButtonDown("Action") ||
             (Input.GetButtonDown("DialogueAction") &&
              !uMan.bControlsActive)))
        {
            TalkWithNPC(colliEnter);
        }
        else if (bContinueDialogue)
        {
            TalkWithNPC(colliEnter);
            bContinueDialogue = false;
        }
    } 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = true;
            bHasExited = false;

            colliEnter = collision;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bHasEntered = false;
            bHasExited = true;

            colliEnter = null;
            
            // Restores NPC movement if it has animation/animator
            if (!dMan.bDialogueActive && 
                anim)
            {
                anim.Play("NPC Movement");
            }
        }
    }

    void TalkWithNPC(Collider2D collision)
    {
        // Opens Dialogue Manager and uses NPC's first line
        if (!dMan.bDialogueActive)
        {
            dMan.portPic = portPic;
            dMan.dialogueLines = dialogueLines;
            dMan.ShowDialogue();

            // Activates Options Holder if any options
            if (GetComponent<OptionsHolder>())
            {
                GetComponent<OptionsHolder>().PrepareOptions();
            }
        }

        // Stop NPC movement
        if (transform.parent.GetComponent<NPCMovement>())
            transform.parent.GetComponent<NPCMovement>().bCanMove = false;

        // NPC looks at player if there's an animation/animator
        if (anim)
            OrientNPC(collision);

        // Stop UI controls / actions 
        touches.bAaction = false;

        // Run any actions
        scriptMan.DialogueAction(action);
        if (bActionOnClose)
            scriptMan.ActionOnClose(action);
    }

    public void OrientNPC(Collider2D collision)
    {
        // NPC above Player
        if ((transform.parent.position.y > collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Down");
        }
        // NPC below Player
        else if ((transform.parent.position.y < collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Up");
        }
        // NPC to the right of Player
        else if ((transform.parent.position.x > collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Left");
        }
        // NPC to the left of Player
        else if ((transform.parent.position.x < collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Right");
        }
    }
}
