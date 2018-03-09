// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/07/2018
// Last:  03/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains all Chapter 0 quests, items, and elements
public class Chp0 : MonoBehaviour
{
    public DialogueManager dMan;
    public GameObject contArrow;
    public GameObject HUD;
    public GameObject sFaderAnim;
    public MusicManager mMan;
    private SFXManager SFXMan;
    public TouchControls touches;

    private bool bAvoidUpdate;
    private bool bStartGame;

    public float strobeTimer;

    public string dialogue;
    public string[] dialogueLines;

    void Start()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        HUD = GameObject.Find("HUD");
        mMan = FindObjectOfType<MusicManager>();
        sFaderAnim = GameObject.FindGameObjectWithTag("Fader");
        SFXMan = FindObjectOfType<SFXManager>();
        touches = FindObjectOfType<TouchControls>();

        bAvoidUpdate = false;
        bStartGame = false;
        strobeTimer = 3.0f;

        // Chapter 0 Start -- Wake Up Dialogue, i.e. hide everything and fade in the dialogue
        dMan.bDialogueActive = false;
        mMan.bMusicCanPlay = false;
        HUD.GetComponent<Canvas>().enabled = false;

        dialogueLines = new string[] {
            "Dan.",
            "Dan...",
            "Dan!",
            "Wake up Dan or you'll be late for your first",
            "day of school."
        };

        dMan.dialogueLines = dialogueLines;
        dMan.currentLine = 0;
        dMan.dbox.transform.localScale = Vector3.one;
    }
	
	void Update ()
    {
        if (strobeTimer > 0 )
        {
            strobeTimer -= Time.deltaTime;

            if (strobeTimer <= 0)
            {
                bStartGame = true;
                contArrow = GameObject.Find("ContinueArrow");
                contArrow.GetComponent<ImageStrobe>().bHide = false;
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

		if (!dMan.bDialogueActive && !bAvoidUpdate && bStartGame)
        {
            HUD.GetComponent<Canvas>().enabled = true;
            mMan.bMusicCanPlay = true;
            sFaderAnim.GetComponent<Animator>().enabled = true;

            // Change to avoid running this logic
            bAvoidUpdate = true;
        }
	}
}
