// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  07/16/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the Quests
public class QuestManager : MonoBehaviour
{
    public DialogueManager theDM;
    public QuestObject[] quests;

    public bool[] questsCompleted;

	void Start ()
    {
        questsCompleted = new bool[quests.Length];
	}
	
	void Update ()
    {
		
	}

    public void ShowQuestText(string[] questText)
    {
        theDM.dialogueLines = new string[questText.Length];
        theDM.dialogueLines = questText;
        theDM.ShowDialogue();
    }
}
