// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  04/26/2021

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
