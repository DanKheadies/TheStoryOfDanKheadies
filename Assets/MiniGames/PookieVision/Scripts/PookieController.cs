// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/23/2019
// Last:  07/13/2021

using UnityEngine;

public class PookieController : MonoBehaviour
{
    public GameObject pookieSplorer;
    public GameObject warpPookieVision;
    public Inventory inv;
    public SaveGame save;
    public UIManager uMan;

    void Start()
    {
        // Get transfer items (if any)
        inv.LoadInventory("transfer");

        // Force dPad
        uMan.DisplayDPad();
    }

    public void GG()
    {
        //warpPookieVision.GetComponent<BoxCollider2D>().enabled = true;
        warpPookieVision.GetComponent<SceneTransitioner>().bAnimationToTransitionScene = true;

        // Save Transfer Values
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", warpPookieVision.GetComponent<SceneTransitioner>().BetaLoad);

        // Stop Dan from moving
        pookieSplorer.GetComponent<PookieSplorer>().enabled = false;
    }
}
