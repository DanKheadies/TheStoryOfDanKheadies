// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/26/2017
// Last:  08/26/2018

using UnityEngine;
using UnityEngine.SceneManagement;

// Pause the game & bring up the menu
public class PauseGame : MonoBehaviour
{
    public MovePauseMenuArrow movePArw;
    public OptionsManager oMan;
    public PlayerMovement pMove;
    public Scene scene;
    public TouchControls touches;
    public Transform gwcMenu;
    public Transform controlsMenu;
    public Transform pauseMenu;
    public Transform pauseTrans;
    public Transform soundMenu;
    public Transform stuffMenu;

    public bool bPauseActive;

    void Start ()
    {
        // Initializers
        movePArw = GameObject.Find("PauseMenu").GetComponent<MovePauseMenuArrow>();
        oMan = FindObjectOfType<OptionsManager>();
        pMove = FindObjectOfType<PlayerMovement>();
        pauseTrans = GameObject.FindGameObjectWithTag("Pause").GetComponent<Transform>();
        pauseMenu = GameObject.Find("PauseMenu").transform;
        stuffMenu = GameObject.Find("StuffMenu").transform;
        soundMenu = GameObject.Find("SoundMenu").transform;
        controlsMenu = GameObject.Find("ControlsMenu").transform;
        scene = SceneManager.GetActiveScene();
        touches = FindObjectOfType<TouchControls>();

        if (scene.name == "GuessWhoColluded")
        {
            gwcMenu = GameObject.Find("GWCMenu").transform;
        }
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

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }

            pauseTrans.transform.localScale = Vector3.one;
            Time.timeScale = 0;

            bPauseActive = true;
            pMove.bStopPlayerMovement = true;
        }
        else
        {
            oMan.bPauseOptions = true;
            pauseTrans.transform.localScale = Vector3.zero;
            Time.timeScale = 1;

            movePArw.ResetArrows();
            
            bPauseActive = false;
            pMove.bStopPlayerMovement = false;
        }
    }

    public void Stuff(bool bOpen)
    {
        if (bOpen)
        {
            stuffMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            stuffMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }
        }
    }

    public void Sound(bool bOpen)
    {
        if (bOpen)
        {
            soundMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            soundMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }
        }
    }

    public void Controls(bool bOpen)
    {
        if (bOpen)
        {
            controlsMenu.transform.localScale = Vector3.one;
            pauseMenu.transform.localScale = Vector3.zero;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.zero;
            }
        }
        else
        {
            controlsMenu.transform.localScale = Vector3.zero;
            pauseMenu.transform.localScale = Vector3.one;

            if (scene.name == "GuessWhoColluded")
            {
                gwcMenu.transform.localScale = Vector3.one;
            }
        }
    }
}
