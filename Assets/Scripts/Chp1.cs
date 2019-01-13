// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  01/13/2019

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Contains all Chapter 1 quests, items, and elements
public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public DialogueManager dMan;
    public GameObject dArrow;
    public GameObject dBox;
    public GameObject greatTree;
    public GameObject kid2;
    public GameObject kid4;
    public GameObject kid5;
    public GameObject kid6;
    public GameObject kid7;
    public GameObject kid8;
    public GameObject kid9;
    public GameObject man1;
    public GameObject oldMan1;
    public GameObject parent2;
    public GameObject person1;
    public GameObject pookieB1;
    public GameObject pookieB2;
    public GameObject quest0;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject quest3;
    public GameObject quest4;
    public GameObject quest5;
    public GameObject quest6;
    public GameObject quest7;
    public GameObject quest8;
    public GameObject questTrigger2;
    public GameObject screenFader;
    public GameObject thePlayer;
    public GameObject warpGWC;
    public GameObject warpMinesweeper;
    public Inventory inv;
    public MoveOptionsMenuArrow moveOptsArw;
    public OptionsManager oMan;
    public PolygonCollider2D pColli;
    public QuestManager qMan;
    public SaveGame save;
    public Text dText;
    public TouchControls touches;
    public UIManager uiMan;

    public bool bAvoidGreatTreeConvo;
    public bool bAvoidUpdateQ0;
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
    public bool bAvoidUpdateQ7Q8DH;
    public bool bAvoidUpdateQ7Q8;

    public bool bContainsQ3Item;
    public bool bContainsQ6Item;
    public bool bContainsQ7ItemGreen;
    public bool bContainsQ7ItemOrange;
    public bool bContainsQ7ItemPurple;
    public bool bContainsQ7ItemWhite;
    public bool bContainsQ8ItemGreen;
    public bool bContainsQ8ItemOrange;
    public bool bContainsQ8ItemPurple;
    public bool bContainsQ8ItemWhite;

    public bool bQ4Seeking;
    public bool bFoundQ4Kid4;
    public bool bFoundQ4Kid5;
    public bool bFoundQ4Kid6;
    public bool bFoundQ4Kid7;
    public bool bFoundQ4Kid8;
    public bool bFoundQ4Kid9;
    public bool bFoundQ4All;

    public bool bGetInventory;

    public float invTimer;
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

    void Start()
    {
        // Initializers
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        dArrow = GameObject.Find("Dialogue_Arrow");
        dBox = GameObject.Find("Dialogue_Box");
        dMan = FindObjectOfType<DialogueManager>();
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        greatTree = GameObject.Find("GreatTree");
        touches = GameObject.Find("GUIControls").GetComponent<TouchControls>();
        inv = FindObjectOfType<Inventory>();
        kid2 = GameObject.Find("Kid.2");
        kid4 = GameObject.Find("Kid.4");
        kid5 = GameObject.Find("Kid.5");
        kid6 = GameObject.Find("Kid.6");
        kid7 = GameObject.Find("Kid.7");
        kid8 = GameObject.Find("Kid.8");
        kid9 = GameObject.Find("Kid.9");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        man1 = GameObject.Find("Man.1");
        moveOptsArw = FindObjectOfType<MoveOptionsMenuArrow>();
        oldMan1 = GameObject.Find("OldMan.1");
        oMan = FindObjectOfType<OptionsManager>();
        parent2 = GameObject.Find("Parent.2");
        person1 = GameObject.Find("Person.1");
        pookieB1 = GameObject.Find("PookieBear.1");
        pookieB2 = GameObject.Find("PookieBear.2");
        qMan = FindObjectOfType<QuestManager>();
        questTrigger2 = GameObject.Find("QT_2");
        save = FindObjectOfType<SaveGame>();
        savedQuestsValue = PlayerPrefs.GetString("Chp1Quests");
        screenFader = GameObject.Find("Screen_Fader");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        warpGWC = GameObject.Find("Chp1.to.GuessWhoColluded");
        warpMinesweeper = GameObject.Find("Chp1.to.Minesweeper");
        uiMan = FindObjectOfType<UIManager>();

        inv.RerunStart();

        quest0 = GameObject.Find("Quest_0"); // Truth or Elaborate Lie w/ Parent2
        quest1 = GameObject.Find("Quest_1"); // Race w/ Kid2
        quest2 = GameObject.Find("Quest_2"); // Treehouse Search
        quest3 = GameObject.Find("Quest_3"); // Item Check w/ OldMan1
        quest4 = GameObject.Find("Quest_4"); // Hide & Seek w/ Kid4
        quest5 = GameObject.Find("Quest_5"); // Talking to GreatTree
        quest6 = GameObject.Find("Quest_6"); // Minesweeper
        quest7 = GameObject.Find("Quest_7"); // PookieBear1
        quest8 = GameObject.Find("Quest_8"); // PookieBear2

        invTimer = 0.333f;
        raceTimer = 0f;

        // Chapter 1 -- First Time
        if (PlayerPrefs.GetString("Chapter") != "Chp1" &&
            PlayerPrefs.GetInt("TransferAnandaCoord") == 0)
        {
            thePlayer.transform.position = new Vector2(-13.68f, -7.625f);
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
            inv.bUpdateItemCount = true;
            // see Update timer for inventory load

            // Set player & camera position
            thePlayer.transform.position = new Vector2(PlayerPrefs.GetFloat("TransferP_x"), (PlayerPrefs.GetFloat("TransferP_y") - 0.05f));
            mainCamera.transform.position = new Vector2(PlayerPrefs.GetFloat("TransferCam_x"), PlayerPrefs.GetFloat("TransferCam_y"));
            camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("TransferAnandaCoord");
        }
        // Chapter 1 -- Saved Game
        else
        {
            save.RerunStart();
            save.GetSavedGame();
            LoadQuests();
            inv.bUpdateItemCount = true;
            // see Update timer for inventory load
        }
    }

    void Update()
    {
        // Load Inventory -- Saved vs. Transfer
        //  Reset Transition Data
        if (invTimer > 0)
        {
            invTimer -= Time.deltaTime;

            if (invTimer <= 0)
            {
                // From Chp0
                if (PlayerPrefs.GetString("Chapter") != "Chp1" ||
                    PlayerPrefs.GetInt("Transferring") == 1)
                {
                    inv.LoadInventory("transfer");
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
        }

        // Quest 0 -- Truth or Elaborate Lie w/ Parent2
        // see below

        // Quest 1 -- Race w/ Kid2 -> Start Timer
        if (quest1.GetComponent<QuestObject>().bHasStarted &&
            !quest1.GetComponent<QuestObject>().bHasEnded)
        {
            raceTimer += Time.deltaTime;
        }

        // Quest 1 -- Race w/ Kid2 -> End, Check Race Time, & Assign Dialogue
        if (quest1.GetComponent<QuestObject>().bHasEnded &&
            !dMan.bDialogueActive &&
            !bAvoidUpdateQ1)
        {
            bAvoidUpdateQ1 = true;
            kid2.transform.GetChild(0).gameObject.SetActive(false);
            kid2.transform.GetChild(1).gameObject.SetActive(true);

            raceTimer = Mathf.Round(raceTimer);

            if (raceTimer <= 10)
            {
                kid2.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Holy goat nipples Batman.. How the.. Did you cheat?",
                    "You set one of the best records at " + raceTimer + " seconds?!?",
                    "Well.. Well ran. Let's see how you do tomorrow."
                };
            }
            else if (raceTimer <= 30 && raceTimer > 10)
            {
                kid2.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Good race Dan! You ran that in " + raceTimer + " seconds.",
                    "Keep practicing and improving. Maybe you'll beat my time!"
                };
            }
            else
            {
                    kid2.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Well.. You made it... You finished after " + raceTimer + " seconds.",
                    "Keep training Dan. It's good for you!"
                };
            }
        }

        // Quest 1 -- Race w/ Kid2 -> Reward
        if (!quest1.GetComponent<QuestObject>().bHasCollected &&
            quest1.GetComponent<QuestObject>().bHasEnded &&
            (int)camFollow.currentCoords == 32)
        {
            if (kid2.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                dMan.bDialogueActive)
            {
                Quest1Reward();
            }
        }

        // Quest 2 -- Treehouse Search -> Start
        if (quest2.GetComponent<QuestObject>().bHasStarted &&
            !dMan.bDialogueActive &&
            !bAvoidUpdateQ2)
        {
            bAvoidUpdateQ2 = true;
            GameObject.Find("HomeSWTreeDoor").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("HomeSWTreeDoor").transform.localScale = Vector3.one;
        }

        // Quest 2 -- Treehouse Search -> Reward
        if (!quest2.GetComponent<QuestObject>().bHasCollected && 
            quest2.GetComponent<QuestObject>().bHasEnded &&
            (int)camFollow.currentCoords == 23)
        {
            Quest2Reward();

        }

        // Quest 3 -- Item Check w/ OldMan1 -> Check on inventory change
        if (quest3.GetComponent<QuestObject>().bHasStarted &&
            inv.bUpdateItemCount &&
            !bAvoidUpdateQ3)
        {
            // Assume not present unless we find it
            bContainsQ3Item = false;

            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                
                if (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)")
                {
                    bContainsQ3Item = true;
                }
            }

            if (bContainsQ3Item)
            {
                oldMan1.transform.GetChild(0).gameObject.SetActive(false);
                oldMan1.transform.GetChild(1).gameObject.SetActive(true);

                oldMan1.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().endQuest = true;
            }
            else if (!bContainsQ3Item)
            {
                oldMan1.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().endQuest = false;

                oldMan1.transform.GetChild(0).gameObject.SetActive(true);
                oldMan1.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        // Quest 3 -- Item Check w/ OldMan1 -> Reward & Removal
        if (quest3.GetComponent<QuestObject>().bHasEnded &&
            !bAvoidUpdateQ3)
        {
            if (quest3.GetComponent<QuestObject>().bHasEnded)
            {
                Quest3Reward();
                bAvoidUpdateQ3 = true;
            }

            // Remove the bud
            bool bTempCheck = true;
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                if (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" && bTempCheck)
                {
                    inv.Remove(inv.items[i]);
                    bTempCheck = false;
                }
            }
        }

        // Quest 4 -- Hide & Seek w/ Kid4 (counting)
        if (quest4.GetComponent<QuestObject>().bHasStarted &&
            !dMan.bDialogueActive &&
            !bAvoidUpdateQ4counting)
        {
            StartCoroutine(HideAndSeek());

            // Avoid talking to Kid4
            kid4.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
            kid4.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasExited = true;

            // Stops the player's movement
            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            // Fade out
            screenFader.GetComponent<Animator>().Play("FadeOut");

            // Turn off GUI
            touches.transform.localScale = Vector3.zero;
        }

        // Quest 4 -- Hide & Seek w/ Kid4 (start seeking)
        if (quest4.GetComponent<QuestObject>().bHasStarted &&
            !dMan.bDialogueActive &&
            bQ4Seeking &&
            !bAvoidUpdateQ4seeking)
        {
            // Fade in
            screenFader.GetComponent<Animator>().SetBool("FadeIn", true);

            // Resume the player's movement
            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = false;

            bAvoidUpdateQ4seeking = true;
        }

        // Quest 4 -- Hide & Seek w/ Kid4 (seeking)
        if (bQ4Seeking)
        {
            if (kid4.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
                !dMan.bDialogueActive &&
                !bFoundQ4Kid4)
            {
                bFoundQ4Kid4 = true;
                Q4LastKidFound = "Kid4";
                Q4KidCounter += 1;

                HideAndSeekFoundKid();
            }
            else if (kid5.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
                !dMan.bDialogueActive &&
                !bFoundQ4Kid5)
            {
                bFoundQ4Kid5 = true;
                Q4LastKidFound = "Kid5";
                Q4KidCounter += 1;

                HideAndSeekFoundKid();
            }
            else if (kid6.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
                !dMan.bDialogueActive &&
                !bFoundQ4Kid6)
            {
                bFoundQ4Kid6 = true;
                Q4LastKidFound = "Kid6";
                Q4KidCounter += 1;

                HideAndSeekFoundKid();
            }
            else if (kid7.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
                !dMan.bDialogueActive &&
                !bFoundQ4Kid7)
            {
                bFoundQ4Kid7 = true;
                Q4LastKidFound = "Kid7";
                Q4KidCounter += 1;

                HideAndSeekFoundKid();
            }
            else if (kid8.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
                !dMan.bDialogueActive &&
                !bFoundQ4Kid8)
            {
                bFoundQ4Kid8 = true;
                Q4LastKidFound = "Kid8";
                Q4KidCounter += 1;

                HideAndSeekFoundKid();
            }
            else if (kid9.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered &&
                !dMan.bDialogueActive &&
                !bFoundQ4Kid9)
            {
                bFoundQ4Kid9 = true;
                Q4LastKidFound = "Kid9";
                Q4KidCounter += 1;

                HideAndSeekFoundKid();
            }

            if (mainCamera.GetComponent<CameraFollow>().currentCoords != CameraFollow.AnandaCoords.WoodsW)
            {
                Quest4Reset();
            }
        }

        // Quest 4 -- Hide & Seek w/ Kid4 (finished)
        if (quest4.GetComponent<QuestObject>().bHasStarted &&
            !dMan.bDialogueActive &&
            Q4KidCounter == 6 &&
            !bQ4Seeking &&
            !bAvoidUpdateQ4)
        {
            StartCoroutine(HideAndSeekFinished());

            // Stops the player's movement
            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            // Fade out
            screenFader.GetComponent<Animator>().Play("FadeOut");

            // Turn off GUI
            touches.transform.localScale = Vector3.zero;

            Quest4Reward();
            bAvoidUpdateQ4 = true;
        }

        // Quest 7 -- Item Check & Options for PookieBear1
        if (quest7.GetComponent<QuestObject>().bHasStarted &&
            inv.bUpdateItemCount &&
            !bAvoidUpdateQ7)
        {
            // Reset the counters
            Q7GreenCounter = 0;
            Q7OrangeCounter = 0;
            Q7PurpleCounter = 0;
            Q7WhiteCounter = 0;

            // Check each item
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                // Green nug check
                if (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" ||
                    item == "Cannabis.Bud.NaturesCandy (Item)" ||
                    item == "Cannabis.Bud.TheDevilsLettuce (Item)")
                {
                    Q7GreenCounter += 1;
                }

                // Orange nug
                if (item == "Cannabis.Bud.BootyJuice (Item)" ||
                    item == "Cannabis.Bud.Catnip (Item)" ||
                    item == "Cannabis.Bud.SnoopLeone (Item)")
                {
                    Q7OrangeCounter += 1;
                }

                // Purple nug
                if (item == "Cannabis.Bud.GranPapasMedicine (Item)" ||
                    item == "Cannabis.Bud.PurpleNurple (Item)" ||
                    item == "Cannabis.Bud.RighteousBud (Item)")
                {
                    Q7PurpleCounter += 1;
                }

                // White nug
                if (item == "Cannabis.Bud.CreeperBud (Item)" ||
                    item == "Cannabis.Bud.MastaRoshi (Item)" ||
                    item == "Cannabis.Bud.WhiteWalker (Item)")
                {
                    Q7WhiteCounter += 1;
                }
            }

            if (Q7GreenCounter > 0)
            {
                bContainsQ7ItemGreen = true;
            }
            else
            {
                bContainsQ7ItemGreen = false;
            }

            if (Q7OrangeCounter > 0)
            {
                bContainsQ7ItemOrange = true;
            }
            else
            {
                bContainsQ7ItemOrange = false;
            }

            if (Q7PurpleCounter > 0)
            {
                bContainsQ7ItemPurple = true;
            }
            else
            {
                bContainsQ7ItemPurple = false;
            }

            if (Q7WhiteCounter > 0)
            {
                bContainsQ7ItemWhite = true;
            }
            else
            {
                bContainsQ7ItemWhite = false;
            }

            // if one of them present, activate options handler on Pookie
            if (bContainsQ7ItemGreen ||
                bContainsQ7ItemOrange ||
                bContainsQ7ItemPurple || 
                bContainsQ7ItemWhite)
            {
                // Activate DH w/ OH
                pookieB1.transform.GetChild(0).gameObject.SetActive(false);
                pookieB1.transform.GetChild(1).gameObject.SetActive(true);

                if (bContainsQ7ItemGreen &&
                    bContainsQ7ItemOrange &&
                    bContainsQ7ItemPurple &&
                    bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[4];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[3] = "Give em a white nug";

                    Q7Options = "gopw";
                }
                else if (bContainsQ7ItemGreen &&
                         bContainsQ7ItemPurple &&
                         bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                    Q7Options = "gpw";
                }
                else if (bContainsQ7ItemGreen &&
                         bContainsQ7ItemOrange &&
                         bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                    Q7Options = "gow";
                }
                else if (bContainsQ7ItemGreen &&
                         bContainsQ7ItemOrange &&
                         bContainsQ7ItemPurple)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";

                    Q7Options = "gop";
                }
                else if (bContainsQ7ItemOrange &&
                         bContainsQ7ItemPurple &&
                         bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                    Q7Options = "opw";
                }
                else if (bContainsQ7ItemGreen &&
                         bContainsQ7ItemOrange)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";

                    Q7Options = "go";
                }
                else if (bContainsQ7ItemGreen &&
                         bContainsQ7ItemPurple)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                    Q7Options = "gp";
                }
                else if (bContainsQ7ItemGreen &&
                         bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                    Q7Options = "gw";
                }
                else if (bContainsQ7ItemOrange &&
                         bContainsQ7ItemPurple)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                    Q7Options = "op";
                }
                else if (bContainsQ7ItemOrange &&
                         bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                    Q7Options = "ow";
                }
                else if (bContainsQ7ItemPurple &&
                         bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                    Q7Options = "pw";
                }
                else if (bContainsQ7ItemGreen)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";

                    Q7Options = "g";
                }
                else if (bContainsQ7ItemOrange)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";

                    Q7Options = "o";
                }
                else if (bContainsQ7ItemPurple)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";

                    Q7Options = "p";
                }
                else if (bContainsQ7ItemWhite)
                {
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB1.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a white nug";

                    Q7Options = "w";
                }
            }
            else
            {
                // Activate DH (no OH)
                pookieB1.transform.GetChild(0).gameObject.SetActive(true);
                pookieB1.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        
        // Quest 8 -- Item Check & Options for PookieBear2
        if (quest8.GetComponent<QuestObject>().bHasStarted &&
            inv.bUpdateItemCount &&
            !bAvoidUpdateQ8)
        {
            // Reset the counters
            Q8GreenCounter = 0;
            Q8OrangeCounter = 0;
            Q8PurpleCounter = 0;
            Q8WhiteCounter = 0;

            // Check each item
            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                // Green nug check
                if (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" ||
                    item == "Cannabis.Bud.NaturesCandy (Item)" ||
                    item == "Cannabis.Bud.TheDevilsLettuce (Item)")
                {
                    Q8GreenCounter += 1;
                }

                // Orange nug
                if (item == "Cannabis.Bud.BootyJuice (Item)" ||
                    item == "Cannabis.Bud.Catnip (Item)" ||
                    item == "Cannabis.Bud.SnoopLeone (Item)")
                {
                    Q8OrangeCounter += 1;
                }

                // Purple nug
                if (item == "Cannabis.Bud.GranPapasMedicine (Item)" ||
                    item == "Cannabis.Bud.PurpleNurple (Item)" ||
                    item == "Cannabis.Bud.RighteousBud (Item)")
                {
                    Q8PurpleCounter += 1;
                }

                // White nug
                if (item == "Cannabis.Bud.CreeperBud (Item)" ||
                    item == "Cannabis.Bud.MastaRoshi (Item)" ||
                    item == "Cannabis.Bud.WhiteWalker (Item)")
                {
                    Q8WhiteCounter += 1;
                }
            }

            if (Q8GreenCounter > 0)
            {
                bContainsQ8ItemGreen = true;
            }
            else
            {
                bContainsQ8ItemGreen = false;
            }

            if (Q8OrangeCounter > 0)
            {
                bContainsQ8ItemOrange = true;
            }
            else
            {
                bContainsQ8ItemOrange = false;
            }

            if (Q8PurpleCounter > 0)
            {
                bContainsQ8ItemPurple = true;
            }
            else
            {
                bContainsQ8ItemPurple = false;
            }

            if (Q8WhiteCounter > 0)
            {
                bContainsQ8ItemWhite = true;
            }
            else
            {
                bContainsQ8ItemWhite = false;
            }

            // if one of them present, activate options handler on Pookie
            if (bContainsQ8ItemGreen ||
                bContainsQ8ItemOrange ||
                bContainsQ8ItemPurple ||
                bContainsQ8ItemWhite)
            {
                // Activate DH w/ OH
                pookieB2.transform.GetChild(0).gameObject.SetActive(false);
                pookieB2.transform.GetChild(1).gameObject.SetActive(true);

                if (bContainsQ8ItemGreen &&
                    bContainsQ8ItemOrange &&
                    bContainsQ8ItemPurple &&
                    bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[4];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[3] = "Give em a white nug";

                    Q8Options = "gopw";
                }
                else if (bContainsQ8ItemGreen &&
                         bContainsQ8ItemPurple &&
                         bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                    Q8Options = "gpw";
                }
                else if (bContainsQ8ItemGreen &&
                         bContainsQ8ItemOrange &&
                         bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                    Q8Options = "gow";
                }
                else if (bContainsQ8ItemGreen &&
                         bContainsQ8ItemOrange &&
                         bContainsQ8ItemPurple)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a purple nug";

                    Q8Options = "gop";
                }
                else if (bContainsQ8ItemOrange &&
                         bContainsQ8ItemPurple &&
                         bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[3];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[2] = "Give em a white nug";

                    Q8Options = "opw";
                }
                else if (bContainsQ8ItemGreen &&
                         bContainsQ8ItemOrange)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a orange nug";

                    Q8Options = "go";
                }
                else if (bContainsQ8ItemGreen &&
                         bContainsQ8ItemPurple)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                    Q8Options = "gp";
                }
                else if (bContainsQ8ItemGreen &&
                         bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                    Q8Options = "gw";
                }
                else if (bContainsQ8ItemOrange &&
                         bContainsQ8ItemPurple)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a purple nug";

                    Q8Options = "op";
                }
                else if (bContainsQ8ItemOrange &&
                         bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                    Q8Options = "ow";
                }
                else if (bContainsQ8ItemPurple &&
                         bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[2];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[1] = "Give em a white nug";

                    Q8Options = "pw";
                }
                else if (bContainsQ8ItemGreen)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a green nug";

                    Q8Options = "g";
                }
                else if (bContainsQ8ItemOrange)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a orange nug";

                    Q8Options = "o";
                }
                else if (bContainsQ8ItemPurple)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a purple nug";

                    Q8Options = "p";
                }
                else if (bContainsQ8ItemWhite)
                {
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options = new string[1];
                    pookieB2.transform.GetChild(1).gameObject.GetComponent<OptionsHolder>().options[0] = "Give em a white nug";

                    Q8Options = "w";
                }
            }
            else
            {
                // Activate DH (no OH)
                pookieB2.transform.GetChild(0).gameObject.SetActive(true);
                pookieB2.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        // Quest 7 & 8 -- Both Fed
        if (bAvoidUpdateQ7 &&
            bAvoidUpdateQ8 &&
            !bAvoidUpdateQ7Q8)
        {
            if (!bAvoidUpdateQ7Q8DH)
            {
                man1.transform.GetChild(0).gameObject.SetActive(false);
                man1.transform.GetChild(1).gameObject.SetActive(true);

                bAvoidUpdateQ7Q8DH = true;
            }

            if (man1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                dMan.bDialogueActive)
            {
                Quest7And8Reward();

                bAvoidUpdateQ7Q8 = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("localD: " + thePlayer.transform.localPosition);
            Debug.Log("regulD: " + thePlayer.transform.position);
            Debug.Log("localP: " + pookieB1.transform.localPosition);
            Debug.Log("regulp: " + pookieB1.transform.position);
        }

        // Minigame -- Guess Who Colluded
        if (thePlayer.GetComponent<PolygonCollider2D>().IsTouching(warpGWC.GetComponent<BoxCollider2D>()))
        {
            // Transition animation
            warpGWC.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

            // Save info
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            save.SavePositionTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", warpGWC.GetComponent<SceneTransitioner>().BetaLoad);

            // Stop the player from bringing up the dialogue again
            dMan.gameObject.SetActive(false);

            // Stop Dan from moving
            thePlayer.GetComponent<Animator>().enabled = false;

            // Stop NPCs from moving
            person1.GetComponent<Animator>().enabled = false;
        }
        
        // Final Item Count Check (allows multiple conditions to be check)
        if (inv.bUpdateItemCount)
        {
            inv.bUpdateItemCount = false;
        }
    }

    public void Quest0Reward()
    {
        if (!quest0.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uiMan.bUpdateBrio = true;

            quest0.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest1Reward()
    {
        if (!quest1.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(10);
            uiMan.bUpdateBrio = true;

            quest1.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest2Reward()
    {
        if (!quest2.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(5);
            uiMan.bUpdateBrio = true;

            quest2.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest3Reward()
    {
        if (!quest3.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(5);
            uiMan.bUpdateBrio = true;

            quest3.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest4Reward()
    {
        if (!quest4.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(15);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(20);
            uiMan.bUpdateBrio = true;

            quest4.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest7And8Reward()
    {
        if (!quest7.GetComponent<QuestObject>().bHasCollected &&
            !quest8.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(25);
            uiMan.bUpdateBrio = true;

            quest7.GetComponent<QuestObject>().bHasCollected = true;
            quest8.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void QuestDialogueCheck()
    {
        // Quest 0 - Dialogue 1 - Option 1
        if (parent2.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            parent2.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest0Dialogue1Opt1();
        }
        // Quest 0 - Dialogue 1 - Option 2
        else if (parent2.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 parent2.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest0Dialogue1Opt2();
        }
        // Quest 0 - Dialogue 2 - Option *
        else if (parent2.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 parent2.transform.GetChild(1).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2))
        {
            oMan.ResetOptions();
            Quest0Dialogue2();
        }


        // Quest 4 - Dialogue 1 - Option 1
        if (kid4.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            kid4.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest4Dialogue1Opt1();
        }
        // Quest 4 - Dialogue 1 - Option 2
        else if (kid4.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 kid4.transform.GetChild(0).gameObject.activeSelf &&
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
        }


        // Quest 6 - Dialogue 1 - Option 1
        if (person1.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
            person1.transform.GetChild(0).gameObject.activeSelf &&
            moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        {
            oMan.ResetOptions();
            Quest6Dialogue1Opt1();
        }
        // Quest 6 - Dialogue 1 - Option 2
        else if (person1.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
                 person1.transform.GetChild(0).gameObject.activeSelf &&
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        {
            oMan.ResetOptions();
            Quest6Dialogue1Opt2();
        }


        // Quest 7 - Dialogue 1 - Option *
        if (pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
            pookieB1.transform.GetChild(1).gameObject.activeSelf &&
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
        if (pookieB2.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
            pookieB2.transform.GetChild(1).gameObject.activeSelf &&
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
    }

    public void Quest0Dialogue1Opt1()
    {
        // Yes -- Play
        // Quest Trigger -> Quest Object text will render first; then we activate the next round of dialogue / options
        // 05/11/2018 DC TODO -- Improve so that options can follow options (when coupled with a quest)

        parent2.transform.GetChild(0).gameObject.SetActive(false);
        parent2.transform.GetChild(1).gameObject.SetActive(true);

        parent2.transform.GetChild(1).GetComponent<DialogueHolder>().bContinueDialogue = true;
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
        dArrow.GetComponent<ImageStrobe>().bStartStrobe = true;
    }

    public void Quest0Dialogue2()
    {
        // 05/11/2018 DC TODO -- Add divergent options, i.e. different text per option selected

        parent2.transform.GetChild(1).gameObject.SetActive(false);
        parent2.transform.GetChild(2).gameObject.SetActive(true);

        // Quest 0 -- Q&A 1 Reward
        Quest0Reward();

        // DC 08/12/2018 -- Have dad walk to the shed and stay there to "work" w/ VR googles and dan k.
        // DC 08/13/2018 -- Dad keeps twisting and turning instead of still looking at dan
    }

    public void Quest4Dialogue1Opt1()
    {
        // Yes -- Play
        kid4.transform.GetChild(0).gameObject.SetActive(false);
        kid4.transform.GetChild(1).gameObject.SetActive(true);

        // Avoid talking to Kid4
        kid4.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;

        thePlayer.transform.position = new Vector3(
            thePlayer.transform.position.x + 0.001f,
            thePlayer.transform.position.y + 0.001f,
            thePlayer.transform.position.z
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
        dArrow.GetComponent<ImageStrobe>().bStartStrobe = true;
    }

    IEnumerator HideAndSeek()
    {
        yield return new WaitForSeconds(2);

        // Move Kid4
        kid4.transform.localPosition = new Vector2(7.429f, -7.797f);

        // "Turn on" Kids 5-9
        kid5.transform.localScale = Vector3.one;
        kid6.transform.localScale = Vector3.one;
        kid7.transform.localScale = Vector3.one;
        kid8.transform.localScale = Vector3.one;
        kid9.transform.localScale = Vector3.one;

        // Need to reset each kid's box collider after first round (but why?)
        kid5.GetComponent<BoxCollider2D>().isTrigger = true;
        kid5.GetComponent<BoxCollider2D>().isTrigger = false;
        kid6.GetComponent<BoxCollider2D>().isTrigger = true;
        kid6.GetComponent<BoxCollider2D>().isTrigger = false;
        kid7.GetComponent<BoxCollider2D>().isTrigger = true;
        kid7.GetComponent<BoxCollider2D>().isTrigger = false;
        kid8.GetComponent<BoxCollider2D>().isTrigger = true;
        kid8.GetComponent<BoxCollider2D>().isTrigger = false;
        kid9.GetComponent<BoxCollider2D>().isTrigger = true;
        kid9.GetComponent<BoxCollider2D>().isTrigger = false;

        // Set dialogue & dialogue elements
        dMan.dialogueLines = new string[] {
                "40..",
                "41..",
                "42... Game on!"
            };
        dMan.portPic = thePlayer.GetComponent<PlayerBrioManager>().portPic;
        dMan.currentLine = 0;
        dText.text = dMan.dialogueLines[dMan.currentLine];
        dMan.ShowDialogue();
        //dArrow.GetComponent<ImageStrobe>().bStartStrobe = true;
        // DC TODO 01/11/2019 -- bStart/bStopStrobe should be fixed in dMan now
        // Go thru the scripts & make sure bStopStrobe is set to true & bStartStrobe = false OR removed altogether

        bAvoidUpdateQ4counting = true;
        bQ4Seeking = true;
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
            dMan.portPic = thePlayer.GetComponent<PlayerBrioManager>().portPic;
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();
        }
        else
        {
            // Set dialogue & dialogue elements
            dMan.dialogueLines = new string[] {
                "Toit! Found all of you."
            };
            dMan.portPic = thePlayer.GetComponent<PlayerBrioManager>().portPic;
            dMan.currentLine = 0;
            dText.text = dMan.dialogueLines[dMan.currentLine];
            dMan.ShowDialogue();

            // Last kid talked to, dh.hasEntered = false
            CheckAndDisableLastKidFound();

            bQ4Seeking = false;
            bFoundQ4All = true;
        }
    }

    IEnumerator HideAndSeekFinished()
    {
        yield return new WaitForSeconds(2);

        // Deactivate Kid4 DH (order matters?)
        kid4.transform.GetChild(1).gameObject.SetActive(false);

        // Move Kid4, Kid4's DH2, Dan & Camera
        kid4.transform.localPosition = new Vector2(4.07f, -9.43f);
        kid4.transform.GetChild(2).gameObject.transform.localPosition = new Vector2(0f, 0f);
        thePlayer.transform.localPosition = new Vector2(-19.157f, -1.674f);
        mainCamera.transform.localPosition = new Vector2(-19.157f, -1.674f);

        // Activate Kid4 DH2 (order matters?)
        kid4.transform.GetChild(2).gameObject.SetActive(true);

        // "Turn off" Kids 5-9
        kid5.transform.localScale = Vector3.zero;
        kid6.transform.localScale = Vector3.zero;
        kid7.transform.localScale = Vector3.zero;
        kid8.transform.localScale = Vector3.zero;
        kid9.transform.localScale = Vector3.zero;

        // Fade in
        screenFader.GetComponent<Animator>().SetBool("FadeIn", true);

        bAvoidUpdateQ4counting = true;
        bQ4Seeking = true;
    }

    public void CheckAndDisableLastKidFound()
    {
        if (Q4LastKidFound == "Kid4")
        {
            kid4.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Kid5")
        {
            kid5.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Kid6")
        {
            kid6.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Kid7")
        {
            kid7.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Kid8")
        {
            kid8.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
        else if (Q4LastKidFound == "Kid9")
        {
            kid9.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasEntered = false;
        }
    }
    
    public void Quest4Reset()
    {
        // Move Kid4 back to his spot
        kid4.transform.localPosition = new Vector2(4.07f, -9.43f);

        // Reset quest checks
        bFoundQ4Kid4 = false;
        bFoundQ4Kid5 = false;
        bFoundQ4Kid6 = false;
        bFoundQ4Kid7 = false;
        bFoundQ4Kid8 = false;
        bFoundQ4Kid9 = false;

        // Reset Kid4 dialogues
        kid4.transform.GetChild(1).gameObject.SetActive(false);
        kid4.transform.GetChild(0).gameObject.SetActive(true);

        // Reset Dialogue Holders
        kid4.transform.GetChild(1).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
        kid5.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
        kid6.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
        kid7.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
        kid8.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;
        kid9.transform.GetChild(0).gameObject.GetComponent<DialogueHolder>().bHasExited = false;

        // Reset everything else
        quest4.GetComponent<QuestObject>().bHasStarted = false;
        bAvoidUpdateQ4counting = false;
        bQ4Seeking = false;
        bAvoidUpdateQ4seeking = false;
        Q4KidCounter = 0;

        // "Turn off" Kids 5-9
        kid5.transform.localScale = Vector3.zero;
        kid6.transform.localScale = Vector3.zero;
        kid7.transform.localScale = Vector3.zero;
        kid8.transform.localScale = Vector3.zero;
        kid9.transform.localScale = Vector3.zero;
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
    }

    public void Quest6Dialogue1Opt1()
    {
        // yes play a game
        // DC TODO -- offer difficulty choices

        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();

            if (item == "VR.Goggles (Item)")
            {
                bContainsQ6Item = true;
            }
        }
        
        if (bContainsQ6Item)
        {
            warpMinesweeper.GetComponent<BoxCollider2D>().enabled = true;
            warpMinesweeper.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

            // Save Transfer Values 
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            save.SavePositionTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", warpMinesweeper.GetComponent<SceneTransitioner>().BetaLoad);

            // Stop the player from bringing up the dialog again
            dMan.gameObject.SetActive(false);

            // Stop Dan from moving
            thePlayer.GetComponent<Animator>().enabled = false;

            // Stop NPCs from moving
            person1.GetComponent<NPCMovement>().moveSpeed = 0;
            person1.GetComponent<Animator>().enabled = false;
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
            dArrow.GetComponent<ImageStrobe>().bStartStrobe = true; // DC TODO -- Not strobing?
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
        dArrow.GetComponent<ImageStrobe>().bStartStrobe = true; // DC TODO -- Not strobing?
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

                if ((item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" ||
                     item == "Cannabis.Bud.NaturesCandy (Item)" ||
                     item == "Cannabis.Bud.TheDevilsLettuce (Item)") && 
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    inv.bUpdateItemCount = true;
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

                if ((item == "Cannabis.Bud.BootyJuice (Item)" ||
                     item == "Cannabis.Bud.Catnip (Item)" ||
                     item == "Cannabis.Bud.SnoopLeone (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    inv.bUpdateItemCount = true;
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

                if ((item == "Cannabis.Bud.GranPapasMedicine (Item)" ||
                     item == "Cannabis.Bud.PurpleNurple (Item)" ||
                     item == "Cannabis.Bud.RighteousBud (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    inv.bUpdateItemCount = true;
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

                if ((item == "Cannabis.Bud.CreeperBud (Item)" ||
                     item == "Cannabis.Bud.MastaRoshi (Item)" ||
                     item == "Cannabis.Bud.WhiteWalker (Item)") &&
                    bTempCheck)

                {
                    inv.Remove(inv.items[i]);
                    inv.bUpdateItemCount = true;
                    bTempCheck = false;
                }
            }
        }

        // Animate Pookie & stop moving
        if (thePlayer.transform.position.x >= pookieB1.transform.position.x)
        {
            // DC TODO 01/13/2019 -- Can't get em to face right?
            pookieB1.GetComponent<Animator>().Play("Eat Right");
        }
        else
        {
            pookieB1.GetComponent<Animator>().Play("Eat Left");
        }

        // Disable Pookie movement & dialogue
        pookieB1.GetComponent<NPCMovement>().enabled = false;
        pookieB1.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(PookieBear1Animations());

        bAvoidUpdateQ7 = true;
    }

    IEnumerator PookieBear1Animations()
    {
        pookieB1.GetComponent<Animator>().Play("Eat Left");

        yield return new WaitForSeconds(2);

        pookieB1.GetComponent<Animator>().Play("Sit Normal");

        yield return new WaitForSeconds(3);

        pookieB1.GetComponent<Animator>().Play("Sit Eyes");

        yield return new WaitForSeconds(2);

        pookieB1.GetComponent<Animator>().Play("Sit Happy");
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
                    inv.bUpdateItemCount = true;
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
                    inv.bUpdateItemCount = true;
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
                    inv.bUpdateItemCount = true;
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
                    inv.bUpdateItemCount = true;
                    bTempCheck = false;
                }
            }
        }

        // Animate Pookie & stop moving
        if (thePlayer.transform.position.x >= pookieB2.transform.position.x)
        {
            // DC TODO 01/13/2019 -- Can't get em to face right?
            pookieB2.GetComponent<Animator>().Play("Eat Right");
        }
        else
        {
            pookieB2.GetComponent<Animator>().Play("Eat Left");
        }

        // Disable Pookie movement & dialogue
        pookieB2.GetComponent<NPCMovement>().enabled = false;
        pookieB2.transform.GetChild(1).gameObject.SetActive(false);

        StartCoroutine(PookieBear2Animations());

        bAvoidUpdateQ8 = true;
    }

    IEnumerator PookieBear2Animations()
    {
        pookieB2.GetComponent<Animator>().Play("Eat Left");

        yield return new WaitForSeconds(2);

        pookieB2.GetComponent<Animator>().Play("Sit Normal");

        yield return new WaitForSeconds(3);

        pookieB2.GetComponent<Animator>().Play("Sit Eyes");

        yield return new WaitForSeconds(2);

        pookieB2.GetComponent<Animator>().Play("Sit Happy");
    }

    public void LoadQuests()
    {
        // 0 = TBStarted
        // 1 = TBEnded
        // 2 = Complete
        // 3 = Collected
        savedQuestsValue = PlayerPrefs.GetString("Chp1Quests");

        for (int i = 0; i < savedQuestsValue.Length; i++)
        {
            GameObject Quest = GameObject.Find("Quest_" + i);

            if (savedQuestsValue.Substring(i, 1) == 3.ToString())
            {
                Quest.GetComponent<QuestObject>().bHasCollected = true;
                Quest.GetComponent<QuestObject>().bHasEnded = true;
                Quest.GetComponent<QuestObject>().bHasStarted = true;
            }
            else if (savedQuestsValue.Substring(i, 1) == 2.ToString())
            {
                Quest.GetComponent<QuestObject>().bHasEnded = true;
                Quest.GetComponent<QuestObject>().bHasStarted = true;
            }
            else if (savedQuestsValue.Substring(i, 1) == 1.ToString())
            {
                Quest.GetComponent<QuestObject>().bHasStarted = true;
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
        string savedQuestsValue = "";

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
