// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/14/2018

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
    public Inventory inv;
    public Item tempItem;
    public MenuControl menuCont;
    public Scene scene;
    public UIManager uiMan;
    public VolumeManager savedVol;
    
    private string savedItem;

    void Start()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        if (scene.name == "MainMenu" ||
            scene.name == "MainMenu_Animation")
        {
            //savedVol = FindObjectOfType<VolumeManager>();
            //Debug.Log("Vol: " + PlayerPrefs.GetFloat("Volume"));
        }
        else if (scene.name == "Showdown")
        {
            savedPlayer = GameObject.FindGameObjectWithTag("Player");
            inv = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();
            menuCont = GetComponent<MenuControl>();
            savedVol = FindObjectOfType<VolumeManager>();
            tempItem = ScriptableObject.CreateInstance<Item>();
            uiMan = FindObjectOfType<UIManager>();
        }
        else
        {
            camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
            savedCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            savedPlayer = GameObject.FindGameObjectWithTag("Player");
            inv = GameObject.FindObjectOfType<Inventory>().GetComponent<Inventory>();
            menuCont = GetComponent<MenuControl>();
            savedVol = FindObjectOfType<VolumeManager>();
            tempItem = ScriptableObject.CreateInstance<Item>();
            uiMan = FindObjectOfType<UIManager>();
        }


        // Avoid console error when no player object is present
        if (scene.name == "MainMenu" ||
            scene.name == "MainMenu_Animation" ||
            scene.name == "Showdown")
        {
            // Here b/c I have to do the logic this way?
            // Debug.Log("No saved data");
        }
        else
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
        
        for (int i = 0; i < inv.items.Count; i++)
        {
            PlayerPrefs.SetString("Item" + i, inv.items[i].ToString());
            PlayerPrefs.SetInt("Item Total", i+1);
        }

        // Check saved values
        //Debug.Log("Cam: (" + PlayerPrefs.GetFloat("Cam_x") + "," + PlayerPrefs.GetFloat("Cam_y") + ")");
        //Debug.Log("Dan: (" + PlayerPrefs.GetFloat("P_x") + "," + PlayerPrefs.GetFloat("P_y") + ")");
        //Debug.Log("Loc: " + PlayerPrefs.GetInt("AnandaCoord"));
        //Debug.Log("Loc: " + (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("AnandaCoord"));
        //Debug.Log("Bri: " + PlayerPrefs.GetFloat("Brio"));
    }

    // Saves UI Volume data
    public void SavingVolume()
    {
        PlayerPrefs.SetFloat("Volume", savedVol.currentVolumeLevel); // Called in VolumeManager

        // Check saved values
        // Debug.Log("Vol: " + PlayerPrefs.GetFloat("Volume"));
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
        // PlayerPrefs.DeleteAll();

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

            for (int i = 0; i < PlayerPrefs.GetInt("Item Total"); i++)
            {
                savedItem = PlayerPrefs.GetString("Item" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);

                tempItem = (Item)Resources.Load("Items/" + savedItem);
                Inventory.instance.Add(tempItem);
            }
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
            
            for (int i = 0; i < PlayerPrefs.GetInt("Item Total"); i++)
            {
                savedItem = PlayerPrefs.GetString("Item" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);

                tempItem = (Item)Resources.Load("Items/" + savedItem);
                Inventory.instance.Add(tempItem);
            }
        }
    }
}
