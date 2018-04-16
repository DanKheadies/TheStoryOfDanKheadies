// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/08/2018
// Last:  04/16/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Controls where dialogue options are displayed
public class OptionsManager : MonoBehaviour
{
    public CameraFollow mainCamera;
    public DialogueManager dMan;
    public GameObject oBox;
    public Image o1Arw;
    public Image o1Text;
    public Image o2Arw;
    public Image o2Text;
    public Image o3Arw;
    public Image o3Text;
    public Image o4Arw;
    public Image o4Text;
    public TouchControls touches;

    public bool bDiaToOpts;
    public bool bOptionsActive;
    public bool bPauseOptions;
    public bool bTempControlActive;

    private float pauseTime;
    private float screenHeight;
    private float screenWidth;

    // Arrays follow CSS rules for orientation
    // [0] = top
    // [1] = right
    // [2] = bottom
    // [3] = left
    private float[] o1ArwPoints;
    private float[] o2ArwPoints;
    private float[] o3ArwPoints;
    private float[] o4ArwPoints;
    private float[] o1TextPoints;
    private float[] o2TextPoints;
    private float[] o3TextPoints;
    private float[] o4TextPoints;

    public string[] options;


    void Start()
    {
        // Initializers
        dMan = FindObjectOfType<DialogueManager>();
        mainCamera = FindObjectOfType<CameraFollow>();
        o1Arw = GameObject.Find("Opt1Arw").GetComponent<Image>();
        o2Arw = GameObject.Find("Opt2Arw").GetComponent<Image>();
        o3Arw = GameObject.Find("Opt3Arw").GetComponent<Image>();
        o4Arw = GameObject.Find("Opt4Arw").GetComponent<Image>();
        o1Text = GameObject.Find("Opt1").GetComponent<Image>();
        o2Text = GameObject.Find("Opt2").GetComponent<Image>();
        o3Text = GameObject.Find("Opt3").GetComponent<Image>();
        o4Text = GameObject.Find("Opt4").GetComponent<Image>();
        oBox = GameObject.Find("Options_Box");
        touches = FindObjectOfType<TouchControls>();

        bDiaToOpts = false;
        bOptionsActive = false;
        bPauseOptions = false; // UX -- Might be needed to prevent options selection
        pauseTime = 0.5f;

        o1ArwPoints = new float[4];
        o1TextPoints = new float[4];
        o2ArwPoints = new float[4];
        o2TextPoints = new float[4];
        o3ArwPoints = new float[4];
        o3TextPoints = new float[4];
        o4ArwPoints = new float[4];
        o4TextPoints = new float[4];

        ConfigureParameters();
    }


    void Update()
    {
        if (bPauseOptions)
        {
            pauseTime -= Time.deltaTime;
            if (pauseTime <= 0)
            {
                UnpauseOptions();
            }
        }

        // Select an option -> hide & reset options && advance dialogue / response
        if ((bOptionsActive && !bPauseOptions && Input.GetButtonDown("Action")) ||
            (bOptionsActive && !bPauseOptions && touches.bAction))
        {
            // DC 04/16/2018 -- Option selection goes here?


            // Close / hide options
            oBox.transform.localScale = Vector3.zero;

            PauseOptions();

            bDiaToOpts = false;
            bOptionsActive = false;

            ShowAllOptions();

            dMan.ResetDialogue();

            // DC 04/16/2018 -- Option advancement goes here?
        }

        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R))
        {
            ConfigureParameters();
        }
    }

    public void ShowOptions()
    {
        // Displays the options box
        bOptionsActive = true;
        oBox.transform.localScale = Vector3.one;

        // text options
        for (int i = 0; i < options.Length; i++)
        {
            GameObject optText = GameObject.Find("Opt" + (i + 1) + "_Text");
            optText.GetComponentInChildren<Text>().text = options[i];
        }

        // Checks (& shows) UI controls
        if (dMan.bTempControlActive)
        {
            touches.GetComponent<Canvas>().enabled = true;
        }

        PauseOptions();
    }

    public void HideSecondPlusOpt()
    {
        o2Arw.transform.localScale = Vector3.zero;
        o2Text.transform.localScale = Vector3.zero;
        o3Arw.transform.localScale = Vector3.zero;
        o3Text.transform.localScale = Vector3.zero;
        o4Arw.transform.localScale = Vector3.zero;
        o4Text.transform.localScale = Vector3.zero;
    }

    public void HideThirdPlusOpt()
    {
        o3Arw.transform.localScale = Vector3.zero;
        o3Text.transform.localScale = Vector3.zero;
        o4Arw.transform.localScale = Vector3.zero;
        o4Text.transform.localScale = Vector3.zero;
    }

    public void HideFourthOpt()
    {
        o4Arw.transform.localScale = Vector3.zero;
        o4Text.transform.localScale = Vector3.zero;
    }

    public void ShowAllOptions()
    {
        o1Arw.transform.localScale = Vector3.one;
        o1Text.transform.localScale = Vector3.one;
        o2Arw.transform.localScale = Vector3.one;
        o2Text.transform.localScale = Vector3.one;
        o3Arw.transform.localScale = Vector3.one;
        o3Text.transform.localScale = Vector3.one;
        o4Arw.transform.localScale = Vector3.one;
        o4Text.transform.localScale = Vector3.one;
    }

    public void ConfigureParameters()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        // UI Image & Text Positioning and Sizing based off camera size vs device size
        if (screenWidth > mainCamera.myCam.pixelWidth)
        {
            // Height => change in height affects variables
            o1TextPoints[0] = -0.00002459f * (screenHeight * screenHeight) + 0.3920f * screenHeight - 0.9068f;
            o1TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            o1TextPoints[2] = 0.00002879f * (screenHeight * screenHeight) + 0.4556f * screenHeight + 3.669f;
            o1TextPoints[3] = -0.0004951f * (screenHeight * screenHeight) + 0.4613f * screenHeight - 32.85f;

            o1ArwPoints[0] = -0.00001187f * (screenHeight * screenHeight) + 0.4307f * screenHeight - 0.9272f;
            o1ArwPoints[1] = 0.0001821f * (screenHeight * screenHeight) + 0.8941f * screenHeight + 15.28f;
            o1ArwPoints[2] = 0.00006967f * (screenHeight * screenHeight) + 0.4728f * screenHeight + 7.653f;
            o1ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            o2TextPoints[0] = 0.00005817f * (screenHeight * screenHeight) + 0.4898f * screenHeight + 4.924f;
            o2TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            o2TextPoints[2] = -0.00005397f * (screenHeight * screenHeight) + 0.3578f * screenHeight - 2.162f;
            o2TextPoints[3] = -0.0004672f * (screenHeight * screenHeight) + 0.4474f * screenHeight - 31.23f;

            o2ArwPoints[0] = 0.0001384f * (screenHeight * screenHeight) + 0.4798f * screenHeight + 12.56f;
            o2ArwPoints[1] = 0.0001260f * (screenHeight * screenHeight) + 0.9296f * screenHeight + 10.66f;
            o2ArwPoints[2] = -0.00009457f * (screenHeight * screenHeight) + 0.4306f * screenHeight - 6.642f;
            o2ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            o3TextPoints[0] = 0.00003636f * (screenHeight * screenHeight) + 0.6811f * screenHeight + 3.640f;
            o3TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            o3TextPoints[2] = -0.0002255f * (screenHeight * screenHeight) + 0.1802f * screenHeight - 15.92f;
            o3TextPoints[3] = -0.0004951f * (screenHeight * screenHeight) + 0.4613f * screenHeight - 32.85f;

            o3ArwPoints[0] = 0.0001061f * (screenHeight * screenHeight) + 0.6501f * screenHeight + 7.987f;
            o3ArwPoints[1] = 0.0001260f * (screenHeight * screenHeight) + 0.9296f * screenHeight + 10.66f;
            o3ArwPoints[2] = -0.00004833f * (screenHeight * screenHeight) + 0.2534f * screenHeight - 1.261f;
            o3ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            o4TextPoints[0] = 0.00003341f * (screenHeight * screenHeight) + 0.7878f * screenHeight + 4.207f;
            o4TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            o4TextPoints[2] = -0.00002921f * (screenHeight * screenHeight) + 0.05969f * screenHeight - 1.445f;
            o4TextPoints[3] = -0.0004951f * (screenHeight * screenHeight) + 0.4613f * screenHeight - 32.85f;

            o4ArwPoints[0] = 0.00004592f * (screenHeight * screenHeight) + 0.8342f * screenHeight + 2.798f;
            o4ArwPoints[1] = 0.0001260f * (screenHeight * screenHeight) + 0.9296f * screenHeight + 10.66f;
            o4ArwPoints[2] = 0.00001187f * (screenHeight * screenHeight) + 0.06926f * screenHeight + 3.927f;
            o4ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            o1Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            o2Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            o3Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            o4Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
        }
        else
        {
            // Width => change in width affects variables 
            o1TextPoints[0] = -0.00002986f * (screenWidth * screenWidth) + 0.3690f * screenWidth - 9.015f;
            o1TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            o1TextPoints[2] = -0.000007465f * (screenWidth * screenWidth) + 0.4266f * screenWidth - 2.311f;
            o1TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            o1ArwPoints[0] = 0.3661f * screenWidth + 2.164f;
            o1ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            o1ArwPoints[2] = 0.00001493f * (screenWidth * screenWidth) + 0.4364f * screenWidth + 3.737f;
            o1ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            o2TextPoints[0] = -0.00001866f * (screenWidth * screenWidth) + 0.4798f * screenWidth - 5.253f;
            o2TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            o2TextPoints[2] = -0.00001866f * (screenWidth * screenWidth) + 0.3158f * screenWidth - 6.073f;
            o2TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            o2ArwPoints[0] = 0.4932f * screenWidth + 1.049f;
            o2ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            o2ArwPoints[2] = 0.00001493f * (screenWidth * screenWidth) + 0.3093f * screenWidth + 4.852f;
            o2ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            o3TextPoints[0] = -((1f / 1071648f) * (screenWidth * screenWidth)) + 0.6150f * screenWidth - 0.1578f;
            o3TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            o3TextPoints[2] = -0.00002333f * (screenWidth * screenWidth) + 0.07376f * screenWidth - 9.780f;
            o3TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            o3ArwPoints[0] = 0.6189f * screenWidth + 1.344f;
            o3ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            o3ArwPoints[2] = 0.00001493f * (screenWidth * screenWidth) + 0.1837f * screenWidth + 4.557f;
            o3ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            o4TextPoints[0] = -0.00002613f * (screenWidth * screenWidth) + 0.7411f * screenWidth - 6.974f;
            o4TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            o4TextPoints[2] = -0.00001120f * (screenWidth * screenWidth) + 0.05453f * screenWidth - 4.352f;
            o4TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            o4ArwPoints[0] = -0.00002613f * (screenWidth * screenWidth) + 0.7780f * screenWidth - 5.040f;
            o4ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            o4ArwPoints[2] = 0.00004106f * (screenWidth * screenWidth) + 0.02455f * screenWidth + 10.94f;
            o4ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            o1Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            o2Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            o3Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            o4Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
        }

        // Text Options
        o1Text.rectTransform.anchoredPosition = new Vector2((-(o1TextPoints[1] - o1TextPoints[3]) / 2), ((o1TextPoints[2] / 2) - (o1TextPoints[0] / 2)));
        o1Text.rectTransform.sizeDelta = new Vector2(-(o1TextPoints[3] + o1TextPoints[1]), -(o1TextPoints[0] + o1TextPoints[2]));

        o2Text.rectTransform.anchoredPosition = new Vector2((-(o2TextPoints[1] - o2TextPoints[3]) / 2), ((o2TextPoints[2] / 2) - (o2TextPoints[0] / 2)));
        o2Text.rectTransform.sizeDelta = new Vector2(-(o2TextPoints[3] + o2TextPoints[1]), -(o2TextPoints[0] + o2TextPoints[2]));

        o3Text.rectTransform.anchoredPosition = new Vector2((-(o3TextPoints[1] - o3TextPoints[3]) / 2), ((o3TextPoints[2] / 2) - (o3TextPoints[0] / 2)));
        o3Text.rectTransform.sizeDelta = new Vector2(-(o3TextPoints[3] + o3TextPoints[1]), -(o3TextPoints[0] + o3TextPoints[2]));

        o4Text.rectTransform.anchoredPosition = new Vector2((-(o4TextPoints[1] - o4TextPoints[3]) / 2), ((o4TextPoints[2] / 2) - (o4TextPoints[0] / 2)));
        o4Text.rectTransform.sizeDelta = new Vector2(-(o4TextPoints[3] + o4TextPoints[1]), -(o4TextPoints[0] + o4TextPoints[2]));

        // Arrow Options
        o1Arw.rectTransform.anchoredPosition = new Vector2((-(o1ArwPoints[1] - o1ArwPoints[3]) / 2), ((o1ArwPoints[2] / 2) - (o1ArwPoints[0] / 2)));
        o1Arw.rectTransform.sizeDelta = new Vector2(-(o1ArwPoints[3] + o1ArwPoints[1]), -(o1ArwPoints[0] + o1ArwPoints[2]));

        o2Arw.rectTransform.anchoredPosition = new Vector2((-(o2ArwPoints[1] - o2ArwPoints[3]) / 2), ((o2ArwPoints[2] / 2) - (o2ArwPoints[0] / 2)));
        o2Arw.rectTransform.sizeDelta = new Vector2(-(o2ArwPoints[3] + o2ArwPoints[1]), -(o2ArwPoints[0] + o2ArwPoints[2]));

        o3Arw.rectTransform.anchoredPosition = new Vector2((-(o3ArwPoints[1] - o3ArwPoints[3]) / 2), ((o3ArwPoints[2] / 2) - (o3ArwPoints[0] / 2)));
        o3Arw.rectTransform.sizeDelta = new Vector2(-(o3ArwPoints[3] + o3ArwPoints[1]), -(o3ArwPoints[0] + o3ArwPoints[2]));

        o4Arw.rectTransform.anchoredPosition = new Vector2((-(o4ArwPoints[1] - o4ArwPoints[3]) / 2), ((o4ArwPoints[2] / 2) - (o4ArwPoints[0] / 2)));
        o4Arw.rectTransform.sizeDelta = new Vector2(-(o4ArwPoints[3] + o4ArwPoints[1]), -(o4ArwPoints[0] + o4ArwPoints[2]));
    }

    public void PauseOptions()
    {
        bPauseOptions = true;
    }

    public void UnpauseOptions()
    {
        bPauseOptions = false;
        pauseTime = 0.5f;
    }
}
