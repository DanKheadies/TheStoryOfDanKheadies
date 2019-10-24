// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  10/21/2019

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TD_SBF_GameOver : MonoBehaviour
{
    public TD_SBF_SceneFader sceneFader;
    public Text roundsText;

    public int rounds;
    public string menuSceneName = "TD_SBF_ModeSelector";

    void OnEnable()
    {
        rounds = TD_SBF_PlayerStatistics.Rounds - 1;
        roundsText.text = rounds.ToString();
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
