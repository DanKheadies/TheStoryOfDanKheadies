// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/29/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Controls where dialogues are displayed
public class DialogueManager : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public CameraFollow oCamera;
    public GameObject dbox;
    private PlayerMovement thePlayer;
    private Scene scene;
    public Text dText;

    public bool dialogueActive;

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
        aspectUtil = GetComponent<AspectUtility>();
        oCamera = FindObjectOfType<CameraFollow>();
        scene = SceneManager.GetActiveScene();
        thePlayer = FindObjectOfType<PlayerMovement>();

        ConfigureParameters();
    }
	

	void Update ()
    {
        // Check sizing stuff
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Debug.Log("A:" + oCamera.myCam.pixelRect);
        //    Debug.Log("(" + Screen.width + ", " + Screen.height + ")");
        //}

        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R))
        {
            ConfigureParameters();
        }

        // Advance active dialogues
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }

        // End of dialogue => reset everything
        if (currentLine >= dialogueLines.Length)
        {
            dbox.SetActive(false);
            dialogueActive = false;

            currentLine = 0;

            // Avoid console error when no player object is present
            if (scene.name != "Showdown")
            {
                thePlayer.bStopPlayerMovement = false;
            }
        }

        // Set current text
        dText.text = dialogueLines[currentLine];
    }

    public void ShowDialogue()
    {
        // Displays the dialogue box
        dialogueActive = true;
        dbox.SetActive(true);

        // Stops the player's movement
        thePlayer.bStopPlayerMovement = true;
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
}
