// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  03/04/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 
public class QuestObject : MonoBehaviour
{
    public Inventory inv;
    public QuestManager theQM;

    public Item tempItem;
    private string invItem;

    public int questNumber;

    public string[] beginText;
    public string[] endText;
    
	void Start ()
    {
        inv = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();
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

    public void CollectionQuest()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Item Total"); i++)
        {
            invItem = PlayerPrefs.GetString("Item" + i);
            invItem = invItem.Substring(0, invItem.Length - 7);

            tempItem = (Item)Resources.Load("Items/" + invItem);
            Inventory.instance.Add(tempItem);
            Debug.Log(tempItem.itemName);
        }
    }
}
