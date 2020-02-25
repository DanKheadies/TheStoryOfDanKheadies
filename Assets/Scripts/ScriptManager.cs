// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  02/20/2020

using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public Chp0 chp0;
    public Chp1 chp1;
    public CS_TreeTunnel treeTunnel;
    public DialogueManager dMan;
    public ScreenOrientation screenOri;
    public TD_SBF_MenuController tdsbfMenuCont;
    public TD_SBF_ScreenOrientator_PauseMenu tdsbfScreenOriPause;

    public void InventoryUpdate()
    {
        if (chp1)
        {
            chp1.GoggleCheck();
            chp1.SmoochyWoochyCheck();
            chp1.PookieCheck();
        }
    }

    public void DialogueAction(string action)
    {
        if (chp1)
        {
            if (action == "Chp1RaceReward")
                chp1.Quest1Reward();
        }
    }

    public void QuestAction(string action)
    {
        if (chp1)
        {
            if (action == "Chp1EnkiReward")
                chp1.Quest3Reward();
        }
    }

    public void ActionOnClose(string action)
    {
        dMan.closingAction = action;
    }

    public void ClosingAction(string action)
    {
        if (chp1)
        {
            if (action == "DagonWalkingToLab")
                chp1.DagonWalkingToLab();

            else if (action == "HideAndSeek")
                chp1.PreHideAndSeek();

            else if (action == "HidNowSeeking")
                chp1.HidNowSeeking();

            else if (action == "PreHideAndSeekFinished")
                chp1.PreHideAndSeekFinished();

            else if (action == "PookieCheck")
                chp1.PookieCheck();

            else if (action == "ResetGreatTreeDialogueToMiddle")
                chp1.ResetGreatTreeDialogueToMiddle();

            else if (action == "SmoochyWoochy")
                chp1.SmoochyWoochyCheck();
        }
        else if (treeTunnel)
        {
            if (action == "WarpOut")
                treeTunnel.WarpOut();
        }
    }

    public void ResetParameters(string sceneName)
    {
        if (treeTunnel &&
            sceneName == "CS_TreeTunnel")
            treeTunnel.ScaleAnimation();
        else if (tdsbfMenuCont &&
                 sceneName == "TD_SBF_MenuController")
            tdsbfMenuCont.OrientationCheck();
        else if (tdsbfScreenOriPause &&
                 sceneName == "TD_SBF_LX")
            tdsbfScreenOriPause.SetTransform();
    }
}
