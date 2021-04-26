// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.SceneManagement;

public class TD_SBF_CompleteLevel : MonoBehaviour
{
    public TD_SBF_SceneFader sceneFader;

    public string menuSceneName = "TD_SBF_Menu";

    public void Continue()
    {
        // TODO: Display summary before moving on (time, built, spend, killed, earned, etc.)

        // Get current level
        string nextLevel = SceneManager.GetActiveScene().name;
        nextLevel = nextLevel.Substring(nextLevel.Length - 1);
        int levelNum = int.Parse(nextLevel);
        levelNum += 1;
        nextLevel = levelNum.ToString();

        if (Application.CanStreamedLevelBeLoaded("TD_SBF_L" + nextLevel))
        {
            // Go to next level
            sceneFader.FadeTo("TD_SBF_L" + nextLevel);
        }
        else
        {
            // Go to menu
            sceneFader.FadeTo(menuSceneName);
        }
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
