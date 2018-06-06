// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 06/03/2018
// Last:  06/06/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minesweeper : MonoBehaviour
{
    public DialogueManager dMan;
    public GameObject pause;
    public GameObject person1;
    public GameObject thePlayer;
    public GameObject warpMinesweeper;
    public MoveOptionsMenuArrow moveOptsArw;
    public OptionsManager oMan;
    public PlayerBrioManager brio;
    public UIManager uiMan;

    public bool bAvoidUpdate;
    public bool bHasWon;
    public bool bHasLost;
    public bool bPauseFlagging;
    public bool bReset;

    private float pauseTimer;

    void Start ()
    {
        // Initializers
        brio = GameObject.FindObjectOfType<PlayerBrioManager>();
        dMan = GameObject.FindObjectOfType<DialogueManager>();
        moveOptsArw = FindObjectOfType<MoveOptionsMenuArrow>();
        oMan = GameObject.FindObjectOfType<OptionsManager>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        person1 = GameObject.Find("Person.1");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        warpMinesweeper = GameObject.Find("Minesweeper.to.Chp1");
        uiMan = FindObjectOfType<UIManager>();
        
        pauseTimer = 0.333f;
    }
	

	void Update ()
    {
        // Avoid spamming flags
        if (bPauseFlagging)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                UnpauseFlagging();
            }
        }

        if (bHasLost && !bAvoidUpdate)
        {
            Lose();

            // Reset the bools
            bAvoidUpdate = true;
        }
        
        if (bHasWon && !bAvoidUpdate)
        {
            Win();

            // Reset the bools
            bAvoidUpdate = true;
        }
        
        // Lose brio every X seconds while playing
        if (brio.playerCurrentBrio > 1 &&
            !pause.activeSelf &&
            (!bHasLost || !bHasWon))
        {
            brio.FatiguePlayer(0.0025f);
            uiMan.bUpdateBrio = true;
        }

        // 06/06/2018 DC -- If out of brio, should the game end?
    }

    public void Win()
    {
        Debug.Log("win");
        // Ask the player if they want to play again
        person1.transform.GetChild(0).gameObject.SetActive(true);
        person1.transform.GetChild(0).GetComponent<DialogueHolder>().bContinueDialogue = true;

        // Ask about difficulty

        // Reward with brio (max cap & current)
        thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
        thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(15);
        uiMan.bUpdateBrio = true;
    }

    public void Lose()
    {
        // Have Person1 ask if players wants again or done?
        person1.transform.GetChild(1).gameObject.SetActive(true);
        person1.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;

        // Ask about difficulty
    }

    public void MinesweeperDialogueCheck()
    {
        // Win or Lose - Option 1
        if ((person1.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered ||
             person1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered) &&
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            // Reset
            oMan.ResetOptions();

            bReset = true;
            bHasLost = false;
            bHasWon = false;
        }
        // Win or Lose - Option 2
        else if ((person1.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered ||
                  person1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered) &&
                  moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            warpMinesweeper.GetComponent<BoxCollider2D>().enabled = true;
            
            // Stop the player from bringing up the dialog again
            dMan.gameObject.SetActive(false);
        }
    }

    public void ResetGame()
    {
        person1.transform.GetChild(0).gameObject.SetActive(false);
        person1.transform.GetChild(1).gameObject.SetActive(false);

        bAvoidUpdate = false;
    }

    public void PauseFlagging()
    {
        bPauseFlagging = true;
    }

    public void UnpauseFlagging()
    {
        bPauseFlagging = false;
        pauseTimer = 0.333f;
    }
}
