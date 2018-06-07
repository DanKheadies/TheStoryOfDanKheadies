// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/07/2018
// Last:  06/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Contains all Chapter 0 quests, items, and elements
public class Chp0 : MonoBehaviour
{
    public Camera mainCamera;
    public PolygonCollider2D playerCollider;
    public BoxCollider2D warpCollider;
    public DialogueManager dMan;
    public GameObject contArrow;
    public GameObject dBox;
    public GameObject HUD;
    public GameObject sFaderAnim;
    public GameObject sFaderAnimDia;
    public GameObject thePlayer;
    public Inventory inv;
    public MusicManager mMan;
    public SaveGame save;
    public SceneTransitioner sceneTrans;
    private SFXManager SFXMan;
    public Text dText;
    public TouchControls touches;

    private bool bAvoidUpdate;
    public bool bGetInventory;
    private bool bStartGame;

    public float strobeTimer;
    public float timer;

    public string dialogue;
    public string[] dialogueLines;

    void Start()
    {
        // Initializers
        contArrow = GameObject.Find("Dialogue_Arrow");
        dBox = GameObject.Find("Dialogue_Box");
        dMan = FindObjectOfType<DialogueManager>();
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        HUD = GameObject.Find("HUD");
        inv = FindObjectOfType<Inventory>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mMan = FindObjectOfType<MusicManager>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>();
        save = FindObjectOfType<SaveGame>();
        sceneTrans = FindObjectOfType<SceneTransitioner>();
        sFaderAnim = GameObject.Find("Screen_Fader");
        sFaderAnimDia = GameObject.Find("Screen_Fader_Dialogue");
        SFXMan = FindObjectOfType<SFXManager>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        touches = FindObjectOfType<TouchControls>();
        warpCollider = GameObject.Find("Chp0.to.Chp1").GetComponent<BoxCollider2D>();

        bAvoidUpdate = false;
        bGetInventory = false;
        bStartGame = false;
        strobeTimer = 3.0f;

        // Chapter 0 New Game -- Wake Up Dialogue, i.e. hide everything and fade in the dialogue
        if (PlayerPrefs.GetString("Chapter") != "Chp0")
        {
            dMan.bDialogueActive = false;
            HUD.transform.GetChild(3).localScale = Vector3.zero; // DC TODO
            HUD.transform.GetChild(4).localScale = Vector3.zero; // DC TODO
            mMan.bMusicCanPlay = false;
            thePlayer.transform.position = new Vector2(1.45f, 3.33f);
            mainCamera.transform.position = new Vector2(1.45f, 3.33f);
            
            dialogueLines = new string[] {
                "Dan.",
                "Dan...",
                "Dan!",
                "Wake up Dan or you'll be late for your first day of school."
            };

            dMan.dialogueLines = dialogueLines;
            dMan.currentLine = 0;
            dText.text = dialogueLines[dMan.currentLine];
            dBox.transform.localScale = Vector3.one;
            sFaderAnimDia.GetComponent<Animator>().enabled = true;
        }
        // Chapter 0 Saved Game
        else
        {
            inv.RerunStart();
            save.RerunStart();
            save.GetSavedGame();
            sFaderAnimDia.GetComponent<Animator>().enabled = true;
            bGetInventory = true;
            timer = 0.33f;
        }
    }
	
	void Update ()
    {
        // New Game -- Dialogue activation & strobe arrow start
        if (strobeTimer > 0 && 
            PlayerPrefs.GetInt("Saved") == 0)
        {
            strobeTimer -= Time.deltaTime;
            if (strobeTimer <= 0)
            {
                bStartGame = true;
                contArrow.GetComponent<ImageStrobe>().bStartStrobe = true;
                dMan.bDialogueActive = true;

                // Sound Effect
                SFXMan.dialogueMedium.PlayOneShot(SFXMan.dialogueMedium.clip);

                // Stops the player's movement
                dMan.thePlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                dMan.anim.SetBool("bIsWalking", false);
                touches.UnpressedAllArrows();
                dMan.thePlayer.bStopPlayerMovement = true;
            }
        }

        // New Game -- Activate music, UI, and fade after dialogue concludes
		if (!dMan.bDialogueActive && 
            !bAvoidUpdate && 
            bStartGame)
        {
            HUD.transform.GetChild(3).localScale = Vector3.one; // DC TODO
            HUD.transform.GetChild(4).localScale = Vector3.one; // DC TODO
            mMan.bMusicCanPlay = true;
            sFaderAnim.GetComponent<Animator>().enabled = true;

            // Change to avoid running this logic
            bAvoidUpdate = true;
        }
        
        // Saved Game -- Starts music and fades (saved during Chp0)
        if (!dMan.bDialogueActive && 
            PlayerPrefs.GetString("Chapter") == "Chp0" && 
            !bAvoidUpdate)
        {
            mMan.bMusicCanPlay = true;
            sFaderAnim.GetComponent<Animator>().enabled = true;

            // Change to avoid running this logic
            bAvoidUpdate = true;
        }

        // Saved Game -- Load inventory
        if (bGetInventory && 
            timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                inv.LoadInventory("saved");
                bGetInventory = false;
            }
        }

        // Save transfer values
        if (playerCollider.IsTouching(warpCollider))
        {
            sceneTrans.bAnimationToTransitionScene = true;

            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", sceneTrans.BetaLoad);

            // Stop the player from bringing up the dialog again
            dMan.gameObject.SetActive(false);

            // 06/07/2018 DC -- Stop NPCs from moving
        }
    }
}
