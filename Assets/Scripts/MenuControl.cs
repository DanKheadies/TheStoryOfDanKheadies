// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/26/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// For Buttons only: transition scene or quiting game
public class MenuControl : MonoBehaviour
{
    void Start()
    {
        
    } 

    public void EndGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
