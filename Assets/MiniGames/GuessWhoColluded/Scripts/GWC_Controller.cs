// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/31/2018
// Last:  11/02/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// TODO: board wont' show up on load

// Contains 'key' GWC code
public class GWC_Controller : MonoBehaviour
{
    public AspectUtility aUtil;
    public CharacterTile[] charTiles;
    public Camera mainCamera;
    public CameraFollow camFollow;
    public Characters chars;
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public GameObject dBox;
    public GameObject HUD;
    public GameObject muellerCards;
    public GameObject oBox;
    public GameObject pauseBtn;
    public GameObject pauseScreen;
    public GameObject player;
    public GameObject playerCard;
    public GameObject sceneTransAnim;
    public GameObject sFaderAnim;
    public GameObject sFaderAnimDia;
    public GameObject trumpCards;
    public GameObject warpGWC;
    public Image dPic;
    public ImageStrobe dArrow;
    public Inventory inv;
    public MoveOptionsMenuArrow moveOptsArw;
    public MusicManager mMan;
    public OptionsManager oMan;
    public PauseGame pause;
    public PlayerBrioManager brio;
    public SaveGame save;
    public Scene scene;
    public SFXManager SFXMan;
    public SinglePlayerLogic spLogic;
    public Sprite[] portPic;
    public Text dText;
    public TouchControls touches;
    public UIManager uMan;

    public bool bAllowPlayerToGuess;
    public bool bAvoidUpdate;
    public bool bBoardReset;
    public bool bCanFlip;
    public bool bIsPlayerG2G;
    public bool bOppMueller;
    public bool bOppTrump;
    public bool bOptModeSelect;
    public bool bOptModeSingle;
    public bool bOptModeMulti;
    public bool bOptTeamSelect;
    public bool bOptOppSelect;
    public bool bSingleReminder;
    public bool bStartGame;
    public bool bTeamMueller;
    public bool bTeamTrump;

    public float buttonTimer;
    public float guessThreshold;
    public float musicTimer1;
    public float musicTimer2;
    public float strobeTimer;

    public int playerCharacter;
    public int opponentCharacter;

    public string teamName;
    public string oppName;
    
    public string[] dialogueLines;
    public string[] optionsLines;

    void Start()
    {
        // Initializers
        chars = GetComponent<Characters>();

        charTiles = new CharacterTile[24];
        
        guessThreshold = 1.25f;
        musicTimer1 = 5.39f;
        musicTimer2 = 1.05f;
        strobeTimer = 1.0f;

        // Trump Dialogue
        //dialogueLines = new string[] {
        //        "There was no collusion. Everybody knows there was no collusion.",
        //        "The Fake News, the Disgusting Democrats, and the Deep State would say different."
        //    };

        // Initial prompt to pick a mode
        dMan.bDialogueActive = false;
        mMan.bMusicCanPlay = false;

        GWC_PromptRestrictions();

        // First time in minigame, give instructions
        if (PlayerPrefs.GetInt("GivenDirectionsForGWC") == 0)
        {
            dialogueLines = new string[] {
                "I want YOU.. to         Guess Who Colluded.",
                "Like classic Guess Who, you'll be flipping tiles and guessing people.",
                "Tap or click options when they appear below this text box.",
                "Tap or click tiles to keep track of who your opponent is.",
                "If you have trouble, click the menu icon in the top right.",
                "It'll show you your character card, let you change settings, etc.",
                "GLHF!"
            };

            // Avoid instructions
            PlayerPrefs.SetInt("GivenDirectionsForGWC", 1);
        }
        else
        {
            dialogueLines = new string[] {
                "I want YOU.. to         Guess Who Colluded."
            };
        }

        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dText.text = dialogueLines[dMan.currentLine];
        dPic.sprite = portPic[48];
        dBox.transform.localScale = Vector3.one;
        sFaderAnimDia.GetComponent<Animator>().enabled = true;

        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        inv.LoadInventory("transfer");
    }

    void Update()
    {
        // New Game -- Dialogue activation & strobe arrow start
        if (strobeTimer > 0)
        {
            // Script Pref -- Run uMan once after initial load in UIManager
            if (strobeTimer == 1f)
            {
                uMan.HideBrioAndButton();
            }

            strobeTimer -= Time.deltaTime;

            if (strobeTimer <= 0)
            {
                bOptModeSelect = true;
                StartCoroutine(dArrow.Strobe());
                dMan.bDialogueActive = true;
                sFaderAnimDia.transform.localScale = Vector3.zero; // Remove to allow mouse click on options prompts

                // Sound Effect
                SFXMan.sounds[2].PlayOneShot(SFXMan.sounds[2].clip);
            }
        }

        // Mode Selection Prompt
        if (bOptModeSelect &&
            !dMan.bDialogueActive)
        {
            GWC_PromptRestrictions();

            dialogueLines = new string[] {
                "First and first mostly, who's playing?"
            };
            GWC_DialogueResetter();

            optionsLines = new string[] {
                "Me (Single player)",
                "Us (Multiplayer)"
            };
            GWC_OptionsResetter_2Q();
        }

        // Begin play -- Activate music, UI, and fade after team selection
        if (!dMan.bDialogueActive &&
            !bAvoidUpdate &&
            bStartGame)
        {
            player.GetComponent<PlayerMovement>().bStopPlayerMovement = false;
            mMan.bMusicCanPlay = true;
            sFaderAnim.GetComponent<Animator>().enabled = true;

            // Change to avoid running this logic
            bAvoidUpdate = true;

            // Allow tile flipping
            StartCoroutine(StartFlipping());

            // Mode Reminders
            if (bOptModeMulti)
            {
                StartCoroutine(StartMulti());
            }
            if (bOptModeSingle)
            {
                StartCoroutine(StartSingle(1.0f));
            }
        }

        // Change from first music track to second
        if (bStartGame &&
            bAvoidUpdate &&
            musicTimer1 > 0)
        {
            musicTimer1 -= Time.deltaTime;

            if (musicTimer1 <= 0)
            {
                mMan.SwitchTrack(1);
            }
        }

        // Change from second music track to third
        if (musicTimer1 <= 0 &&
            musicTimer2 > 0)
        {
            musicTimer2 -= Time.deltaTime;

            if (musicTimer2 <= 0)
            {
                mMan.SwitchTrack(2);
            }
        }

        // Resetting
        if (!dMan.bDialogueActive &&
            bBoardReset &&
            !bCanFlip)
        {
            StartCoroutine(DelayedResetBoard());
        }

        // Zoom In -- Scroll Forward or press Y
        if (mainCamera.orthographicSize >= aUtil._wantedAspectRatio &&
            !dMan.bDialogueActive &&
            !pause.bPauseActive &&
            !pause.bPausing &&
            (Input.GetAxis("Mouse ScrollWheel") > 0 ||
             Input.GetKeyDown(KeyCode.Comma) ||
             //Input.GetKeyDown(KeyCode.JoystickButton3) ||
             contSupp.ControllerButtonPadTop("down") ||
             touches.bYaction))
        {
            mainCamera.orthographicSize = mainCamera.orthographicSize - 0.25f;
            touches.bYaction = false;

            sceneTransAnim.transform.localScale = new Vector2
                (sceneTransAnim.transform.localScale.x - 0.35f,
                 sceneTransAnim.transform.localScale.y - 0.35f);
        }

        // Zoom Out -- Scroll Back or press X
        if (mainCamera.orthographicSize < 5.642857f &&
            !dMan.bDialogueActive &&
            !pause.bPauseActive &&
            !pause.bPausing &&
            (Input.GetAxis("Mouse ScrollWheel") < 0 ||
             Input.GetKeyDown(KeyCode.Period) ||
             //Input.GetKeyDown(KeyCode.JoystickButton2) ||
             contSupp.ControllerButtonPadLeft("down") ||
             touches.bXaction))
        {
            mainCamera.orthographicSize = mainCamera.orthographicSize + 0.25f;
            touches.bXaction = false;

            sceneTransAnim.transform.localScale = new Vector2
                (sceneTransAnim.transform.localScale.x + 0.35f,
                 sceneTransAnim.transform.localScale.y + 0.35f);
        }

        // Single Player - Reminder to Guess
        if (bSingleReminder &&
            !dMan.bDialogueActive)
        {
            bSingleReminder = false;
            bAllowPlayerToGuess = true;

            GWC_PromptRestrictions();

            dialogueLines = new string[] {
                "And when you're ready, just hold down for a couple of seconds."
            };
            GWC_DialogueResetter();
        }

        // Single Player - Player Guess
        if (bAllowPlayerToGuess &&
            !dMan.bDialogueActive &&
            (Input.GetKey(KeyCode.Space) ||
             Input.GetKey(KeyCode.G) ||
             Mathf.Abs(contSupp.ControllerTriggerLeft()) > 0 ||
             Mathf.Abs(contSupp.ControllerTriggerRight()) > 0 ||
             (Input.GetMouseButton(0) &&
              Input.touchCount < 2)))
        {
            buttonTimer += Time.deltaTime;

            if (buttonTimer >= guessThreshold &&
                !spLogic.bGuessingFTW)
            {
                if (spLogic.bPlayerMidGuess)
                {
                    spLogic.bPlayerGuessing = true;
                }
                else if (!bIsPlayerG2G)
                {
                    IsPlayerGoodToGuess();
                }

                buttonTimer = 0;
            }

        }

        // Single Player - Reset Player Guess timer
        if (Input.GetMouseButtonUp(0) ||
            Input.GetKeyUp(KeyCode.Space))
        {
            buttonTimer = 0;
        }

        // Lose brio every X seconds while playing
        if (brio.playerCurrentBrio > 1 &&
            pauseScreen.transform.localScale != Vector3.one &&
            !dMan.bDialogueActive)
        {
            if (!warpGWC.GetComponent<SceneTransitioner>().bAnimationToTransitionScene)
            {
                brio.FatiguePlayer(0.0025f);
                brio.bRestoreOverTime = false;
                uMan.UpdateBrio();
            }
        }
    }

    IEnumerator StartFlipping()
    {
        yield return new WaitForSeconds(1.0f);

        bCanFlip = true;
    }

    IEnumerator StartSingle(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        GWC_PromptRestrictions();

        if (bTeamMueller)
        {
            teamName = "Mueller";
        }
        else if (bTeamTrump)
        {
            teamName = "Trump";
        }

        if (bOppMueller)
        {
            oppName = "Mueller";
            opponentCharacter = Random.Range(0, 24);
        }
        else if (bOppTrump)
        {
            oppName = "Trump";
            opponentCharacter = Random.Range(24, 48);
        }

        // Randomize Dialogue
        int randomInt = Random.Range(0, 3);
        if (bBoardReset)
        {
            if (randomInt == 0)
            {
                dialogueLines = new string[] {
                    "I'll be Team " + oppName + " again. To make it fair..",
                    "3.. ",
                    "2.. ",
                    "1..",
                    "Dibs on going first!",
                    "Ha works every time..."
                };
            }
            else if (randomInt == 1)
            {
                dialogueLines = new string[] {
                    "Good luck Team " + teamName + ", I'll go first..",
                    "Again..."
                };
            }
            else if (randomInt == 2)
            {
                dialogueLines = new string[] {
                    "Alright Team " + teamName + ", imma come at you like a spider monkey."
                };
            }
        }
        else
        {
            if (randomInt == 0)
            {
                dialogueLines = new string[] {
                    "Sure, I'll be Team " + oppName + ". Please, age before beauty.."
                };
            }
            else if (randomInt == 1)
            {
                dialogueLines = new string[] {
                    "Good luck Team " + teamName + ", I'll go first..."
                };
            }
            else if (randomInt == 2)
            {
                dialogueLines = new string[] {
                    "Alright Team " + teamName + ", it's you and your friends vs Me and the Revolution.."
                };
            }
        }

        GWC_DialogueResetter();
        dPic.sprite = portPic[48];

        spLogic.bOppQ1 = true;
    }

    IEnumerator StartMulti()
    {
        yield return new WaitForSeconds(1.0f);

        GWC_PromptRestrictions();

        if (bBoardReset)
        {
            dialogueLines = new string[] {
                "Remember to think outside the box.. But not the tile."
            };
        }
        else
        {
            dialogueLines = new string[] {
                "Oh and don't forget about the Colluminac in the menu. GLHF"
            };
        }

        GWC_DialogueResetter();
        dPic.sprite = portPic[48];

        bCanFlip = false;
    }

    IEnumerator DelayedResetBoard()
    {
        yield return new WaitForSeconds(1.0f);

        // Change to avoid running this logic
        bBoardReset = false;

        // Allow tile flipping
        bCanFlip = true;
    }

    public void OptionsSelection()
    {
        // Dialogue 1 - Option * - Selected Mode
        if (bOptModeSelect &&
           (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2))
        {
            bOptModeSelect = false;
            bOptTeamSelect = true;

            // Selected Single Player
            if (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
            {
                bOptModeSingle = true;
            }
            // Selected Multi Player
            else if (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
            {
                bOptModeMulti = true;
            }

            GWC_PromptRestrictions();

            dialogueLines = new string[] {
                "Got it. Now more importantly, whose side are you on?"
            };
            GWC_DialogueResetter();

            optionsLines = new string[] {
                "Team Trump",
                "Team Mueller"
            };

            GWC_OptionsResetter_2Q();
        }

        // Dialogue 2 - Option 1 - Selected Trump
        else if (bOptTeamSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            bOptTeamSelect = false;
            bOptOppSelect = true;
            bTeamTrump = true;

            GWC_PromptRestrictions();

            dialogueLines = new string[] {
                "But what to do, what to do... Are you going to.."
            };
            GWC_DialogueResetter();

            optionsLines = new string[] {
                "Stop the leaks!",
                "End the witch hunt!"
            };

            GWC_OptionsResetter_2Q();
        }

        // Dialogue 2 - Option 2 - Selected Mueller
        else if (bOptTeamSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            bOppTrump = true;
            bOptTeamSelect = false;
            bTeamMueller = true;

            oMan.ResetOptions();

            // Display Trump board
            trumpCards.transform.localScale = Vector3.one;

            // 'Store' Trump board
            for (int i = 0; i < 24; i++)
            {
                charTiles[i] = trumpCards.transform.GetChild(i).GetComponent<CharacterTile>();
            }

            dMan.dialogueLines = new string[] {
                "Time to find out who on Team Trump is colluding...",
                "And I better do it quickly."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();

            bStartGame = true;

            // Pick random Mueller character for the player
            playerCharacter = Random.Range(0, 24);
            dPic.sprite = portPic[playerCharacter];
            playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.one;
        }

        // Dialogue 3 - Option 1 - Selected Trump (and very Fine People)
        else if (bOptOppSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            bOppTrump = true;
            bOptOppSelect = false;
            bOptTeamSelect = false;

            oMan.ResetOptions();

            // Display Trump board
            trumpCards.transform.localScale = Vector3.one;

            // 'Store' Trump board
            for (int i = 0; i < 24; i++)
            {
                charTiles[i] = trumpCards.transform.GetChild(i).GetComponent<CharacterTile>();
            }

            dMan.dialogueLines = new string[] {
                "Time to find out who on Team Trump is leaking information...",
                "And I better do it quickly."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();

            bStartGame = true;

            // Pick random Trump character for the player
            playerCharacter = Random.Range(24, 48);
            dPic.sprite = portPic[playerCharacter];
            playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.one;
        }

        // Dialogue 3 - Option 2 - Selected Mueller (and the Fake News)
        else if (bOptOppSelect &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            bOppMueller = true;
            bOptOppSelect = false;
            bOptTeamSelect = false;

            oMan.ResetOptions();

            // Display Mueller board
            muellerCards.transform.localScale = Vector3.one;

            // 'Store' Mueller board
            for (int i = 0; i < 24; i++)
            {
                charTiles[i] = muellerCards.transform.GetChild(i).GetComponent<CharacterTile>();
            }

            dMan.dialogueLines = new string[] {
                "Time to find out who on Team Mueller is leading this witch hunt...",
                "And I better do it quickly."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();

            bStartGame = true;

            // Pick random Trump character for the player
            playerCharacter = Random.Range(24, 48);
            dPic.sprite = portPic[playerCharacter];
            playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.one;
        }

        // Single Player - Player Guess Check
        else if (bIsPlayerG2G)
        {
            if (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
            {
                spLogic.playerGuessNumber = spLogic.playerGuessNumber + 1;
                spLogic.bPlayerGuessing = true;
            }
            else if (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
            {
                spLogic.bPlayerGuessing = false;
            }

            bIsPlayerG2G = false;

            oMan.ResetOptions();
        }

        if (spLogic.bGuessingTrait)
        {
            spLogic.TraitTree();
        }
        else if (!spLogic.bAvoidingQuickGuess)
        {
            spLogic.LogicTree();
        }
    }

    public void IsPlayerGoodToGuess()
    {
        bIsPlayerG2G = true;

        GWC_PromptRestrictions();

        dialogueLines = new string[] {
                "Would you like to guess?"
            };
        GWC_DialogueResetter();

        optionsLines = new string[] {
                "Yes",
                "No"
            };
        GWC_OptionsResetter_2Q();
    }

    public void GG()
    {
        oMan.ResetOptions();
        warpGWC.GetComponent<BoxCollider2D>().enabled = true;
        warpGWC.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpGWC.GetComponent<SceneTransitioner>().BetaLoad);

        // Stop Dan from moving
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        // Remove and prevent dialog 
        dMan.gameObject.transform.localScale = Vector3.zero;
    }

    public void ResetBoard()
    {
        oMan.ResetOptions();

        // Hide current character card on Pause screen
        playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.zero;

        bBoardReset = true;
        bCanFlip = false;
        bIsPlayerG2G = false;

        if (bOptModeSingle)
        {
            spLogic.ResetSingle();
            StartCoroutine(StartSingle(0.0f));
        }
        else
        {
            StartCoroutine(StartMulti());
        }

        if (bOppMueller)
        {
            // Turn over tiles
            for (int i = 0; i < 24; i++)
            {
                charTiles[i] = muellerCards.transform.GetChild(i).GetComponent<CharacterTile>();
                charTiles[i].ShowFront();
            }
        }

        if (bOppTrump)
        {
            // Turn over tiles
            for (int i = 0; i < 24; i++)
            {
                charTiles[i] = trumpCards.transform.GetChild(i).GetComponent<CharacterTile>();
                charTiles[i].ShowFront();
            }
        }

        if (bTeamMueller)
        {
            // Pick random Mueller character for the player
            playerCharacter = Random.Range(0, 24);
            dPic.sprite = portPic[playerCharacter];
            playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.one;
        }
        else if (bTeamTrump &&
                 bOppMueller)
        {
            // Pick random Trump character for the player
            playerCharacter = Random.Range(24, 48);
            dPic.sprite = portPic[playerCharacter];
            playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.one;
        }
        else if (bTeamTrump &&
                 bOppTrump)
        {
            // Pick random Trump character for the player
            playerCharacter = Random.Range(24, 48);
            dPic.sprite = portPic[playerCharacter];
            playerCard.gameObject.transform.GetChild(playerCharacter).localScale = Vector3.one;
        }
    }

    public void GWC_PromptRestrictions()
    {
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;
    }

    public void GWC_DialogueResetter()
    {
        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dText.text = dialogueLines[dMan.currentLine];
        dBox.transform.localScale = Vector3.one;
        dMan.bDialogueActive = true;
        uMan.HideBrioAndButton();
        StartCoroutine(dMan.ResetStrobes());
    }

    public void GWC_OptionsResetter()
    {
        oMan.tempOptsCount = 0;
        oMan.options = optionsLines;
        oMan.ShowOptions();
    }

    public void GWC_OptionsResetter_2Q()
    {
        GWC_OptionsResetter();
        oMan.HideThirdPlusOpt();
    }

    public void GWC_OptionsResetter_3Q()
    {
        GWC_OptionsResetter();
        oMan.HideFourthOpt();
    }

    public void GWC_OptionsResetter_4Q()
    {
        GWC_OptionsResetter();
    }
}