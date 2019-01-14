// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  01/14/2019

using UnityEngine;

// Settings for the Quest object
public class QuestObject : MonoBehaviour
{
    public DialogueManager dMan;
    public QuestManager qMan;
    public Sprite portPic;

    public bool bHasStarted;
    public bool bHasEnded;
    public bool bHasCollected;

    public int questNumber;

    public string[] beginText;
    public string[] endText;

    void Start()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        qMan = FindObjectOfType<QuestManager>();
    } 

    public void BeginQuest()
    {
        if (!bHasStarted)
        {
            dMan.portPic = portPic;
            qMan.ShowQuestText(beginText);
            qMan.questsStarted[questNumber] = true;
            bHasStarted = true;
        }
    }

    public void EndQuest()
    {
        if (!bHasEnded)
        {
            dMan.portPic = portPic;
            qMan.ShowQuestText(endText);
            qMan.questsEnded[questNumber] = true;
            bHasEnded = true;
        }
    }

    public void CollectedQuest()
    {
        if (!bHasCollected)
        {
            qMan.questsCollected[questNumber] = true;
            bHasCollected = true;
        }
    }
}
