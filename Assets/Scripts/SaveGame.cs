// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  05/09/2019

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
    public Scene scene;
    public TouchControls touches;
    public UIManager uiMan;
    public VolumeManager savedVol;
    
    private string savedItem;

    void Start()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        if (scene.name == "MainMenu")
        {
            savedVol = FindObjectOfType<VolumeManager>();
        }
        else if (scene.name == "Showdown")
        {
            savedPlayer = GameObject.FindGameObjectWithTag("Player");
            inv = FindObjectOfType<Inventory>().GetComponent<Inventory>();
            savedVol = FindObjectOfType<VolumeManager>();
            tempItem = ScriptableObject.CreateInstance<Item>();
            uiMan = FindObjectOfType<UIManager>();
        }
        else
        {
            camFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
            savedCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            savedPlayer = GameObject.FindGameObjectWithTag("Player");
            inv = FindObjectOfType<Inventory>().GetComponent<Inventory>();
            savedVol = FindObjectOfType<VolumeManager>();
            tempItem = ScriptableObject.CreateInstance<Item>();
            touches = FindObjectOfType<TouchControls>();
            uiMan = FindObjectOfType<UIManager>();
        }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            DeleteAllPrefs();
        }


        if (Input.GetKeyUp(KeyCode.C))
        {
            CheckSavedData();
        }
    } 

    public void RerunStart()
    {
        Start();
    }

    // Saves *majority* of user data
    public void SavingGame()
    {
        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.SetString("Chapter", scene.name);
        PlayerPrefs.SetFloat("Cam_x", savedCamera.transform.position.x);
        PlayerPrefs.SetFloat("Cam_y", savedCamera.transform.position.y);
        PlayerPrefs.SetFloat("P_x", savedPlayer.transform.position.x);
        PlayerPrefs.SetFloat("P_y", savedPlayer.transform.position.y);
        PlayerPrefs.SetInt("AnandaCoord", (int)camFollow.currentCoords);
        PlayerPrefs.SetFloat("BrioMax", savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio);
        PlayerPrefs.SetFloat("Brio", savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio);
        
        for (int i = 0; i < inv.items.Count; i++)
        {
            PlayerPrefs.SetString("Item" + i, inv.items[i].ToString());
            PlayerPrefs.SetInt("ItemTotal", i+1);
        }

        SavingVolume();
        SavingUIControls();
    }

    // Test to check saved values
    public void CheckSavedData()
    {
        Debug.Log("Sav: " + PlayerPrefs.GetInt("Saved"));
        Debug.Log("Sce: " + PlayerPrefs.GetString("Chapter"));
        Debug.Log("Cam: (" + PlayerPrefs.GetFloat("Cam_x") + "," + PlayerPrefs.GetFloat("Cam_y") + ")");
        Debug.Log("Dan: (" + PlayerPrefs.GetFloat("P_x") + "," + PlayerPrefs.GetFloat("P_y") + ")");
        Debug.Log("Loc: " + PlayerPrefs.GetInt("AnandaCoord"));
        Debug.Log("Loc: " + ((CameraFollow.AnandaCoords)PlayerPrefs.GetInt("AnandaCoord")).ToString());
        Debug.Log("MxB: " + PlayerPrefs.GetFloat("BrioMax"));
        Debug.Log("Bri: " + PlayerPrefs.GetFloat("Brio"));

        if (PlayerPrefs.GetInt("Transferring") == 1)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("TransferItemTotal"); i++)
            {
                string savedItem = PlayerPrefs.GetString("TransferItem" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);
                Item tempItem = (Item)Resources.Load("Items/" + savedItem);
                Debug.Log("TInv " + i + ": " + PlayerPrefs.GetString("TransferItem" + i, inv.items[i].ToString()));
            }
        }
        else
        {
            for (int i = 0; i < PlayerPrefs.GetInt("ItemTotal"); i++)
            {
                string savedItem = PlayerPrefs.GetString("Item" + i);
                savedItem = savedItem.Substring(0, savedItem.Length - 7);
                Item tempItem = (Item)Resources.Load("Items/" + savedItem);
                Debug.Log("Inv " + i + ": " + PlayerPrefs.GetString("Item" + i, inv.items[i].ToString()));
            }
        }

        Debug.Log("Vol: " + PlayerPrefs.GetFloat("Volume"));
        Debug.Log("Con: " + PlayerPrefs.GetInt("ControlsActive"));
        Debug.Log("COp: " + PlayerPrefs.GetFloat("ControlsOpac"));
        Debug.Log("CDP: " + PlayerPrefs.GetString("ControlsDPad"));
        Debug.Log("CVi: " + PlayerPrefs.GetString("ControlsVibrate"));

        Debug.Log("C1Q: " + PlayerPrefs.GetString("Chp1Quests"));

        Debug.Log("Tran: " + PlayerPrefs.GetInt("Transferring"));
        Debug.Log("TSce: " + PlayerPrefs.GetInt("TransferScene"));
        Debug.Log("TCam: (" + PlayerPrefs.GetFloat("TransferCam_x") + "," + PlayerPrefs.GetFloat("TransferCam_y") + ")");
        Debug.Log("TDan: (" + PlayerPrefs.GetFloat("TransferP_x") + "," + PlayerPrefs.GetFloat("TransferP_y") + ")");
        Debug.Log("TLoc: " + PlayerPrefs.GetInt("TransferAnandaCoord"));
        Debug.Log("TLoc: " + ((CameraFollow.AnandaCoords)PlayerPrefs.GetInt("TransferAnandaCoord")).ToString());
        Debug.Log("TMxB: " + PlayerPrefs.GetFloat("TransferBrioMax"));
        Debug.Log("TBri: " + PlayerPrefs.GetFloat("TransferBrio"));
    }

    // Saves UI Volume data
    public void SavingVolume()
    {
        PlayerPrefs.SetFloat("Volume", savedVol.currentVolumeLevel); // Also called in VolumeManager
    }

    // Saves UI controls' opacity and data
    public void SavingUIControls()
    {
        PlayerPrefs.SetInt("ControlsDPad", uiMan.currentContDPad); // Also called in UIManager
        PlayerPrefs.SetFloat("ControlsOpac", uiMan.currentContOpac); // Also called in UIManager
        PlayerPrefs.SetInt("ControlsVibrate", touches.currentContVibe); // Also called in TouchControls

        if (uiMan.bControlsActive)
        {
            PlayerPrefs.SetInt("ControlsActive", 1);
        }
        else
        {
            PlayerPrefs.SetInt("ControlsActive", 0);
        }
    }

    // Temp save inventory for switching scenes
    public void SaveInventoryTransfer()
    {
        for (int i = 0; i < inv.items.Count; i++)
        {
            PlayerPrefs.SetString("TransferItem" + i, inv.items[i].ToString());
            PlayerPrefs.SetInt("TransferItemTotal", i + 1);
        }
    }

    // Temp save position when leaving and returning to main world
    public void SavePositionTransfer()
    {
        PlayerPrefs.SetFloat("TransferCam_x", savedCamera.transform.position.x);
        PlayerPrefs.SetFloat("TransferCam_y", savedCamera.transform.position.y);
        PlayerPrefs.SetFloat("TransferP_x", savedPlayer.transform.position.x);
        PlayerPrefs.SetFloat("TransferP_y", savedPlayer.transform.position.y);
        PlayerPrefs.SetInt("TransferAnandaCoord", (int)camFollow.currentCoords);
    }

    // Temp save brio when leaving and returning to main world
    public void SaveBrioTransfer()
    {
        PlayerPrefs.SetFloat("TransferBrioMax", savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio);
        PlayerPrefs.SetFloat("TransferBrio", savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio);
    }

    // Temp save UI controls' opacity and data
    public void SaveUITransfer()
    {
        SavingUIControls();
    }

    // Loads *all* user data at the start
    public void GetSavedGame()
    {
        // Player
        savedPlayer.transform.position = new Vector2(
            PlayerPrefs.GetFloat("P_x"),
            PlayerPrefs.GetFloat("P_y"));

        savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio = PlayerPrefs.GetFloat("BrioMax");
        savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio = PlayerPrefs.GetFloat("Brio");

        // Camera
        savedCamera.transform.position = new Vector2(PlayerPrefs.GetFloat("Cam_x"), PlayerPrefs.GetFloat("Cam_y"));
        float posX = Mathf.SmoothDamp(savedCamera.transform.position.x, savedPlayer.transform.position.x, ref camFollow.smoothVelocity.x, camFollow.smoothTime);
        float posY = Mathf.SmoothDamp(savedCamera.transform.position.y, savedPlayer.transform.position.y, ref camFollow.smoothVelocity.y, camFollow.smoothTime);
        savedCamera.transform.position = new Vector3(posX, posY, -10);

        camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("AnandaCoord");

        // See UIManager for UI getters

        // See TouchControls for touches getters

        // See Inventory for inventory getters
    }

    // Loads *all* user data at the start
    public void GetTransferData()
    {
        savedPlayer.transform.position = new Vector2(
            PlayerPrefs.GetFloat("TransferP_x"),
            PlayerPrefs.GetFloat("TransferP_y"));

        savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio = PlayerPrefs.GetFloat("TransferBrioMax");
        savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio = PlayerPrefs.GetFloat("TransferBrio");

        savedCamera.transform.position = new Vector2(PlayerPrefs.GetFloat("TransferCam_x"), PlayerPrefs.GetFloat("TransferCam_y"));
        float posX = Mathf.SmoothDamp(savedCamera.transform.position.x, savedPlayer.transform.position.x, ref camFollow.smoothVelocity.x, camFollow.smoothTime);
        float posY = Mathf.SmoothDamp(savedCamera.transform.position.y, savedPlayer.transform.position.y, ref camFollow.smoothVelocity.y, camFollow.smoothTime);
        savedCamera.transform.position = new Vector3(posX, posY, -10);

        camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("TransferAnandaCoord");

        // See Chapter start for inventory getters
    }

    // Testing -- Delete all transfer
    public void DeleteTransPrefs()
    {
        PlayerPrefs.DeleteKey("Transferring");
        PlayerPrefs.DeleteKey("TransferScene");
        PlayerPrefs.DeleteKey("TransferCam_x");
        PlayerPrefs.DeleteKey("TransferCam_y");
        PlayerPrefs.DeleteKey("TransferP_x");
        PlayerPrefs.DeleteKey("TransferP_y");
        PlayerPrefs.DeleteKey("TransferAnandaCoord");
        PlayerPrefs.DeleteKey("TransferBrioMax");
        PlayerPrefs.DeleteKey("TransferBrio");
    }

    // Delete all values
    public void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
