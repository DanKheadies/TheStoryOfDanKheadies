// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 11/16/2016
// Last:  08/14/2019

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public SceneFader sceneFader;
    public string levelToLoad = "TD_LevelSelector";

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TransitionToChp1()
    {
        // Save Transfer Values
        PlayerPrefs.SetInt("Transferring", 1);

        sceneFader.FadeTo("Chp1");
    }
}
