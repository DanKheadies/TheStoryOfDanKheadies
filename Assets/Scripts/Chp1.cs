// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  10/04/2018

using UnityEngine;
using UnityEngine.UI;

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
    public UIManager uiMan;

    public bool bAvoidGreatTreeConvo;
    public bool bAvoidUpdateQ0;
    public bool bAvoidUpdateQ1;
    public bool bAvoidUpdateQ2;
    public bool bAvoidUpdateQ3;
    public bool bAvoidUpdateQ4;
  //public bool bAvoidUpdateQ5;
  //public bool bAvoidUpdateQ6;
    public bool bAvoidUpdateQ7;
    public bool bAvoidUpdateQ8;

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

    public bool bGetInventory;

    public float invTimer;
    public float raceTimer;

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
        inv = FindObjectOfType<Inventory>();
        kid2 = GameObject.Find("Kid2");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        moveOptsArw = FindObjectOfType<MoveOptionsMenuArrow>();
        oldMan1 = GameObject.Find("OldMan1");
        oMan = FindObjectOfType<OptionsManager>();
        parent2 = GameObject.Find("Parent.2");
        person1 = GameObject.Find("Person.1");
        pookieB1 = GameObject.Find("PookieBear.1");
        pookieB2 = GameObject.Find("PookieBear.2");
        qMan = FindObjectOfType<QuestManager>();
        questTrigger2 = GameObject.Find("QT_2");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        savedQuestsValue = PlayerPrefs.GetString("Chp1Quests");
        save = FindObjectOfType<SaveGame>();
        warpGWC = GameObject.Find("Chp1.to.GuessWhoColluded");
        warpMinesweeper = GameObject.Find("Chp1.to.Minesweeper");
        uiMan = FindObjectOfType<UIManager>();

        inv.RerunStart();

        quest0 = GameObject.Find("Quest_0"); // Truth or Elaborate Lie w/ Parent2
        //quest1 = GameObject.Find("QuestManager").transform.GetChild(1).gameObject; // 03/29/18 DC -- Avoid Null Exception (wtf?)
        // fixed above?
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
            GameObject.Find("TreeHouseDoor").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("TreeHouseDoor").transform.localScale = Vector3.one;
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
                
            inv.bUpdateItemCount = false;
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

        // Quest 4 -- Manhunt w/ Kid4 -> TBD
        // TODO w/ kid in the woods

        // Quest 7 -- Item Delivery w/ Man1 -> 
        if (quest7.GetComponent<QuestObject>().bHasStarted &&
            inv.bUpdateItemCount &&
            !bAvoidUpdateQ7)
        {
            // Assume not present unless we find it
            //bContainsQ4Item = false;

            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();

                // Green nug check
                if (!bContainsQ7ItemGreen &&
                   (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)" ||
                    item == "Cannabis.Bud.NaturesCandy (Item)" ||
                    item == "Cannabis.Bud.TheDevilsLettuce (Item)"))
                {
                    // activate option 1
                    //pookieB1.GetComponent<OptionsHolder>().
                    //bContainsQ4ItemGreen = true;
                }

                // White nug
                if (!bContainsQ7ItemWhite &&
                   (item == "Cannabis.Bud.CreeperBud (Item)" ||
                    item == "Cannabis.Bud.MastaRoshi (Item)" ||
                    item == "Cannabis.Bud.WhiteWalker (Item)"))
                {
                    // activate option 2
                }

                // Purple nug
                if (!bContainsQ7ItemPurple &&
                   (item == "Cannabis.Bud.GranPapasMedicine (Item)" ||
                    item == "Cannabis.Bud.PurpleNurple (Item)" ||
                    item == "Cannabis.Bud.RighteousBud (Item)"))
                {
                    // activate option 3
                }

                // Orange nug
                if (!bContainsQ7ItemOrange &&
                   (item == "Cannabis.Bud.BootyJuice (Item)" ||
                    item == "Cannabis.Bud.Catnip (Item)" ||
                    item == "Cannabis.Bud.SnoopLeone (Item)"))
                {
                    // activate option 4
                }
            }

            //if (bContainsQ4Item)
            //{
            //    oldMan1.transform.GetChild(0).gameObject.SetActive(false);
            //    oldMan1.transform.GetChild(1).gameObject.SetActive(true);

            //    oldMan1.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().endQuest = true;
            //}
            //else if (!bContainsQ4Item)
            //{
            //    oldMan1.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().endQuest = false;

            //    oldMan1.transform.GetChild(0).gameObject.SetActive(true);
            //    oldMan1.transform.GetChild(1).gameObject.SetActive(false);
            //}

            inv.bUpdateItemCount = false;
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
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(5);
            uiMan.bUpdateBrio = true;

            quest4.GetComponent<QuestObject>().bHasCollected = true;
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
        // Quest 5 - Dialogue 1 - Option *
        else if (greatTree.transform.GetChild(0).GetComponent<DialogueHolder>().bHasEntered &&
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
        else if (pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 pookieB1.transform.GetChild(1).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt4))
        {
            oMan.ResetOptions();
            dMan.ResetDialogue();
        }
        else if (pookieB2.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
                 pookieB2.transform.GetChild(1).gameObject.activeSelf &&
                (moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3 ||
                 moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt4))
        {
            oMan.ResetOptions();
            dMan.ResetDialogue();
        }
        //// Quest 7 - Dialogue 1 - Option 1
        //else if (pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
        //         pookieB1.transform.GetChild(1).gameObject.activeSelf &&
        //         moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt1)
        //{
        //    oMan.ResetOptions();

        //    // Option 1 selected
        //    // Remove nug
        //    // Eating animation

        //    // If correct nug
        //    // Sit down, bug out, and remain sitting & smiling
        //    // Quest complete -> quest reward
        //    // Else keep walking
        //}
        //// Quest 7 - Dialogue 1 - Option 2
        //else if (pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
        //         pookieB1.transform.GetChild(1).gameObject.activeSelf &&
        //         moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt2)
        //{
        //    oMan.ResetOptions();

        //    // Option 2 selected
        //    // Remove nug
        //    // Eating animation

        //    // If correct nug
        //    // Sit down, bug out, and remain sitting & smiling
        //    // Quest complete -> quest reward
        //    // Else keep walking
        //}
        //// Quest 7 - Dialogue 1 - Option 3
        //else if (pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
        //         pookieB1.transform.GetChild(1).gameObject.activeSelf &&
        //         moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt3)
        //{
        //    oMan.ResetOptions();

        //    // Option 3 selected
        //    // Remove nug
        //    // Eating animation

        //    // If correct nug
        //    // Sit down, bug out, and remain sitting & smiling
        //    // Quest complete -> quest reward
        //    // Else keep walking
        //}
        //// Quest 7 - Dialogue 1 - Option 4
        //else if (pookieB1.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
        //         pookieB1.transform.GetChild(1).gameObject.activeSelf &&
        //         moveOptsArw.currentPosition == MoveOptionsMenuArrow.ArrowPos.Opt4)
        //{
        //    oMan.ResetOptions();

        //    // Option 4 selected
        //    // Remove nug
        //    // Eating animation

        //    // If correct nug
        //    // Sit down, bug out, and remain sitting & smiling
        //    // Quest complete -> quest reward
        //    // Else keep walking
        //}
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

        // DC 08/12/2018 -- Have dad walk downstairs and stay there to "work" w/ VR googles and dan k.
        // DC 08/13/2018 -- Dad keeps twisting and turning instead of still looking at dan
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
