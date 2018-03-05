// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/03/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds NPC text (in Unity)
public class DialogueHolder : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    private TouchControls touches;

    public string dialogue;
    public string[] dialogueLines;

	void Start ()
    {
        anim = GetComponentInParent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        touches = FindObjectOfType<TouchControls>();
	}

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!dMan.bPauseDialogue && Input.GetButtonUp("Action") ||
                !dMan.bPauseDialogue && touches.bAction)
            {
                // Opens Dialogue Manager and uses NPC's first line
                if (!dMan.bDialogueActive)
                {
                    dMan.dialogueLines = dialogueLines;
                    dMan.currentLine = 0;
                    dMan.ShowDialogue();
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
            
            // Keeps NPC moving if no dialogue
            if (!dMan.bDialogueActive)
            {
                anim.Play("NPC Movement");
            }
        }
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
