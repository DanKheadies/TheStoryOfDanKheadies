// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 06/03/2018
// Last:  02/25/2020

// DC TODO -- Bring in QuestMananger & complete quest when won (but still able to keep playing for restored brio & not more brio)

using UnityEngine;
using UnityEngine.UI;

// Main Minesweeper logic
public class Minesweeper : MonoBehaviour
{
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject npc_chun;
    public GameObject pause;
    public GameObject player;
    public GameObject warpMinesweeper;
    public Image dPic;
    public ImageStrobe dArrow;
    public Inventory inv;
    public MoveOptionsMenuArrow moveOptsArw;
    public MusicManager mMan;
    public OptionsManager oMan;
    public PlayerBrioManager brio;
    public SaveGame save;
    public SFXManager SFXMan;
    public Sprite[] portPic;
    public Text dText;
    public TouchControls touches;
    public UIManager uMan;

    public bool bAvoidInvestigating;
    public bool bAvoidInvestionUpdate;
    public bool bAvoidUpdate;
    public bool bHasWon;
    public bool bHasLost;
    public bool bPauseFlagging;
    public bool bReset;
    
    public float strobeTimer;
    public float timer;

    public string[] dialogueLines;

    void Start ()
    {
        strobeTimer = 1.0f;
        timer = 0.333f;
        
        // Initial prompt to pick a mode
        dMan.bDialogueActive = false;
        mMan.bMusicCanPlay = false;

        // Restrict player movement
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        // First time in minigame, give instructions
        if (PlayerPrefs.GetInt("GivenDirectionsForMinesweeper") == 0)
        {
            dialogueLines = new string[] {
                "Alright Dan, this is classic Minesweeper.",
                "Knock a block to see if there's a mine under it.",
                "A number means there are X mines near that block.",
                "You can throw a flag on blocks you think are mines to avoid em.",
                "And once you knock all the blocks without mines, you'll win!",
                "Happy huntin.."
            };

            // Avoid instructions
            PlayerPrefs.SetInt("GivenDirectionsForMinesweeper", 1);
        }
        else
        {
            dialogueLines = new string[] {
                "Happy huntin Dan.."
            };
        }

        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dText.text = dialogueLines[dMan.currentLine];
        dPic.sprite = portPic[0];
        dBox.transform.localScale = Vector3.one;

        // Hide BrioBar & Pause Button (Opac)
        uMan.HideBrioAndButton();

        // Get transfer items (if any)
        inv.LoadInventory("transfer");
    }

    void Update ()
    {
        if (strobeTimer > 0)
        {
            strobeTimer -= Time.deltaTime;

            if (strobeTimer <= 0)
            {
                StartCoroutine(dArrow.Strobe());
                dMan.bDialogueActive = true;

                // Sound Effect
                SFXMan.sounds[2].PlayOneShot(SFXMan.sounds[2].clip);
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
            if (Input.GetButton("Action") ||
                Input.GetMouseButton(0) ||
                contSupp.ControllerButtonPadBottom("hold") ||
                touches.bAaction
                )
            {
                // Avoid reseting the booleans until actionable is let up
            }
            else
            {
                bAvoidInvestigating = false;
                bAvoidInvestionUpdate = false;
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
            if (!warpMinesweeper.GetComponent<SceneTransitioner>().bAnimationToTransitionScene)
            {
                brio.FatiguePlayer(0.0025f);
                brio.bRestoreOverTime = false;
                uMan.UpdateBrio();
            }
        }

        // 06/06/2018 DC -- If out of brio, should the game end?
    }

    public void Win()
    {
        // Ask the player if they want to play again
        npc_chun.transform.GetChild(0).gameObject.SetActive(true);
        npc_chun.transform.GetChild(0).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();

        // Ask about difficulty

        // Reward 
        PlayerPrefs.SetString("TransferActions", "Quest6Reward");
    }

    public void Lose()
    {
        // Have Person1 ask if players wants again or done?
        npc_chun.transform.GetChild(1).gameObject.SetActive(true);
        npc_chun.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();

        // Ask about difficulty
    }

    public void MinesweeperDialogueCheck()
    {
        // Win or Lose - Option 1 - Play again
        if ((npc_chun.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered ||
             npc_chun.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered) &&
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            // Reset
            oMan.ResetOptions();

            bHasLost = false;
            bHasWon = false;
            bReset = true;
        }
        // Win or Lose - Option 2 - Stop playing
        else if ((npc_chun.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered ||
                  npc_chun.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered) &&
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
            player.GetComponent<Animator>().enabled = false;

            // Stop the player from bringing up the dialog again
            dMan.gameObject.transform.localScale = Vector3.zero;
        }
    }

    public void ResetGame()
    {
        npc_chun.transform.GetChild(0).gameObject.SetActive(false);
        npc_chun.transform.GetChild(1).gameObject.SetActive(false);

        bAvoidUpdate = false;
    }
}
