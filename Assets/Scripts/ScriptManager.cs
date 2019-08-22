// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  08/21/2019

using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public Chp1 chp1;
    public DialogueManager dMan;

    public void InventoryUpdate()
    {
        chp1.GoggleCheck();
        chp1.SmoochyWoochyCheck();
        chp1.PookieCheck();
    }

    public void DialogueAction(string action)
    {
        if (action == "Chp1RaceReward")
            chp1.Quest1Reward();
    }

    public void QuestAction(string action)
    {
        if (action == "Chp1EnkiReward")
            chp1.Quest3Reward();
    }

    public void ActionOnClose(string action)
    {
        dMan.closingAction = action;
    }

    public void ClosingAction(string action)
    {
        if (action == "SmoochyWoochy")
            chp1.SmoochyWoochyCheck();

        if (action == "HideAndSeek")
            chp1.PreHideAndSeek();

        if (action == "HidNowSeeking")
            chp1.HidNowSeeking();

        if (action == "PreHideAndSeekFinished")
            chp1.PreHideAndSeekFinished();
    }
}
