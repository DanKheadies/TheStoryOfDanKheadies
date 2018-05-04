// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/28/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls where dialogues are displayed
public class DialogueManager : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Animator anim;
    public CameraFollow mainCamera;
    public GameObject dBox;
    public Image dArrow;
    public Image dFrame;
    public Image dPic;
    public ImageStrobe imgStrobe;
    public OptionsManager oMan;
    public PlayerMovement thePlayer;
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

    //private float rtLeft;
    //private float rtRight;
    //private float rtTop;
    //private float rtBottom;
    //private float ptLeft;
    //private float ptRight;
    //private float ptTop;
    //private float ptBottom;

    // Arrays follow CSS rules for orientation
    // [0] = top
    // [1] = right
    // [2] = bottom
    // [3] = left
    private float[] dArrowPoints;
    private float[] dFramePoints;
    private float[] dPicPoints;
    private float[] dTextPoints;

    public int currentLine;

    public string[] dialogueLines;


    void Start()
    {
        // Initializers
        thePlayer = FindObjectOfType<PlayerMovement>();
        anim = thePlayer.GetComponent<Animator>();
        aspectUtil = GameObject.Find("Main Camera").GetComponent<AspectUtility>();
        dArrow = GameObject.Find("Dialogue_Arrow").GetComponent<Image>();
        dBox = GameObject.Find("Dialogue_Box");
        dFrame = GameObject.Find("Dialogue_Frame").GetComponent<Image>();
        dText = GameObject.Find("Dialogue_Text").GetComponent<Text>();
        dPic = GameObject.Find("Dialogue_Picture").GetComponent<Image>();
        imgStrobe = GameObject.Find("Dialogue_Arrow").GetComponent<ImageStrobe>();
        mainCamera = FindObjectOfType<CameraFollow>();
        oMan = FindObjectOfType<OptionsManager>();
        SFXMan = FindObjectOfType<SFXManager>();
        touches = FindObjectOfType<TouchControls>();
        uiMan = FindObjectOfType<UIManager>();

        bDialogueActive = false;
        bPauseDialogue = false; // UX -- Prevents immediately reopening a dialogue while moving / talking
        pauseTime = 0.333f;

        dArrowPoints = new float[4];
        dFramePoints = new float[4];
        dPicPoints = new float[4];
        dTextPoints = new float[4];

        ConfigureParameters();
    }


    void Update()
    {
        if (bPauseDialogue)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime < 0)
            {
                UnpauseDialogue();
            }
        }

        // Advance active dialogues
        if ((bDialogueActive && !bPauseDialogue && Input.GetButtonDown("Action")) ||
            (bDialogueActive && !bPauseDialogue && Input.GetButtonDown("DialogueAction")) ||
            (bDialogueActive && !bPauseDialogue && touches.bAction))
        {
            if (currentLine < dialogueLines.Length)
            {
                currentLine++;
            }
        }

        // Set current text whenever Options are hidden
        if (bDialogueActive && !oMan.bOptionsActive && currentLine <= dialogueLines.Length - 1)
        {
            dText.text = dialogueLines[currentLine];
        }

        // Show Options if present and w/ the last dialogue prompt; otherwise, reset the dialogue
        if (bDialogueActive && oMan.bDiaToOpts && !oMan.bOptionsActive && currentLine >= dialogueLines.Length - 1)
        {
            oMan.ShowOptions();
        }
        else if (!oMan.bOptionsActive && currentLine >= dialogueLines.Length)
        {
            ResetDialogue();
        }

        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R))
        {
            ConfigureParameters();
        }

        //Check sizing stuff
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("A:" + mainCamera.myCam.pixelRect);
            Debug.Log("(" + Screen.width + ", " + Screen.height + ")");
        }
    }

    public void ResetDialogue()
    {
        // Mini-pause on triggering the same dialogue
        PauseDialogue();

        dBox.transform.localScale = Vector3.zero;
        bDialogueActive = false;

        currentLine = 0;

        // Reset ContinueArrow strobing
        imgStrobe.bStopStrobe = true;

        // Reactivate the player
        thePlayer.bStopPlayerMovement = false;
        anim.Play("Idle");

        // Show controls if visible
        if (bTempControlActive)
        {
            touches.GetComponent<Canvas>().enabled = true;
        }
    }

    public void ShowDialogue()
    {
        // Set the text
        dText.text = dialogueLines[currentLine];

        // Set current picture
        dPic.sprite = portPic;

        // Displays the dialogue box
        bDialogueActive = true;
        dBox.transform.localScale = Vector3.one;
        imgStrobe.bStartStrobe = true;

        // Sound Effect
        SFXMan.dialogueMedium.PlayOneShot(SFXMan.dialogueMedium.clip);

        // Stops the player's movement
        thePlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        anim.SetBool("bIsWalking", false);
        touches.UnpressedAllArrows();
        thePlayer.bStopPlayerMovement = true;

        // Checks (& hides) UI controls
        //bTempControlActive = uiMan.bControlsActive;
        //if (uiMan.bControlsActive)
        //{
        //    touches.GetComponent<Canvas>().enabled = false;
        //}
    }

    public void ConfigureParameters()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        // UI Image & Text Positioning and Sizing based off camera size vs device size
        if (screenWidth > mainCamera.myCam.pixelWidth)
        {
            // Height => change in height affects variables
            //rtLeft = 0.00001599f * (screenHeight * screenHeight) + 0.2902f * screenHeight + 3.027f;
            //rtRight = 0.00001165f * (screenHeight * screenHeight) + 0.1030f * screenHeight + 1.719f;
            //rtTop = 0.000008856f * (screenHeight * screenHeight) + 0.03531f * screenHeight + 0.5884f;
            //rtBottom = -0.000003886f * (screenHeight * screenHeight) + 0.7015f * screenHeight + 0.5588f;

            //ptLeft = 0.000005286f * (screenHeight * screenHeight) + 0.04938f * screenHeight + 0.2483f;
            //ptRight = 0.000004195f * (screenHeight * screenHeight) + 0.8573f * screenHeight + 1.704f;
            //ptTop = 0.000005689f * (screenHeight * screenHeight) + 0.05098f * screenHeight + 0.9569f;
            //ptBottom = 0.000007448f * (screenHeight * screenHeight) + 0.7089f * screenHeight + 1.419f;


            dArrowPoints[0] = 0f;
            dArrowPoints[1] = 0.001657f * (screenHeight * screenHeight) - 2.604f * screenHeight + 1230f;
            dArrowPoints[2] = 0f;
            dArrowPoints[3] = 0.001657f * (screenHeight * screenHeight) - 2.604f * screenHeight + 1230f;

            dFramePoints[0] = 0f;
            dFramePoints[1] = 0.001657f * (screenHeight * screenHeight) - 2.604f * screenHeight + 1230f;
            dFramePoints[2] = 0f;
            dFramePoints[3] = 0.001657f * (screenHeight * screenHeight) - 2.604f * screenHeight + 1230f;

            dPicPoints[0] = 0.000005689f * (screenHeight * screenHeight) + 0.05098f * screenHeight + 0.9569f;
            dPicPoints[1] = 0.000004195f * (screenHeight * screenHeight) + 0.8573f * screenHeight + 1.704f;
            dPicPoints[2] = 0.000007448f * (screenHeight * screenHeight) + 0.7089f * screenHeight + 1.419f;
            dPicPoints[3] = 0.000005286f * (screenHeight * screenHeight) + 0.04938f * screenHeight + 0.2483f;

            dTextPoints[0] = 0.000008856f * (screenHeight * screenHeight) + 0.03531f * screenHeight + 0.5884f;
            dTextPoints[1] = 0.00001165f * (screenHeight * screenHeight) + 0.1030f * screenHeight + 1.719f;
            dTextPoints[2] = -0.000003886f * (screenHeight * screenHeight) + 0.7015f * screenHeight + 0.5588f;
            dTextPoints[3] = 0.00001599f * (screenHeight * screenHeight) + 0.2902f * screenHeight + 3.027f;

            dText.fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            dText.fontSize = (int)(0.00003463f * (screenHeight * screenHeight) - 0.05137f * screenHeight + 55.89f);
        }
        else
        {
            // Width => change in width affects variables 
            //rtLeft = -0.000005696f * (screenWidth * screenWidth) + 0.2767f * screenWidth - 3.120f;
            //rtRight = -0.00001282f * (screenWidth * screenWidth) + 0.1149f * screenWidth - 2.470f;
            //rtTop = 0.03699f * screenWidth - 0.3986f;
            //rtBottom = 0.6134f * screenWidth - 0.1730f;

            //ptLeft = -0.000005696f * (screenWidth * screenWidth) + 0.05120f * screenWidth - 1.013f;
            //ptRight = 0.000009398f * (screenWidth * screenWidth) + 0.7461f * screenWidth + 1.400f;
            //ptTop = 0.000004272f * (screenWidth * screenWidth) + 0.04543f * screenWidth + 0.7437f;
            //ptBottom = 0.000001794f * (screenWidth * screenWidth) + 0.6249f * screenWidth - 0.04518f;


            dArrowPoints[0] = 0f;
            dArrowPoints[1] = -0.0005348f * (screenWidth * screenWidth) + 0.8013f * screenWidth - 123.2f;
            dArrowPoints[2] = 0f;
            dArrowPoints[3] = -0.0005348f * (screenWidth * screenWidth) + 0.8013f * screenWidth - 123.2f;

            dFramePoints[0] = 0f;
            dFramePoints[1] = -0.0005348f * (screenWidth * screenWidth) + 0.8013f * screenWidth - 123.2f;
            dFramePoints[2] = 0f;
            dFramePoints[3] = -0.0005348f * (screenWidth * screenWidth) + 0.8013f * screenWidth - 123.2f;

            dPicPoints[0] = -0.00001248f * (screenWidth * screenWidth) + 0.03960f * screenWidth + 7.529f;
            dPicPoints[1] = -0.00007569f * (screenWidth * screenWidth) + 0.4277f * screenWidth + 172.6f;
            dPicPoints[2] = 0.0005866f * (screenWidth * screenWidth) - 1.617f * screenWidth + 1542f;
            dPicPoints[3] = 0.000002924f * (screenWidth * screenWidth) + 0.01351f * screenWidth + 14.90f;

            dTextPoints[0] = 0.00001195f * (screenWidth * screenWidth) - 0.01724f * screenWidth + 28.34f;
            dTextPoints[1] = -0.0005597f * (screenWidth * screenWidth) + 0.8003f * screenWidth - 50.14f;
            dTextPoints[2] = 0.0003136f * (screenWidth * screenWidth) - 0.4462f * screenWidth + 519.1f;
            dTextPoints[3] = -0.0004518f * (screenWidth * screenWidth) + 0.6457f * screenWidth + 103.3f;

            dText.fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            dText.fontSize = (int)(0.00003463f * (screenWidth * screenWidth) - 0.05137f * screenWidth + 55.89f);
        }

        dArrow.rectTransform.anchoredPosition = new Vector2((-(dArrowPoints[1] - dArrowPoints[3]) / 2), ((dArrowPoints[2] / 2) - (dArrowPoints[0] / 2)));
        dArrow.rectTransform.sizeDelta = new Vector2(-(dArrowPoints[3] + dArrowPoints[1]), -(dArrowPoints[0] + dArrowPoints[2]));

        dFrame.rectTransform.anchoredPosition = new Vector2((-(dFramePoints[1] - dFramePoints[3]) / 2), ((dFramePoints[2] / 2) - (dFramePoints[0] / 2)));
        dFrame.rectTransform.sizeDelta = new Vector2(-(dFramePoints[3] + dFramePoints[1]), -(dFramePoints[0] + dFramePoints[2]));

        dPic.rectTransform.anchoredPosition = new Vector2((-(dPicPoints[1] - dPicPoints[3]) / 2), ((dPicPoints[2] / 2) - (dPicPoints[0] / 2)));
        dPic.rectTransform.sizeDelta = new Vector2(-(dPicPoints[3] + dPicPoints[1]), -(dPicPoints[0] + dPicPoints[2]));

        dText.rectTransform.anchoredPosition = new Vector2((-(dTextPoints[1] - dTextPoints[3]) / 2), ((dTextPoints[2] / 2) - (dTextPoints[0] / 2)));
        dText.rectTransform.sizeDelta = new Vector2(-(dTextPoints[3] + dTextPoints[1]), -(dTextPoints[0] + dTextPoints[2]));
    }

    public void PauseDialogue()
    {
        bPauseDialogue = true;
    }

    public void UnpauseDialogue()
    {
        bPauseDialogue = false;
        pauseTime = 0.333f;
    }
}
