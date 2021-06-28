// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  06/25/2021

using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    public FillBackground mainMenuBack1;
    public FillBackground mainMenuBack2;
    public FillBackground sceneTransBack;
    public Chp0 chp0;
    public Chp1 chp1;
    public ControllerSupport contSupp;
    public CS_TreeTunnel treeTunnel;
    public DialogueManager dMan;
    public SaveGame save;
    public ScreenOrientation screenOri;
    public TD_SBF_ControlManagement tdsbfContMan;
    public TD_SBF_MenuController tdsbfMenuCont;
    public TD_SBF_ModeSelector tdsbfModeSelector;
    public TD_SBF_MovePauseMenuSelector tdsbfmpmSelector;
    public TD_SBF_PauseMenu tdsbfPause;
    
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
                treeTunnel.CompleteCutscene();
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

    public void InventoryUpdate()
    {
        if (chp1)
        {
            chp1.GoggleCheck();
            chp1.SmoochyWoochyCheck();
            chp1.PookieCheck();
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

    public void ResetParameters(string sceneName)
    {
        if (treeTunnel &&
            sceneName == "CS_TreeTunnel")
            treeTunnel.ScaleAnimation();
        else if (mainMenuBack1 &&
                 mainMenuBack2 &&
                 sceneName == "MainMenu")
        {
            mainMenuBack1.SetBackground();
            mainMenuBack2.SetBackground();
        }
        else if (sceneTransBack &&
                 sceneName == "SceneTransitioner")
            sceneTransBack.SetBackground();
        else if (tdsbfMenuCont &&
                 sceneName == "TD_SBF_MenuController")
            tdsbfMenuCont.OrientationCheck();
        else if (tdsbfModeSelector &&
                 sceneName == "TD_SBF_ModeSelector")
            tdsbfModeSelector.OrientationCheck();
        else if (tdsbfContMan &&
                 tdsbfPause &&
                 sceneName == "TD_SBF_L1")
        {
            tdsbfContMan.ResetForOrientation();
            tdsbfPause.ResetOrientation();
        }
    }

    public void SavingSpecificInfo()
    {
        if (chp1)
        {
            chp1.SaveSpecificInfo();
        }
    }

    public void ToggleDevSupport()
    {
        if (contSupp)
            contSupp.ToggleDevSupport();
    }
}
