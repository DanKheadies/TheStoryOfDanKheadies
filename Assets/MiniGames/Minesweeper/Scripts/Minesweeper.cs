// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: noobtuts.com
// Contributors: David W. Corso
// Start: 06/03/2018
// Last:  05/10/2019

// DC TODO -- Bring in QuestMananger & complete quest when won (but still able to keep playing for restored brio & not more brio)

using UnityEngine;
using UnityEngine.UI;

// Main Minesweeper logic
public class Minesweeper : MonoBehaviour
{
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject pause;
    public GameObject person1;
    public GameObject thePlayer;
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
        // Initializers
        brio = FindObjectOfType<PlayerBrioManager>();
        dArrow = GameObject.Find("Dialogue_Arrow").GetComponent<ImageStrobe>();
        dBox = GameObject.Find("Dialogue_Box");
        dMan = FindObjectOfType<DialogueManager>();
        dPic = GameObject.Find("Dialogue_Picture").GetComponent<Image>();
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        inv = FindObjectOfType<Inventory>();
        mMan = FindObjectOfType<MusicManager>();
        moveOptsArw = FindObjectOfType<MoveOptionsMenuArrow>();
        oMan = FindObjectOfType<OptionsManager>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        person1 = GameObject.Find("Person.1");
        save = FindObjectOfType<SaveGame>();
        SFXMan = FindObjectOfType<SFXManager>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        touches = FindObjectOfType<TouchControls>();
        warpMinesweeper = GameObject.Find("Minesweeper.to.Chp1");
        uMan = FindObjectOfType<UIManager>();
        
        strobeTimer = 1.0f;
        timer = 0.333f;
        
        // Initial prompt to pick a mode
        dMan.bDialogueActive = false;
        mMan.bMusicCanPlay = false;

        // Restrict player movement
        thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

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

        // 05/10/2019 DC TODO 
        // need to hide brio and button here (and restore once dialgoue goes away)
        // need t oallow joystick to work on the options (chedk dpad too)
    }

    void Update ()
    {
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
                uMan.bUpdateBrio = true;
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
        uMan.bUpdateBrio = true;
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
            dMan.gameObject.transform.localScale = Vector3.zero;
        }
    }

    public void ResetGame()
    {
        person1.transform.GetChild(0).gameObject.SetActive(false);
        person1.transform.GetChild(1).gameObject.SetActive(false);

        bAvoidUpdate = false;
    }
}
