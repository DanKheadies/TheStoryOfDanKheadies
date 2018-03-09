// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/07/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controls where dialogues are displayed
public class DialogueManager : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Animator anim;
    public CameraFollow oCamera;
    //public CannabisPlant cannaP;
    //public Chp1 chp1;
    public GameObject dbox;
    public ImageStrobe imgStrobe;
    public PlayerMovement thePlayer;
    private Scene scene;
    private SFXManager SFXMan;
    public Text dText;
    public TouchControls touches;
    public UIManager uiMan;

    public bool bTempControlActive;
    public bool bDialogueActive;
    public bool bPauseDialogue;

    private float pauseTime;
    private float screenHeight;
    private float screenWidth;
    private float rtLeft;
    private float rtRight;
    private float rtTop;
    private float rtBottom;

    public int currentLine;

    public string[] dialogueLines;

    
	void Start ()
    {
        // Initializers
        thePlayer = FindObjectOfType<PlayerMovement>();
        anim = thePlayer.GetComponent<Animator>();
        aspectUtil = GetComponent<AspectUtility>();
        oCamera = FindObjectOfType<CameraFollow>();
        scene = SceneManager.GetActiveScene();
        SFXMan = FindObjectOfType<SFXManager>();
        touches = FindObjectOfType<TouchControls>();
        uiMan = FindObjectOfType<UIManager>();

        // DC TODO -- In Chp1.cs, create public cannaP and have dMan reach out to it and get it, then remove from this file
        // As well as this segement below
        if (scene.name == "Chp1")
        {
            //cannaP = FindObjectOfType<CannabisPlant>();
        }

        // UX -- Prevents immediately reopening a dialogue while moving / talking
        bPauseDialogue = false;
        pauseTime = 0.25f;

        bDialogueActive = false;

        if (scene.name == "Showdown")
        {
            // Debug.Log("Showndow");
            ConfigureShowdownParameters();
        }
        else
        {
            // Debug.Log("Normal");
            ConfigureParameters();
        }
    }
	

	void Update ()
    {
        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (scene.name == "Showdown")
            {
                ConfigureShowdownParameters();
            }
            else
            {
                ConfigureParameters();
            }
        }
        
        // Advance active dialogues
        if ((bDialogueActive && Input.GetButtonDown("Action")) ||
            (bDialogueActive && touches.bAction))
        {
            currentLine++;
        }

        // End of dialogue => reset everything
        if (currentLine >= dialogueLines.Length)
        {
            // Mini-pause on triggering the same dialogue
            pauseDialogue();

            dbox.SetActive(false);
            bDialogueActive = false;

            currentLine = 0;

            // Reset ContinueArrow strobing
            imgStrobe.bCoRunning = false;

            // Avoid console error when no player object is present
            // DC 09/17/2017 -- TODO: Remove this conditional on basis that ShowdownDiaMan is it's own thing
            // i.e. this no longer needs to check
            if (scene.name != "Showdown")
            {
                // Stop the player
                thePlayer.bStopPlayerMovement = false;
                anim.Play("Idle");
                
                // Show controls if visible
                if (bTempControlActive)
                {
                    touches.GetComponent<Canvas>().enabled = true;
                }
            }

            // Hide Items
            if (scene.name == "Chp0")
            {
                // Hide goggles
            }
            else if (scene.name == "Chp1")
            {
                //cannaP.HideBud();
            }
        }

        // Set current text
        dText.text = dialogueLines[currentLine];

        // Check sizing stuff
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("A:" + oCamera.myCam.pixelRect);
        //    Debug.Log("(" + Screen.width + ", " + Screen.height + ")");
        //}

        if (bPauseDialogue)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime < 0)
            {
                unpauseDialogue();
            }
        }
    }

    public void ShowDialogue()
    {
        // Displays the dialogue box
        bDialogueActive = true;
        dbox.SetActive(true);
        dbox.transform.localScale = Vector3.one;

        // Sound Effect
        SFXMan.dialogueMedium.PlayOneShot(SFXMan.dialogueMedium.clip);

        // Stops the player's movement
        thePlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim.SetBool("bIsWalking", false);
        touches.UnpressedAllArrows();
        thePlayer.bStopPlayerMovement = true;

        // Checks (& hides) UI controls
        bTempControlActive = uiMan.bControlsActive;
        if (uiMan.bControlsActive)
        {
            touches.GetComponent<Canvas>().enabled = false;
        }
    }

    public void ConfigureParameters()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        // UI Text Positioning & Sizing based off camera size
        if (screenWidth > oCamera.myCam.pixelWidth)
        {
            // Height => change in height affects variables
            rtLeft = 0.00004293f * (screenHeight * screenHeight) + 0.04109f * screenHeight + 6.043f;
            rtRight = -0.0001284f * (screenHeight * screenHeight) + 0.2052f * screenHeight - 3.257f;
            rtTop = -0.00000348f * (screenHeight * screenHeight) + 0.05072f * screenHeight + 1.834f;
            rtBottom = -0.0001692f * (screenHeight * screenHeight) + 0.7994f * screenHeight - 14.63f;

            dText.fontSize = (int)(-0.000007955f * (screenHeight * screenHeight) + 0.1159f * screenHeight - 2.95f);
        }
        else
        {
            // Width => change in width affects variables
            rtLeft = 0.0623f * screenWidth + 1.537f;
            rtRight = 0.1187f * screenWidth + 1.2611f;
            rtTop = 0.0000272f * (screenWidth * screenWidth) + 0.02149f * screenWidth + 5.613f;
            rtBottom = 0.00007596f * (screenWidth * screenWidth) + 0.5523f * screenWidth + 6.496f;

            dText.fontSize = (int)(0.00001684f * (screenWidth * screenWidth) + 0.08254f * screenWidth + 0.818f);
        }

        dText.rectTransform.anchoredPosition = new Vector2((-(rtRight - rtLeft) / 2), ((rtBottom / 2) - (rtTop / 2)));
        dText.rectTransform.sizeDelta = new Vector2(-(rtLeft + rtRight), -(rtTop + rtBottom));
    }

    public void ConfigureShowdownParameters()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        // UI Text Positioning & Sizing based off camera size
        if (screenWidth > oCamera.myCam.pixelWidth)
        {
            // Height => change in height affects variables
            rtLeft = -0.0000343f * (screenHeight * screenHeight) + 0.0998f * screenHeight - 0.2669f;
            rtRight = -0.0003106f * (screenHeight * screenHeight) + 0.3093f * screenHeight - 16.9628f;
            rtTop = -0.00006188f * (screenHeight * screenHeight) + 0.7605f * screenHeight - 3.5725f;
            rtBottom = 0.00006524f * (screenHeight * screenHeight) + 0.01997f * screenHeight + 3.0532f;

            dText.fontSize = (int)(0.00005445f * (screenHeight * screenHeight) + 0.08284 * screenHeight + 1.151f);
            dText.lineSpacing = 1.25f;
        }
        else
        {
            // Width => change in width affects variables
            rtLeft = -0.00002965f * (screenWidth * screenWidth) + 0.08495f * screenWidth + 0.1969f;
            rtRight = -0.0000709f * (screenWidth * screenWidth) + 0.153f * screenWidth + 2.2435f;
            rtTop = 0.000025783f * (screenWidth * screenWidth) + 0.6017f * screenWidth + 8.6247f;
            rtBottom = -0.00007993f * (screenWidth * screenWidth) + 0.08852f * screenWidth - 4.5059f;

            dText.fontSize = (int)(0.00002448f * (screenWidth * screenWidth) + 0.08702 * screenWidth - 1.3834f);
            dText.lineSpacing = 0.0000003996f * (screenWidth * screenWidth) - 0.0004426f * screenWidth + 1.3225f;
        }

        dText.rectTransform.anchoredPosition = new Vector2((-(rtRight - rtLeft) / 2), ((rtBottom / 2) - (rtTop / 2)));
        dText.rectTransform.sizeDelta = new Vector2(-(rtLeft + rtRight), -(rtTop + rtBottom));
    }

    public void pauseDialogue()
    {
        bPauseDialogue = true;
    }

    public void unpauseDialogue()
    {
        bPauseDialogue = false;
        pauseTime = 0.25f;
    }
}
