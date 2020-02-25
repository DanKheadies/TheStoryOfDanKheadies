// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/24/2020

using UnityEngine;
using UnityEngine.UI;

// Controls where dialogues are displayed
public class ShowdownDiaMan : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Camera showdownCamera;
    public ControllerSupport contSupp;
    public GameObject dbox;
    public Text dText;
    public TouchControls touches;

    public bool bDialogueActive;

    private float screenHeight;
    private float screenWidth;
    private float rtLeft;
    private float rtRight;
    private float rtTop;
    private float rtBottom;
    private float testicle;

    public int currentLine;

    public string[] dialogueLines;


    void Start()
    {
        // Size w/ respect to AspectUtility.cs
        showdownCamera.orthographicSize = aspectUtil._wantedAspectRatio;

        ConfigureParameters();
    }


    void Update()
    {

        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R) ||
            //Input.GetKeyUp(KeyCode.JoystickButton6))
            contSupp.ControllerMenuLeft("up"))
        {
            aspectUtil.Awake();
            ConfigureParameters();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("W:" + Screen.width);
            Debug.Log("H:" + Screen.height);
            //Debug.Log("CW:" + showdownCamera.pixelWidth);

            //Debug.Log("R:" + (float)(Screen.width/showdownCamera.pixelWidth));
        }

        // Advance active dialogues
        if ((bDialogueActive && Input.GetKeyDown(KeyCode.Space)) ||
            (bDialogueActive && Input.GetMouseButtonDown(0)) ||
            (bDialogueActive && touches.bAaction))
        {
            currentLine++;
        }

        // End of dialogue => reset everything
        if (currentLine >= dialogueLines.Length)
        {
            dbox.SetActive(false);
            bDialogueActive = false;

            currentLine = 0;
        }

        // Set current text
        dText.text = dialogueLines[currentLine];
    }

    public void ShowDialogue()
    {
        // Displays the dialogue box
        bDialogueActive = true;
        dbox.SetActive(true);
    }

    public void ConfigureParameters()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        // UI Text Positioning & Sizing based off camera size
        if (screenWidth > showdownCamera.pixelWidth)
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
}
