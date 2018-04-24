// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/06/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds NPC text (in Unity)
public class DialogueHolder : MonoBehaviour
{
    private Animator anim;
    public Collider2D colliEnter;
    private DialogueManager dMan;
    public Sprite portPic;
    private TouchControls touches;

    public bool bHasEntered;
    public bool bHasExited;

    public string dialogue;
    public string[] dialogueLines;

	void Start ()
    {
        anim = GetComponentInParent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        touches = FindObjectOfType<TouchControls>();

        bHasEntered = false;
        bHasExited = true;
    }

    void Update ()
    {
        if ((bHasEntered && !bHasExited && !dMan.bDialogueActive && !dMan.bPauseDialogue && Input.GetButtonUp("Action")) ||
            (bHasEntered && !bHasExited && !dMan.bDialogueActive && !dMan.bPauseDialogue && touches.bAction))
        {
            TalkWithNPC(colliEnter);
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
            
            // Restores NPC movement
            if (!dMan.bDialogueActive)
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
            dMan.currentLine = 0;
            dMan.ShowDialogue();

            // Activates Options Holder if any options
            if (this.GetComponent<OptionsHolder>())
            {
                this.GetComponent<OptionsHolder>().PrepareOptions();
            }
        }

        // Stop NPC movement
        if (transform.parent.GetComponent<NPCMovement>() != null)
        {
            transform.parent.GetComponent<NPCMovement>().bCanMove = false;
        }

        // NPC looks at player
        OrientNPC(collision);

        // Stop UI controls / actions 
        touches.bAction = false;
    }

    void OrientNPC(Collider2D collision)
    {
        //// NPC above Player
        if ((transform.parent.position.y > collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Down");
        }
        //// NPC below Player
        else if ((transform.parent.position.y < collision.transform.position.y) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) >
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Up");
        }
        //// NPC to the right of Player
        else if ((transform.parent.position.x > collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Left");
        }
        //// NPC to the left of Player
        else if ((transform.parent.position.x < collision.transform.position.x) &&
            (Mathf.Abs((transform.parent.position.y - collision.transform.position.y)) <
                Mathf.Abs((transform.parent.position.x - collision.transform.position.x))))
        {
            anim.Play("Right");
        }
    }
}
