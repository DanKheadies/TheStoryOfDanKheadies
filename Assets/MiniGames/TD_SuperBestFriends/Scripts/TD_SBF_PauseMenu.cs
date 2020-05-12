// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  05/05/2020

using UnityEngine;
using UnityEngine.SceneManagement;

public class TD_SBF_PauseMenu : MonoBehaviour
{
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public GameObject audioButtons;
    public GameObject controlsButtons;
    public GameObject creditsButtons;
    public GameObject muorButtons;
    public GameObject optionsButtons;
    public GameObject pauseButtons;
    public GameObject ui;
    public ScriptManager scriptMan;
    public TD_SBF_CameraFollow camFollow;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_MovePauseMenuSelector mpmSelector;
    public TD_SBF_SceneFader sceneFader;
    public TD_SBF_TouchControls touchConts;

    public bool bIsModeSelector;

    public string levelToLoad = "TD_SBF_ModeSelector";

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) ||
             Input.GetKeyDown(KeyCode.P) ||
             contSupp.ControllerMenuRight("down")) &&
             !bIsModeSelector)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);
        
        ResetPauseMenu();

        if (ui.activeSelf &&
            !bIsModeSelector)
        {
            Time.timeScale = 0f;

            if (touchConts && 
                touchConts.currentContOpac > 0)
                touchConts.HideControls();

            if (contSupp.bControllerConnected)
                mpmSelector.mainGoOnBtn.Select();
        }
        else
        {
            Time.timeScale = 1f;

            if (touchConts &&
                touchConts.currentContOpac > 0)
                touchConts.DisplayControls();
        }

        if (gMan)
        {
            if (gMan.bIsHeroMode &&
                ui.activeSelf)
                camFollow.enabled = false;
            else if (gMan.bIsHeroMode &&
                     !ui.activeSelf)
                camFollow.enabled = true;
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(levelToLoad);
    }

    public void ToggleOptions()
    {
        optionsButtons.SetActive(!optionsButtons.activeSelf);
        pauseButtons.SetActive(!pauseButtons.activeSelf);
    }

    public void ToggleAudio()
    {
        audioButtons.SetActive(!audioButtons.activeSelf);
        optionsButtons.SetActive(!optionsButtons.activeSelf);
    }

    public void ToggleControls()
    {
        controlsButtons.SetActive(!controlsButtons.activeSelf);
        optionsButtons.SetActive(!optionsButtons.activeSelf);
    }

    public void ToggleMuor()
    {
        optionsButtons.SetActive(!optionsButtons.activeSelf);
        muorButtons.SetActive(!muorButtons.activeSelf);
    }

    public void ToggleCredits()
    {
        muorButtons.SetActive(!muorButtons.activeSelf);
        creditsButtons.SetActive(!creditsButtons.activeSelf);
    }

    public void ResetPauseMenu()
    {
        audioButtons.SetActive(false);
        controlsButtons.SetActive(false);
        creditsButtons.SetActive(false);
        muorButtons.SetActive(false);
        optionsButtons.SetActive(false);
        pauseButtons.SetActive(true);
    }
}
