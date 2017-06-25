// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/25/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds NPC text (in Unity)
public class DialogueHolder : MonoBehaviour
{
    private DialogueManager dMan;

    public string dialogue;
    public string[] dialogueLines;

	void Start ()
    {
        dMan = FindObjectOfType<DialogueManager>();
	}

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                // Opens Dialogue Manager and uses NPC's first line
                if (!dMan.dialogueActive)
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
            }
        }
    }
}
