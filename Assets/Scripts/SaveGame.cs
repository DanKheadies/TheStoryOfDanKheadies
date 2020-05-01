// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  05/01/2020

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
    public ScriptManager scriptMan;
    public TouchControls touches;
    public UIManager uMan;
    public VolumeManager savedVol;

    public int invTotal;

    public string savedItem;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
            DeleteAllPrefs();
        
        if (Input.GetKeyUp(KeyCode.C))
            CheckSavedData();
    }

    // Saves *majority* of user data
    public void SavingGame()
    {
        scene = SceneManager.GetActiveScene();

        PlayerPrefs.SetInt("Saved", 1);
        PlayerPrefs.SetString("Chapter", scene.name);
        PlayerPrefs.SetFloat("Cam_x", savedCamera.transform.position.x);
        PlayerPrefs.SetFloat("Cam_y", savedCamera.transform.position.y);
        PlayerPrefs.SetFloat("P_x", savedPlayer.transform.position.x);
        PlayerPrefs.SetFloat("P_y", savedPlayer.transform.position.y);
        PlayerPrefs.SetInt("AnandaCoord", (int)camFollow.currentCoords);
        PlayerPrefs.SetFloat("BrioMax", savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio);
        PlayerPrefs.SetFloat("Brio", savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio);

        // Clear out the inventory prefs...
        PlayerPrefs.SetInt("ItemTotal", 0);
        for (int i = 0; i < inv.totalItems; i++)
        {
            PlayerPrefs.SetString("Item" + i, "");
        }
        // ...before saving again
        for (int i = 0; i < inv.items.Count; i++)
        {
            PlayerPrefs.SetString("Item" + i, inv.items[i].ToString());
            PlayerPrefs.SetInt("ItemTotal", i + 1);
        }

        SavingVolume();
        SavingUIControls();
        scriptMan.SavingSpecificInfo();
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
        Debug.Log("VRG: " + PlayerPrefs.GetInt("HasVRGoggles"));

        Debug.Log("Vol: " + PlayerPrefs.GetFloat("Volume"));
        Debug.Log("Con: " + PlayerPrefs.GetInt("ControlsActive"));
        Debug.Log("COp: " + PlayerPrefs.GetFloat("ControlsOpac"));
        Debug.Log("CDP: " + PlayerPrefs.GetString("ControlsDPad"));
        Debug.Log("CVi: " + PlayerPrefs.GetString("ControlsVibrate"));

        Debug.Log("C1Q: " + PlayerPrefs.GetString("Chp1Quests"));

        if (PlayerPrefs.GetInt("TransferItemTotal") > 0 ||
            PlayerPrefs.GetInt("ItemTotal") > 0)
        {
            if (PlayerPrefs.GetInt("Transferring") == 1)
            {
                Debug.Log("TInv: " + PlayerPrefs.GetInt("TransferItemTotal"));

                for (int i = 0; i < PlayerPrefs.GetInt("TransferItemTotal"); i++)
                {
                    string savedItem = PlayerPrefs.GetString("TransferItem" + i);
                    savedItem = savedItem.Substring(0, savedItem.Length - 7);

                    Item tempItem = tempItem = ScriptableObject.CreateInstance<Item>();
                    tempItem = (Item)Resources.Load("Items/" + savedItem);
                    Debug.Log("TInv " + (i + 1) + ": " + PlayerPrefs.GetString("TransferItem" + i, inv.items[i].ToString()));
                }
            }
            else
            {
                Debug.Log("Inv: " + PlayerPrefs.GetInt("ItemTotal"));

                for (int i = 0; i < PlayerPrefs.GetInt("ItemTotal"); i++)
                {
                    string savedItem = PlayerPrefs.GetString("Item" + i);
                    savedItem = savedItem.Substring(0, savedItem.Length - 7);

                    Item tempItem = tempItem = ScriptableObject.CreateInstance<Item>();
                    tempItem = (Item)Resources.Load("Items/" + savedItem);
                    Debug.Log("Inv " + (i + 1) + ": " + PlayerPrefs.GetString("Item" + i, inv.items[i].ToString()));
                }
            }
        }
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
        PlayerPrefs.SetInt("ControlsDPad", uMan.currentContDPad); 
        PlayerPrefs.SetFloat("ControlsOpac", uMan.currentContOpac); 
        PlayerPrefs.SetInt("ControlsVibrate", touches.currentContVibe); // Also called in TouchControls

        if (uMan.bControlsActive)
            PlayerPrefs.SetInt("ControlsActive", 1);
        else
            PlayerPrefs.SetInt("ControlsActive", 0);
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
        savedCamera.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Cam_x"), 
            PlayerPrefs.GetFloat("Cam_y"), 
            -10);

        // Location
        camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("AnandaCoord");

        // Inventory
        inv.LoadInventory("saved");

        // UI
        // see UIManager.Start()

        // Vibrate
        // see TouchControls.Start()
    }

    // Loads *all* user data at the start
    public void GetTransferData()
    {
        // Player
        savedPlayer.transform.position = new Vector2(
            PlayerPrefs.GetFloat("TransferP_x"),
            PlayerPrefs.GetFloat("TransferP_y") - 0.05f);

        savedPlayer.GetComponent<PlayerBrioManager>().playerMaxBrio = PlayerPrefs.GetFloat("TransferBrioMax");
        savedPlayer.GetComponent<PlayerBrioManager>().playerCurrentBrio = PlayerPrefs.GetFloat("TransferBrio");

        // Camera
        savedCamera.transform.position = new Vector3(
            PlayerPrefs.GetFloat("TransferCam_x"), 
            PlayerPrefs.GetFloat("TransferCam_y"), 
            -10);

        // Location
        camFollow.currentCoords = (CameraFollow.AnandaCoords)PlayerPrefs.GetInt("TransferAnandaCoord");

        // Inventory
        inv.LoadInventory("transfer");
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
        PlayerPrefs.DeleteKey("TransferActions");

        invTotal = PlayerPrefs.GetInt("TransferItemTotal");
        PlayerPrefs.DeleteKey("TransferItemTotal");

        for (int i = 0; i < invTotal; i++)
        {
            PlayerPrefs.DeleteKey("TransferItem" + i);
        }
    }

    // Delete all values
    public void DeleteAllPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
