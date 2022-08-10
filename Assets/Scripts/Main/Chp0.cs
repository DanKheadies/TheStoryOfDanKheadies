// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/07/2018
// Last:  08/09/2022

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Contains all Chapter 0 quests, items, and elements
public class Chp0 : MonoBehaviour
{
    public Camera mainCamera;
    public DialogueManager dMan;
    public GameObject brioBar;
    public GameObject dArrow;
    public GameObject dBox;
    public GameObject homeVRGoggles;
    public GameObject npc_ashera;
    public GameObject npc_dagon;
    public GameObject pauseButton;
    public GameObject player;
    public GameObject sFaderAnim;
    public GameObject sFaderAnimDia;
    public GameObject upStairsStairs;
    public GameObject upStairsStairsWarp;
    public Inventory inv;
    public MusicManager mMan;
    public SaveGame save;
    public SFXManager SFXMan;
    public Text dText;
    public TouchControls touches;
    public UIManager uMan;

    public bool bAvoidUpdate;
    public bool bHasGoggles;
    public bool bStartGame;
    
    public string[] dialogueLines;

    void Start()
    {
        // Chapter 0 New Game -- Wake Up Dialogue, i.e. hide everything and fade in the dialogue
        if (PlayerPrefs.GetString("Chapter") != "Chp0")
        {
            // Hide UI & music
            touches.transform.localScale = Vector3.zero;
            mMan.bMusicCanPlay = false;
            pauseButton.transform.localScale = Vector3.zero;
            brioBar.transform.localScale = Vector3.zero;

            // Set player
            //mainCamera.transform.position = new Vector2(1.525f, 3.33f);
            //player.transform.position = new Vector2(1.525f, 3.33f);
            mainCamera.transform.position = new Vector2(1.32f, 3.3f);
            player.transform.position = new Vector2(1.32f, 3.3f);

            // Set dialogue & dialogue elements
            dialogueLines = new string[] {
                "Dan.",
                "Dan...",
                "Dan!",
                "Wake up Dan or you'll be late for your first day of skewl."
            };
            dMan.dialogueLines = dialogueLines;
            dMan.currentLine = 0;
            dText.text = dialogueLines[dMan.currentLine];
            dBox.transform.localScale = Vector3.one;

            // Stops the player's movement
            player.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            // Allows tapping / clicking of the dialogue
            uMan.bControlsActive = false;

            // Fade in
            sFaderAnimDia.GetComponent<Animator>().enabled = true;

            // Dialogue activation & strobe arrow start
            StartCoroutine(DelayDialogue());
        }
        // Chapter 0 Saved Game
        else
        {
            save.GetSavedGame();

            VisorCheck();

            if (bHasGoggles)
                RevealStairs();
            
            sFaderAnimDia.GetComponent<Animator>().enabled = true;
        }

        npc_ashera.GetComponent<Animator>().SetBool("bIsSitting", true);
        npc_dagon.GetComponent<Animator>().SetBool("bIsSitting", true);
    }

    void Update ()
    {
        // New Game -- Activate music, UI, and fade after dialogue concludes
		if (!bAvoidUpdate && 
            !dMan.bDialogueActive && 
            bStartGame)
        {
            // Start music
            mMan.bMusicCanPlay = true;

            // Set UI
            pauseButton.transform.localScale = Vector3.one;
            brioBar.transform.localScale = Vector3.one;
            uMan.CheckIfMobile();

            // Fade in scene
            sFaderAnim.GetComponent<Animator>().enabled = true;

            bAvoidUpdate = true;
        }
        
        // Saved Game -- Starts music and fades (saved during Chp0)
        if (!bAvoidUpdate &&
            !dMan.bDialogueActive && 
            PlayerPrefs.GetString("Chapter") == "Chp0")
        {
            // Start music
            mMan.bMusicCanPlay = true;

            // Show UI
            pauseButton.transform.localScale = Vector3.one;
            brioBar.transform.localScale = Vector3.one;
            if (uMan.bControlsActive)
                touches.transform.localScale = Vector3.one;

            // Fade in scene
            sFaderAnim.GetComponent<Animator>().enabled = true;

            bAvoidUpdate = true;
        }
    }

    public void RevealStairs()
    {
        upStairsStairs.transform.localScale = Vector3.one;
        upStairsStairsWarp.SetActive(true);
    }

    public void VisorCheck()
    {
        // Item Check -- Check on VR Goggles to avoid farming
        for (int i = 0; i < inv.items.Count; i++)
        {
            string item = inv.items[i].ToString();
            item = item.Substring(0, item.Length - 7);

            if (item == "VR.Goggles")
            {
                bHasGoggles = true;
                homeVRGoggles.transform.localScale = Vector3.zero;
            }
        }
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(3.0f);

        bStartGame = true;
        dArrow.GetComponent<ImageStrobe>().StartCoroutine(dArrow.GetComponent<ImageStrobe>().Strobe());
        dMan.bDialogueActive = true;

        // Sound Effect
        SFXMan.sounds[2].PlayOneShot(SFXMan.sounds[2].clip);
    }
}
