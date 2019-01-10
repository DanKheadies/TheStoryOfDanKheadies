// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 06/03/2018
// Last:  01/10/2019

using UnityEngine;

// Main Minesweeper logic
public class Minesweeper : MonoBehaviour
{
    public DialogueManager dMan;
    public GameObject pause;
    public GameObject person1;
    public GameObject thePlayer;
    public GameObject warpMinesweeper;
    public Inventory inv;
    public MoveOptionsMenuArrow moveOptsArw;
    public OptionsManager oMan;
    public PlayerBrioManager brio;
    public SaveGame save;
    public UIManager uiMan;

    public bool bAvoidInvestigating;
    public bool bAvoidInvestionUpdate;
    public bool bAvoidUpdate;
    public bool bHasWon;
    public bool bHasLost;
    public bool bPauseFlagging;
    public bool bReset;

    private float pauseTimer;
    public float timer;

    void Start ()
    {
        // Initializers
        brio = FindObjectOfType<PlayerBrioManager>();
        dMan = FindObjectOfType<DialogueManager>();
        inv = FindObjectOfType<Inventory>();
        moveOptsArw = FindObjectOfType<MoveOptionsMenuArrow>();
        oMan = FindObjectOfType<OptionsManager>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        person1 = GameObject.Find("Person.1");
        save = FindObjectOfType<SaveGame>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        warpMinesweeper = GameObject.Find("Minesweeper.to.Chp1");
        uiMan = FindObjectOfType<UIManager>();
        
        pauseTimer = 0.333f;
        timer = 0.333f;
    }
	
	void Update ()
    {
        // 06/07/2018 DC -- Volume Bug
        //                  Volume kept creeping up / on while playing; no ideas why

        // Transfer -- Load inventory
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                inv.LoadInventory("transfer");

                // Reset Transfer
                PlayerPrefs.SetInt("Transferring", 0);
            }
        }

        if ((pause.transform.localScale == Vector3.one ||
             dMan.bDialogueActive) &&
             !bAvoidInvestionUpdate)
        {
            bAvoidInvestigating = true;
            bAvoidInvestionUpdate = true;
        }

        // Avoid investigating when paused or dialogue up (w/ spacebar still down)
        if ((pause.transform.localScale == Vector3.zero &&
            !dMan.bDialogueActive) &&
            bAvoidInvestionUpdate)
        {
            // DC TODO 01/10/2019 -- Add touches and controller support
            if (Input.GetKey(KeyCode.Space))
            {
                // Avoid reseting the booleans until spacebar is let up
            }
            else
            {
                bAvoidInvestigating = false;
                bAvoidInvestionUpdate = false;
            }
        }

        // Avoid spamming flags
        if (bPauseFlagging)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {
                UnpauseFlagging();
            }
        }

        if (bHasLost && 
            !bAvoidUpdate)
        {
            Lose();

            // Reset the bools
            bAvoidUpdate = true;
        }
        
        if (bHasWon && 
            !bAvoidUpdate)
        {
            Win();

            // Reset the bools
            bAvoidUpdate = true;
        }
        
        // Lose brio every X seconds while playing
        if (brio.playerCurrentBrio > 1 &&
            pause.transform.localScale != Vector3.one &&
            !dMan.bDialogueActive &&
            (!bHasLost || !bHasWon))
        {
            if (warpMinesweeper.GetComponent<SceneTransitioner>().bAnimationToTransitionScene)
            {
                // Avoid losing brio if scene transition animation is going
            }
            else
            {
                brio.FatiguePlayer(0.0025f);
                uiMan.bUpdateBrio = true;
            }
        }

        // 06/06/2018 DC -- If out of brio, should the game end?
    }

    public void Win()
    {
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
        // Win or Lose - Option 1 - Play again
        if ((person1.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered ||
             person1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered) &&
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            // Reset
            oMan.ResetOptions();

            bHasLost = false;
            bHasWon = false;
            bReset = true;
        }
        // Win or Lose - Option 2 - Stop playing
        else if ((person1.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered ||
                  person1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered) &&
                  moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            warpMinesweeper.GetComponent<BoxCollider2D>().enabled = true;
            warpMinesweeper.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

            // Save Transfer Values
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", warpMinesweeper.GetComponent<SceneTransitioner>().BetaLoad);

            // Stop Dan from moving
            thePlayer.GetComponent<Animator>().enabled = false;

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
