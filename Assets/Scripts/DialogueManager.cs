// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/25/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 
public class DialogueManager : MonoBehaviour
{
    public AspectUtility aspectUtil;

    public bool dialogueActive;

    public CameraFollow oCamera;

    private float screenHeight;
    private float screenWidth;
    private float rtLeft;
    private float rtRight;
    private float rtTop;
    private float rtBottom;

    public GameObject dbox;

    public int currentLine;

    private PlayerMovement thePlayer;

    public string[] dialogueLines;

    public Text dText;

    
	void Start ()
    {
        // Initializers
        aspectUtil = GetComponent<AspectUtility>();
        oCamera = FindObjectOfType<CameraFollow>();
        thePlayer = FindObjectOfType<PlayerMovement>();

        ConfigureParameters();
    }
	

	void Update ()
    {
        // Check sizing stuff
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("A:" + oCamera.myCam.pixelRect);
            Debug.Log("W:" + oCamera.myCam.pixelWidth);
            Debug.Log("(" + Screen.width + ", " + Screen.height + ")");
        }

        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R))
        {
            ConfigureParameters();
        }


        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;
        }

        if (currentLine >= dialogueLines.Length)
        {
            dbox.SetActive(false);
            dialogueActive = false;

            currentLine = 0;
            thePlayer.bStopPlayerMovement = false;
        }

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
            Debug.Log("IF");
            rtLeft = 0.00004293f * (screenHeight * screenHeight) + 0.04109f * screenHeight + 6.043f;
            rtRight = -0.0001284f * (screenHeight * screenHeight) + 0.2052f * screenHeight - 3.257f;
            rtTop = -0.00000348f * (screenHeight * screenHeight) + 0.05072f * screenHeight + 1.834f;
            rtBottom = -0.0001692f * (screenHeight * screenHeight) + 0.7994f * screenHeight - 14.63f;

            dText.fontSize = (int)(-0.000007955f * (screenHeight * screenHeight) + 0.1159f * screenHeight - 2.95f);
            dText.rectTransform.anchoredPosition = new Vector2(-(rtLeft / 2), ((rtBottom / 2) - (rtTop / 2)));
            dText.rectTransform.sizeDelta = new Vector2(-(rtLeft + rtRight), -(rtTop + rtBottom));
        }
        else
        {
            // Width => change in width affects variables
            Debug.Log("ELSE");
            //rtLeft = 0.000005545f * (screenWidth * screenWidth) + 0.5823f * screenWidth + 2.133f;
            //rtRight = 0.0001993f * (screenWidth * screenWidth) + 0.2656f * screenWidth - 20.14f;
            rtLeft = 11; //14
            rtLeft = 24; //24
            rtRight = 28; //25
            rtRight = 50; //50
            rtTop = 0.0000272f * (screenWidth * screenWidth) + 0.02149f * screenWidth + 5.613f;
            rtBottom = 0.00007596f * (screenWidth * screenWidth) + 0.5523f * screenWidth + 6.496f;

            Debug.Log("L: " + rtLeft);
            Debug.Log("R: " + rtRight);
            Debug.Log("T: " + rtTop);
            Debug.Log("B: " + rtBottom);

            dText.fontSize = (int)(0.00001684f * (screenWidth * screenWidth) + 0.08254f * screenWidth + 0.818f);
            dText.rectTransform.anchoredPosition = new Vector2(-(rtLeft / 2), ((rtBottom / 2) - (rtTop / 2)));
            dText.rectTransform.sizeDelta = new Vector2(-(rtLeft + rtRight), -(rtTop + rtBottom));
        }

    }
}
