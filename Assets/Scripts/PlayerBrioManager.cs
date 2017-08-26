// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  07/02/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the Player's Brio
public class PlayerBrioManager : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    private PlayerMovement thePlayer;

    public float diffMaxAndCurrent;
    public float playerMaxBrio;
    public float playerCurrentBrio;

    private string[] warningLines;

    void Start ()
    {
        anim = GetComponent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        thePlayer = FindObjectOfType<PlayerMovement>();

        if (playerCurrentBrio == 0)
        {
            playerCurrentBrio = playerMaxBrio;
        }

        warningLines = new string[2];
        warningLines[0] = "Phew.. I am le tired...";
        warningLines[1] = "I may need a nap or something.";
	}
	
	void Update ()
    {
        if (playerCurrentBrio <= 0)
        {
            // Opens Dialogue Manager and gives a warning
            anim.SetBool("bIsWalking", false);
            dMan.dialogueLines = warningLines;
            dMan.ShowDialogue();
            playerCurrentBrio = 1;
        }

        BasicRestorePlayer();

        if (Input.GetKeyUp(KeyCode.X))
        {
            RestorePlayer(50);
        }

    }

    public void FatiguePlayer (float damageToGive)
    {
        playerCurrentBrio -= damageToGive;
    }

    public void RestorePlayer (float brioToGive)
    {
        playerCurrentBrio += brioToGive;
    }

    public void BasicRestorePlayer()
    {
        diffMaxAndCurrent = playerMaxBrio - playerCurrentBrio;
        if ((playerCurrentBrio < diffMaxAndCurrent) && !dMan.dialogueActive)
        {
            playerCurrentBrio += 0.01f;
        }
    }

    public void SetMaxBrio()
    {
        playerCurrentBrio = playerMaxBrio;
    }
}
