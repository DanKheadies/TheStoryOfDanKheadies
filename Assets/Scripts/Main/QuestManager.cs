// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  04/26/2021

using UnityEngine;

// Manage the Quests
public class QuestManager : MonoBehaviour
{
    public DialogueManager dMan;
    public QuestObject[] quests;

    public bool[] questsStarted;
    public bool[] questsEnded;
    public bool[] questsCollected;

	void Awake ()
    {
        // Initializers
        questsStarted = new bool[quests.Length];
        questsEnded = new bool[quests.Length];
        questsCollected = new bool[quests.Length];
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("QL: " + quests.Length);
            for (int i = 0; i < quests.Length; i++)
            {
                Debug.Log("Q#: " + quests[i].questNumber);
                Debug.Log("St: " + quests[i].bHasStarted);
                Debug.Log("En: " + quests[i].bHasEnded);
            }

            //Debug.Log("QC: " + questsCompleted.Length);
            //for (int i = 0; i < questsCompleted.Length; i++)
            //{
            //    Debug.Log("Q#: " + questsCompleted[i]);
            //    //Debug.Log("St: " + questsCompleted[i].bHasStarted);
            //    //Debug.Log("En: " + questsCompleted[i].bHasEnded);
            //}
        }
    }

    public void ShowQuestText(string[] questText)
    {
        if (questText.Length >= 1)
        {
            dMan.dialogueLines = new string[questText.Length];
            dMan.dialogueLines = questText;
            dMan.PauseDialogue();
            dMan.ShowDialogue();
        }
    }
}
