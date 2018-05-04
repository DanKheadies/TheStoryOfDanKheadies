// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/08/2018
// Last:  04/28/2018

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
    public Image oFrame;
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
    private float[] oFramePoints;

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
        oFrame = GameObject.Find("Options_Frame").GetComponent<Image>();
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
        oFramePoints = new float[4];

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

    public void ResetOptions()
    {
        // Close / hide options
        oBox.transform.localScale = Vector3.zero;

        PauseOptions();

        bDiaToOpts = false;
        bOptionsActive = false;

        ShowAllOptionsText();

        dMan.ResetDialogue();
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

    // Arrows are addressed in MoveOptionsMenuArrow
    public void ShowAllOptionsText()
    {
        o1Text.transform.localScale = Vector3.one;
        o2Text.transform.localScale = Vector3.one;
        o3Text.transform.localScale = Vector3.one;
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
            // Old values
            //o1TextPoints[0] = -0.00002459f * (screenHeight * screenHeight) + 0.3920f * screenHeight - 0.9068f;
            //o1TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            //o1TextPoints[2] = 0.00002879f * (screenHeight * screenHeight) + 0.4556f * screenHeight + 3.669f;
            //o1TextPoints[3] = -0.0004951f * (screenHeight * screenHeight) + 0.4613f * screenHeight - 32.85f;

            //o1ArwPoints[0] = -0.00001187f * (screenHeight * screenHeight) + 0.4307f * screenHeight - 0.9272f;
            //o1ArwPoints[1] = 0.0001821f * (screenHeight * screenHeight) + 0.8941f * screenHeight + 15.28f;
            //o1ArwPoints[2] = 0.00006967f * (screenHeight * screenHeight) + 0.4728f * screenHeight + 7.653f;
            //o1ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            //o2TextPoints[0] = 0.00005817f * (screenHeight * screenHeight) + 0.4898f * screenHeight + 4.924f;
            //o2TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            //o2TextPoints[2] = -0.00005397f * (screenHeight * screenHeight) + 0.3578f * screenHeight - 2.162f;
            //o2TextPoints[3] = -0.0004672f * (screenHeight * screenHeight) + 0.4474f * screenHeight - 31.23f;

            //o2ArwPoints[0] = 0.0001384f * (screenHeight * screenHeight) + 0.4798f * screenHeight + 12.56f;
            //o2ArwPoints[1] = 0.0001260f * (screenHeight * screenHeight) + 0.9296f * screenHeight + 10.66f;
            //o2ArwPoints[2] = -0.00009457f * (screenHeight * screenHeight) + 0.4306f * screenHeight - 6.642f;
            //o2ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            //o3TextPoints[0] = 0.00003636f * (screenHeight * screenHeight) + 0.6811f * screenHeight + 3.640f;
            //o3TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            //o3TextPoints[2] = -0.0002255f * (screenHeight * screenHeight) + 0.1802f * screenHeight - 15.92f;
            //o3TextPoints[3] = -0.0004951f * (screenHeight * screenHeight) + 0.4613f * screenHeight - 32.85f;

            //o3ArwPoints[0] = 0.0001061f * (screenHeight * screenHeight) + 0.6501f * screenHeight + 7.987f;
            //o3ArwPoints[1] = 0.0001260f * (screenHeight * screenHeight) + 0.9296f * screenHeight + 10.66f;
            //o3ArwPoints[2] = -0.00004833f * (screenHeight * screenHeight) + 0.2534f * screenHeight - 1.261f;
            //o3ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            //o4TextPoints[0] = 0.00003341f * (screenHeight * screenHeight) + 0.7878f * screenHeight + 4.207f;
            //o4TextPoints[1] = -0.00007166f * (screenHeight * screenHeight) + 0.09967f * screenHeight - 10.84f;
            //o4TextPoints[2] = -0.00002921f * (screenHeight * screenHeight) + 0.05969f * screenHeight - 1.445f;
            //o4TextPoints[3] = -0.0004951f * (screenHeight * screenHeight) + 0.4613f * screenHeight - 32.85f;

            //o4ArwPoints[0] = 0.00004592f * (screenHeight * screenHeight) + 0.8342f * screenHeight + 2.798f;
            //o4ArwPoints[1] = 0.0001260f * (screenHeight * screenHeight) + 0.9296f * screenHeight + 10.66f;
            //o4ArwPoints[2] = 0.00001187f * (screenHeight * screenHeight) + 0.06926f * screenHeight + 3.927f;
            //o4ArwPoints[3] = -0.0001283f * (screenHeight * screenHeight) + 0.1542f * screenHeight - 8.930f;

            //o1Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            //o2Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            //o3Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);
            //o4Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001528f * (screenHeight * screenHeight) + 0.09002f * screenHeight - 3.201f);

            o1TextPoints[0] = -0.00007780f * (screenHeight * screenHeight) + 0.2577f * screenHeight + 65.96f;
            o1TextPoints[1] = 0.0006683f * (screenHeight * screenHeight) - 1.535f * screenHeight + 897.1f;
            o1TextPoints[2] = -0.00008179f * (screenHeight * screenHeight) + 0.3116f * screenHeight + 83.63f;
            o1TextPoints[3] = 0.0006294f * (screenHeight * screenHeight) - 1.414f * screenHeight + 902.6f;

            o1ArwPoints[0] = -0.0001021f * (screenHeight * screenHeight) + 0.3016f * screenHeight + 68.80f;
            o1ArwPoints[1] = 0.0004773f * (screenHeight * screenHeight) - 0.8931f * screenHeight + 1065f;
            o1ArwPoints[2] = -0.00008538f * (screenHeight * screenHeight) + 0.3298f * screenHeight + 91.62f;
            o1ArwPoints[3] = 0.0006460f * (screenHeight * screenHeight) - 1.488f * screenHeight + 887.9f;

            o2TextPoints[0] = -0.0001113f * (screenHeight * screenHeight) + 0.3625f * screenHeight + 89.22f;
            o2TextPoints[1] = 0.0006683f * (screenHeight * screenHeight) - 1.535f * screenHeight + 897.1f;
            o2TextPoints[2] = -0.00004828f * (screenHeight * screenHeight) + 0.2067f * screenHeight + 60.38f;
            o2TextPoints[3] = 0.0006294f * (screenHeight * screenHeight) - 1.414f * screenHeight + 902.6f;

            o2ArwPoints[0] = 0.0001384f * (screenHeight * screenHeight) + 0.4798f * screenHeight + 12.56f;
            o2ArwPoints[1] = -0.0001253f * (screenHeight * screenHeight) + 0.3934f * screenHeight + 95.31f;
            o2ArwPoints[2] = -0.00006224f * (screenHeight * screenHeight) + 0.2381f * screenHeight + 65.11f;
            o2ArwPoints[3] = 0.0006460f * (screenHeight * screenHeight) - 1.488f * screenHeight + 887.9f;

            o3TextPoints[0] = -0.0001539f * (screenHeight * screenHeight) + 0.4943f * screenHeight + 115.4f;
            o3TextPoints[1] = 0.0006683f * (screenHeight * screenHeight) - 1.535f * screenHeight + 897.1f;
            o3TextPoints[2] = -0.00002651f * (screenHeight * screenHeight) + 0.05155f * screenHeight + 2.986f;
            o3TextPoints[3] = 0.0006294f * (screenHeight * screenHeight) - 1.414f * screenHeight + 902.6f;

            o3ArwPoints[0] = -0.0001535f * (screenHeight * screenHeight) + 0.4886f * screenHeight + 118.8f;
            o3ArwPoints[1] = 0.0004773f * (screenHeight * screenHeight) - 0.8931f * screenHeight + 1065f;
            o3ArwPoints[2] = -0.00003399f * (screenHeight * screenHeight) + 0.1429f * screenHeight + 41.58f;
            o3ArwPoints[3] = 0.0006460f * (screenHeight * screenHeight) - 1.488f * screenHeight + 887.9f;

            o4TextPoints[0] = -0.0001487f * (screenHeight * screenHeight) + 0.5365f * screenHeight + 142.0f;
            o4TextPoints[1] = 0.0006683f * (screenHeight * screenHeight) - 1.535f * screenHeight + 897.1f;
            o4TextPoints[2] = -0.00001087f * (screenHeight * screenHeight) + 0.03280f * screenHeight + 7.596f;
            o4TextPoints[3] = 0.0006294f * (screenHeight * screenHeight) - 1.414f * screenHeight + 902.6f;

            o4ArwPoints[0] = -0.0001394f * (screenHeight * screenHeight) + 0.5365f * screenHeight + 155.3f;
            o4ArwPoints[1] = 0.0004773f * (screenHeight * screenHeight) - 0.8931f * screenHeight + 1065f;
            o4ArwPoints[2] = -0.00004812f * (screenHeight * screenHeight) + 0.09496f * screenHeight + 5.152f;
            o4ArwPoints[3] = 0.0006460f * (screenHeight * screenHeight) - 1.488f * screenHeight + 887.9f;

            oFramePoints[0] = 0f;
            oFramePoints[1] = 0.0006623f * (screenHeight * screenHeight) - 1.548f * screenHeight + 882.2f;
            oFramePoints[2] = 0f;
            oFramePoints[3] = 0.0006623f * (screenHeight * screenHeight) - 1.548f * screenHeight + 882.2f;

            o1Text.GetComponentInChildren<Text>().fontSize = (int)(-0.000007980f * (screenHeight * screenHeight) + 0.04118f * screenHeight + 15.13f);
            o2Text.GetComponentInChildren<Text>().fontSize = (int)(-0.000007980f * (screenHeight * screenHeight) + 0.04118f * screenHeight + 15.13f);
            o3Text.GetComponentInChildren<Text>().fontSize = (int)(-0.000007980f * (screenHeight * screenHeight) + 0.04118f * screenHeight + 15.13f);
            o4Text.GetComponentInChildren<Text>().fontSize = (int)(-0.000007980f * (screenHeight * screenHeight) + 0.04118f * screenHeight + 15.13f);
        }
        else
        {
            // Width => change in width affects variables 
            // Old values 
            //o1TextPoints[0] = -0.00002986f * (screenWidth * screenWidth) + 0.3690f * screenWidth - 9.015f;
            //o1TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            //o1TextPoints[2] = -0.000007465f * (screenWidth * screenWidth) + 0.4266f * screenWidth - 2.311f;
            //o1TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            //o1ArwPoints[0] = 0.3661f * screenWidth + 2.164f;
            //o1ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            //o1ArwPoints[2] = 0.00001493f * (screenWidth * screenWidth) + 0.4364f * screenWidth + 3.737f;
            //o1ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            //o2TextPoints[0] = -0.00001866f * (screenWidth * screenWidth) + 0.4798f * screenWidth - 5.253f;
            //o2TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            //o2TextPoints[2] = -0.00001866f * (screenWidth * screenWidth) + 0.3158f * screenWidth - 6.073f;
            //o2TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            //o2ArwPoints[0] = 0.4932f * screenWidth + 1.049f;
            //o2ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            //o2ArwPoints[2] = 0.00001493f * (screenWidth * screenWidth) + 0.3093f * screenWidth + 4.852f;
            //o2ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            //o3TextPoints[0] = -((1f / 1071648f) * (screenWidth * screenWidth)) + 0.6150f * screenWidth - 0.1578f;
            //o3TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            //o3TextPoints[2] = -0.00002333f * (screenWidth * screenWidth) + 0.07376f * screenWidth - 9.780f;
            //o3TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            //o3ArwPoints[0] = 0.6189f * screenWidth + 1.344f;
            //o3ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            //o3ArwPoints[2] = 0.00001493f * (screenWidth * screenWidth) + 0.1837f * screenWidth + 4.557f;
            //o3ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            //o4TextPoints[0] = -0.00002613f * (screenWidth * screenWidth) + 0.7411f * screenWidth - 6.974f;
            //o4TextPoints[1] = -0.00001493f * (screenWidth * screenWidth) + 0.05267f * screenWidth - 2.458f;
            //o4TextPoints[2] = -0.00001120f * (screenWidth * screenWidth) + 0.05453f * screenWidth - 4.352f;
            //o4TextPoints[3] = -0.00002240f * (screenWidth * screenWidth) + 0.1910f * screenWidth - 7.294f;

            //o4ArwPoints[0] = -0.00002613f * (screenWidth * screenWidth) + 0.7780f * screenWidth - 5.040f;
            //o4ArwPoints[1] = 0.00005599f * (screenWidth * screenWidth) + 0.8079f * screenWidth + 17.58f;
            //o4ArwPoints[2] = 0.00004106f * (screenWidth * screenWidth) + 0.02455f * screenWidth + 10.94f;
            //o4ArwPoints[3] = -0.00003359f * (screenWidth * screenWidth) + 0.1131f * screenWidth - 9.892f;

            //o1Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            //o2Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            //o3Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);
            //o4Text.GetComponentInChildren<Text>().fontSize = (int)(0.000005696f * (screenWidth * screenWidth) + 0.05977f * screenWidth + 0.8173f);

            o1TextPoints[0] = -0.00005084f * (screenWidth * screenWidth) + 0.2107f * screenWidth + 70.83f;
            o1TextPoints[1] = -0.00001209f * (screenWidth * screenWidth) + 0.03668f * screenWidth + 7.444f;
            o1TextPoints[2] = 0.0006163f * (screenWidth * screenWidth) - 1.743f * screenWidth + 1496f;
            o1TextPoints[3] = 0.00004441f * (screenWidth * screenWidth) + 0.001702f * screenWidth + 60.62f;

            o1ArwPoints[0] = -0.00005238f * (screenWidth * screenWidth) + 0.2220f * screenWidth + 82.52f;
            o1ArwPoints[1] = -0.0001285f * (screenWidth * screenWidth) + 0.5565f * screenWidth + 182.4f;
            o1ArwPoints[2] = 0.0005999f * (screenWidth * screenWidth) - 1.707f * screenWidth + 1498f;
            o1ArwPoints[3] = 0.00002906f * (screenWidth * screenWidth) - 0.02217f * screenWidth + 36.23f;

            o2TextPoints[0] = -0.00005709f * (screenWidth * screenWidth) + 0.2727f * screenWidth + 102.7f;
            o2TextPoints[1] = -0.00001209f * (screenWidth * screenWidth) + 0.03668f * screenWidth + 7.444f;
            o2TextPoints[2] = 0.0006225f * (screenWidth * screenWidth) - 1.805f * screenWidth + 1465f;
            o2TextPoints[3] = 0.00004441f * (screenWidth * screenWidth) + 0.001702f * screenWidth + 60.62f;

            o2ArwPoints[0] = -0.00006031f * (screenWidth * screenWidth) + 0.2869f * screenWidth + 113.6f;
            o2ArwPoints[1] = -0.0001285f * (screenWidth * screenWidth) + 0.5565f * screenWidth + 182.4f;
            o2ArwPoints[2] = 0.0006078f * (screenWidth * screenWidth) - 1.771f * screenWidth + 1467f;
            o2ArwPoints[3] = 0.00002906f * (screenWidth * screenWidth) - 0.02217f * screenWidth + 36.23f;

            o3TextPoints[0] = -0.00007302f * (screenWidth * screenWidth) + 0.3598f * screenWidth + 139.0f;
            o3TextPoints[1] = -0.00001209f * (screenWidth * screenWidth) + 0.03668f * screenWidth + 7.444f;
            o3TextPoints[2] = 0.0006449f * (screenWidth * screenWidth) - 1.946f * screenWidth + 1405f;
            o3TextPoints[3] = 0.00004441f * (screenWidth * screenWidth) + 0.001702f * screenWidth + 60.62f;

            o3ArwPoints[0] = -0.00008235f * (screenWidth * screenWidth) + 0.3714f * screenWidth + 136.0f;
            o3ArwPoints[1] = -0.0001285f * (screenWidth * screenWidth) + 0.5565f * screenWidth + 182.4f;
            o3ArwPoints[2] = 0.0006299f * (screenWidth * screenWidth) - 1.856f * screenWidth + 1445f;
            o3ArwPoints[3] = 0.00002906f * (screenWidth * screenWidth) - 0.02217f * screenWidth + 36.23f;

            o4TextPoints[0] = -0.00006713f * (screenWidth * screenWidth) + 0.3924f * screenWidth + 167.7f;
            o4TextPoints[1] = -0.00001209f * (screenWidth * screenWidth) + 0.03668f * screenWidth + 7.444f;
            o4TextPoints[2] = 0.0006326f * (screenWidth * screenWidth) - 1.925f * screenWidth + 1400f;
            o4TextPoints[3] = 0.00004441f * (screenWidth * screenWidth) + 0.001702f * screenWidth + 60.62f;

            o4ArwPoints[0] = -0.00007332f * (screenWidth * screenWidth) + 0.4103f * screenWidth + 177.7f;
            o4ArwPoints[1] = -0.0001285f * (screenWidth * screenWidth) + 0.5565f * screenWidth + 182.4f;
            o4ArwPoints[2] = 0.0006208f * (screenWidth * screenWidth) - 1.895f * screenWidth + 1403f;
            o4ArwPoints[3] = 0.00002906f * (screenWidth * screenWidth) - 0.02217f * screenWidth + 36.23f;

            oFramePoints[0] = 0f;
            oFramePoints[1] = 0f;
            oFramePoints[2] = 0.0006536f * (screenWidth * screenWidth) - 1.974f * screenWidth + 1399f;
            oFramePoints[3] = 0f;

            o1Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001352f * (screenWidth * screenWidth) + 0.04805f * screenWidth + 10.88f);
            o2Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001352f * (screenWidth * screenWidth) + 0.04805f * screenWidth + 10.88f);
            o3Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001352f * (screenWidth * screenWidth) + 0.04805f * screenWidth + 10.88f);
            o4Text.GetComponentInChildren<Text>().fontSize = (int)(-0.00001352f * (screenWidth * screenWidth) + 0.04805f * screenWidth + 10.88f);
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

        // Options Frame
        oFrame.rectTransform.anchoredPosition = new Vector2((-(oFramePoints[1] - oFramePoints[3]) / 2), ((oFramePoints[2] / 2) - (oFramePoints[0] / 2)));
        oFrame.rectTransform.sizeDelta = new Vector2(-(oFramePoints[3] + oFramePoints[1]), -(oFramePoints[0] + oFramePoints[2]));
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
