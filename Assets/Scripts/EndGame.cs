// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  11/08/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public MenuControl menuCont;

    void Start ()
    {
        // Initializers
        menuCont = GetComponent<MenuControl>();
    }

    public void QuitGame(bool bQuit)
    {
        if (bQuit)
        {
            Time.timeScale = 1;
            menuCont.LoadScene("MainMenu_Animation");
        }
    }
}
