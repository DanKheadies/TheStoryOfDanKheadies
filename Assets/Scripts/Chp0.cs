// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/07/2018
// Last:  08/11/2018

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
    public GameObject guiConts;
    public GameObject HUD;
    public GameObject pauseButton;
    public GameObject sFaderAnim;
    public GameObject sFaderAnimDia;
    public GameObject thePlayer;
    public Inventory inv;
    public MusicManager mMan;
    public NPCMovement[] npcMove;
    public SaveGame save;
    public SceneTransitioner sceneTrans;
    private SFXManager SFXMan;
    public Text dText;
    public TouchControls touches;

    private bool bAvoidUpdate;
    public bool bGetInventory;
    private bool bStartGame;

    public float invTimer;
    public float strobeTimer;
    
    public string[] dialogueLines;

    void Start()
    {
        // Initializers
        contArrow = GameObject.Find("Dialogue_Arrow");
        dBox = GameObject.Find("Dialogue_Box");
        dMan = FindObjectOfType<DialogueManager>();
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        guiConts = GameObject.Find("GUIControls");
        HUD = GameObject.Find("HUD");
        inv = FindObjectOfType<Inventory>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mMan = FindObjectOfType<MusicManager>();
        npcMove = FindObjectsOfType<NPCMovement>();
        pauseButton = GameObject.Find("PauseButton");
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<PolygonCollider2D>();
        save = FindObjectOfType<SaveGame>();
        sceneTrans = FindObjectOfType<SceneTransitioner>();
        sFaderAnim = GameObject.Find("Screen_Fader");
        sFaderAnimDia = GameObject.Find("Screen_Fader_Dialogue");
        SFXMan = FindObjectOfType<SFXManager>();
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        touches = FindObjectOfType<TouchControls>();
        warpCollider = GameObject.Find("Chp0.to.Chp1").GetComponent<BoxCollider2D>();
        
        strobeTimer = 3.0f;

        // Chapter 0 New Game -- Wake Up Dialogue, i.e. hide everything and fade in the dialogue
        if (PlayerPrefs.GetString("Chapter") != "Chp0")
        {
            // Hide UI & music
            guiConts.transform.localScale = Vector3.zero;
            mMan.bMusicCanPlay = false;
            pauseButton.transform.localScale = Vector3.zero;

            // Set player
            mainCamera.transform.position = new Vector2(1.45f, 3.33f);
            thePlayer.transform.position = new Vector2(1.45f, 3.33f);
            
            // Set dialogue & dialogue elements
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

            // Stops the player's movement
            thePlayer.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            // Fade in
            sFaderAnimDia.GetComponent<Animator>().enabled = true;
        }
        // Chapter 0 Saved Game
        else
        {
            // Initialize
            inv.RerunStart();
            save.RerunStart();

            // Get saved info
            save.GetSavedGame();
            bGetInventory = true;

            // Cue timer to get saved inventory info
            invTimer = 0.333f;

            // Fade in
            sFaderAnimDia.GetComponent<Animator>().enabled = true;
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
            }
        }

        // New Game -- Activate music, UI, and fade after dialogue concludes
		if (!dMan.bDialogueActive && 
            !bAvoidUpdate && 
            bStartGame)
        {
            // Display UI & music
            guiConts.transform.localScale = Vector3.one;
            mMan.bMusicCanPlay = true;
            pauseButton.transform.localScale = Vector3.one;
            
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
            invTimer > 0)
        {
            invTimer -= Time.deltaTime;

            if (invTimer <= 0)
            {
                inv.LoadInventory("saved");
                bGetInventory = false;
            }
        }

        // Save transfer values and transition to next scene
        if (playerCollider.IsTouching(warpCollider))
        {
            // Transition animation
            sceneTrans.bAnimationToTransitionScene = true;

            // Save info
            save.SaveBrioTransfer();
            save.SaveInventoryTransfer();
            PlayerPrefs.SetInt("Transferring", 1);
            PlayerPrefs.SetString("TransferScene", sceneTrans.BetaLoad);

            // Stop the player from bringing up the dialogue again
            dMan.gameObject.SetActive(false);

            // Stop Dan from moving
            thePlayer.GetComponent<Animator>().enabled = false;

            // Stop NPCs from moving
            for (int i = 0; i < npcMove.Length; i++)
            {
                npcMove[i].moveSpeed = 0;
                npcMove[i].GetComponent<Animator>().enabled = false;
            }
        }
    }
}
