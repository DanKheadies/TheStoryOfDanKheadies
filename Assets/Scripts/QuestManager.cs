// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  03/03/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage the Quests
public class QuestManager : MonoBehaviour
{
    public DialogueManager dMan;
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
        dMan.dialogueLines = new string[questText.Length];
        dMan.dialogueLines = questText;
        dMan.ShowDialogue();
    }
}
