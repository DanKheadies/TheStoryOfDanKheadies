// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/30/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the Player's Brio
public class PlayerBrioManager : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    public Sprite portPic;

    public float diffMaxAndCurrent;
    public float playerMaxBrio;
    public float playerCurrentBrio;

    private string[] warningLines;

    void Start ()
    {
        // Initializers
        anim = GetComponent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();

        // Setting the Max Brio
        if (PlayerPrefs.GetInt("Saved") == 1)
        {
            playerMaxBrio = PlayerPrefs.GetFloat("BrioMax");
        }
        else
        {
            playerMaxBrio = 50;
        }

        // Give player full Brio if none
        if (playerCurrentBrio == 0)
        {
            playerCurrentBrio = playerMaxBrio;
        }

        // Set warning dialogue
        warningLines = new string[2];
        warningLines[0] = "Phew.. I am le tired...";
        warningLines[1] = "I may need a nap or something.";
	}
	
	void Update ()
    {
        // Warning Message when Brio is out
        if (playerCurrentBrio <= 0)
        {
            // Opens Dialogue Manager and gives a warning
            anim.SetBool("bIsWalking", false);
            dMan.dialogueLines = warningLines;
            dMan.portPic = portPic;
            dMan.ShowDialogue();
            playerCurrentBrio = 1;
        }
        
        BasicRestorePlayer();

        // Temp solution to give Brio
        if (Input.GetKeyUp(KeyCode.X))
        {
            RestorePlayer(50);
        }
    }

    // Removes Brio
    public void FatiguePlayer (float damageToGive)
    {
        playerCurrentBrio -= damageToGive;
    }

    // Adds Brio
    public void RestorePlayer (float brioToGive)
    {
        playerCurrentBrio += brioToGive;
    }

    // Adds Brio
    public void BasicRestorePlayer()
    {
        diffMaxAndCurrent = playerMaxBrio - playerCurrentBrio;
        if ((playerCurrentBrio < diffMaxAndCurrent) && !dMan.bDialogueActive)
        {
            playerCurrentBrio += 0.01f;
        }
    }

    // Return to Max Brio
    public void SetMaxBrio()
    {
        playerCurrentBrio = playerMaxBrio;
    }

    // Increase the Max Brio
    public void IncreaseMaxBrio(float increaseAmount)
    {
        playerMaxBrio = playerMaxBrio + increaseAmount;
    }
}
