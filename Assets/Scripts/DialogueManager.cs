// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 
public class DialogueManager : MonoBehaviour {

    public GameObject dbox;
    private PlayerMovement thePlayer;
    public Text dText;

    public bool dialogueActive;
    
    public int currentLine;

    public string[] dialogueLines;

	void Start ()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
	}
	

	void Update ()
    {
		if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }

        if (currentLine >= dialogueLines.Length)
        {
            dbox.SetActive(false);
            dialogueActive = false;

            currentLine = 0;
            Debug.Log("Starting");
            thePlayer.bStopPlayerMovement = false;
        }

        dText.text = dialogueLines[currentLine];
    }

    public void ShowDialogue()
    {
        // Displays the dialogue box
        dialogueActive = true;
        dbox.SetActive(true);

        // Stops the player's movement
        thePlayer.bStopPlayerMovement = true;
    }
}
