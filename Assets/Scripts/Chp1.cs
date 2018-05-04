// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  04/23/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all Chapter 1 quests, items, and elements
public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public CannabisPlant cannaP;
    public DialogueManager dMan;
    public GameObject kid2;
    public GameObject oldMan1;
    public GameObject parent2;
    public GameObject quest0;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject quest3;
    public GameObject questTrigger2;
    public GameObject thePlayer;
    public Inventory inv;
    public OptionsManager oMan;
    public QuestManager qMan;
    public SaveGame sGame;

    public bool bAvoidUpdateQ0;
    public bool bAvoidUpdateQ1;
    public bool bAvoidUpdateQ2;
    public bool bAvoidUpdateQ3;
    public bool bContainsItem;
    public bool bGetInventory;

    public float timer;
    public float raceTimer;

    public string savedQuestsValue;

    void Start()
    {
        // Initializers
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cannaP = GameObject.FindGameObjectWithTag("SmoochyWoochyPoochy").GetComponent<CannabisPlant>();
        dMan = FindObjectOfType<DialogueManager>();
        inv = FindObjectOfType<Inventory>();
        kid2 = GameObject.Find("Kid2");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        oldMan1 = GameObject.Find("OldMan1");
        oMan = GameObject.FindObjectOfType<OptionsManager>();
        parent2 = GameObject.Find("Parent.2");
        qMan = FindObjectOfType<QuestManager>();
        questTrigger2 = GameObject.Find("QT_2");
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        savedQuestsValue = PlayerPrefs.GetString("Chp1Quests");
        sGame = FindObjectOfType<SaveGame>();

        inv.RerunStart();

        quest0 = GameObject.Find("Quest_0");
        quest1 = GameObject.Find("QuestManager").transform.GetChild(1).gameObject; // 03/29/18 DC -- Avoid Null Exception (wtf?)
        quest2 = GameObject.Find("Quest_2");
        quest3 = GameObject.Find("Quest_3");

        timer = 0.33f;
        raceTimer = 0f;

        // Chapter 1 -- First Time
        if (PlayerPrefs.GetString("Chapter") != "Chp1")
        {
            thePlayer.transform.position = new Vector2(-13.68f, -7.625f);
            mainCamera.transform.position = new Vector2(-13.68f, -7.625f);
            camFollow.currentCoords = CameraFollow.AnandaCoords.Home;
        }
        // Chapter 1 -- Saved Game
        else
        {
            sGame.RerunStart();
            sGame.GetSavedGame();
            LoadQuests();
            inv.bUpdateItemCount = true;
        }
    }

    void Update()
    {
        // Saved Game -- Load inventory
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                if (PlayerPrefs.GetString("Chapter") != "Chp1")
                {
                    inv.LoadInventory("transfer");
                }
                else
                {
                    inv.LoadInventory("saved");
                }
            }
        }

        // Quest 0 -- Q&A 1 Reward
        if (!quest0.GetComponent<QuestObject>().bHasCollected &&
            quest0.GetComponent<QuestObject>().bHasEnded &&
            (int)camFollow.currentCoords == 0)
        {
            //if (kid2.transform.GetChild(1).GetComponent<DialogueHolder>().bHasEntered &&
            //    dMan.bDialogueActive)
            //{
            //    Quest0Reward();
            //}
        }

        // Quest 1 -- Race Start -> Start Timer
        if (quest1.GetComponent<QuestObject>().bHasStarted &&
            !quest1.GetComponent<QuestObject>().bHasEnded)
        {
            raceTimer += Time.deltaTime;
        }

        // Quest 1 -- Race End -> Check Race Time & Assign Dialogue
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

        // Quest 1 -- Race Reward
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

        // Quest 2 -- Search Start
        if (quest2.GetComponent<QuestObject>().bHasStarted &&
            !dMan.bDialogueActive &&
            !bAvoidUpdateQ2)
        {
            bAvoidUpdateQ2 = true;
            GameObject.Find("TreeHouseDoor").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("TreeHouseDoor").transform.localScale = Vector3.one;
        }

        // Quest 2 -- Search Reward
        if (!quest2.GetComponent<QuestObject>().bHasCollected && 
            quest2.GetComponent<QuestObject>().bHasEnded &&
            (int)camFollow.currentCoords == 23)
        {
            Quest2Reward();
            
        }

        // Quest 3 -- Item Check
        if (quest3.GetComponent<QuestObject>().bHasStarted &&
            inv.bUpdateItemCount &&
            !bAvoidUpdateQ3)
        {
            // Assume not present unless we find it
            bContainsItem = false;

            for (int i = 0; i < inv.items.Count; i++)
            {
                string item = inv.items[i].ToString();
                
                if (item == "Cannabis.Bud.SmoochyWoochyPoochy (Item)")
                {
                    bContainsItem = true;
                }
            }

            if (bContainsItem)
            {
                oldMan1.transform.GetChild(0).gameObject.SetActive(false);
                oldMan1.transform.GetChild(1).gameObject.SetActive(true);

                oldMan1.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().endQuest = true;
            }
            else if (!bContainsItem)
            {
                oldMan1.transform.GetChild(1).gameObject.GetComponent<QuestTrigger>().endQuest = false;

                oldMan1.transform.GetChild(0).gameObject.SetActive(true);
                oldMan1.transform.GetChild(1).gameObject.SetActive(false);
            }
                
            inv.bUpdateItemCount = false;
        }
        
        // Quest 3 -- Item Reward & Removal
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
    }

    public void Quest0Reward()
    {
        if (!quest0.GetComponent<QuestObject>().bHasCollected)
        {
            //thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            //thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(10);

            quest0.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest1Reward()
    {
        if (!quest1.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(10);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(10);

            quest1.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest2Reward()
    {
        if (!quest2.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(5);

            quest2.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest3Reward()
    {
        if (!quest3.GetComponent<QuestObject>().bHasCollected)
        {
            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(5);

            quest3.GetComponent<QuestObject>().bHasCollected = true;
        }
    }

    public void Quest0Dialogue1Opt1()
    {
        // Yes play a game
        // Start Quest
        oMan.ResetOptions();
        // Start Truth or Elaborate Lie prompt

    }
    public void Quest0Dialogue1Opt2()
    {
        // No play a game
        oMan.ResetOptions();
    }
    public void Quest0Dialogue2()
    {
        // Picks Truth or Elaborate Lie
        // Finish Quest & dialogue
        oMan.ResetOptions();
        // Revert dialogue to standard convo / text

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
