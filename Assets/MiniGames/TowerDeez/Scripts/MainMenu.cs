// CC 4.0 International License: Attribution--Brackeys & DTFun--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 11/16/2016
// Last:  04/26/2021

using UnityEngine;

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

        sceneFader.FadeTo("SceneTransitioner");
        PlayerPrefs.SetString("TransferScene", "Chp1");
    }
}
