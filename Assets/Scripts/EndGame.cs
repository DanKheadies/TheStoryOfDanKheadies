// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 11/08/2017
// Last:  08/17/2019

using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public SaveGame save;

    void Start()
    {
        save = FindObjectOfType<SaveGame>();
    }

    public void EndApp()
    {
        save.DeleteTransPrefs();
        
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        save.DeleteTransPrefs();

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
