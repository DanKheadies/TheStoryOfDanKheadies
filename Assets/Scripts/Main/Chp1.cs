// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  07/12/2021

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Contains all Chapter 1 quests, items, and elements
public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public DeviceDetector devDetect;
    public DialogueManager dMan;
    public GameObject greatTree;
    public GameObject homeCushion;
    public GameObject homeCushionCollider;
    public GameObject item_homeVRGoggles;
    public GameObject npc_aaron;
    public GameObject npc_akira;
    public GameObject npc_al_khidr;
    public GameObject npc_ashera;
    public GameObject npc_atandwa;
    public GameObject npc_bazlo;
    public GameObject npc_brackey;
    public GameObject npc_canaan;
    public GameObject npc_chun;
    public GameObject npc_dagon;
    public GameObject npc_dilum;
    public GameObject npc_eliz;
    public GameObject npc_eloher;
    public GameObject npc_emma;
    public GameObject npc_enki;
    public GameObject npc_heartha;
    public GameObject npc_hugo;
    public GameObject npc_hye;
    public GameObject npc_jamaal;
    public GameObject npc_jamie;
    public GameObject npc_kalika;
    public GameObject npc_kart;
    public GameObject npc_khalid;
    public GameObject npc_luli;
    public GameObject npc_lupita;
    public GameObject npc_marija;
    public GameObject npc_marklar;
    public GameObject npc_mawkus;
    public GameObject npc_maximiliano;
    public GameObject npc_molleen;
    public GameObject npc_nesbit;
    public GameObject npc_pebs;
    public GameObject npc_poeia;
    public GameObject npc_pookieB1;
    public GameObject npc_pookieB2;
    public GameObject npc_ryuu;
    public GameObject npc_sula;
    public GameObject npc_sydelle;
    public GameObject npc_tevin;
    public GameObject npc_thabo;
    public GameObject npc_wharf;
    public GameObject npc_yvette;
    public GameObject npc_zola;
    public GameObject player;
    public GameObject quest0;  // Truth or Elaborate Lie w/ Dagon
    public GameObject quest1;  // Race w/ Canaan
    public GameObject quest2;  // Treehouse Search
    public GameObject quest3;  // Item Check w/ Enki
    public GameObject quest4;  // Hide & Seek w/ Al-khidr
    public GameObject quest5;  // Talking to GreatTree
    public GameObject quest6;  // Minesweeper
    public GameObject quest7;  // PookieBear1
    public GameObject quest8;  // PookieBear2
    public GameObject quest9;  // PookieVision
    public GameObject quest10; // TowerDeez
    public GameObject quest11; // TowerDeez SBF
    public GameObject quest12; // Talking to Pebs
    public GameObject questTrigger1;
    public GameObject questTrigger2;
    public GameObject screenFader;
    public GameObject treeHouseDoor;
    public GameObject warpCSTreeTunnel;
    public GameObject warpCSTyrannyTunnel;
    public GameObject warpGWC;
    public GameObject warpMinesweeper;
    public GameObject warpPookieVision;
    public GameObject warpTowerDeez;
    public GameObject warpTD_SBF;
    public Inventory inv;
    public MoveOptionsMenuArrow moveOptsArw;
    public MusicManager mMan;
    public OptionsManager oMan;
    public PauseGame pause;
    public QuestManager qMan;
    public SaveGame save;
    public ScriptManager scriptMan;
    public Text dText;
    public TouchControls touches;
    public UIManager uMan;

    public bool bHasFedPookie1;
    public bool bHasFedPookie2;
    public bool bHasGoggles;
    public bool bHasQ3SmoochWoochy;

    public float raceTimer;

    public int Q4KidCounter;

    public string Q4LastKidFound;
    public string Q7Options;
    public string Q7SelectedOption;
    public string Q8Options;
    public string Q8SelectedOption;
    public string savedQuestsValue;

    void Start()
    {
        // Chapter 1 -- First Time
        if (PlayerPrefs.GetString("Chapter") != "Chp1" &&
            PlayerPrefs.GetInt("TransferAnandaCoord") == 0)
        {
            player.transform.position = new Vector2(-13.68f, -7.625f);
            mainCamera.transform.position = new Vector2(-13.68f, -7.625f);
            camFollow.currentCoords = CameraFollow.AnandaCoords.Home;

            // Get transfer items (if any)
            inv.LoadInventory("transfer");

            // Cleanse transfer data
            save.DeleteTransPrefs();
        }
        // Chapter 1 -- Transferring from a mini-game or cutscene
        else if (PlayerPrefs.GetInt("Transferring") == 1)
        {
            // Get transfer data
            save.GetTransferData();

            // Execute transfer actions
            TransferActions(PlayerPrefs.GetString("TransferActions"));

            // Cleanse transfer data
            save.DeleteTransPrefs();
        }
        // Chapter 1 -- Saved Game
        else
        {
            save.GetSavedGame();

            // When loading from PlaygroundW, play Jurassic Dank
            if (camFollow.currentCoords == (CameraFollow.AnandaCoords)32)
                mMan.SwitchTrack(1);
        }

        LoadNPCLocations();
        LoadQuests();
        Chp1QuestChecker();

        StartCoroutine(TogglePlayerHitBoxDelay());
    }

    void Update()
    {
        // Quest 1 -- Race w/ Canaan -> Start Timer
        if (!pause.bPauseActive &&
            quest1.GetComponent<QuestObject>().bHasStarted &&
            !quest1.GetComponent<QuestObject>().bHasEnded)
        {
            raceTimer += Time.deltaTime;
        }
    }

    public void TransferActions(string functionName)
    {
        if (functionName == "Quest5Reward")
            Quest5Reward();

        if (functionName == "Quest6Reward")
            Quest6Reward();
    }


    public void CheckAndDisableLastKidFound()
    {
        if (Q4LastKidFound == "Al-khidr")
        {
            npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Atandwa")
        {
            npc_atandwa.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Eliz")
        {
            npc_eliz.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Thabo")
        {
            npc_thabo.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Zola")
        {
            npc_zola.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Marija")
        {
            npc_marija.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
    }

    public void CheckingForGoggles()
    {
        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();
            item = item.Substring(0, item.Length - 7);

            if (item == "VR.Goggles")
                bHasGoggles = true;
        }
    }

    public void DagonInLab()
    {
        // Move Dagon's position
        npc_dagon.transform.localPosition = new Vector3(-6.63f, -7.075f, 0);

        // Remove cushion
        homeCushionCollider.SetActive(false);
        homeCushion.SetActive(false);

        // Activate new Dagon prompt(s)
        npc_dagon.transform.GetChild(0).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(1).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(2).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(3).gameObject.SetActive(true);
    }

    public void DagonWalkingToLab()
    {
        // Activate "pipes" prompt
        npc_dagon.transform.GetChild(1).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(2).gameObject.SetActive(true);
        npc_dagon.transform.GetChild(2).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();

        npc_dagon.transform.GetChild(2).GetComponent<PolygonCollider2D>().enabled = false;

        // Walk Dagon
        StartCoroutine(DagonWalkingToLabDelay());
    }

    IEnumerator DagonWalkingToLabDelay()
    {
        // Visually prevent player from dismissing
        dMan.bStartStrobing = false;
        StartCoroutine(dMan.dArrow.gameObject.GetComponent<ImageStrobe>().StopStrobe());
        dMan.dArrow.transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(0.001f);

        // Physically prevent player from dimissing
        dMan.bDialogueActive = false;
        dMan.RefreshPause();

        yield return new WaitForSeconds(0.333f);

        // Stop player (incase they pause)
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        npc_dagon.GetComponent<Animator>().SetBool("bIsVogging", false);
        npc_dagon.GetComponent<Animator>().SetBool("bIsWalking", true);
        npc_dagon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        npc_dagon.GetComponent<NPCIdle>().enabled = false;

        npc_dagon.GetComponent<Animator>().Play("NPC Movement", 0, 0.25f);

        if (npc_dagon.transform.position.y > player.transform.position.y)
        {
            npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.5f);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveX", 0);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveY", 1f);

            yield return new WaitForSeconds(1f);

            // Stop player (incase they pause)
            player.GetComponent<PlayerMovement>().StopPlayerMovement();

            npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.55f, 0);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveX", -1f);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveY", 0);

            yield return new WaitForSeconds(1f);

            // Stop player (incase they pause)
            player.GetComponent<PlayerMovement>().StopPlayerMovement();

            yield return new WaitForSeconds(1f);

            // Stop player (incase they pause)
            player.GetComponent<PlayerMovement>().StopPlayerMovement();

            npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.4f);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveX", 0);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveY", -1f);
        }
        else
        {
            npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.4f);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveX", 0);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveY", -1f);

            yield return new WaitForSeconds(1f);

            // Stop player (incase they pause)
            player.GetComponent<PlayerMovement>().StopPlayerMovement();

            npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.55f, 0);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveX", -1f);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveY", 0);

            yield return new WaitForSeconds(1f);

            // Stop player (incase they pause)
            player.GetComponent<PlayerMovement>().StopPlayerMovement();

            yield return new WaitForSeconds(1f);

            // Stop player (incase they pause)
            player.GetComponent<PlayerMovement>().StopPlayerMovement();

            npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.5f);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveX", 0);
            npc_dagon.GetComponent<Animator>().SetFloat("MoveY", 1f);
        }

        yield return new WaitForSeconds(1f);

        // Stop player (incase they pause)
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0);
        npc_dagon.GetComponent<Animator>().SetFloat("MoveX", -1f);
        npc_dagon.GetComponent<Animator>().SetFloat("MoveY", 0);

        yield return new WaitForSeconds(0.666f);

        // Stop player (incase they pause)
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        npc_dagon.GetComponent<Animator>().SetBool("bIsWalking", false);

        // Remove cushion
        homeCushionCollider.SetActive(false);
        homeCushion.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        // Stop player (incase they pause)
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        npc_dagon.GetComponent<Collider2D>().enabled = false;
        npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0);
        npc_dagon.GetComponent<Animator>().SetFloat("MoveX", -1f);
        npc_dagon.GetComponent<Animator>().SetFloat("MoveY", 0);
        npc_dagon.GetComponent<Animator>().SetBool("bIsWalking", true);

        yield return new WaitForSeconds(1f);

        // Stop player (incase they pause)
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        // Reactivate Dagon
        npc_dagon.transform.localPosition = new Vector3(-6.63f, -7.075f, 0);
        npc_dagon.GetComponent<Animator>().SetBool("bIsWalking", false);
        npc_dagon.GetComponent<Collider2D>().enabled = true;
        npc_dagon.GetComponent<NPCIdle>().enabled = true;
        npc_dagon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        npc_dagon.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        // Activate new Dagon prompt
        npc_dagon.transform.GetChild(2).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(3).gameObject.SetActive(true);

        // Visually allow player to dismiss
        dMan.bStartStrobing = true;
        dMan.dArrow.transform.localScale = Vector3.one;

        // Physically allow player to dismiss
        dMan.bDialogueActive = true;
    }

    IEnumerator DelayTreeHouse()
    {
        yield return new WaitForSeconds(0.5f);

        questTrigger2.transform.GetChild(1).GetComponent<Transform>().localScale = new Vector3(0.2625f, 0.24937f, 1f);

        // Disable door again & move Dan back
        treeHouseDoor.transform.tag = "LockedDoor";
        treeHouseDoor.transform.localScale = Vector3.one;
        treeHouseDoor.GetComponent<BoxCollider2D>().isTrigger = false;
        treeHouseDoor.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
        treeHouseDoor.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void EndRace()
    {
        npc_canaan.transform.GetChild(0).gameObject.SetActive(false);
        npc_canaan.transform.GetChild(1).gameObject.SetActive(true);

        raceTimer = Mathf.Round(raceTimer);

        if (raceTimer == 0)
        {
            npc_canaan.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Feel free to keep practicing. It's always good to practice."
                };
        }
        else if (raceTimer <= 10)
        {
            npc_canaan.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Holy goat nipples Batman.. How the.. Did you cheat?",
                    "You set one of the best records at " + raceTimer + " seconds?!?",
                    "Well.. Well ran. Let's see how you do tomorrow."
                };
        }
        else if (raceTimer <= 30 && raceTimer > 10)
        {
            npc_canaan.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Good race Dan! You ran that in " + raceTimer + " seconds.",
                    "Keep practicing and improving. Maybe you'll beat my time!"
                };
        }
        else
        {
            npc_canaan.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Well.. You made it... You finished after " + raceTimer + " seconds.",
                    "Keep training Dan. It's good for you!"
                };
        }
    }

    public void GoggleCheck()
    {
        // Optimization: PlayerPref that tracks if ever picked up goggles

        CheckingForGoggles();

        if (bHasGoggles)
        {
            npc_ashera.transform.GetChild(0).gameObject.SetActive(true);
            npc_ashera.transform.GetChild(1).gameObject.SetActive(false);

            item_homeVRGoggles.transform.localScale = Vector3.zero;
        }
        else
        {
            npc_ashera.transform.GetChild(0).gameObject.SetActive(false);
            npc_ashera.transform.GetChild(1).gameObject.SetActive(true);

            item_homeVRGoggles.transform.localScale = Vector3.one;
        }

        // Reset
        bHasGoggles = false;
    }

    IEnumerator HideAndSeek()
    {
        // Turn off Quest Trigger ActionOnClose
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().bActionOnClose = false;

        yield return new WaitForSeconds(2);

        // Move Al-khidr & turn on the HideAndSeekKid script
        npc_al_khidr.transform.localPosition = new Vector2(7.429f, -7.797f);
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = true;

        // "Turn on" Kids 5-9
        npc_atandwa.transform.localScale = Vector3.one;
        npc_eliz.transform.localScale = Vector3.one;
        npc_thabo.transform.localScale = Vector3.one;
        npc_zola.transform.localScale = Vector3.one;
        npc_marija.transform.localScale = Vector3.one;

        // Need to reset each kid's box collider after first round (but why?)
        npc_atandwa.GetComponent<BoxCollider2D>().isTrigger = true;
        npc_atandwa.GetComponent<BoxCollider2D>().isTrigger = false;
        npc_eliz.GetComponent<BoxCollider2D>().isTrigger = true;
        npc_eliz.GetComponent<BoxCollider2D>().isTrigger = false;
        npc_thabo.GetComponent<BoxCollider2D>().isTrigger = true;
        npc_thabo.GetComponent<BoxCollider2D>().isTrigger = false;
        npc_zola.GetComponent<BoxCollider2D>().isTrigger = true;
        npc_zola.GetComponent<BoxCollider2D>().isTrigger = false;
        npc_marija.GetComponent<BoxCollider2D>().isTrigger = true;
        npc_marija.GetComponent<BoxCollider2D>().isTrigger = false;

        // Set dialogue & dialogue elements
        dMan.dialogueLines = new string[] {
                "40..",
                "41..",
                "42... Game on!"
            };
        dMan.portPic = player.GetComponent<PlayerBrioManager>().portPic;
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
        scriptMan.ActionOnClose("HidNowSeeking");

        // Turn on GUI if present
        if (uMan.bControlsActive)
            touches.transform.localScale = Vector3.one;
    }

    public void HidNowSeeking()
    {
        // Fade in
        screenFader.GetComponent<Animator>().SetBool("FadeIn", true);

        // Resume the player's movement
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = false;
    }

    public void HideAndSeekFindingKid(string _name)
    {
        if (_name == "Al-khidr")
        {
            Q4LastKidFound = "Al-khidr";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Atandwa")
        {
            Q4LastKidFound = "Atandwa";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Eliz")
        {
            Q4LastKidFound = "Eliz";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Thabo")
        {
            Q4LastKidFound = "Thabo";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Zola")
        {
            Q4LastKidFound = "Zola";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Marija")
        {
            Q4LastKidFound = "Marija";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
    }

    public void HideAndSeekFoundKid()
    {
        var tempCool = "";

        for (int i = 0; i < Q4KidCounter; i++)
        {
            if (i == 0)
            {
                tempCool = "Cool";
            }
            else
            {
                tempCool += " cool";
            }
        }

        if (Q4KidCounter < 6)
        {
            // Set dialogue & dialogue elements
            dMan.dialogueLines = new string[] {
                tempCool + ".. That's " + Q4KidCounter + " of 6."
            };
            dMan.portPic = player.GetComponent<PlayerBrioManager>().portPic;
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
        }
        else
        {
            // Set dialogue & dialogue elements
            dMan.dialogueLines = new string[] {
                "Shibby.. Found all of you."
            };
            dMan.portPic = player.GetComponent<PlayerBrioManager>().portPic;
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
            scriptMan.ActionOnClose("PreHideAndSeekFinished");

            // Last kid talked to, dh.hasEntered = false
            CheckAndDisableLastKidFound();
        }
    }

    IEnumerator HideAndSeekFinished()
    {
        yield return new WaitForSeconds(2);

        // Deactivate Al-khidr DH (order matters?)
        npc_al_khidr.transform.GetChild(1).gameObject.SetActive(false);

        // Move Al-khidr, Al-khidr's DH2, Dan & Camera
        npc_al_khidr.transform.localPosition = new Vector2(4.07f, -9.43f);
        npc_al_khidr.transform.GetChild(2).gameObject.transform.localPosition = new Vector2(0f, 0f);
        player.transform.localPosition = new Vector2(-19.157f, -1.674f);
        mainCamera.transform.localPosition = new Vector2(-19.157f, -1.674f);

        // Activate Al-khidr DH2 (order matters?)
        npc_al_khidr.transform.GetChild(2).gameObject.SetActive(true);

        // "Turn off" Kids 5-9
        npc_atandwa.transform.localScale = Vector3.zero;
        npc_eliz.transform.localScale = Vector3.zero;
        npc_thabo.transform.localScale = Vector3.zero;
        npc_zola.transform.localScale = Vector3.zero;
        npc_marija.transform.localScale = Vector3.zero;

        // Fade in
        screenFader.GetComponent<Animator>().SetBool("FadeIn", true);

        // Turn on GUI if present
        if (uMan.bControlsActive)
        {
            touches.transform.localScale = Vector3.one;
        }

        yield return new WaitForSeconds(1);

        Quest4Reward();
    }

    public void HideAndSeekReset()
    {
        if (!quest4.GetComponent<QuestObject>().bHasCollected)
        {
            // Turn on Quest Trigger ActionOnClose
            npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().bActionOnClose = true;

            // Move Al-khidr back to his spot & disable HideAndSeekKid script
            npc_al_khidr.transform.localPosition = new Vector2(4.07f, -9.43f);
            npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = false;

            // Renable all of the other kids' HideAndSeekKid scripts
            npc_atandwa.transform.GetChild(0).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = true;
            npc_eliz.transform.GetChild(0).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = true;
            npc_marija.transform.GetChild(0).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = true;
            npc_thabo.transform.GetChild(0).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = true;
            npc_zola.transform.GetChild(0).gameObject.GetComponent<Chp1HideAndSeekKid>().enabled = true;

            // Reset Al-khidr dialogues
            npc_al_khidr.transform.GetChild(1).gameObject.SetActive(false);
            npc_al_khidr.transform.GetChild(0).gameObject.SetActive(true);
            npc_al_khidr.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;

            // Reset Dialogue Holders
            npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
            npc_atandwa.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
            npc_eliz.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
            npc_thabo.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
            npc_zola.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
            npc_marija.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;

            // Reset everything else
            qMan.questsStarted[4] = false;
            quest4.GetComponent<QuestObject>().bHasStarted = false;
            Q4KidCounter = 0;
            oMan.ResetOptions();

            // "Turn off" Kids 5-9
            npc_atandwa.transform.localScale = Vector3.zero;
            npc_eliz.transform.localScale = Vector3.zero;
            npc_thabo.transform.localScale = Vector3.zero;
            npc_zola.transform.localScale = Vector3.zero;
            npc_marija.transform.localScale = Vector3.zero;
        }
    }

    public void LoadNPCLocations()
    {
        //npc;
        //if (PlayerPrefs.GetFloat("Location-NPC-Prop"))
        //npc.transform.position = (SavedX, SavedY);

        //npc_akira;
        if (PlayerPrefs.GetFloat("CampusAkiraPosX") != 0 ||
            PlayerPrefs.GetFloat("CampusAkiraPosY") != 0)
            npc_akira.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CampusAkiraPosX"),
                PlayerPrefs.GetFloat("CampusAkiraPosY"));

        //npc_aaron;
        // n/a 

        //npc_al_khidr;
        // n/a

        //npc_ashera;
        // n/a

        //npc_atandwa;
        // n/a

        //npc_bazlo;
        // n/a

        //npc_brackey;
        // n/a

        //npc_canaan;
        // n/a

        //npc_chun;
        // n/a

        //npc_dagon;
        // TBD

        //npc_dilum;
        if (PlayerPrefs.GetFloat("CannaDilumPosX") != 0 ||
            PlayerPrefs.GetFloat("CannaDilumPosY") != 0)
            npc_dilum.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CannaDilumPosX"),
                PlayerPrefs.GetFloat("CannaDilumPosY"));

        //npc_eliz;
        // n/a

        //npc_eloher;
        if (PlayerPrefs.GetFloat("CampusEloherPosX") != 0 ||
            PlayerPrefs.GetFloat("CampusEloherPosY") != 0)
            npc_eloher.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CampusEloherPosX"),
                PlayerPrefs.GetFloat("CampusEloherPosY"));

        //npc_emma;
        // n/a 

        //npc_enki;
        if (PlayerPrefs.GetFloat("HomeEnkiPosX") != 0 ||
            PlayerPrefs.GetFloat("HomeEnkiPosY") != 0)
            npc_enki.transform.position = new Vector2(
                PlayerPrefs.GetFloat("HomeEnkiPosX"),
                PlayerPrefs.GetFloat("HomeEnkiPosY"));

        //npc_heartha;
        // n/a

        //npc_hugo;
        if (PlayerPrefs.GetFloat("CampusHugoPosX") != 0 ||
            PlayerPrefs.GetFloat("CampusHugoPosY") != 0)
            npc_hugo.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CampusHugoPosX"),
                PlayerPrefs.GetFloat("CampusHugoPosY"));

        //npc_hye;
        // n/a

        //npc_jamaal;
        // n/a

        //npc_jamie;
        // n/a

        //npc_kalika;
        if (PlayerPrefs.GetFloat("CampusFaithPosX") != 0 ||
            PlayerPrefs.GetFloat("CampusFaithPosY") != 0)
            npc_kalika.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CampusFaithPosX"),
                PlayerPrefs.GetFloat("CampusFaithPosY"));

        //npc_kart;
        // n/a

        //npc_khalid;
        // n/a 

        //npc_luli;
        // n/a

        //npc_lupita;
        // n/a

        //npc_marija;
        // n/a

        //npc_marklar;
        if (PlayerPrefs.GetFloat("BatteryMarklarPosX") != 0 ||
            PlayerPrefs.GetFloat("BatteryMarklarPosY") != 0)
            npc_marklar.transform.position = new Vector2(
                PlayerPrefs.GetFloat("BatteryMarklarPosX"),
                PlayerPrefs.GetFloat("BatteryMarklarPosY"));

        //npc_mawkus;
        // n/a

        //npc_maximiliano;
        // n/a

        //npc_molleen;
        // n/a

        //npc_nesbit;
        // n/a

        //npc_pebs;
        // n/a

        //npc_poeia;
        // n/a

        //npc_pookieB1;
        if (PlayerPrefs.GetFloat("CannaPookieBear1PosX") != 0 ||
            PlayerPrefs.GetFloat("CannaPookieBear1PosY") != 0)
            npc_pookieB1.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CannaPookieBear1PosX"),
                PlayerPrefs.GetFloat("CannaPookieBear1PosY"));

        //npc_pookieB2;
        if (PlayerPrefs.GetFloat("CannaPookieBear2PosX") != 0 ||
            PlayerPrefs.GetFloat("CannaPookieBear2PosY") != 0)
            npc_pookieB2.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CannaPookieBear2PosX"),
                PlayerPrefs.GetFloat("CannaPookieBear2PosY"));

        //npc_ryuu;
        if (PlayerPrefs.GetFloat("BuildersRyuuPosX") != 0 ||
            PlayerPrefs.GetFloat("BuildersRyuuPosY") != 0)
            npc_ryuu.transform.position = new Vector2(
                PlayerPrefs.GetFloat("BuildersRyuuPosX"),
                PlayerPrefs.GetFloat("BuildersRyuuPosY"));

        //npc_sula;
        if (PlayerPrefs.GetFloat("CannaSulaPosX") != 0 ||
            PlayerPrefs.GetFloat("CannaSulaPosY") != 0)
            npc_sula.transform.position = new Vector2(
                PlayerPrefs.GetFloat("CannaSulaPosX"),
                PlayerPrefs.GetFloat("CannaSulaPosY"));

        //npc_sydelle;
        // n/a

        //npc_tevin;
        // n/a 

        //npc_thabo;
        // n/a

        //npc_wharf;
        // n/a 

        //npc_yvette;
        // n/a 

        //npc_zola;
        // n/a
    }

    public void PookieCheck()
    {
        if (!bHasFedPookie1 &&
            quest7.GetComponent<QuestObject>().bHasStarted)
        {
            PookieCheck(npc_pookieB1, Q7Options, "Q7");
        }

        if (!bHasFedPookie2 &&
            quest8.GetComponent<QuestObject>().bHasStarted)
        {
            PookieCheck(npc_pookieB2, Q8Options, "Q8");
        }
    }

    public void PookieCheck(GameObject _pookieBear, string _qOptions, string _questNum)
    {
        if (_questNum == "Q7" &&
            (bHasFedPookie1 || 
             PlayerPrefs.GetInt("Chp1Pookie1Fed") == 1))
            return;
        if (_questNum == "Q8" &&
            (bHasFedPookie2 ||
             PlayerPrefs.GetInt("Chp1Pookie2Fed") == 1))
            return;

        // Set the counters
        int greenCounter = 0;
        int orangeCounter = 0;
        int purpleCounter = 0;
        int whiteCounter = 0;

        // Check each item
        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();
            item = item.Substring(0, item.Length - 7);

            // Green nug check
            if (item == "Cannabis.Bud.SmoochyWoochyPoochy" ||
                item == "Cannabis.Bud.NaturesCandy" ||
                item == "Cannabis.Bud.TheDevilsLettuce")
            {
                greenCounter += 1;
            }

            // Orange nug
            if (item == "Cannabis.Bud.BootyJuice" ||
                item == "Cannabis.Bud.Catnip" ||
                item == "Cannabis.Bud.SnoopLeone")
            {
                orangeCounter += 1;
            }

            // Purple nug
            if (item == "Cannabis.Bud.GranPapasMedicine" ||
                item == "Cannabis.Bud.PurpleNurple" ||
                item == "Cannabis.Bud.RighteousBud")
            {
                purpleCounter += 1;
            }

            // White nug
            if (item == "Cannabis.Bud.CreeperBud" ||
                item == "Cannabis.Bud.MastaRoshi" ||
                item == "Cannabis.Bud.WhiteWalker")
            {
                whiteCounter += 1;
            }
        }

        // Set the bools
        bool bHasGreen;
        bool bHasOrange;
        bool bHasPurple;
        bool bHasWhite;

        if (greenCounter > 0)
            bHasGreen = true;
        else
            bHasGreen = false;

        if (orangeCounter > 0)
            bHasOrange = true;
        else
            bHasOrange = false;

        if (purpleCounter > 0)
            bHasPurple = true;
        else
            bHasPurple = false;

        if (whiteCounter > 0)
            bHasWhite = true;
        else
            bHasWhite = false;

        // if one of them present, activate options handler on Pookie
        if (bHasGreen ||
            bHasOrange ||
            bHasPurple ||
            bHasWhite)
        {
            // Activate DH w/ OH
            _pookieBear.transform.GetChild(0).gameObject.SetActive(false);
            _pookieBear.transform.GetChild(1).gameObject.SetActive(true);

            if (bHasGreen &&
                bHasOrange &&
                bHasPurple &&
                bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[4];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[3] = "Give em a white nug";

                _qOptions = "gopw";
            }
            else if (bHasGreen &&
                     bHasPurple &&
                     bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                _qOptions = "gpw";
            }
            else if (bHasGreen &&
                     bHasOrange &&
                     bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                _qOptions = "gow";
            }
            else if (bHasGreen &&
                     bHasOrange &&
                     bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";

                _qOptions = "gop";
            }
            else if (bHasOrange &&
                     bHasPurple &&
                     bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                _qOptions = "opw";
            }
            else if (bHasGreen &&
                     bHasOrange)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";

                _qOptions = "go";
            }
            else if (bHasGreen &&
                     bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                _qOptions = "gp";
            }
            else if (bHasGreen &&
                     bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                _qOptions = "gw";
            }
            else if (bHasOrange &&
                     bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                _qOptions = "op";
            }
            else if (bHasOrange &&
                     bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                _qOptions = "ow";
            }
            else if (bHasPurple &&
                     bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                _qOptions = "pw";
            }
            else if (bHasGreen)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";

                _qOptions = "g";
            }
            else if (bHasOrange)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";

                _qOptions = "o";
            }
            else if (bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";

                _qOptions = "p";
            }
            else if (bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a white nug";

                _qOptions = "w";
            }
        }
        else
        {
            // Activate DH (no OH)
            _pookieBear.transform.GetChild(0).gameObject.SetActive(true);
            _pookieBear.transform.GetChild(1).gameObject.SetActive(false);
        }

        // Set QXOptions
        if (_questNum == "Q7")
            Q7Options = _qOptions;
        if (_questNum == "Q8")
            Q8Options = _qOptions;
    }
    
    IEnumerator PookieBear1Animations()
    {
        yield return new WaitForSeconds(2);

        npc_pookieB1.GetComponent<Animator>().Play("Sit Normal");

        yield return new WaitForSeconds(3);

        npc_pookieB1.GetComponent<Animator>().Play("Sit Eyes");

        yield return new WaitForSeconds(2);

        npc_pookieB1.GetComponent<Animator>().Play("Sit Happy");
    }

    public void PookieBear1DisableAndAnimate()
    {
        // Disable Pookie movement & dialogue
        npc_pookieB1.GetComponent<NPCMovement>().enabled = false;
        npc_pookieB1.transform.GetChild(0).gameObject.SetActive(false);
        npc_pookieB1.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(PookieBear1Animations());
    }

    IEnumerator PookieBear2Animations()
    {
        yield return new WaitForSeconds(2);

        npc_pookieB2.GetComponent<Animator>().Play("Sit Normal");

        yield return new WaitForSeconds(3);

        npc_pookieB2.GetComponent<Animator>().Play("Sit Eyes");

        yield return new WaitForSeconds(2);

        npc_pookieB2.GetComponent<Animator>().Play("Sit Happy");
    }

    public void PookieBear2DisableAndAnimate()
    {
        // Disable Pookie movement & dialogue
        npc_pookieB2.GetComponent<NPCMovement>().enabled = false;
        npc_pookieB2.transform.GetChild(0).gameObject.SetActive(false);
        npc_pookieB2.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(PookieBear2Animations());
    }

    public void PookieQuestStatus()
    {
        if (bHasFedPookie1 &&
            bHasFedPookie2)
        {
            npc_dilum.transform.GetChild(0).gameObject.SetActive(false);
            npc_dilum.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public void PookieQuestComplete()
    {
        if (!quest7.GetComponent<QuestObject>().bHasCollected &&
            !quest8.GetComponent<QuestObject>().bHasCollected)
        {
            dMan.portPic = quest7.GetComponent<QuestObject>().portPic;
            qMan.ShowQuestText(quest7.GetComponent<QuestObject>().endText);
        }

        Quest7And8Reward();
    }

    public void PreHideAndSeek()
    {
        // Avoid talking to Al-khidr
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasExited = true;

        // Stops the player's movement
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        // Fade out
        screenFader.GetComponent<Animator>().Play("FadeOut");

        // Turn off GUI
        touches.transform.localScale = Vector3.zero;

        StartCoroutine(HideAndSeek());
    }

    public void PreHideAndSeekFinished()
    {
        StartCoroutine(HideAndSeekFinished());

        // Stops the player's movement
        player.GetComponent<PlayerMovement>().StopPlayerMovement();

        // Fade out
        screenFader.GetComponent<Animator>().Play("FadeOut");

        // Turn off GUI
        touches.transform.localScale = Vector3.zero;
    }

    public void ResetGreatTreeDialogueToMiddle()
    {
        if (greatTree.transform.GetChild(3).gameObject.activeSelf)
            greatTree.transform.GetChild(3).gameObject.SetActive(false);

        if (greatTree.transform.GetChild(4).gameObject.activeSelf)
            greatTree.transform.GetChild(4).gameObject.SetActive(false);

        greatTree.transform.GetChild(2).gameObject.SetActive(true); 
    }

    public void SaveNPCLocations()
    {
        //npc
        //PlayerPrefs.SetFloat("Location-NPC-Prop", value);

        //npc_aaron;
        // n/a 

        //npc_akira;
        PlayerPrefs.SetFloat("CampusAkiraPosX", npc_akira.transform.position.x);
        PlayerPrefs.SetFloat("CampusAkiraPosY", npc_akira.transform.position.y);

        //npc_al_khidr;
        // n/a

        //npc_ashera;
        // n/a

        //npc_atandwa;
        // n/a

        //npc_bazlo;
        // n/a

        //npc_brackey;
        // n/a

        //npc_canaan;
        // n/a

        //npc_chun;
        // n/a

        //npc_dagon;
        // TBD

        //npc_dilum;
        PlayerPrefs.SetFloat("CannaDilumPosX", npc_dilum.transform.position.x);
        PlayerPrefs.SetFloat("CannaDilumPosY", npc_dilum.transform.position.y);

        //npc_eliz;
        // n/a

        //npc_eloher;
        PlayerPrefs.SetFloat("CampusEloherPosX", npc_eloher.transform.position.x);
        PlayerPrefs.SetFloat("CampusEloherPosY", npc_eloher.transform.position.y);

        //npc_emma;
        // n/a 

        //npc_enki;
        PlayerPrefs.SetFloat("HomeEnkiPosX", npc_enki.transform.position.x);
        PlayerPrefs.SetFloat("HomeEnkiPosY", npc_enki.transform.position.y);

        //npc_heartha;
        // n/a

        //npc_hugo;
        PlayerPrefs.SetFloat("CampusHugoPosX", npc_hugo.transform.position.x);
        PlayerPrefs.SetFloat("CampusHugoPosY", npc_hugo.transform.position.y);

        //npc_hye;
        // n/a

        //npc_jamaal;
        // n/a

        //npc_jamie;
        // n/a

        //npc_juman;
        // n/a

        //npc_kalika;
        PlayerPrefs.SetFloat("CampusFaithPosX", npc_kalika.transform.position.x);
        PlayerPrefs.SetFloat("CampusFaithPosY", npc_kalika.transform.position.y);

        //npc_kart;
        // n/a

        //npc_khalid;
        // n/a 

        //npc_luli;
        // n/a

        //npc_lupita;
        // n/a

        //npc_marija;
        // n/a

        //npc_marklar;
        PlayerPrefs.SetFloat("BatteryMarklarPosX", npc_marklar.transform.position.x);
        PlayerPrefs.SetFloat("BatteryMarklarPosY", npc_marklar.transform.position.y);

        //npc_mawkus;
        // n/a

        //npc_maximiliano;
        // n/a

        //npc_molleen;
        // n/a

        //npc_nesbit;
        // n/a

        //npc_poeia;
        // n/a

        //npc_pookieB1;
        PlayerPrefs.SetFloat("CannaPookieBear1PosX", npc_pookieB1.transform.position.x);
        PlayerPrefs.SetFloat("CannaPookieBear1PosY", npc_pookieB1.transform.position.y);

        //npc_pookieB2;
        PlayerPrefs.SetFloat("CannaPookieBear2PosX", npc_pookieB2.transform.position.x);
        PlayerPrefs.SetFloat("CannaPookieBear2PosY", npc_pookieB2.transform.position.y);

        //npc_ryuu;
        PlayerPrefs.SetFloat("BuildersRyuuPosX", npc_ryuu.transform.position.x);
        PlayerPrefs.SetFloat("BuildersRyuuPosY", npc_ryuu.transform.position.y);

        //npc_sula;
        PlayerPrefs.SetFloat("CannaSulaPosX", npc_sula.transform.position.x);
        PlayerPrefs.SetFloat("CannaSulaPosY", npc_sula.transform.position.y);
        
        //npc_sydelle;
        // n/a

        //npc_tevin;
        // n/a 

        //npc_thabo;
        // n/a

        //npc_wharf;
        // n/a 

        //npc_yvette;
        // n/a 

        //npc_zola;
        // n/a
    }

    public void SaveSpecificInfo()
    {
        if (bHasFedPookie1)
            PlayerPrefs.SetInt("Chp1Pookie1Fed", 1);
        if (bHasFedPookie2)
            PlayerPrefs.SetInt("Chp1Pookie2Fed", 1);

        SaveNPCLocations();
    }

    public void SetAsheraVogging()
    {
        npc_ashera.GetComponent<Animator>().SetBool("bIsVogging", true);
    }

    public void SetNPCsVogging()
    {
        npc_ashera.GetComponent<Animator>().SetBool("bIsVogging", true);
        npc_dagon.GetComponent<Animator>().SetBool("bIsVogging", true);
    }

    public void SmoochyWoochyCheck()
    {
        if (!quest3.GetComponent<QuestObject>().bHasCollected)
        {
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (item == "Cannabis.Bud.SmoochyWoochyPoochy")
                {
                    bHasQ3SmoochWoochy = true;
                }
            }

            if (bHasQ3SmoochWoochy &&
                !dMan.bDialogueActive &&
                quest3.GetComponent<QuestObject>().bHasStarted)
            {
                npc_enki.transform.GetChild(0).gameObject.SetActive(false);
                npc_enki.transform.GetChild(1).gameObject.SetActive(true);

                npc_enki.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().bEndQuest = true;

                npc_enki.transform.GetChild(1).gameObject.GetComponent<PolygonCollider2D>().isTrigger = false;
                npc_enki.transform.GetChild(1).gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
            }
            else
            {
                npc_enki.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().bEndQuest = false;

                npc_enki.transform.GetChild(0).gameObject.SetActive(true);
                npc_enki.transform.GetChild(1).gameObject.SetActive(false);
            }

            // Reset bool check
            bHasQ3SmoochWoochy = false;
        }
    }

    IEnumerator TogglePlayerHitBoxDelay()
    {
        yield return new WaitForSeconds(0.1f);

        TogglePlayerHitbox();
    }

    public void TogglePlayerHitbox()
    {
        player.GetComponent<PolygonCollider2D>().enabled = false;
        player.GetComponent<PolygonCollider2D>().enabled = true;
    }

    public void TreeHouse()
    {
        StartCoroutine(DelayTreeHouse());
    }

    public void WarpToGuessWhoColluded()
    {
        // Transition animation
        warpGWC.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save info
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        save.SavePositionTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpGWC.GetComponent<SceneTransitioner>().BetaLoad);

        // Save Quests
        SaveQuests();

        // Stop the player from bringing up the dialogue again
        dMan.gameObject.SetActive(false);

        // Stop Dan from moving
        player.GetComponent<Animator>().enabled = false;

        // Stop NPCs from moving
        npc_chun.GetComponent<Animator>().enabled = false;
    }
    
    /** Quests **/

    public void Quest0Reward()
    {
        if (!quest0.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uMan.UpdateBrio();

            quest0.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest1Reward()
    {
        if (!quest1.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uMan.UpdateBrio();

            quest1.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest2Reward()
    {
        if (!quest2.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(5);
            uMan.UpdateBrio();

            quest2.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest3Reward()
    {
        if (quest3.GetComponent<QuestObject>().bHasEnded &&
            !quest3.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(5);
            uMan.UpdateBrio();

            quest3.GetComponent<QuestObject>().CollectedQuest();

            // Remove the first bud in inv
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (item == "Cannabis.Bud.SmoochyWoochyPoochy")
                {
                    inv.Remove(inv.items[i]);
                    return;
                }
            }

            // TODO: disable SmoochyWoochyCheck() here, QT, DH, SM, etc.
        }
    }

    public void Quest4Reward()
    {
        if (!quest4.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(15);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(20);
            uMan.UpdateBrio();

            quest4.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest5Reward()
    {
        if (!quest5.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(420);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(420);
            uMan.UpdateBrio();

            greatTree.transform.GetChild(0).gameObject.SetActive(false);
            greatTree.transform.GetChild(5).gameObject.SetActive(true);

            quest5.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest6Reward()
    {
        if (!quest6.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(15);
            uMan.UpdateBrio();

            quest6.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest7And8Reward()
    {
        if (!quest7.GetComponent<QuestObject>().bHasCollected &&
            !quest8.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(25);
            uMan.UpdateBrio();

            quest7.GetComponent<QuestObject>().CollectedQuest();
            quest8.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest12Reward()
    {
        if (!quest12.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(100);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(100);
            uMan.UpdateBrio();

            npc_pebs.transform.GetChild(0).gameObject.SetActive(false);
            npc_pebs.transform.GetChild(1).gameObject.SetActive(true);

            quest12.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void QuestDialogueCheck()
    {
        // Quest 0 - Dialogue 1 - Option 1
        if (npc_dagon.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            npc_dagon.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest0Dialogue1Opt1();
        }
        // Quest 0 - Dialogue 1 - Option 2
        else if (npc_dagon.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_dagon.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest0Dialogue1Opt2();
        }
        // Quest 0 - Dialogue 2 - Option *
        else if (npc_dagon.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_dagon.transform.GetChild(1).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2))
        {
            oMan.ResetOptions();
            Quest0Dialogue2();
        }


        // Quest 4 - Dialogue 1 - Option 1
        if (npc_al_khidr.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            npc_al_khidr.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest4Dialogue1Opt1();
        }
        // Quest 4 - Dialogue 1 - Option 2
        else if (npc_al_khidr.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_al_khidr.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest4Dialogue1Opt2();
        }


        // Quest 5 - Dialogue 1 - Option *
        if (greatTree.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            greatTree.transform.GetChild(0).gameObject.activeSelf &&
            (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2))
        {
            oMan.ResetOptions();
            Quest5Dialogue1();
        }
        // Quest 5 - Dialogue 2 - Option *
        else if (greatTree.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(1).gameObject.activeSelf &&
                 (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                  moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2))
        {
            oMan.ResetOptions();
            Quest5Dialogue2();
        }
        // Quest 5 - Dialogue 3 - Option 1
        else if (greatTree.transform.GetChild(2).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(2).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest5Dialogue3Opt1();
        }
        // Quest 5 - Dialogue 3 - Option 2
        else if (greatTree.transform.GetChild(2).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(2).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest5Dialogue3Opt2();
        }
        // Quest 5 - Dialogue 3 - Option 3
        else if (greatTree.transform.GetChild(2).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(2).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3)
        {
            oMan.ResetOptions();
            Quest5Dialogue3Opt3();
        }
        // Quest 5 - Dialogue 6 - Option *
        else if (greatTree.transform.GetChild(5).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(5).gameObject.activeSelf &&
                 (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                  moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
                  moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3))
        {
            oMan.ResetOptions();
            Quest5Dialogue6();
        }


        // Quest 6 - Dialogue 1 - Option 1
        if (npc_chun.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            npc_chun.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest6Dialogue1Opt1();
        }
        // Quest 6 - Dialogue 1 - Option 2
        else if (npc_chun.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_chun.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest6Dialogue1Opt2();
        }


        // Quest 7 - Dialogue 1 - Option *
        if (npc_pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
            npc_pookieB1.transform.GetChild(1).gameObject.activeSelf &&
            (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt4))
        {
            Q7SelectedOption = moveOptsArw.currentPosition.ToString();

            oMan.ResetOptions();
            dMan.ResetDialogue();
            Quest7Dialogue1Opt();
        }

        // Quest 8 - Dialogue 1 - Option *
        if (npc_pookieB2.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
            npc_pookieB2.transform.GetChild(1).gameObject.activeSelf &&
            (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3 ||
             moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt4))
        {
            Q8SelectedOption = moveOptsArw.currentPosition.ToString();

            oMan.ResetOptions();
            dMan.ResetDialogue();
            Quest8Dialogue1Opt();
        }


        // Quest 9 - Dialogue 1 - Option 1
        if (npc_heartha.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            npc_heartha.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest9Dialogue1Opt1();
        }
        // Quest 9 - Dialogue 1 - Option 2
        else if (npc_heartha.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_heartha.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest9Dialogue1Opt2();
        }


        // Quest 10 - Dialogue 1 - Option 1
        if (npc_brackey.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            npc_brackey.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest10Dialogue1Opt1();
        }
        // Quest 10 - Dialogue 1 - Option 2
        else if (npc_brackey.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_brackey.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest10Dialogue1Opt2();
        }
        // Quest 10 - Dialogue 2 - Option 1
        else if(npc_brackey.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                npc_brackey.transform.GetChild(1).gameObject.activeSelf &&
                moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest10Dialogue2Opt1();
        }
        // Quest 10 - Dialogue 2 - Option 2
        else if (npc_brackey.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_brackey.transform.GetChild(1).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest10Dialogue2Opt2();
        }


        // Quest 12 - Dialogue 1 - Option 1
        if (npc_pebs.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            npc_pebs.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest12Dialogue1Opt1();
        }
        // Quest 12 - Dialogue 1 - Option 2
        else if (npc_pebs.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 npc_pebs.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest12Dialogue1Opt2();
        }
    }

    public void Quest0Dialogue1Opt1()
    {
        npc_dagon.transform.GetChild(0).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(1).gameObject.SetActive(true);

        npc_dagon.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();
    }

    public void Quest0Dialogue1Opt2()
    {
        // No play a game
        dMan.dialogueLines = new string[] {
                "Hmm.. Perhaps later..."
            };
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
    }

    public void Quest0Dialogue2()
    {
        // 05/11/2018 DC TODO -- Add divergent options, i.e. different text per option selected

        npc_dagon.transform.GetChild(1).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(2).gameObject.SetActive(true);

        quest0.GetComponent<QuestObject>().EndQuest();

        // Quest 0 -- Q&A 1 Reward
        Quest0Reward();

        // DC TODO 08/12/2018 -- Have dad walk to the shed and stay there to "work" w/ VR googles and dan k.
        // DC TODO 08/13/2018 -- Dad keeps twisting and turning instead of still looking at dan
    }

    public void Quest4Dialogue1Opt1()
    {
        // Yes -- Play
        npc_al_khidr.transform.GetChild(0).gameObject.SetActive(false);
        npc_al_khidr.transform.GetChild(1).gameObject.SetActive(true);

        // Avoid talking to Al-khidr
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;

        player.transform.position = new Vector3(
            player.transform.position.x + 0.001f,
            player.transform.position.y + 0.001f,
            player.transform.position.z
        );
    }

    public void Quest4Dialogue1Opt2()
    {
        // No play a game
        dMan.dialogueLines = new string[] {
                "Hmm.. Perhaps later..."
            };
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
    }

    public void Quest5Dialogue1()
    {
        greatTree.transform.GetChild(0).gameObject.SetActive(false);
        greatTree.transform.GetChild(1).gameObject.SetActive(true);

        greatTree.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();
    }

    public void Quest5Dialogue2()
    {
        greatTree.transform.GetChild(1).gameObject.SetActive(false);
        greatTree.transform.GetChild(2).gameObject.SetActive(true);

        greatTree.transform.GetChild(2).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();
    }

    public void Quest5Dialogue3Opt1()
    {
        greatTree.transform.GetChild(2).gameObject.SetActive(false);
        greatTree.transform.GetChild(3).gameObject.SetActive(true);

        warpCSTreeTunnel.GetComponent<BoxCollider2D>().enabled = true;
        warpCSTreeTunnel.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values 
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        save.SavePositionTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpCSTreeTunnel.GetComponent<SceneTransitioner>().BetaLoad);

        // Save Quests
        SaveQuests();

        // Stop the player from bringing up the dialog again 
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Stop Dan from moving
        dMan.gameObject.SetActive(false);
    }

    public void Quest5Dialogue3Opt2()
    {
        greatTree.transform.GetChild(2).gameObject.SetActive(false);
        greatTree.transform.GetChild(3).gameObject.SetActive(true);

        greatTree.transform.GetChild(3).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();

        dMan.closingAction = "ResetGreatTreeDialogueToMiddle";
    }

    public void Quest5Dialogue3Opt3()
    {
        greatTree.transform.GetChild(2).gameObject.SetActive(false);
        greatTree.transform.GetChild(4).gameObject.SetActive(true);

        greatTree.transform.GetChild(4).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();

        dMan.closingAction = "ResetGreatTreeDialogueToMiddle";
    }

    public void Quest5Dialogue6()
    {
        greatTree.transform.GetChild(5).gameObject.SetActive(false);
        greatTree.transform.GetChild(6).gameObject.SetActive(true);

        greatTree.transform.GetChild(6).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();
    }

    public void Quest6Dialogue1Opt1()
    {
        // yes play a game
        // DC TODO -- offer difficulty choices

        CheckingForGoggles();

        if (bHasGoggles)
        {
            warpMinesweeper.GetComponent<BoxCollider2D>().enabled = true;
            warpMinesweeper.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

            // Save Transfer Values 
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            save.SavePositionTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", warpMinesweeper.GetComponent<SceneTransitioner>().BetaLoad);

            // Save Quests
            SaveQuests();

            // Stop the player from bringing up the dialog again 
            dMan.gameObject.transform.localScale = Vector3.zero;

            // Stop Dan from moving
            dMan.gameObject.SetActive(false);

            // Stop NPCs from moving
            npc_chun.GetComponent<NPCMovement>().moveSpeed = 0;
            npc_chun.GetComponent<Animator>().enabled = false;

            // TODO: stop other NPCs in the area
            // TODO: restoring NPC movement when revisiting, post mini-game
        }
        else
        {
            // No play a game
            dMan.dialogueLines = new string[] {
                "Oh, well.. You'll need Particle Visors to play.",
                "Come back when you have some."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
        }
    }

    public void Quest6Dialogue1Opt2()
    {
        // No play a game
        dMan.dialogueLines = new string[] {
                "Sure.. Perhaps later..."
            };
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
    }

    public void Quest7Dialogue1Opt()
    {
        // Get the selected option, i.e. OptX
        char[] Q7SelectionChar = Q7SelectedOption.ToCharArray();
        int Q7SelectionInt = Q7SelectionChar[3] - '1';

        // Get the corresponding value, i.e. gopw
        char[] Q7BudsChar = Q7Options.ToCharArray();

        bool bTempCheck = true;

        if (Q7BudsChar[Q7SelectionInt] == 'g')
        {
            // Remove green
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.SmoochyWoochyPoochy" ||
                     item == "Cannabis.Bud.NaturesCandy" ||
                     item == "Cannabis.Bud.TheDevilsLettuce"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }
        else if (Q7BudsChar[Q7SelectionInt] == 'o')
        {
            // Remove orange
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.BootyJuice" ||
                     item == "Cannabis.Bud.Catnip" ||
                     item == "Cannabis.Bud.SnoopLeone"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }
        else if (Q7BudsChar[Q7SelectionInt] == 'p')
        {
            // Remove purple
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.GranPapasMedicine" ||
                     item == "Cannabis.Bud.PurpleNurple" ||
                     item == "Cannabis.Bud.RighteousBud"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }
        else if (Q7BudsChar[Q7SelectionInt] == 'w')
        {
            // Remove white
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.CreeperBud" ||
                     item == "Cannabis.Bud.MastaRoshi" ||
                     item == "Cannabis.Bud.WhiteWalker"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }

        // Animate Pookie & stop moving
        if (player.transform.position.x >= npc_pookieB1.transform.position.x)
            npc_pookieB1.GetComponent<Animator>().Play("Eat Right");
        else
            npc_pookieB1.GetComponent<Animator>().Play("Eat Left");

        PookieBear1DisableAndAnimate();

        quest7.GetComponent<QuestObject>().bHasEnded = true;
        qMan.questsEnded[quest7.GetComponent<QuestObject>().questNumber] = true;
        
        bHasFedPookie1 = true;
        PookieQuestStatus();
    }

    public void Quest8Dialogue1Opt()
    {
        // Get the selected option, i.e. OptX
        char[] Q8SelectionChar = Q8SelectedOption.ToCharArray();
        int Q8SelectionInt = Q8SelectionChar[3] - '1';

        // Get the corresponding value, i.e. gopw
        char[] Q8BudsChar = Q8Options.ToCharArray();

        bool bTempCheck = true;

        if (Q8BudsChar[Q8SelectionInt] == 'g')
        {
            // Remove green
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.SmoochyWoochyPoochy" ||
                     item == "Cannabis.Bud.NaturesCandy" ||
                     item == "Cannabis.Bud.TheDevilsLettuce"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }
        else if (Q8BudsChar[Q8SelectionInt] == 'o')
        {
            // Remove orange
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.BootyJuice" ||
                     item == "Cannabis.Bud.Catnip" ||
                     item == "Cannabis.Bud.SnoopLeone"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }
        else if (Q8BudsChar[Q8SelectionInt] == 'p')
        {
            // Remove purple
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.GranPapasMedicine" ||
                     item == "Cannabis.Bud.PurpleNurple" ||
                     item == "Cannabis.Bud.RighteousBud"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }
        else if (Q8BudsChar[Q8SelectionInt] == 'w')
        {
            // Remove white
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if (bTempCheck &&
                    (item == "Cannabis.Bud.CreeperBud" ||
                     item == "Cannabis.Bud.MastaRoshi" ||
                     item == "Cannabis.Bud.WhiteWalker"))
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }

        // Animate Pookie & stop moving
        if (player.transform.position.x >= npc_pookieB2.transform.position.x)
            npc_pookieB2.GetComponent<Animator>().Play("Eat Right");
        else
            npc_pookieB2.GetComponent<Animator>().Play("Eat Left");
        
        PookieBear2DisableAndAnimate();

        quest8.GetComponent<QuestObject>().bHasEnded = true;
        qMan.questsEnded[quest8.GetComponent<QuestObject>().questNumber] = true;
        
        bHasFedPookie2 = true;
        PookieQuestStatus();
    }

    public void Quest9Dialogue1Opt1()
    {
        // yes play a game
        CheckingForGoggles();

        if (bHasGoggles)
        {
            warpPookieVision.GetComponent<BoxCollider2D>().enabled = true;
            warpPookieVision.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

            // Save Transfer Values 
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            save.SavePositionTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", warpPookieVision.GetComponent<SceneTransitioner>().BetaLoad);

            // Save Quests
            SaveQuests();

            // Stop the player from bringing up the dialog again 
            dMan.gameObject.transform.localScale = Vector3.zero;

            // Stop Dan from moving
            dMan.gameObject.SetActive(false);

            // Stop NPCs from moving
            //npc_heartha.GetComponent<NPCMovement>().moveSpeed = 0;
            //npc_heartha.GetComponent<Animator>().enabled = false;
        }
        else
        {
            // No visors
            dMan.dialogueLines = new string[] {
                "Oh, well.. You'll need Particle Visors to play.",
                "Come back when you have some."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
        }
    }

    public void Quest9Dialogue1Opt2()
    {
        // No play a game
        dMan.dialogueLines = new string[] {
                "Sure.. Perhaps later..."
            };
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
    }

    public void Quest10Dialogue1Opt1()
    {
        // yes play a game
        CheckingForGoggles();

        if (!bHasGoggles)
        {
            // No visors
            dMan.dialogueLines = new string[] {
                "Oh, well.. You'll need Particle Visors to play.",
                "Come back when you have some."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();

            return;
        }

        npc_brackey.transform.GetChild(0).gameObject.SetActive(false);
        npc_brackey.transform.GetChild(1).gameObject.SetActive(true);
        
        if (devDetect.bIsMobile)
        {
            npc_brackey.transform.GetChild(1).GetComponent<OptionsHolder>().options = new string[]
            {
                "TD's Super Best Friends TD"
            };
        }
        else
        {
            npc_brackey.transform.GetChild(1).GetComponent<OptionsHolder>().options = new string[]
            {
                "TD's Super Best Friends TD",
                "TD's Tower Defense"
            };
        }

        // TODO: needed to re-run TriggerCollider in DiaHolder
        player.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y - 0.001f,
            player.transform.position.z);

        npc_brackey.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
        dMan.PauseDialogue();
    }

    public void Quest10Dialogue1Opt2()
    {
        // No play a game
        dMan.dialogueLines = new string[] {
                "Sure.. Perhaps later..."
            };
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
    }

    public void Quest10Dialogue2Opt1()
    {
        warpTD_SBF.GetComponent<BoxCollider2D>().enabled = true;
        warpTD_SBF.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values 
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        save.SavePositionTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpTD_SBF.GetComponent<SceneTransitioner>().BetaLoad);

        // Save Quests
        SaveQuests();

        // Stop the player from bringing up the dialog again 
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Stop Dan from moving
        dMan.gameObject.SetActive(false);

        // Stop NPCs from moving
        //npc_brackey.GetComponent<NPCMovement>().moveSpeed = 0;
        //npc_brackey.GetComponent<Animator>().enabled = false;  
    }

    public void Quest10Dialogue2Opt2()
    {
        warpTowerDeez.GetComponent<BoxCollider2D>().enabled = true;
        warpTowerDeez.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values 
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        save.SavePositionTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpTowerDeez.GetComponent<SceneTransitioner>().BetaLoad);

        // Save Quests
        SaveQuests();

        // Stop the player from bringing up the dialog again 
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Stop Dan from moving
        dMan.gameObject.SetActive(false);

        // Stop NPCs from moving
        //npc_brackey.GetComponent<NPCMovement>().moveSpeed = 0;
        //npc_brackey.GetComponent<Animator>().enabled = false;  
    }

    public void Quest12Dialogue1Opt1()
    {
        // yes for cutscene

        CheckingForGoggles();

        if (bHasGoggles)
        {
            warpCSTyrannyTunnel.GetComponent<BoxCollider2D>().enabled = true;
            warpCSTyrannyTunnel.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

            // Save Transfer Values 
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            save.SavePositionTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", warpCSTyrannyTunnel.GetComponent<SceneTransitioner>().BetaLoad);

            // Save Quests
            SaveQuests();

            // Stop the player from bringing up the dialog again 
            dMan.gameObject.transform.localScale = Vector3.zero;

            // Stop Dan from moving
            dMan.gameObject.SetActive(false);

            // Stop NPCs from moving
            npc_pebs.GetComponent<NPCMovement>().moveSpeed = 0;
            npc_pebs.GetComponent<Animator>().enabled = false;

            // TODO: stop other NPCs in the area
            // TODO: restoring NPC movement when revisiting, post cutscene
        }
        else
        {
            // No cutscene
            dMan.dialogueLines = new string[] {
                "Oh, well.. You'll need Particle Visors to experience it.",
                "Come back when you have some."
            };
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
        }
    }

    public void Quest12Dialogue1Opt2()
    {
        // No play a game
        dMan.dialogueLines = new string[] {
                "No worries. Perhaps later..."
            };
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
    }


    public void Chp1QuestChecker()
    {
        // Q0 
        if (qMan.questsCollected[0])
        {
            DagonInLab();
            SetAsheraVogging();
        }
        else if (qMan.questsStarted[0])
        {
            npc_dagon.transform.GetChild(0).gameObject.SetActive(false);
            npc_dagon.transform.GetChild(1).gameObject.SetActive(true);

            SetNPCsVogging();
        }
        else
            SetNPCsVogging();

        // Q1
        if (qMan.questsCollected[1])
        {
            npc_canaan.transform.GetChild(0).gameObject.SetActive(false);
            npc_canaan.transform.GetChild(1).gameObject.SetActive(true);
            questTrigger1.transform.GetChild(0)
                .GetComponent<QuestTrigger>().bBeginQuest = false;
        }
        else if (qMan.questsEnded[1])
        {
            qMan.questsStarted[1] = false;
            qMan.questsEnded[1] = false;
            quest1.GetComponent<QuestObject>().bHasStarted = false;
            quest1.GetComponent<QuestObject>().bHasEnded = false;
        }
        else if (qMan.questsStarted[1])
        {
            qMan.questsStarted[1] = false;
            quest1.GetComponent<QuestObject>().bHasStarted = false;
        }

        // Q2
        if (qMan.questsCollected[2]) // || questsEnded[2]
        {
            questTrigger2.transform.GetChild(0).gameObject.SetActive(false);
            questTrigger2.transform.GetChild(1).gameObject.SetActive(false);
            treeHouseDoor.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else if (qMan.questsStarted[2])
        {
            TreeHouse();
        }

        // Q3
        if (qMan.questsCollected[3]) // || questsEnded[3]
        {
            npc_enki.transform.GetChild(0).gameObject.SetActive(false);
            npc_enki.transform.GetChild(1).gameObject.SetActive(true);
        }

        // Q4
        if (qMan.questsCollected[4])
        {
            npc_al_khidr.transform.GetChild(0).gameObject.SetActive(false);
            npc_al_khidr.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (qMan.questsEnded[4])
        {
            qMan.questsStarted[4] = false;
            qMan.questsEnded[4] = false;
            quest4.GetComponent<QuestObject>().bHasStarted = false;
            quest4.GetComponent<QuestObject>().bHasEnded = false;
        }
        else if (qMan.questsStarted[4])
        {
            qMan.questsStarted[4] = false;
            quest4.GetComponent<QuestObject>().bHasStarted = false;
        }

        // Q5
        if (qMan.questsCollected[5] &&
            !qMan.questsEnded[5])
        {
            greatTree.transform.GetChild(0).gameObject.SetActive(false);
            greatTree.transform.GetChild(5).gameObject.SetActive(true);
        }
        else if (qMan.questsCollected[5] &&
                 qMan.questsEnded[5])
        {
            greatTree.transform.GetChild(0).gameObject.SetActive(false);
            greatTree.transform.GetChild(6).gameObject.SetActive(true);
        }
        else if (qMan.questsStarted[5])
        {
            greatTree.transform.GetChild(0).gameObject.SetActive(false);
            greatTree.transform.GetChild(2).gameObject.SetActive(true);
        }

        // Q7 & Q8
        if (qMan.questsCollected[7] ||
            qMan.questsCollected[8])
        {
            npc_dilum.transform.GetChild(0).gameObject.SetActive(false);
            npc_dilum.transform.GetChild(1).gameObject.SetActive(true);

            PookieBear1DisableAndAnimate();
            PookieBear2DisableAndAnimate();
        }
        else if (qMan.questsEnded[7] ||
                 qMan.questsEnded[8])
        {
            if (qMan.questsEnded[7])
            {
                bHasFedPookie1 = true;
                PookieBear1DisableAndAnimate();
            }

            if (qMan.questsEnded[8])
            {
                bHasFedPookie2 = true;
                PookieBear2DisableAndAnimate();
            }

            PookieQuestStatus();
        }
        
        // Q7 start and fed (or not) and not complete
        if (!qMan.questsCollected[7] &&
            !qMan.questsEnded[7] &&
            qMan.questsStarted[7])
        {
            if (PlayerPrefs.GetInt("Chp1Pookie1Fed") == 1)
            {
                bHasFedPookie1 = true;
                PookieBear1DisableAndAnimate();
            }
            else
                PookieCheck(npc_pookieB1, Q7Options, "Q7");
        }
        else if (qMan.questsStarted[7])
            PookieCheck(npc_pookieB1, Q7Options, "Q7");

        // Q8 start and fed (or not) and not complete
        if (!qMan.questsCollected[8] &&
            !qMan.questsEnded[8] &&
            qMan.questsStarted[8])
        {
            if (PlayerPrefs.GetInt("Chp1Pookie2Fed") == 1)
            {
                bHasFedPookie2 = true;
                PookieBear2DisableAndAnimate();
            }
            else
                PookieCheck(npc_pookieB2, Q8Options, "Q8");
        }
        else if (qMan.questsStarted[8])
            PookieCheck(npc_pookieB2, Q8Options, "Q8");
    }


    public void LoadQuests()
    {
        // 0 = Nothing
        // 1 = Started
        // 2 = Ended
        // 3 = Collected
        savedQuestsValue = PlayerPrefs.GetString("Chp1Quests");

        for (int i = 0; i < savedQuestsValue.Length; i++)
        {
            GameObject Quest = GameObject.Find("Quest_" + i);

            if (savedQuestsValue.Substring(i, 1) == "3")
            {
                Quest.GetComponent<QuestObject>().bHasCollected = true;
                Quest.GetComponent<QuestObject>().bHasEnded = true;
                Quest.GetComponent<QuestObject>().bHasStarted = true;

                qMan.questsCollected[i] = true;
                qMan.questsEnded[i] = true;
                qMan.questsStarted[i] = true;
            }
            else if (savedQuestsValue.Substring(i, 1) == "2")
            {
                Quest.GetComponent<QuestObject>().bHasEnded = true;
                Quest.GetComponent<QuestObject>().bHasStarted = true;

                qMan.questsEnded[i] = true;
                qMan.questsStarted[i] = true;
            }
            else if (savedQuestsValue.Substring(i, 1) == "1")
            {
                Quest.GetComponent<QuestObject>().bHasStarted = true;
                
                qMan.questsStarted[i] = true;
            }
        }

        PlayerPrefs.SetString("Chp1Quests", savedQuestsValue);
    }

    public void SaveQuests()
    {
        // 0 = TBStarted
        // 1 = TBEnded
        // 2 = Complete
        // 3 = Collected

        savedQuestsValue = "";

        for (int i = 0; i < qMan.quests.Length; i++)
        {
            savedQuestsValue += i;

            if (qMan.quests[i].bHasCollected)
            {
                savedQuestsValue = savedQuestsValue.Remove(i, 1).Insert(i, "3");
            }
            else if (qMan.quests[i].bHasEnded)
            {
                savedQuestsValue = savedQuestsValue.Remove(i, 1).Insert(i, "2");
            }
            else if (qMan.quests[i].bHasStarted)
            {
                savedQuestsValue = savedQuestsValue.Remove(i, 1).Insert(i, "1");
            }
            else
            {
                savedQuestsValue = savedQuestsValue.Remove(i, 1).Insert(i, "0");
            }
        }

        PlayerPrefs.SetString("Chp1Quests", savedQuestsValue);
    }
}
