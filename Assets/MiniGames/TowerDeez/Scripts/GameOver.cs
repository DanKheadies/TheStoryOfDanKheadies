// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 10/25/2016
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public Text roundsText;
    public string menuSceneName = "TD_Menu";

    void OnEnable()
    {
        roundsText.text = PlayerStatistics.Rounds.ToString();
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
