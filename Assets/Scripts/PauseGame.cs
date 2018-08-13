// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/26/2017
// Last:  08/12/2018

using UnityEngine;
using UnityEngine.SceneManagement;

// Pause the game & bring up the menu
public class PauseGame : MonoBehaviour
{
    public OptionsManager oMan;
    public PlayerMovement pMove;
    public Scene scene;
    public Transform controlsMenu;
    public Transform pauseMenu;
    public Transform pauseTrans;
    public Transform soundMenu;
    public Transform stuffMenu;


    void Start ()
    {
        // Initializers
        oMan = FindObjectOfType<OptionsManager>();
        pMove = FindObjectOfType<PlayerMovement>();
        pauseTrans = GameObject.FindGameObjectWithTag("Pause").GetComponent<Transform>();
        pauseMenu = GameObject.Find("PauseMenu").transform;
        stuffMenu = GameObject.Find("StuffMenu").transform;
        soundMenu = GameObject.Find("SoundMenu").transform;
        controlsMenu = GameObject.Find("ControlsMenu").transform;
    } 

	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pause();
        }
	}

    public void Pause()
    {
        if (pauseTrans.localScale != Vector3.one)
        {
            pauseMenu.transform.localScale = Vector3.one;
            stuffMenu.transform.localScale = Vector3.zero;
            soundMenu.transform.localScale = Vector3.zero;
            controlsMenu.transform.localScale = Vector3.zero;

            pauseTrans.transform.localScale = Vector3.one;
            Time.timeScale = 0;

            pMove.bStopPlayerMovement = true;
        }
        else
        {
            oMan.bPauseOptions = true;
            pauseTrans.transform.localScale = Vector3.zero;
            Time.timeScale = 1;

            pMove.bStopPlayerMovement = false;
        }
    }

    public void Stuff(bool bOpen)
    {
        if (bOpen)
        {
            stuffMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            stuffMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;
        }
    }

    public void Sound(bool bOpen)
    {
        if (bOpen)
        {
            soundMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            soundMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;
        }
    }

    public void Controls(bool bOpen)
    {
        if (bOpen)
        {
            controlsMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;
        }
        else
        {
            controlsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;
        }
    }
}
