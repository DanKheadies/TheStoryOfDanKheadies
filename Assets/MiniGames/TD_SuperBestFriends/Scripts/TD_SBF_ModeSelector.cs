// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/27/2019
// Last:  11/20/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_ModeSelector : MonoBehaviour
{
    public Button[] characterButtons;
    public Button[] modeButtons;
    public GameObject characterCanvas;
    public GameObject modeCanvas;
    public GameObject optionsCanvas;
    public TD_SBF_SceneFader fader;

    void Start()
    {
        modeButtons[0].interactable = false;
        modeButtons[2].interactable = false;

        Color temp = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 125.0f / 255.0f);

        characterButtons[1].interactable = false;
        characterButtons[2].interactable = false;
        characterButtons[3].interactable = false;
        characterButtons[4].interactable = false;
        characterButtons[5].interactable = false;
        characterButtons[6].interactable = false;
        characterButtons[7].interactable = false;

        characterButtons[1].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[2].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[3].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[4].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[5].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[6].transform.GetChild(0).GetComponent<Image>().color = temp;
        characterButtons[7].transform.GetChild(0).GetComponent<Image>().color = temp;


        //int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        //for (int i = 0; i < levelButtons.Length; i++)
        //{
        //    if (i + 1 > levelReached)
        //        levelButtons[i].interactable = false;
        //}
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) ||
             Input.GetKeyDown(KeyCode.P) ||
             Input.GetButtonDown("Controller Start Button")) &&
             !characterCanvas.activeSelf)
        {
            ToggleOptions();
        }
    }

    public void ToggleMode()
    {
        if (modeCanvas.activeSelf)
        {
            modeCanvas.SetActive(false);
            characterCanvas.SetActive(true);
        }
        else
        {
            modeCanvas.SetActive(true);
            characterCanvas.SetActive(false);
        }
    }

    public void ToggleOptions()
    {
        if (modeCanvas.activeSelf)
        {
            modeCanvas.SetActive(false);
            optionsCanvas.SetActive(true);
        }
        else
        {
            modeCanvas.SetActive(true);
            optionsCanvas.SetActive(false);
        }
    }

    public void SelectStory()
    {
        Debug.Log("Selected Story");
    }

    public void SelectArcade()
    {
        Debug.Log("Selected Arcade");
    }

    public void SelectMultiplayer()
    {
        Debug.Log("Selected Multiplayer");
    }

    public void LoadLevel(string levelName)
    {
        fader.FadeTo(levelName);
    }

    public void EndGame()
    {
        Debug.Log("End Game");
        Application.Quit();
    }
}
