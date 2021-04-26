﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  02/09/2020

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public SaveGame save;

    public void EndApp()
    {
        save.DeleteTransPrefs();
        
        Application.Quit();

        Debug.Log("i ended");
    }

    public void GoToMainMenu()
    {
        save.DeleteTransPrefs();

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}