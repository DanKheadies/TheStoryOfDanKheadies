// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/13/2019

using UnityEngine;

public class TD_SBF_GameManagement : MonoBehaviour
{
    public GameObject cameraCont;
    public GameObject completeLevelUI_Horizontal;
    public GameObject completeLevelUI_Vertical;
    public GameObject gameOverUI_Horizontal;
    public GameObject gameOverUI_Vertical;
    public GameObject HUD_Horizontal;
    public GameObject HUD_Vertical;

    public static bool IsGameOver;
    public static bool IsLevelWon;

    void Awake()
    {
        // TODO: Enable if device flips

        if (Screen.width >= Screen.height)
        {
            HUD_Horizontal.SetActive(true);
            HUD_Vertical.SetActive(false);
        }
        else
        {
            HUD_Horizontal.SetActive(false);
            HUD_Vertical.SetActive(true);
        }
    }

    void Start()
    {
        IsGameOver = false;
        IsLevelWon = false;

        cameraCont.GetComponent<TD_SBF_CameraController>().enabled = true;
    }

    void Update()
    {
        if (IsGameOver)
            return;

        if (TD_SBF_PlayerStatistics.Lives <= 0)
        {
            EndGame();
        }
    }

    public void WinLevel()
    {
        IsLevelWon = true;

        Debug.Log("level won");
        Debug.Log("current level: " + PlayerPrefs.GetInt("TD_SBF_LevelReached"));
        // Unlock next level
        PlayerPrefs.SetInt("TD_SBF_LevelReached", (PlayerPrefs.GetInt("TD_SBF_LevelReached") + 1));
        Debug.Log("current level: " + PlayerPrefs.GetInt("TD_SBF_LevelReached"));

        // TODO: Enable if device flips

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

        // TODO: Enable if device flips

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
    }
}
