// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/27/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Controls where dialogues are displayed
public class DialogueManager : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Animator playerAnim;
    public CameraFollow mainCamera;
    public ControllerSupport contSupp;
    public FixedJoystick fixedJoy;
    public GameObject dArrow;
    public GameObject dBox;
    public Image dFrame;
    public Image dPic;
    public OptionsManager oMan;
    public PauseGame pause;
    public PlayerMovement playerMove;
    public ScriptManager scriptMan;
    public SFXManager SFXMan;
    public Sprite portPic;
    public Text dText;
    public TouchControls touches;
    public UIManager uMan;

    public bool bDialogueActive;
    public bool bStartStrobing;
    public bool bPauseDialogue;
    public bool bTempControlActive;
    public bool bTypewriting;

    private float cameraHeight;
    private float cameraWidth;
    public float defaultDelayForTypewriting;
    public float delayForTypewriting;
    private float pauseTime;

    // Arrays follow CSS rules for orientation
    // [0] = x
    // [1] = y
    // [2] = width
    // [3] = height
    private float[] dArrowPoints;
    private float[] dFramePoints;
    private float[] dPicPoints;
    private float[] dTextPoints;

    public int currentLine;

    public string closingAction;
    public string currentDialogue;
    public string[] dialogueLines;

    void Start()
    {
        // TODO: Still needed?
        bDialogueActive = false;
        bPauseDialogue = false; // UX -- Prevents immediately reopening a dialogue while moving / talking
        defaultDelayForTypewriting = 0.0666f;
        delayForTypewriting = defaultDelayForTypewriting;
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
                UnpauseDialogue();
        }

        // Advance active dialogues
        if (bDialogueActive &&
            !bPauseDialogue &&
            !pause.bPausing &&
            !pause.bPauseActive &&
            (touches.bAaction || 
             Input.GetButtonDown("Action") ||
             Input.GetButtonDown("BAction") ||
             contSupp.ControllerButtonPadBottom("down") ||
             contSupp.ControllerButtonPadRight("down") ||
             (Input.GetButtonDown("DialogueAction") && 
              !uMan.bControlsActive)))
        {
            touches.Vibrate();

            if (!bTypewriting)
            {
                if (currentLine < dialogueLines.Length)
                {
                    currentLine++;

                    // 05/10/2019 DC -- Avoids the GWC double tap bug
                    if (!oMan.bOptionsActive)
                        touches.bAaction = false;
                }

                if (bDialogueActive &&
                    !oMan.bOptionsActive &&
                    currentLine <= dialogueLines.Length - 1)
                {
                    StartCoroutine(TypewriteText());
                }
            }
            else
                delayForTypewriting = 0;
        }

        // Show Options if present and w/ the last dialogue prompt; otherwise, reset the dialogue
        if (bDialogueActive &&
            oMan.bDiaToOpts &&
            !bTypewriting &&
            !oMan.bOptionsActive &&
            currentLine >= dialogueLines.Length - 1)
        {
            oMan.ShowOptions();
        }
        else if (!oMan.bOptionsActive &&
                 currentLine >= dialogueLines.Length)
        {
            ResetDialogue();
        }

        // Temp: Update Camera display / aspect ratio & virtual joystick
        if (Input.GetKeyUp(KeyCode.R) ||
            contSupp.ControllerMenuLeft("up"))
        {
            ConfigureParameters();
            fixedJoy.JoystickPosition();
            uMan.SetMenus();
        }

        //Check sizing stuff
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("Cam rect:" + mainCamera.GetComponent<Camera>().rect);
        //    Debug.Log("Cam width:" + mainCamera.GetComponent<Camera>().rect.width);
        //    Debug.Log("Cam height:" + mainCamera.GetComponent<Camera>().rect.height);
        //}
    }

    public void ResetDialogue()
    {
        // Mini-pause on triggering the same dialogue
        PauseDialogue();

        dBox.transform.localScale = Vector3.zero;
        bDialogueActive = false;

        currentLine = 0;

        // Stop Arrow Strobing
        StartCoroutine(dArrow.gameObject.GetComponent<ImageStrobe>().StopStrobe());

        // Reactivate the player
        playerMove.bStopPlayerMovement = false;
        if (playerAnim)
            playerAnim.Play("Idle");

        // Restore Brio & Button if no more dialogue
        StartCoroutine(WaitForOptions());

        // Action on close
        if (closingAction != "")
            scriptMan.ClosingAction(closingAction);

        closingAction = "";
    }

    public void ShowDialogue()
    {
        // Sound Effect
        SFXMan.sounds[2].PlayOneShot(SFXMan.sounds[2].clip);

        // Set the text
        //dText.text = dialogueLines[currentLine];
        StartCoroutine(TypewriteText());

        // Set current picture
        dPic.sprite = portPic;

        // Displays the dialogue box & strobing arrow
        bDialogueActive = true;
        dBox.transform.localScale = Vector3.one;
        StartCoroutine(ResetStrobes());

        // Stops the player's movement
        playerMove.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        if (playerAnim)
            playerAnim.SetBool("bIsWalking", false);
        
        touches.UnpressedAllArrows();
        playerMove.bStopPlayerMovement = true;

        // Hide BrioBar & Pause Button (Opac)
        uMan.HideBrioAndButton();
    }

    public IEnumerator TypewriteText()
    {
        for (int i = 0; i < dialogueLines[currentLine].Length + 1; i++)
        {
            bTypewriting = true;
            currentDialogue = dialogueLines[currentLine].Substring(0, i);
            dText.text = currentDialogue;

            yield return new WaitForSeconds(delayForTypewriting);
        }

        PauseDialogue();
        bTypewriting = false;
        delayForTypewriting = defaultDelayForTypewriting;
    }

    public IEnumerator WaitForOptions()
    {
        yield return new WaitForSeconds(0.0125f);

        if (!bDialogueActive &&
            !oMan.bOptionsActive)
        {
            uMan.ShowBrioAndButton();
        }
    }

    public IEnumerator ResetStrobes()
    {
        StartCoroutine(dArrow.gameObject.GetComponent<ImageStrobe>().StopStrobe());

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(dArrow.gameObject.GetComponent<ImageStrobe>().Strobe());
    }

    public void ConfigureParameters()
    {
        cameraHeight = mainCamera.GetComponent<Camera>().rect.height;
        cameraWidth = mainCamera.GetComponent<Camera>().rect.width;

        // UI Image & Text Positioning and Sizing based off camera size vs device size
        if (Screen.width > mainCamera.GetComponent<Camera>().pixelWidth)
        {
            // Height => change in height affects variables, so look at the width of the camera
            dArrowPoints[0] = 0f;
            dArrowPoints[1] = 0f;
            dArrowPoints[2] = (-204.2f * (cameraWidth * cameraWidth) + 682.6f * cameraWidth + 166.0f) * 1.142857f;
            dArrowPoints[3] = -204.2f * (cameraWidth * cameraWidth) + 682.6f * cameraWidth + 166.0f;
            
            dFramePoints[0] = 0f;
            dFramePoints[1] = 0f;
            dFramePoints[2] = (-204.2f * (cameraWidth * cameraWidth) + 682.6f * cameraWidth + 166.0f) * 1.142857f;
            dFramePoints[3] = -204.2f * (cameraWidth * cameraWidth) + 682.6f * cameraWidth + 166.0f;

            dPicPoints[0] = 94.21f * (cameraWidth * cameraWidth) - 290.1f * cameraWidth - 64.71f;
            dPicPoints[1] = -71.54f * (cameraWidth * cameraWidth) + 231.9f * cameraWidth + 52.86f;
            dPicPoints[2] = -42.35f * (cameraWidth * cameraWidth) + 152.6f * cameraWidth + 38.30f;
            dPicPoints[3] = -42.35f * (cameraWidth * cameraWidth) + 152.6f * cameraWidth + 38.30f;

            dTextPoints[0] = -17.79f * (cameraWidth * cameraWidth) + 60.12f * cameraWidth + 17.46f;
            dTextPoints[1] = -67.45f * (cameraWidth * cameraWidth) + 226.5f * cameraWidth + 53.70f;
            dTextPoints[2] = -150.9f * (cameraWidth * cameraWidth) + 503.3f * cameraWidth + 118.9f;
            dTextPoints[3] = -47.90f * (cameraWidth * cameraWidth) + 171.6f * cameraWidth + 42.95f;

            dText.fontSize = (int)(-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f);
        }
        else
        {
            // Width => change in width affects variables, so look at the height of the camera
            dArrowPoints[0] = 0f;
            dArrowPoints[1] = 533.3f * (cameraHeight * cameraHeight) - 1274f * cameraHeight + 738.9f - 6f; // DC TODO -- Edit to move down from top of screen
            dArrowPoints[2] = -198.5f * (cameraHeight * cameraHeight) + 733.5f * cameraHeight + 205.3f;
            dArrowPoints[3] = (-198.5f * (cameraHeight * cameraHeight) + 733.5f * cameraHeight + 205.3f) / 1.142857f;

            dFramePoints[0] = 0f;
            dFramePoints[1] = 533.3f * (cameraHeight * cameraHeight) - 1274f * cameraHeight + 738.9f - 6f; // DC TODO -- Edit to move down from top of screen
            dFramePoints[2] = -198.5f * (cameraHeight * cameraHeight) + 733.5f * cameraHeight + 205.3f;
            dFramePoints[3] = (-198.5f * (cameraHeight * cameraHeight) + 733.5f * cameraHeight + 205.3f) / 1.142857f;

            dPicPoints[0] = 70.80f * (cameraHeight * cameraHeight) - 260.5f * cameraHeight - 72.74f;
            dPicPoints[1] = 483.3f * (cameraHeight * cameraHeight) - 1070f * cameraHeight + 799.8f - 6f; // DC TODO -- Edit to move down from top of screen
            dPicPoints[2] = -35.93f * (cameraHeight * cameraHeight) + 143.9f * cameraHeight + 41.46f;
            dPicPoints[3] = -35.93f * (cameraHeight * cameraHeight) + 143.9f * cameraHeight + 41.46f;

            dTextPoints[0] = -4.963f * (cameraHeight * cameraHeight) + 44.29f * cameraHeight + 23.76f;
            dTextPoints[1] = 494.4f * (cameraHeight * cameraHeight) - 1081f * cameraHeight + 802.1f - 6f; // DC TODO -- Edit to move down from top of screen
            dTextPoints[2] = -114.1f * (cameraHeight * cameraHeight) + 452.0f * cameraHeight + 131.5f;
            dTextPoints[3] = -39.92f * (cameraHeight * cameraHeight) + 167.9f * cameraHeight + 43.11f;
            
            dText.fontSize = (int)(2.432f * (cameraHeight * cameraHeight) + 25.84f * cameraHeight + 20.05f);
        }

        // Jamma 8x16 Font Change
        dText.fontSize = (int)(dText.fontSize * 0.75f - 1f);

        dArrow.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(dArrowPoints[0], dArrowPoints[1]);
        dArrow.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(dArrowPoints[2], dArrowPoints[3]);

        dFrame.rectTransform.anchoredPosition = new Vector2(dFramePoints[0], dFramePoints[1]);
        dFrame.rectTransform.sizeDelta = new Vector2(dFramePoints[2], dFramePoints[3]);

        dPic.rectTransform.anchoredPosition = new Vector2(dPicPoints[0], dPicPoints[1]);
        dPic.rectTransform.sizeDelta = new Vector2(dPicPoints[2], dPicPoints[3]);

        dText.rectTransform.anchoredPosition = new Vector2(dTextPoints[0], dTextPoints[1]);
        dText.rectTransform.sizeDelta = new Vector2(dTextPoints[2], dTextPoints[3]);
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

    public void RefreshPause()
    {
        pauseTime = 0.333f;
    }
}
