// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/25/2018

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
    public GameObject dBox;
    public Image dPic;
    public ImageStrobe imgStrobe;
    public PlayerMovement thePlayer;
    private Scene scene;
    private SFXManager SFXMan;
    public Sprite portPic;
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
    private float ptLeft;
    private float ptRight;
    private float ptTop;
    private float ptBottom;

    public int currentLine;

    public string[] dialogueLines;

    
	void Start ()
    {
        // Initializers
        thePlayer = FindObjectOfType<PlayerMovement>();
        anim = thePlayer.GetComponent<Animator>();
        aspectUtil = GetComponent<AspectUtility>();
        dBox = GameObject.Find("Dialogue_Box");
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        dPic = GameObject.Find("Dialogue_Picture").GetComponent<Image>();
        imgStrobe = GameObject.Find("Dialogue_Arrow").GetComponent<ImageStrobe>();
        oCamera = FindObjectOfType<CameraFollow>();
        scene = SceneManager.GetActiveScene();
        SFXMan = FindObjectOfType<SFXManager>();
        touches = FindObjectOfType<TouchControls>();
        uiMan = FindObjectOfType<UIManager>();

        bDialogueActive = false;
        bPauseDialogue = false; // UX -- Prevents immediately reopening a dialogue while moving / talking
        pauseTime = 0.25f;


        if (scene.name == "Showdown")
        {
            ConfigureShowdownParameters();
        }
        else
        {
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
            (bDialogueActive && Input.GetButtonDown("DialogueAction")) ||
            (bDialogueActive && touches.bAction))
        {
            currentLine++;
        }

        // End of dialogue => reset everything
        if (currentLine >= dialogueLines.Length)
        {
            // Mini-pause on triggering the same dialogue
            pauseDialogue();
            
            dBox.transform.localScale = Vector3.zero;
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
        // Set current picture
        dPic.sprite = portPic;

        // Displays the dialogue box
        bDialogueActive = true;
        dBox.transform.localScale = Vector3.one;

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

        // UI Image & Text Positioning and Sizing based off camera size vs device size
        if (screenWidth > oCamera.myCam.pixelWidth)
        {
            // Height => change in height affects variables
            // OG
            //rtLeft = 0.00004293f * (screenHeight * screenHeight) + 0.04109f * screenHeight + 6.043f;
            //rtRight = -0.0001284f * (screenHeight * screenHeight) + 0.2052f * screenHeight - 3.257f;
            //rtTop = -0.00000348f * (screenHeight * screenHeight) + 0.05072f * screenHeight + 1.834f;
            //rtBottom = -0.0001692f * (screenHeight * screenHeight) + 0.7994f * screenHeight - 14.63f;

            // 2.0
            //rtLeft = -0.1017f * (screenHeight * screenHeight) + 50.16f * screenHeight - 5408f;
            rtLeft = -0.0004627f * (screenHeight * screenHeight) + 0.5400f * screenHeight - 27.33f;
            rtRight = 0.00008921f * (screenHeight * screenHeight) + 0.07075f * screenHeight + 5.198f;
            rtTop = 0.00007871f * (screenHeight * screenHeight) + 0.003600f * screenHeight + 4.351f;
            rtBottom = 0.00006559f * (screenHeight * screenHeight) + 0.6697f * screenHeight + 3.293f;

            ptLeft = 0.00007871f * (screenHeight * screenHeight) + 0.003600f * screenHeight + 7.351f;
            ptRight = 0.00005247f * (screenHeight * screenHeight) + 0.8357f * screenHeight + 3.234f;
            ptTop = 0.000007871f * (screenHeight * screenHeight) + 0.05036f * screenHeight + 1.635f;
            ptBottom = 0.0001749f * (screenHeight * screenHeight) + 0.6191f * screenHeight + 12.11f;

            // 2.1
            rtLeft = 0.00001599f * (screenHeight * screenHeight) + 0.2902f * screenHeight + 3.027f;
            rtRight = 0.00001165f * (screenHeight * screenHeight) + 0.1030f * screenHeight + 1.719f;
            rtTop = 0.000008856f * (screenHeight * screenHeight) + 0.03531f * screenHeight + 0.5884f;
            rtBottom = -0.000003886f * (screenHeight * screenHeight) + 0.7015f * screenHeight + 0.5588f;

            ptLeft = 0.000005286f * (screenHeight * screenHeight) + 0.04938f * screenHeight + 0.2483f;
            ptRight = 0.000004195f * (screenHeight * screenHeight) + 0.8573f * screenHeight + 1.704f;
            ptTop = 0.000005689f * (screenHeight * screenHeight) + 0.05098f * screenHeight + 0.9569f;
            ptBottom = 0.000007448f * (screenHeight * screenHeight) + 0.7089f * screenHeight + 1.419f;

            // OG
            //dText.fontSize = (int)(-0.000007955f * (screenHeight * screenHeight) + 0.1159f * screenHeight - 2.80f);
            // 2.0
            dText.fontSize = (int)(-0.00006122f * (screenHeight * screenHeight) + 0.1083f * screenHeight - 4.940f);
            // 2.1
            dText.fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
        }
        else
        {
            // Width => change in width affects variables
            // OG
            //rtLeft = 0.0623f * screenWidth + 1.537f;
            //rtRight = 0.1187f * screenWidth + 1.2611f;
            //rtTop = 0.0000272f * (screenWidth * screenWidth) + 0.02149f * screenWidth + 5.613f;
            //rtBottom = 0.00007596f * (screenWidth * screenWidth) + 0.5523f * screenWidth + 6.496f;

            // 2.0
            rtLeft = 0.001662f * (screenWidth * screenWidth) - 0.6957f * screenWidth + 126.7f;
            rtRight = -0.00004883f * (screenWidth * screenWidth) + 0.1304f * screenWidth - 4.126f;
            rtTop = -0.00005749f * (screenWidth * screenWidth) + 0.07289f * screenWidth - 5.278f;
            rtBottom = -0.00007599f * (screenWidth * screenWidth) + 0.6546f * screenWidth - 5.374f;

            ptLeft = -0.000006300f * (screenWidth * screenWidth) + 0.04908f * screenWidth - 0.5647f;
            ptRight = -0.00003465f * (screenWidth * screenWidth) + 0.7700f * screenWidth - 1.606f;
            ptTop = 0.00004489f * (screenWidth * screenWidth) + 0.02528f * screenWidth + 3.148f;
            ptBottom = 0.00003544f * (screenWidth * screenWidth) + 0.5989f * screenWidth + 3.801f;

            // 2.1
            rtLeft = -0.000005696f * (screenWidth * screenWidth) + 0.2767f * screenWidth - 3.120f;
            rtRight = -0.00001282f * (screenWidth * screenWidth) + 0.1149f * screenWidth - 2.470f;
            rtTop = 0.03699f * screenWidth - 0.3986f;
            rtBottom = 0.6134f * screenWidth - 0.1730f;

            ptLeft = -0.000005696f * (screenWidth * screenWidth) + 0.05120f * screenWidth - 1.013f;
            ptRight = 0.000009398f * (screenWidth * screenWidth) + 0.7461f * screenWidth + 1.400f;
            ptTop = 0.000004272f * (screenWidth * screenWidth) + 0.04543f * screenWidth + 0.7437f;
            ptBottom = 0.000001794f * (screenWidth * screenWidth) + 0.6249f * screenWidth - 0.04518f;

            // OG
            //dText.fontSize = (int)(0.00001684f * (screenWidth * screenWidth) + 0.08254f * screenWidth + 0.818f);
            // 2.0
            dText.fontSize = (int)(0.000008663f * (screenWidth * screenWidth) + 0.05751f * screenWidth + 1.151f);
            // 2.1
            dText.fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
        }

        // OG
        dText.rectTransform.anchoredPosition = new Vector2((-(rtRight - rtLeft) / 2), ((rtBottom / 2) - (rtTop / 2)));
        dText.rectTransform.sizeDelta = new Vector2(-(rtLeft + rtRight), -(rtTop + rtBottom));
        // 2.0 & 2.1
        dPic.rectTransform.anchoredPosition = new Vector2((-(ptRight - ptLeft) / 2), ((ptBottom / 2) - (ptTop / 2)));
        dPic.rectTransform.sizeDelta = new Vector2(-(ptLeft + ptRight), -(ptTop + ptBottom));
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
