// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour {

	public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
