// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TD_SBF_GameOver : MonoBehaviour
{
    public TD_SBF_SceneFader sceneFader;
    public Text roundsText;
    public string menuSceneName = "TD_Menu";

    void OnEnable()
    {
        roundsText.text = TD_SBF_PlayerStatistics.Rounds.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void End()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
