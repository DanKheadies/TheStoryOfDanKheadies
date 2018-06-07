// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Manage the Player's Brio
public class PlayerBrioManager : MonoBehaviour
{
    private Animator anim;
    private DialogueManager dMan;
    public GameObject pause;
    public Scene scene;
    public Sprite portPic;
    public UIManager uiMan;

    public float diffMaxAndCurrent;
    public float playerMaxBrio;
    public float playerCurrentBrio;

    private string[] warningLines;

    void Start ()
    {
        // Initializers
        anim = GetComponent<Animator>();
        dMan = FindObjectOfType<DialogueManager>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        scene = SceneManager.GetActiveScene();
        uiMan = FindObjectOfType<UIManager>();

        // Setting the brio
        if (PlayerPrefs.GetInt("Transferring") == 1)
        {
            playerMaxBrio = PlayerPrefs.GetFloat("TransferBrioMax");
            playerCurrentBrio = PlayerPrefs.GetFloat("TransferBrio");
        }
        else if (PlayerPrefs.GetInt("Saved") == 1)
        {
            playerMaxBrio = PlayerPrefs.GetFloat("BrioMax");
            playerCurrentBrio = PlayerPrefs.GetFloat("Brio");
        }
        else
        {
            playerMaxBrio = 50;
            playerCurrentBrio = 50;
        }

        // Set warning dialogue
        warningLines = new string[2];
        warningLines[0] = "Phew.. I am le tired...";
        warningLines[1] = "I may need a nap or something.";
	}
	
	void Update ()
    {
        // Warning Message when Brio is out
        // DC 04/16/2018 -- Runs and hides at the start; should stop?
        if (playerCurrentBrio <= 0)
        {
            // Opens Dialogue Manager and gives a warning
            anim.SetBool("bIsWalking", false);
            dMan.dialogueLines = warningLines;
            dMan.portPic = portPic;
            dMan.ShowDialogue();
            playerCurrentBrio = 1;
        }
        
        // Restore on certain scenes / conditions
        if (pause.transform.localScale == Vector3.one ||
            dMan.bDialogueActive ||
            GameObject.FindObjectOfType<SceneTransitioner>().bAnimationToTransitionScene == true)
        {
            // Avoid basic restore
        }
        else if (scene.name == "Chp1")
        {
            BasicRestorePlayer();
        }

        // Temp solution to give Brio
        if (Input.GetKeyUp(KeyCode.X))
        {
            RestorePlayer(50);
            uiMan.bUpdateBrio = true;
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

    // Adds Brio once below half (temp algo)
    public void BasicRestorePlayer()
    {
        diffMaxAndCurrent = playerMaxBrio - playerCurrentBrio;

        if ((playerCurrentBrio < diffMaxAndCurrent) && !dMan.bDialogueActive)
        {
            playerCurrentBrio += 0.01f;
            uiMan.bUpdateBrio = true;
        }
    }

    // Increase the Max Brio
    public void IncreaseMaxBrio(float increaseAmount)
    {
        playerMaxBrio = playerMaxBrio + increaseAmount;
    }
}
