// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 10/08/2016
// Last:  07/21/2021

using UnityEngine;

public class GameManagement : MonoBehaviour
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
        
        cameraCont.GetComponent<CameraController>().enabled = true;
    }

    void Update()
    {
        if (IsGameOver)
            return;

        if (PlayerStatistics.Lives <= 0)
        {
            EndGame();
        }
    }

    public void WinLevel()
    {
        IsLevelWon = true;

        // Reward player
        // TODO: Figure later on different levels / modes
        PlayerPrefs.SetString("TransferActions", "Quest10Reward");

        Debug.Log("level won");
        Debug.Log("current level: " + PlayerPrefs.GetInt("levelReached"));
        // Unlock next level
        PlayerPrefs.SetInt("levelReached", (PlayerPrefs.GetInt("levelReached") + 1));
        Debug.Log("current level: " + PlayerPrefs.GetInt("levelReached"));

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
    }
}
