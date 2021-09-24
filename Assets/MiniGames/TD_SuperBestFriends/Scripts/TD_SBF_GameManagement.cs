// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  07/21/2021

using UnityEngine;

public class TD_SBF_GameManagement : MonoBehaviour
{
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public GameObject cameraCont;
    public GameObject completeLevelUI_Horizontal;
    public GameObject completeLevelUI_Vertical;
    public GameObject gameOverUI_Horizontal;
    public GameObject gameOverUI_Vertical;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_HeroBarManager heroBMan;
    public TD_SBF_NodeUISelector nodeUISel;
    public TD_SBF_ShopSelector shopSel;

    public bool bIsHeroMode;
    public bool bIsTowerMode;
    public static bool IsGameOver;
    public static bool IsLevelWon;

    void Start()
    {
        IsGameOver = false;
        IsLevelWon = false;

        cameraCont.GetComponent<TD_SBF_CameraController>().enabled = true;
        cameraCont.GetComponent<TD_SBF_CameraFollow>().GetHero();
    }

    void Update()
    {
        if (IsGameOver)
            return;

        if (TD_SBF_PlayerStatistics.Lives <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.B) ||
            contSupp.ControllerBumperLeft("down"))
        {
            cMan.ToggleHeroBar();
            cMan.DisableHeroBar();
            cMan.ToggleBuildBar();
            DisableHeroMode();
            ToggleTowerMode();
        }

        if (Input.GetKeyDown(KeyCode.H) ||
            contSupp.ControllerBumperRight("down"))
        {
            cMan.ToggleBuildBar();
            cMan.DisableBuildBar();
            cMan.ToggleHeroBar();
            DisableTowerMode();
            ToggleHeroMode();
        }
    }

    public void WinLevel()
    {
        IsLevelWon = true;

        // Reward player
        // TODO: Figure later on different levels / modes
        PlayerPrefs.SetString("TransferActions", "Quest11Reward");

        Debug.Log("level won");
        Debug.Log("current level: " + PlayerPrefs.GetInt("TD_SBF_LevelReached"));
        // Unlock next level
        PlayerPrefs.SetInt("TD_SBF_LevelReached", (PlayerPrefs.GetInt("TD_SBF_LevelReached") + 1));
        Debug.Log("current level: " + PlayerPrefs.GetInt("TD_SBF_LevelReached"));

        if (Screen.width >= Screen.height)
        {
            completeLevelUI_Horizontal.SetActive(true);
            completeLevelUI_Vertical.SetActive(false);
        }
        else
        {
            completeLevelUI_Horizontal.SetActive(false);
            completeLevelUI_Vertical.SetActive(true);
        }
    }

    void EndGame()
    {
        IsGameOver = true;

        if (Screen.width >= Screen.height)
        {
            gameOverUI_Horizontal.SetActive(true);
            gameOverUI_Vertical.SetActive(false);
        }
        else
        {
            gameOverUI_Vertical.SetActive(true);
            gameOverUI_Horizontal.SetActive(false);
        }

        heroBMan.HideGUIControls();
    }

    public void ToggleHeroMode()
    {
        bIsHeroMode = !bIsHeroMode;

        if (bIsHeroMode)
        {
            cameraCont.GetComponent<TD_SBF_CameraFollow>().enabled = true;

            if (devDetect.bIsMobile &&
                !contSupp.bControllerConnected)
            {
                heroBMan.ShowGUIControls();
            }
        }
        else
        {
            cameraCont.GetComponent<TD_SBF_CameraFollow>().enabled = false;

            if (devDetect.bIsMobile &&
                !contSupp.bControllerConnected)
            {
                heroBMan.HideGUIControls();
            }
        }
        
        GameObject prevAreaTBC = GameObject.FindGameObjectWithTag("PrevGridNode");
        if (prevAreaTBC)
            Destroy(prevAreaTBC);
    }

    public void DisableHeroMode()
    {
        if (bIsHeroMode)
        {
            bIsHeroMode = false;
            cameraCont.GetComponent<TD_SBF_CameraFollow>().enabled = false;
        }
    }

    public void ToggleTowerMode()
    {
        bIsTowerMode = !bIsTowerMode;

        if (!bIsTowerMode)
        {
            GameObject prevAreaTBC = GameObject.FindGameObjectWithTag("PrevGridNode");
            if (prevAreaTBC)
                Destroy(prevAreaTBC);

            shopSel.ResetTowerMode();
            nodeUISel.ResetNodeUI();
        }
    }

    public void DisableTowerMode()
    {
        if (bIsTowerMode)
            bIsTowerMode = false;

        shopSel.ResetTowerMode();
    }
}
