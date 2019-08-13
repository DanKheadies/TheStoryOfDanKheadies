// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 10/08/2016
// Last:  08/12/2019

using UnityEngine;

public class GameManagement : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject HUD_Horizontal;
    public GameObject HUD_Vertical;

    public static bool GameIsOver = false;
    
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
        GameIsOver = false;
    }

    void Update()
    {
        if (GameIsOver)
            return;

        if (PlayerStatistics.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
