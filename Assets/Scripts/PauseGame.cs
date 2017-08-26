// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/26/2017
// Last:  08/26/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Pause the game & bring up the menu
public class PauseGame : MonoBehaviour
{
    public Scene scene;
    public Transform controlsMenu;
    public Transform pauseMenu;
    public Transform pauseTrans;
    public Transform soundMenu;


    void Start ()
    {
        pauseTrans = GameObject.FindGameObjectWithTag("Pause").GetComponent<Transform>();
        pauseMenu = GameObject.Find("PauseMenu").transform;
        soundMenu = GameObject.Find("SoundMenu").transform;
        controlsMenu = GameObject.Find("ControlsMenu").transform;

        pauseTrans.gameObject.SetActive(false);
        pauseTrans.transform.localScale = new Vector3(1, 1, 1);
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
        if (pauseTrans.gameObject.activeInHierarchy == false)
        {
            if (pauseMenu.gameObject.activeInHierarchy == false)
            {
                pauseMenu.gameObject.SetActive(true);
                soundMenu.gameObject.SetActive(false);
                controlsMenu.gameObject.SetActive(false);
            }
            pauseTrans.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseTrans.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Sound(bool bOpen)
    {
        if (bOpen)
        {
            soundMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            soundMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public void Controls(bool bOpen)
    {
        if (bOpen)
        {
            controlsMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            controlsMenu.gameObject.SetActive(false);
            pauseMenu.gameObject.SetActive(true);
        }
    }
}
