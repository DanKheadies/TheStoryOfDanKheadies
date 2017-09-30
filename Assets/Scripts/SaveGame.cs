// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  09/29/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Save the game and then pull the saved info
public class SaveGame : MonoBehaviour
{
    public Camera savedCamera;
    public CameraFollow camFollow;
    public GameObject savedPlayer;
    public MenuControl menuCont;
    public Scene scene;
    public UIManager uiMan;
    public VolumeManager savedVol;

    public float savedVolume;

    void Start()
    {
        // Initializers
        camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        savedCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        savedPlayer = GameObject.FindGameObjectWithTag("Player");
        menuCont = GetComponent<MenuControl>();
        scene = SceneManager.GetActiveScene();
        savedVol = FindObjectOfType<VolumeManager>();
        uiMan = FindObjectOfType<UIManager>();

        // Avoid console error when no player object is present
        if (scene.name != "Showdown" ||
            scene.name != "MainMenu_Animation" ||
            scene.name != "MainMenu")
        {
            // Load any saved player data
            GetSavedGame();
        }
    }

    // Saves *majority* of user data
    public void SavingGame(bool bQuit)
    {
        PlayerPrefs.SetFloat("Cam_x", savedCamera.transform.position.x);
        PlayerPrefs.SetFloat("Cam_y", savedCamera.transform.position.y);
        PlayerPrefs.SetFloat("P_x", savedPlayer.transform.position.x);
        PlayerPrefs.SetFloat("P_y", savedPlayer.transform.position.y);
        PlayerPrefs.SetInt("AnandaCoord", (int)camFollow.currentCoords);
        PlayerPrefs.SetFloat("Brio", savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio);

        // Check saved values
        Debug.Log("Cam: (" + PlayerPrefs.GetFloat("Cam_x") + "," + PlayerPrefs.GetFloat("Cam_y") + ")");
        Debug.Log("Dan: (" + PlayerPrefs.GetFloat("P_x") + "," + PlayerPrefs.GetFloat("P_y") + ")");
        Debug.Log("Loc: " + PlayerPrefs.GetInt("AnandaCoord"));
        Debug.Log("Loc: " + (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("AnandaCoord"));
        Debug.Log("Bri: " + PlayerPrefs.GetFloat("Brio"));

        // On Quit, resumes time
        if (bQuit)
        {
            Time.timeScale = 1;
            menuCont.LoadScene("MainMenu_Animation");
        }
    }

    // Saves UI Volume data
    public void SavingVolume()
    {
        PlayerPrefs.SetFloat("Volume", savedVol.currentVolumeLevel); // Called in VolumeManager

        // Check saved values
        Debug.Log("Vol: " + PlayerPrefs.GetFloat("Volume"));
    }

    // Saves UI controls' opacity and  data
    public void SavingUIControls()
    {
        PlayerPrefs.SetFloat("ControlsOpac", uiMan.currentContOpac); // Called in UIManager

        if (uiMan.bControlsActive)
        {
            PlayerPrefs.SetInt("ControlsActive", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ControlsActive", 0);
        }

        // Check saved values
        Debug.Log("Opa: " + PlayerPrefs.GetFloat("ControlsOpac"));
        Debug.Log("Act: " + PlayerPrefs.GetInt("ControlsActive"));
    }

    // Loads *all* user data at the start
    public void GetSavedGame()
    {
        // Temp delete all for testing
        //PlayerPrefs.DeleteAll();

        // Use initial values if no saved data
        if (PlayerPrefs.GetInt("AnandaCoord") == 0)
        {
            savedPlayer.transform.position = new Vector2(-13, -8);

            savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio = savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio;
            // DC 08/26/2017 -- Weird bug that gives 5 Brio every time you Save & Quit and then Start

            camFollow.currentCoords = CameraFollow.AnandaCoords.Home;

            savedCamera.transform.position = new Vector2(-13, -8);
            //float posX = Mathf.SmoothDamp(savedCamera.transform.position.x, savedPlayer.transform.position.x, ref camFollow.smoothVelocity.x, camFollow.smoothTime);
            //float posY = Mathf.SmoothDamp(savedCamera.transform.position.y, savedPlayer.transform.position.y, ref camFollow.smoothVelocity.y, camFollow.smoothTime);
            //savedCamera.transform.position = new Vector3(posX, posY, -10);

        }
        else
        {
            savedPlayer.transform.position = new Vector2(
                PlayerPrefs.GetFloat("P_x"),
                PlayerPrefs.GetFloat("P_y"));

            savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio = PlayerPrefs.GetFloat("Brio");
            // DC 08/26/2017 -- Weird bug that gives 5 Brio every time you Save & Quit and then Start

            savedCamera.transform.position = new Vector2(PlayerPrefs.GetFloat("Cam_x"), PlayerPrefs.GetFloat("Cam_y"));
            float posX = Mathf.SmoothDamp(savedCamera.transform.position.x, savedPlayer.transform.position.x, ref camFollow.smoothVelocity.x, camFollow.smoothTime);
            float posY = Mathf.SmoothDamp(savedCamera.transform.position.y, savedPlayer.transform.position.y, ref camFollow.smoothVelocity.y, camFollow.smoothTime);
            savedCamera.transform.position = new Vector3(posX, posY, -10);

            camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("AnandaCoord");
        }
    }
}
