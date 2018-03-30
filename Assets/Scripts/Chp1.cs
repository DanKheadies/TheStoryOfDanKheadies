// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/08/2018
// Last:  03/30/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all Chapter 1 quests, items, and elements
public class Chp1 : MonoBehaviour
{
    public Camera mainCamera;
    public CameraFollow camFollow;
    public DialogueManager dMan;
    public GameObject quest0;
    public GameObject quest1;
    public GameObject quest2;
    public GameObject quest3;
    public GameObject thePlayer;
    public Inventory inv;
    public QuestManager qMan;
    public SaveGame sGame;

    public bool bAvoidUpdateQ1;
    public bool bAvoidUpdateQ2;
    public bool bGetInventory;

    public float timer;
    public float raceTimer;

    void Start ()
    {
        // Initializers
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        dMan = FindObjectOfType<DialogueManager>();
        inv = FindObjectOfType<Inventory>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        qMan = FindObjectOfType<QuestManager>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        sGame = FindObjectOfType<SaveGame>();

        inv.RerunStart();

        quest0 = GameObject.Find("Quest_0");
        quest1 = GameObject.Find("QuestManager").transform.GetChild(1).gameObject; // 03/29/18 DC -- Avoid Null Exception (wtf?)
        quest2 = GameObject.Find("Quest_2");
        quest3 = GameObject.Find("Quest_3");

        bAvoidUpdateQ1 = false;
        bAvoidUpdateQ2 = false;
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
        }
    }
	
	void Update ()
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

        if (quest1.GetComponent<QuestObject>().bHasStarted &&
            !quest1.GetComponent<QuestObject>().bHasEnded)
        {
            raceTimer += Time.deltaTime;
        }

        if (quest1.GetComponent<QuestObject>().bHasEnded && 
            !dMan.bDialogueActive && 
            !bAvoidUpdateQ1)
        {
            bAvoidUpdateQ1 = true;
            GameObject kid2 = GameObject.Find("Kid2");
            kid2.transform.GetChild(0).gameObject.SetActive(false);
            kid2.transform.GetChild(1).gameObject.SetActive(true);

            raceTimer = Mathf.Round(raceTimer);

            if (raceTimer <= 10)
            {
                string raceTimeText = "Well.. Well ran. You set one of the best records at " + raceTimer + " seconds.";

                kid2.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Holy goat nipples Batman.. How the.. Did you cheat?",
                    raceTimeText,

                };
            }
            else if (raceTimer <= 30 && raceTimer > 10)
            {
                string raceTimeText = "You ran that in " + raceTimer + " seconds.";

                kid2.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Good race Dan! " + raceTimeText,
                    "Keep practicing and improving, and maybe one day, you'll beat my time!"
                };
            }
            else
            {
                string raceTimeText = "You finished after " + raceTimer + " seconds.";

                kid2.transform.GetChild(1).GetComponent<DialogueHolder>().dialogueLines = new string[] {
                    "Well.. You made it... " + raceTimeText,
                    "Keep training Dan. It's good for you!"
                };
            }

            thePlayer.GetComponent<PlayerBrioManager>().IncreaseMaxBrio(5);
            thePlayer.GetComponent<PlayerBrioManager>().RestorePlayer(10);
        }

        if (quest2.GetComponent<QuestObject>().bHasStarted && 
            !dMan.bDialogueActive && 
            !bAvoidUpdateQ2)
        {
            bAvoidUpdateQ2 = true;
            GameObject.Find("TreeHouseDoor").GetComponent<BoxCollider2D>().isTrigger = false;
            GameObject.Find("TreeHouseDoor").transform.localScale = Vector3.one;
        }
    }

    public void LoadQuests()
    {
        // 0 = TBStarted
        // 1 = TBEnded
        // 2 = Complete
        string savedQuestsValue = PlayerPrefs.GetString("Chp1Quests");
        Debug.Log(savedQuestsValue);

        for (int i = 0; i < savedQuestsValue.Length; i++)
        {
            GameObject Quest = GameObject.Find("Quest_" + i);

            if (savedQuestsValue.Substring(i, 1) == 2.ToString())
            {
                Quest.GetComponent<QuestObject>().bHasEnded = true; // DC 03/30/2018 -- Null Ref Ex
                Quest.GetComponent<QuestObject>().bHasStarted = true;
            }
            else if (savedQuestsValue.Substring(i, 1) == 1.ToString())
            {
                Quest.GetComponent<QuestObject>().bHasStarted = true;
            }
        }

        PlayerPrefs.SetString("Chp1Quests", savedQuestsValue);
        Debug.Log("C1Q: " + PlayerPrefs.GetString("Chp1Quests"));
    }

    public void SaveQuests()
    {
        // 0 = TBStarted
        // 1 = TBEnded
        // 2 = Complete
        string savedQuestsValue = "";

        for (int i = 0; i < qMan.quests.Length; i++)
        {
            savedQuestsValue += i;

            if (qMan.quests[i].bHasEnded)
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
        Debug.Log("C1Q: " + PlayerPrefs.GetString("Chp1Quests"));
    }
}
