// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/12/2018

using UnityEngine;
using UnityEngine.SceneManagement;

// For Buttons only: transition scene or quiting game
public class MenuControl : MonoBehaviour
{
    public GameObject endB;
    public GameObject goOnB;
    public GameObject newB;
    public GameObject newT;
    public GameObject noB;
    public GameObject startB;
    public GameObject yesB;

    public string savedScene;

    void Start()
    {
        // Initializers
        endB = GameObject.Find("End_Button");
        goOnB = GameObject.Find("Go_On_Button");
        newB = GameObject.Find("New_Button");
        newT = GameObject.Find("New_Text");
        noB = GameObject.Find("No_Button");
        startB = GameObject.Find("Start_Button");
        yesB = GameObject.Find("Yes_Button");

        if (PlayerPrefs.GetInt("Saved") > 0)
        {
            startB.transform.localScale = Vector3.zero;
            goOnB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            newB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
    }

    public void GoToScene()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.GetString("Chapter") != "")
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("Chapter"));
        }
        else
        {
            SceneManager.LoadScene("Chp0");
        }
    }

    public void NewGame()
    {
        startB.transform.localScale = Vector3.zero;
        goOnB.transform.localScale = Vector3.zero;
        newB.transform.localScale = Vector3.zero;
        endB.transform.localScale = Vector3.zero;
        newT.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        yesB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        noB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    public void NewGameNo()
    {
        newT.transform.localScale = Vector3.zero;
        yesB.transform.localScale = Vector3.zero;
        noB.transform.localScale = Vector3.zero;

        if (PlayerPrefs.GetInt("Saved") > 0)
        {
            goOnB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            newB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else
        {
            startB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        endB.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }

    public void NewGameYes()
    {
        PlayerPrefs.DeleteAll();
        GoToScene();
    }
}
