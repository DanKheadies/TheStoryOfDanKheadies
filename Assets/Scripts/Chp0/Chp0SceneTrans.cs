// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/19/2019
// Last:  04/26/2021

using UnityEngine;

public class Chp0SceneTrans : MonoBehaviour
{
    public DialogueManager dMan;
    public GameObject player;
    public NPCMovement[] npcMove;
    public SaveGame save;
    public SceneTransitioner sceneTrans;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Transition animation
        sceneTrans.bAnimationToTransitionScene = true;

        // Save info
        save.SaveBrioTransfer();
        save.SaveInventoryTransfer();
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", sceneTrans.BetaLoad);

        // Stop the player from bringing up the dialogue again
        dMan.gameObject.transform.localScale = Vector3.zero;

        // Stop Dan from moving
        player.GetComponent<Animator>().enabled = false;

        // Stop NPCs from moving
        for (int i = 0; i < npcMove.Length; i++)
        {
            npcMove[i].moveSpeed = 0;
            npcMove[i].GetComponent<Animator>().enabled = false;
        }
    } 
}
