// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  08/22/2019

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Contains all Chapter 1 quests, items, and elements
public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public DialogueManager dMan;
    public GameObject greatTree;
    public GameObject item_homeVRGoggles;
    public GameObject npc_al_khidr;
    public GameObject npc_ashera;
    public GameObject npc_atandwa;
    public GameObject npc_brackey;
    public GameObject npc_canaan;
    public GameObject npc_chun;
    public GameObject npc_dagon;
    public GameObject npc_dilum;
    public GameObject npc_eliz;
    public GameObject npc_enki;
    public GameObject npc_heartha;
    public GameObject npc_marija;
    public GameObject npc_pookieB1;
    public GameObject npc_pookieB2;
    public GameObject npc_thabo;
    public GameObject npc_zola;
    public GameObject player;
    public GameObject quest0;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject quest3;
    public GameObject quest4;
    public GameObject quest5;
    public GameObject quest6;
    public GameObject quest7;
    public GameObject quest8;
    public GameObject quest9;
    public GameObject quest10;
    public GameObject questTrigger2;
    public GameObject screenFader;
    public GameObject treeHouseDoor;
    public GameObject warpGWC;
    public GameObject warpMinesweeper;
    public GameObject warpPookieVision;
    public GameObject warpTowerDeez;
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

    //public bool bAvoidUpdateQ0;
    public bool bAvoidUpdateQ1;
    public bool bAvoidUpdateQ2;
    public bool bAvoidUpdateQ3;
    public bool bAvoidUpdateQ4;
    public bool bAvoidUpdateQ4counting;
    public bool bAvoidUpdateQ4seeking;
    //public bool bAvoidUpdateQ5;
    //public bool bAvoidUpdateQ6;
    public bool bAvoidUpdateQ7;
    public bool bAvoidUpdateQ8;
    public bool bAvoidUpdateQ7Q8Ini;
    public bool bAvoidUpdateQ7Q8DH;
    public bool bAvoidUpdateQ7Q8;
    //public bool bAvoidUpdateQ9;
    //public bool bAvoidUpdateQ10;

    public bool bHasGoggles;
    public bool bHasQ3SmoochWoochy;
    public bool bHasQ7Green;
    public bool bHasQ7Orange;
    public bool bHasQ7Purple;
    public bool bHasQ7White;
    public bool bHasQ8Green;
    public bool bHasQ8Orange;
    public bool bHasQ8Purple;
    public bool bHasQ8White;
    public bool bContainsQ9Item; // TODO
    public bool bContainsQ10Item; // TODO

    public bool bQ4Seeking;
    public bool bFoundQ4Kid1;
    public bool bFoundQ4Kid2;
    public bool bFoundQ4Kid3;
    public bool bFoundQ4Kid4;
    public bool bFoundQ4Kid5;
    public bool bFoundQ4Kid6;
    public bool bFoundQ4All;

    public bool bQ3InitialCheck;
    
    public float raceTimer;

    public int Q4KidCounter;
    public int Q7GreenCounter;
    public int Q7OrangeCounter;
    public int Q7PurpleCounter;
    public int Q7WhiteCounter;
    public int Q8GreenCounter;
    public int Q8OrangeCounter;
    public int Q8PurpleCounter;
    public int Q8WhiteCounter;

    public string Q4LastKidFound;
    public string Q7Options;
    public string Q7SelectedOption;
    public string Q8Options;
    public string Q8SelectedOption;
    public string savedQuestsValue;

    //quest0  = Truth or Elaborate Lie w/ Dagon
    //quest1  = Race w/ canaan
    //quest2  = Treehouse Search
    //quest3  = Item Check w/ Enki
    //quest4  = Hide & Seek w/ Al-khidr
    //quest5  = Talking to GreatTree
    //quest6  = Minesweeper
    //quest7  = PookieBear1
    //quest8  = PookieBear2
    //quest9  = PookieVision
    //quest10 = TowerDeez

    void Start()
    {
        inv.RerunStart();

        // Chapter 1 -- First Time
        if (PlayerPrefs.GetString("Chapter") != "Chp1" &&
            PlayerPrefs.GetInt("TransferAnandaCoord") == 0)
        {
            player.transform.position = new Vector2(-13.68f, -7.625f);
            mainCamera.transform.position = new Vector2(-13.68f, -7.625f);
            camFollow.currentCoords = CameraFollow.AnandaCoords.Home;
            // see Update timer for inventory load
        }
        // Chapter 1 -- Transferring from a mini-game
        else if (PlayerPrefs.GetInt("Transferring") == 1)
        {
            save.RerunStart();
            save.GetTransferData();
            LoadQuests();
            GoggleCheck();
            StartCoroutine(LoadInventory());

            // Show UI if mobile (assumes coming from GWC where they might be off)
            // see Update timer for mobile check (needs time for uMan to load fully)

            // Set player & camera position
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("TransferP_x"), (PlayerPrefs.GetFloat("TransferP_y") - 0.1f));
            mainCamera.transform.position = new Vector2(PlayerPrefs.GetFloat("TransferCam_x"), PlayerPrefs.GetFloat("TransferCam_y"));
            camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("TransferAnandaCoord");
        }
        // Chapter 1 -- Saved Game
        else
        {
            save.RerunStart();
            save.GetSavedGame();
            LoadQuests();
            GoggleCheck(); 
            StartCoroutine(LoadInventory());

            Chp1QuestDialogueChecker();

            // When loading from PlaygroundW, play Jurassic Dank
            if (camFollow.currentCoords == (CameraFollow.AnandaCoords)32)
            {
                mMan.SwitchTrack(1);
            }
        }
    }

    //public void ResetHideAndSeek()
    //{
    //    if (mainCamera.GetComponent<CameraFollow>().currentCoords != CameraFollow.AnandaCoords.WoodsW)
    //    {
    //        Quest4Reset();
    //    }
    //}

    void Update()
    {
        // 06/20/19 DC TODO -- Drop goggles in Chp0 and they can't be gotten again (can see, but can't get)
        // Occurs even after getting another item
        // Item Check -- VR Goggles Prompts Ashera's response
        //if (inv.bUpdateItemCount)
        //{
        //    GoggleCheck();
        //}

        // Quest 0 -- Truth or Elaborate Lie w/ Dagon
        // see below

        // Quest 1 -- Race w/ Canaan -> Start Timer
        if (!pause.bPauseActive &&
            quest1.GetComponent<QuestObject>().bHasStarted &&
            !quest1.GetComponent<QuestObject>().bHasEnded)
        {
            raceTimer += Time.deltaTime;
        }

        // Quest 1 -- Race w/ canaan -> End, Check Race Time, & Assign Dialogue
        //if (!dMan.bDialogueActive &&
        //    !bAvoidUpdateQ1 &&
        //    quest1.GetComponent<QuestObject>().bHasEnded)
        //{
        //    //bAvoidUpdateQ1 = true;

        //    //EndRace();
        //}

        // Quest 1 -- Race w/ Canaan -> Reward
        //if ((int)camFollow.currentCoords == 32 &&
        //    !quest1.GetComponent<QuestObject>().bHasCollected &&
        //    quest1.GetComponent<QuestObject>().bHasEnded)
        //{
        //    if (npc_canaan.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
        //        dMan.bDialogueActive)
        //    {
        //        Quest1Reward();
        //    }
        //}

        //// Quest 2 -- Treehouse Search -> Start
        //if (!dMan.bDialogueActive &&
        //    !bAvoidUpdateQ2 &&
        //    quest2.GetComponent<QuestObject>().bHasStarted)
        //{
        //    //bAvoidUpdateQ2 = true;

        //    //TreeHouse();
        //}

        //// Quest 2 -- Treehouse Search -> Reward
        //if ((int)camFollow.currentCoords == 23 &&
        //    !quest2.GetComponent<QuestObject>().bHasCollected &&
        //    quest2.GetComponent<QuestObject>().bHasEnded)
        //{
        //    //Quest2Reward();
        //}

        // Quest 3 -- Item Check w/ Enki -> Initial check
        //if (quest3.GetComponent<QuestObject>().bHasStarted &&
        //    !bQ3InitialCheck)
        //{
        //    bQ3InitialCheck = true;
        //    //inv.bUpdateItemCount = true;
        //    SmoochyWoochyCheck();
        //}

        // Quest 3 -- Item Check w/ Enki -> Check on inventory change
        //if (quest3.GetComponent<QuestObject>().bHasStarted &&
        //    !bAvoidUpdateQ3 &&
        //    (inv.bUpdateItemCount ||
        //     bHasQ3SmoochWoochy))
        //{
            
        //}

        // Quest 3 -- Item Check w/ Enki -> Reward & Removal
        //if (!quest3.GetComponent<QuestObject>().bHasCollected && 
        //    quest3.GetComponent<QuestObject>().bHasEnded &&
        //    !bAvoidUpdateQ3)
        //{
        //    //bAvoidUpdateQ3 = true;

        //    //Quest3Reward();

            
        //}

        // Quest 4 -- Hide & Seek w/ Al-khidr (counting)
        //if (quest4.GetComponent<QuestObject>().bHasStarted &&
        //    !dMan.bDialogueActive &&
        //    !bAvoidUpdateQ4counting)
        //{
        //    //StartCoroutine(HideAndSeek());

        //    //// Avoid talking to Al-khidr
        //    //npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        //    //npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasExited = true;

        //    //// Stops the player's movement
        //    //player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        //    //// Fade out
        //    //screenFader.GetComponent<Animator>().Play("FadeOut");

        //    //// Turn off GUI
        //    //touches.transform.localScale = Vector3.zero;
        //}

        // Quest 4 -- Hide & Seek w/ Al-khidr (start seeking)
        if (quest4.GetComponent<QuestObject>().bHasStarted &&
            !dMan.bDialogueActive &&
            bQ4Seeking &&
            !bAvoidUpdateQ4seeking)
        {
            //// Fade in
            //screenFader.GetComponent<Animator>().SetBool("FadeIn", true);

            //// Resume the player's movement
            //player.GetComponent<PlayerMovement>().bStopPlayerMovement = false;

            //bAvoidUpdateQ4seeking = true;
        }

        // Quest 4 -- Hide & Seek w/ Al-khidr (seeking)
        if (bQ4Seeking)
        {
            //if (npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
            //    !dMan.bDialogueActive &&
            //    !bFoundQ4Kid1)
            //{
            //    bFoundQ4Kid1 = true;
            //    Q4LastKidFound = "Al-khidr";
            //    Q4KidCounter += 1;

            //    HideAndSeekFoundKid();
            //}
            //else if (npc_atandwa.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
            //    !dMan.bDialogueActive &&
            //    !bFoundQ4Kid2)
            //{
            //    bFoundQ4Kid2 = true;
            //    Q4LastKidFound = "Atandwa";
            //    Q4KidCounter += 1;

            //    HideAndSeekFoundKid();
            //}
            //else if (npc_eliz.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
            //    !dMan.bDialogueActive &&
            //    !bFoundQ4Kid3)
            //{
            //    bFoundQ4Kid3 = true;
            //    Q4LastKidFound = "Eliz";
            //    Q4KidCounter += 1;

            //    HideAndSeekFoundKid();
            //}
            //else if (npc_thabo.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
            //    !dMan.bDialogueActive &&
            //    !bFoundQ4Kid4)
            //{
            //    bFoundQ4Kid4 = true;
            //    Q4LastKidFound = "Thabo";
            //    Q4KidCounter += 1;

            //    HideAndSeekFoundKid();
            //}
            //else if (npc_zola.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
            //    !dMan.bDialogueActive &&
            //    !bFoundQ4Kid5)
            //{
            //    bFoundQ4Kid5 = true;
            //    Q4LastKidFound = "Zola";
            //    Q4KidCounter += 1;

            //    HideAndSeekFoundKid();
            //}
            //else if (npc_marija.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
            //    !dMan.bDialogueActive &&
            //    !bFoundQ4Kid6)
            //{
            //    bFoundQ4Kid6 = true;
            //    Q4LastKidFound = "Marija";
            //    Q4KidCounter += 1;

            //    HideAndSeekFoundKid();
            //}

            //if (mainCamera.GetComponent<CameraFollow>().currentCoords != CameraFollow.AnandaCoords.WoodsW)
            //{
            //    Quest4Reset();
            //}
        }

        //// Quest 4 -- Hide & Seek w/ Al-khidr (finished)
        //if (quest4.GetComponent<QuestObject>().bHasStarted &&
        //    !dMan.bDialogueActive &&
        //    Q4KidCounter == 6 &&
        //    !bQ4Seeking &&
        //    !bAvoidUpdateQ4)
        //{
        //    bAvoidUpdateQ4 = true;

        //    StartCoroutine(HideAndSeekFinished());

        //    // Stops the player's movement
        //    player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        //    // Fade out
        //    screenFader.GetComponent<Animator>().Play("FadeOut");

        //    // Turn off GUI
        //    touches.transform.localScale = Vector3.zero;
        //}

        //// Quest 7 & 8 -- Check current inventory
        //if (quest7.GetComponent<QuestObject>().bHasStarted &&
        //    quest7.GetComponent<QuestObject>().bHasStarted &&
        //    !bAvoidUpdateQ7Q8Ini)
        //{
        //    bAvoidUpdateQ7Q8Ini = true;

        //    //inv.bUpdateItemCount = true;
        //    GoggleCheck();
        //}

        //// Quest 7 -- Item Check & Options for PookieBear1
        //if (quest7.GetComponent<QuestObject>().bHasStarted &&
        //    //inv.bUpdateItemCount &&
        //    !bAvoidUpdateQ7)
        //{
            
            
        //    //// Reset the counters
        //    //Q7GreenCounter = 0;
        //    //Q7OrangeCounter = 0;
        //    //Q7PurpleCounter = 0;
        //    //Q7WhiteCounter = 0;

        //    //// Check each item
        //    //for (int i = 0; i < inv.items.Count; i++)
        //    //{
        //    //    string item = inv.items[i].ToString();

        //    //    // Green nug check
        //    //    if (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" ||
        //    //        item == "Cannabis.Bud.NaturesCandy (Item)" ||
        //    //        item == "Cannabis.Bud.TheDevilsLettuce (Item)")
        //    //    {
        //    //        Q7GreenCounter += 1;
        //    //    }

        //    //    // Orange nug
        //    //    if (item == "Cannabis.Bud.BootyJuice (Item)" ||
        //    //        item == "Cannabis.Bud.Catnip (Item)" ||
        //    //        item == "Cannabis.Bud.SnoopLeone (Item)")
        //    //    {
        //    //        Q7OrangeCounter += 1;
        //    //    }

        //    //    // Purple nug
        //    //    if (item == "Cannabis.Bud.GranPapasMedicine (Item)" ||
        //    //        item == "Cannabis.Bud.PurpleNurple (Item)" ||
        //    //        item == "Cannabis.Bud.RighteousBud (Item)")
        //    //    {
        //    //        Q7PurpleCounter += 1;
        //    //    }

        //    //    // White nug
        //    //    if (item == "Cannabis.Bud.CreeperBud (Item)" ||
        //    //        item == "Cannabis.Bud.MastaRoshi (Item)" ||
        //    //        item == "Cannabis.Bud.WhiteWalker (Item)")
        //    //    {
        //    //        Q7WhiteCounter += 1;
        //    //    }
        //    //}

        //    //if (Q7GreenCounter > 0)
        //    //{
        //    //    bContainsQ7ItemGreen = true;
        //    //}
        //    //else
        //    //{
        //    //    bContainsQ7ItemGreen = false;
        //    //}

        //    //if (Q7OrangeCounter > 0)
        //    //{
        //    //    bContainsQ7ItemOrange = true;
        //    //}
        //    //else
        //    //{
        //    //    bContainsQ7ItemOrange = false;
        //    //}

        //    //if (Q7PurpleCounter > 0)
        //    //{
        //    //    bContainsQ7ItemPurple = true;
        //    //}
        //    //else
        //    //{
        //    //    bContainsQ7ItemPurple = false;
        //    //}

        //    //if (Q7WhiteCounter > 0)
        //    //{
        //    //    bContainsQ7ItemWhite = true;
        //    //}
        //    //else
        //    //{
        //    //    bContainsQ7ItemWhite = false;
        //    //}

        //    //// if one of them present, activate options handler on Pookie
        //    //if (bContainsQ7ItemGreen ||
        //    //    bContainsQ7ItemOrange ||
        //    //    bContainsQ7ItemPurple || 
        //    //    bContainsQ7ItemWhite)
        //    //{
        //    //    // Activate DH w/ OH
        //    //    npc_pookieB1.transform.GetChild(0).gameObject.SetActive(false);
        //    //    npc_pookieB1.transform.GetChild(1).gameObject.SetActive(true);

        //    //    if (bContainsQ7ItemGreen &&
        //    //        bContainsQ7ItemOrange &&
        //    //        bContainsQ7ItemPurple &&
        //    //        bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[4];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[3] = "Give em a white nug";

        //    //        Q7Options = "gopw";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen &&
        //    //             bContainsQ7ItemPurple &&
        //    //             bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

        //    //        Q7Options = "gpw";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen &&
        //    //             bContainsQ7ItemOrange &&
        //    //             bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

        //    //        Q7Options = "gow";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen &&
        //    //             bContainsQ7ItemOrange &&
        //    //             bContainsQ7ItemPurple)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";

        //    //        Q7Options = "gop";
        //    //    }
        //    //    else if (bContainsQ7ItemOrange &&
        //    //             bContainsQ7ItemPurple &&
        //    //             bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

        //    //        Q7Options = "opw";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen &&
        //    //             bContainsQ7ItemOrange)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";

        //    //        Q7Options = "go";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen &&
        //    //             bContainsQ7ItemPurple)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

        //    //        Q7Options = "gp";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen &&
        //    //             bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

        //    //        Q7Options = "gw";
        //    //    }
        //    //    else if (bContainsQ7ItemOrange &&
        //    //             bContainsQ7ItemPurple)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

        //    //        Q7Options = "op";
        //    //    }
        //    //    else if (bContainsQ7ItemOrange &&
        //    //             bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

        //    //        Q7Options = "ow";
        //    //    }
        //    //    else if (bContainsQ7ItemPurple &&
        //    //             bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

        //    //        Q7Options = "pw";
        //    //    }
        //    //    else if (bContainsQ7ItemGreen)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";

        //    //        Q7Options = "g";
        //    //    }
        //    //    else if (bContainsQ7ItemOrange)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";

        //    //        Q7Options = "o";
        //    //    }
        //    //    else if (bContainsQ7ItemPurple)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";

        //    //        Q7Options = "p";
        //    //    }
        //    //    else if (bContainsQ7ItemWhite)
        //    //    {
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
        //    //        npc_pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a white nug";

        //    //        Q7Options = "w";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    // Activate DH (no OH)
        //    //    npc_pookieB1.transform.GetChild(0).gameObject.SetActive(true);
        //    //    npc_pookieB1.transform.GetChild(1).gameObject.SetActive(false);
        //    //}
        //}

        // Quest 8 -- Item Check & Options for PookieBear2
        //if (quest8.GetComponent<QuestObject>().bHasStarted &&
        //    inv.bUpdateItemCount &&
        //    !bAvoidUpdateQ8)
        //{
        //    //PookieCheck(npc_pookieB2, Q8Options,
        //    //    Q8GreenCounter, Q8OrangeCounter, Q8PurpleCounter, Q8WhiteCounter,
        //    //    bHasQ8Green, bHasQ8Orange, bHasQ8Purple, bHasQ8White);
        //}

        // Quest 7 & 8 -- Both Fed
        if (bAvoidUpdateQ7 &&
            bAvoidUpdateQ8 &&
            !bAvoidUpdateQ7Q8)
        {
            if (!bAvoidUpdateQ7Q8DH)
            {
                npc_dilum.transform.GetChild(0).gameObject.SetActive(false);
                npc_dilum.transform.GetChild(1).gameObject.SetActive(true);

                bAvoidUpdateQ7Q8DH = true;
            }

            if (npc_dilum.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                dMan.bDialogueActive)
            {
                Quest7And8Reward();

                bAvoidUpdateQ7Q8 = true;
            }
        }

        // Minigame -- Guess Who Colluded
        if (player.GetComponent<PolygonCollider2D>().IsTouching(warpGWC.GetComponent<BoxCollider2D>()))
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
    }

    public void TreeHouse()
    {
        StartCoroutine(DelayTreeHouse());
    }

    IEnumerator DelayTreeHouse()
    {
        yield return new WaitForSeconds(0.5f);

        questTrigger2.transform.GetChild(1).GetComponent<Transform>().localScale = new Vector3(0.2625f, 0.24937f, 1f);

        // Disable door again & move Dan back (need isTrigger double-tap change?)
        // DC 02/12/19 -- Don't move back Dan here?
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

    public void Quest0Reward()
    {
        if (!quest0.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uMan.bUpdateBrio = true;

            quest0.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest1Reward()
    {
        if (!quest1.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uMan.bUpdateBrio = true;

            quest1.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest2Reward()
    {
        if (!quest2.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(5);
            uMan.bUpdateBrio = true;

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
            uMan.bUpdateBrio = true;

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
            uMan.bUpdateBrio = true;
            
            quest4.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest5Reward()
    {
        if (!quest5.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uMan.bUpdateBrio = true;
            
            quest5.GetComponent<QuestObject>().CollectedQuest();
        }
    }

    public void Quest7And8Reward()
    {
        if (!quest7.GetComponent<QuestObject>().bHasCollected &&
            !quest8.GetComponent<QuestObject>().bHasCollected)
        {
            player.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            player.GetComponent<PlayerBrioManager>().RestorePlayer(25);
            uMan.bUpdateBrio = true;
            
            dMan.portPic = quest7.GetComponent<QuestObject>().portPic;
            qMan.ShowQuestText(quest7.GetComponent<QuestObject>().endText);

            quest7.GetComponent<QuestObject>().CollectedQuest();
            quest8.GetComponent<QuestObject>().CollectedQuest();
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
            Quest5Dialogue2();
        }
        // Quest 5 - Dialogue 2 - Option *
        else if (greatTree.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(1).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2))
        {
            oMan.ResetOptions();
            Quest5Dialogue3();
        }
        // Quest 5 - Dialogue 3 - Option *
        else if (greatTree.transform.GetChild(2).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(2).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3))
        {
            oMan.ResetOptions();
            Quest5Dialogue4();
        }
        // Quest 5 - Dialogue 4 - Option *
        else if (greatTree.transform.GetChild(3).GetComponent<DialogueHolder>().bHasEntered &&
                 greatTree.transform.GetChild(3).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3))
        {
            oMan.ResetOptions();
            Quest5Reward();
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
    }

    public void Quest0Dialogue1Opt1()
    {
        // Yes -- Play
        // Quest Trigger -> Quest Object text will render first; then we activate the next round of dialogue / options
        // 05/11/2018 DC TODO -- Improve so that options can follow options (when coupled with a quest)

        npc_dagon.transform.GetChild(0).gameObject.SetActive(false);
        npc_dagon.transform.GetChild(1).gameObject.SetActive(true);

        npc_dagon.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
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

    public void PreHideAndSeek()
    {
        // Avoid talking to Al-khidr
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        npc_al_khidr.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasExited = true;

        // Stops the player's movement
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        // Fade out
        screenFader.GetComponent<Animator>().Play("FadeOut");

        // Turn off GUI
        touches.transform.localScale = Vector3.zero;

        StartCoroutine(HideAndSeek());
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
            bFoundQ4Kid1 = true;
            Q4LastKidFound = "Al-khidr";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Atandwa")
        {
            bFoundQ4Kid2 = true;
            Q4LastKidFound = "Atandwa";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Eliz")
        {
            bFoundQ4Kid3 = true;
            Q4LastKidFound = "Eliz";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Thabo")
        {
            bFoundQ4Kid4 = true;
            Q4LastKidFound = "Thabo";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Zola")
        {
            bFoundQ4Kid5 = true;
            Q4LastKidFound = "Zola";
            Q4KidCounter += 1;

            HideAndSeekFoundKid();
        }
        else if (_name == "Marija")
        {
            bFoundQ4Kid6 = true;
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

            //bQ4Seeking = false;
            //bFoundQ4All = true;

            
        }
    }

    public void PreHideAndSeekFinished()
    {
        StartCoroutine(HideAndSeekFinished());

        // Stops the player's movement
        player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

        // Fade out
        screenFader.GetComponent<Animator>().Play("FadeOut");

        // Turn off GUI
        touches.transform.localScale = Vector3.zero;
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

        //bAvoidUpdateQ4counting = true;
        //bAvoidUpdateQ4seeking = true;

        yield return new WaitForSeconds(1);

        Quest4Reward();
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
    
    public void Quest4Reset()
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

            // Reset quest checks
            bFoundQ4Kid1 = false;
            bFoundQ4Kid2 = false;
            bFoundQ4Kid3 = false;
            bFoundQ4Kid4 = false;
            bFoundQ4Kid5 = false;
            bFoundQ4Kid6 = false;

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
            quest4.GetComponent<QuestObject>().bHasStarted = false;
            bAvoidUpdateQ4counting = false;
            bQ4Seeking = false;
            bAvoidUpdateQ4seeking = false;
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

    public void Quest5Dialogue2()
    {
        greatTree.transform.GetChild(0).gameObject.SetActive(false);
        greatTree.transform.GetChild(1).gameObject.SetActive(true);

        greatTree.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
    }

    public void Quest5Dialogue3()
    {
        greatTree.transform.GetChild(1).gameObject.SetActive(false);
        greatTree.transform.GetChild(2).gameObject.SetActive(true);

        greatTree.transform.GetChild(2).GetComponent<DialogueHolder>().bContinueDialogue = true;
    }

    public void Quest5Dialogue4()
    {
        greatTree.transform.GetChild(2).gameObject.SetActive(false);
        greatTree.transform.GetChild(3).gameObject.SetActive(true);

        greatTree.transform.GetChild(3).GetComponent<DialogueHolder>().bContinueDialogue = true;

        // derp
    }

    public void Quest6Dialogue1Opt1()
    {
        // yes play a game
        // DC TODO -- offer difficulty choices

        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();
            item = item.Substring(0, item.Length - 7);

            if (item == "VR.Goggles")
            {
                bHasGoggles = true;
            }
        }
        
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

        if (Q7BudsChar[Q7SelectionInt] == 'g')
        {
            // Remove green
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if ((item == "Cannabis.Bud.SmoochyWoochyPoochy" ||
                     item == "Cannabis.Bud.NaturesCandy" ||
                     item == "Cannabis.Bud.TheDevilsLettuce") && 
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }
        else if (Q7BudsChar[Q7SelectionInt] == 'o')
        {
            // Remove orange
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if ((item == "Cannabis.Bud.BootyJuice" ||
                     item == "Cannabis.Bud.Catnip" ||
                     item == "Cannabis.Bud.SnoopLeone") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }
        else if (Q7BudsChar[Q7SelectionInt] == 'p')
        {
            // Remove purple
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if ((item == "Cannabis.Bud.GranPapasMedicine" ||
                     item == "Cannabis.Bud.PurpleNurple" ||
                     item == "Cannabis.Bud.RighteousBud") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }
        else if (Q7BudsChar[Q7SelectionInt] == 'w')
        {
            // Remove white
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                item = item.Substring(0, item.Length - 7);

                if ((item == "Cannabis.Bud.CreeperBud" ||
                     item == "Cannabis.Bud.MastaRoshi" ||
                     item == "Cannabis.Bud.WhiteWalker") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }

        // Animate Pookie & stop moving
        if (player.transform.position.x >= npc_pookieB1.transform.position.x)
        {
            npc_pookieB1.GetComponent<Animator>().Play("Eat Right");
        }
        else
        {
            npc_pookieB1.GetComponent<Animator>().Play("Eat Left");
        }

        // Disable Pookie movement & dialogue
        npc_pookieB1.GetComponent<NPCMovement>().enabled = false;
        npc_pookieB1.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(PookieBear1Animations());

        quest7.GetComponent<QuestObject>().bHasEnded = true;
        qMan.questsEnded[quest7.GetComponent<QuestObject>().questNumber] = true;

        bAvoidUpdateQ7 = true;
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

    public void Quest8Dialogue1Opt()
    {
        // Get the selected option, i.e. OptX
        char[] Q8SelectionChar = Q8SelectedOption.ToCharArray();
        int Q8SelectionInt = Q8SelectionChar[3] - '1';

        // Get the corresponding value, i.e. gopw
        char[] Q8BudsChar = Q8Options.ToCharArray();

        if (Q8BudsChar[Q8SelectionInt] == 'g')
        {
            // Remove green
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                if ((item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" ||
                     item == "Cannabis.Bud.NaturesCandy (Item)" ||
                     item == "Cannabis.Bud.TheDevilsLettuce (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }
        else if (Q8BudsChar[Q8SelectionInt] == 'o')
        {
            // Remove orange
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                if ((item == "Cannabis.Bud.BootyJuice (Item)" ||
                     item == "Cannabis.Bud.Catnip (Item)" ||
                     item == "Cannabis.Bud.SnoopLeone (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }
        else if (Q8BudsChar[Q8SelectionInt] == 'p')
        {
            // Remove purple
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                if ((item == "Cannabis.Bud.GranPapasMedicine (Item)" ||
                     item == "Cannabis.Bud.PurpleNurple (Item)" ||
                     item == "Cannabis.Bud.RighteousBud (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    GoggleCheck();
                    bTempCheck = false;
                }
            }
        }
        else if (Q8BudsChar[Q8SelectionInt] == 'w')
        {
            // Remove white
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                if ((item == "Cannabis.Bud.CreeperBud (Item)" ||
                     item == "Cannabis.Bud.MastaRoshi (Item)" ||
                     item == "Cannabis.Bud.WhiteWalker (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    //inv.bUpdateItemCount = true;
                    bTempCheck = false;
                }
            }
        }

        // Animate Pookie & stop moving
        if (player.transform.position.x >= npc_pookieB2.transform.position.x)
        {
            npc_pookieB2.GetComponent<Animator>().Play("Eat Right");
        }
        else
        {
            npc_pookieB2.GetComponent<Animator>().Play("Eat Left");
        }

        // Disable Pookie movement & dialogue
        npc_pookieB2.GetComponent<NPCMovement>().enabled = false;
        npc_pookieB2.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(PookieBear2Animations());

        quest8.GetComponent<QuestObject>().bHasEnded = true;
        qMan.questsEnded[quest8.GetComponent<QuestObject>().questNumber] = true;

        bAvoidUpdateQ8 = true;
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

    public void Quest9Dialogue1Opt1()
    {
        // yes play a game

        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();

            if (item == "VR.Goggles (Item)")
            {
                bContainsQ9Item = true;
            }
        }

        if (bContainsQ9Item)
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

        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();

            if (item == "VR.Goggles (Item)")
            {
                bContainsQ10Item = true;
            }
        }

        if (bContainsQ10Item)
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

    public void Chp1QuestDialogueChecker()
    {
        // Q0
        if (qMan.questsCollected[0])
        {
            npc_dagon.transform.GetChild(0).gameObject.SetActive(false);
            npc_dagon.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (qMan.questsStarted[0])
        {
            npc_dagon.transform.GetChild(0).gameObject.SetActive(false);
            npc_dagon.transform.GetChild(1).gameObject.SetActive(true);
        }

        // Q1
        if (qMan.questsCollected[1])
        {
            bAvoidUpdateQ1 = true;
            npc_canaan.transform.GetChild(0).gameObject.SetActive(false);
            npc_canaan.transform.GetChild(1).gameObject.SetActive(true);
        }

        // Q3
        if (qMan.questsCollected[3])
        {
            bAvoidUpdateQ3 = true;
            npc_enki.transform.GetChild(0).gameObject.SetActive(false);
            npc_enki.transform.GetChild(1).gameObject.SetActive(true);
        }

        // Q4
        if (qMan.questsCollected[4])
        {
            npc_al_khidr.transform.GetChild(0).gameObject.SetActive(false);
            npc_al_khidr.transform.GetChild(2).gameObject.SetActive(true);
        }

        // Q5
        if (qMan.questsCollected[5])
        {
            greatTree.transform.GetChild(0).gameObject.SetActive(false);
            greatTree.transform.GetChild(3).gameObject.SetActive(true);
        }

        // Q7 & Q8
        if (qMan.questsCollected[7] ||
            qMan.questsCollected[8])
        {
            npc_dilum.transform.GetChild(0).gameObject.SetActive(false);
            npc_dilum.transform.GetChild(1).gameObject.SetActive(true);

            // DC 02/13/19 -- "Bug" that allows pookie bears to get back up & look "normal" after quest complete, saved & quit, and then talked to again
        }
    }

    public void GoggleCheck()
    {
        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();
            item = item.Substring(0, item.Length - 7);

            if (item == "VR.Goggles")
                bHasGoggles = true;
        }

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

    public void PookieCheck()
    {
        PookieCheck(npc_pookieB1, Q7Options, "Q7",
                Q7GreenCounter, Q7OrangeCounter, Q7PurpleCounter, Q7WhiteCounter,
                bHasQ7Green, bHasQ7Orange, bHasQ7Purple, bHasQ7White);

        PookieCheck(npc_pookieB2, Q8Options, "Q8",
                Q8GreenCounter, Q8OrangeCounter, Q8PurpleCounter, Q8WhiteCounter,
                bHasQ8Green, bHasQ8Orange, bHasQ8Purple, bHasQ8White);
    }

    public void PookieCheck(GameObject _pookieBear, string _qOptions, string _questNum,
        int _greenCount, int _orangeCount, int _purpleCount, int _whiteCount,
        bool _bHasGreen, bool _bHasOrange, bool _bHasPurple, bool _bHasWhite)
    {
        // Reset the counters
        _greenCount = 0;
        _orangeCount = 0;
        _purpleCount = 0;
        _whiteCount = 0;

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
                _greenCount += 1;
            }

            // Orange nug
            if (item == "Cannabis.Bud.BootyJuice" ||
                item == "Cannabis.Bud.Catnip" ||
                item == "Cannabis.Bud.SnoopLeone")
            {
                _orangeCount += 1;
            }

            // Purple nug
            if (item == "Cannabis.Bud.GranPapasMedicine" ||
                item == "Cannabis.Bud.PurpleNurple" ||
                item == "Cannabis.Bud.RighteousBud")
            {
                _purpleCount += 1;
            }

            // White nug
            if (item == "Cannabis.Bud.CreeperBud" ||
                item == "Cannabis.Bud.MastaRoshi" ||
                item == "Cannabis.Bud.WhiteWalker")
            {
                _whiteCount += 1;
            }
        }

        if (_greenCount > 0)
            _bHasGreen = true;
        else
            _bHasGreen = false;

        if (_orangeCount > 0)
            _bHasOrange = true;
        else
            _bHasOrange = false;

        if (_purpleCount > 0)
            _bHasPurple = true;
        else
            _bHasPurple = false;

        if (_whiteCount > 0)
            _bHasWhite = true;
        else
            _bHasWhite = false;

        // if one of them present, activate options handler on Pookie
        if (_bHasGreen ||
            _bHasOrange ||
            _bHasPurple ||
            _bHasWhite)
        {
            // Activate DH w/ OH
            _pookieBear.transform.GetChild(0).gameObject.SetActive(false);
            _pookieBear.transform.GetChild(1).gameObject.SetActive(true);

            if (_bHasGreen &&
                _bHasOrange &&
                _bHasPurple &&
                _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[4];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[3] = "Give em a white nug";

                _qOptions = "gopw";
            }
            else if (_bHasGreen &&
                     _bHasPurple &&
                     _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                _qOptions = "gpw";
            }
            else if (_bHasGreen &&
                     _bHasOrange &&
                     _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                _qOptions = "gow";
            }
            else if (_bHasGreen &&
                     _bHasOrange &&
                     _bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";

                _qOptions = "gop";
            }
            else if (_bHasOrange &&
                     _bHasPurple &&
                     _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                _qOptions = "opw";
            }
            else if (_bHasGreen &&
                     _bHasOrange)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em an orange nug";

                _qOptions = "go";
            }
            else if (_bHasGreen &&
                     _bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                _qOptions = "gp";
            }
            else if (_bHasGreen &&
                     _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                _qOptions = "gw";
            }
            else if (_bHasOrange &&
                     _bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                _qOptions = "op";
            }
            else if (_bHasOrange &&
                     _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                _qOptions = "ow";
            }
            else if (_bHasPurple &&
                     _bHasWhite)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                _qOptions = "pw";
            }
            else if (_bHasGreen)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";

                _qOptions = "g";
            }
            else if (_bHasOrange)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em an orange nug";

                _qOptions = "o";
            }
            else if (_bHasPurple)
            {
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                _pookieBear.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";

                _qOptions = "p";
            }
            else if (_bHasWhite)
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


    IEnumerator LoadInventory()
    {
        yield return new WaitForSeconds(0.333f);

        // From Chp0
        if (PlayerPrefs.GetString("Chapter") != "Chp1" ||
            PlayerPrefs.GetInt("Transferring") == 1)
        {
            inv.LoadInventory("transfer");

            uMan.CheckIfMobile();
        }
        // From saved 
        else
        {
            inv.LoadInventory("saved");
        }

        // Reset Transfer
        PlayerPrefs.SetInt("Transferring", 0);

        // Clear Transition data
        save.DeleteTransPrefs();
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

        // DC 01/13/2019 -- Most quests close themselves out in their update sections once the above are assigned
        // These conditions are for the stragglers
        if (savedQuestsValue.Substring(4, 1) == "3" ||
            savedQuestsValue.Substring(4, 1) == "2")
        {
            bAvoidUpdateQ4 = true;
            bAvoidUpdateQ4counting = true;
            bAvoidUpdateQ4seeking = true;
        }
        else if (savedQuestsValue.Substring(4, 1) == "1")
        {
            quest4.GetComponent<QuestObject>().bHasStarted = false;
        }

        if (savedQuestsValue.Substring(7, 1) == "3" &&
            savedQuestsValue.Substring(8, 1) == "3")
        {
            bAvoidUpdateQ7 = true;
            bAvoidUpdateQ8 = true;
            bAvoidUpdateQ7Q8DH = true;
            bAvoidUpdateQ7Q8 = true;

            // PookieBear 1 Satisfied
            npc_pookieB1.GetComponent<NPCMovement>().enabled = false;
            npc_pookieB1.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(PookieBear1Animations());

            // PookieBear 2 Satisfied
            npc_pookieB2.GetComponent<NPCMovement>().enabled = false;
            npc_pookieB2.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(PookieBear2Animations());
        }

        if (savedQuestsValue.Substring(7, 1) == "2")
        {
            bAvoidUpdateQ7 = true;

            // PookieBear 1 Satisfied
            npc_pookieB1.GetComponent<NPCMovement>().enabled = false;
            npc_pookieB1.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(PookieBear1Animations());
        }

        if (savedQuestsValue.Substring(8, 1) == "2")
        {
            bAvoidUpdateQ8 = true;

            // PookieBear 2 Satisfied
            npc_pookieB2.GetComponent<NPCMovement>().enabled = false;
            npc_pookieB2.transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(PookieBear2Animations());
        }
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
