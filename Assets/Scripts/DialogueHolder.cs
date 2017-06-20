// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {

    public string dialogue;
    public string[] dialogueLines;

    private DialogueManager dMan;

	void Start () {
        dMan = FindObjectOfType<DialogueManager>();
	}
	

	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!dMan.dialogueActive)
                {
                    dMan.dialogueLines = dialogueLines;
                    dMan.currentLine = 0;
                    dMan.ShowDialogue();
                }

                if (transform.parent.GetComponent<NPCMovement>() != null)
                {
                    transform.parent.GetComponent<NPCMovement>().bCanMove = false;
                }
            }
        }
    }
}
