// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  08/13/2018

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
            bHasStarted = true;
        }
    }

    public void EndQuest()
    {
        if (!bHasEnded)
        {
            dMan.portPic = portPic;
            qMan.ShowQuestText(endText);
            qMan.questsCompleted[questNumber] = true;
            bHasEnded = true;
        }
    }
}
