// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  07/16/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class QuestObject : MonoBehaviour
{
    public QuestManager theQM;

    public int questNumber;

    public string[] beginText;
    public string[] endText;
    
	void Start ()
    {
        // theQM = FindObjectOfType<QuestManager>();
	}
	
	void Update ()
    {
		
	}

    public void BeginQuest()
    {
        theQM.ShowQuestText(beginText); 
    }

    public void EndQuest()
    {
        theQM.ShowQuestText(endText);
        theQM.questsCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
