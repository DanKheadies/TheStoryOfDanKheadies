// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/16/2017
// Last:  02/13/2020

using UnityEngine;

// Settings for the Quest object
public class QuestObject : MonoBehaviour
{
    public DialogueManager dMan;
    public QuestManager qMan;
    public ScriptManager sMan;
    public Sprite portPic;

    public bool bActionOnClose;
    public bool bHasStarted;
    public bool bHasEnded;
    public bool bHasCollected;

    public int questNumber;

    public string action;
    public string[] beginText;
    public string[] endText;

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

            // Run any actions
            if (bActionOnClose)
                sMan.ActionOnClose(action);
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
