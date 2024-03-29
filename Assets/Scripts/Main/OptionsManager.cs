﻿// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/08/2018
// Last:  04/26/2021

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Controls where dialogue options are displayed
public class OptionsManager : MonoBehaviour
{
    public CameraFollow mainCamera;
    public ControllerSupport contSupp;
    public DialogueManager dMan;
    public FixedJoystick fixedJoy;
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
    public MoveOptionsMenuArrow moveOptsArw;
    public UIManager uMan;

    public bool bDiaToOpts;
    public bool bOptionsActive;
    public bool bPauseOptions;
    public bool bTempControlActive;

    public float cameraHeight;
    public float cameraWidth;
    public float pauseTime;

    // Arrays follow CSS rules for orientation
    // [0] = x
    // [1] = y
    // [2] = width
    // [3] = height
    public float[] o1ArwPoints;
    public float[] o2ArwPoints;
    public float[] o3ArwPoints;
    public float[] o4ArwPoints;
    public float[] o1TextPoints;
    public float[] o2TextPoints;
    public float[] o3TextPoints;
    public float[] o4TextPoints;
    public float[] oFramePoints;

    public int tempOptsCount;

    public string[] options;


    void Start()
    {
        // DC TODO -- Default is false?
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

        // Temp: Update Camera display / aspect ratio & virtual joystick
        if (Input.GetKeyUp(KeyCode.R) ||
            contSupp.ControllerMenuLeft("up"))
        {
            ConfigureParameters();
            fixedJoy.JoystickPosition();
        }
    }

    public void ShowOptions()
    {
        // Displays the options box
        bOptionsActive = true;
        oBox.transform.localScale = Vector3.one;

        // Text Options
        // 05/10/2019 DC TODO -- Should set temptOptsCount = 0 here
        for (int i = 0; i < options.Length; i++)
        {
            GameObject optText = GameObject.Find("Opt" + (i + 1) + "_Text");
            optText.GetComponentInChildren<Text>().text = options[i];
            tempOptsCount += 1;
        }
        
        // Hide Brio Bar & Pause Button (Overlay)
        uMan.HideBrioAndButton();

        // "Lock" Joystick to vertical direction
        fixedJoy.joystickMode = JoystickMode.Vertical;

        PauseOptions();
    }

    public void ResetOptions()
    {
        // Close / hide options
        oBox.transform.localScale = Vector3.zero;

        PauseOptions();

        bDiaToOpts = false;
        bOptionsActive = false;

        tempOptsCount = 0;

        ShowAllOptionsText();
        
        dMan.ResetDialogue();

        StartCoroutine(WaitForDialogue());
        
        fixedJoy.joystickMode = JoystickMode.AllAxis;
    }

    public IEnumerator WaitForDialogue()
    {
        yield return new WaitForSeconds(0.0125f);

        if (!bOptionsActive &&
            !dMan.bDialogueActive)
        {
            uMan.ShowBrioAndButton();
        }
    }

    public void CheckAndAssignClickedValue(int option)
    {
        if (option == 1)
        {
            moveOptsArw.currentPosition = MoveOptionsMenuArrow.ArrowPos.Opt1;
        }
        else if (option == 2)
        {
            moveOptsArw.currentPosition = MoveOptionsMenuArrow.ArrowPos.Opt2;
        }
        else if (option == 3)
        {
            moveOptsArw.currentPosition = MoveOptionsMenuArrow.ArrowPos.Opt3;
        }
        else if (option == 4)
        {
            moveOptsArw.currentPosition = MoveOptionsMenuArrow.ArrowPos.Opt4;
        }
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
        cameraHeight = mainCamera.GetComponent<Camera>().rect.height;
        cameraWidth = mainCamera.GetComponent<Camera>().rect.width;

        // UI Image & Text Positioning and Sizing based off camera size vs device size
        if (Screen.width > mainCamera.GetComponent<Camera>().pixelWidth)
        {
            // Height => change in height affects variables, so look at the width of the camera
            o1TextPoints[0] = -20.14f * (cameraWidth * cameraWidth) + 54.65f * cameraWidth + 5.423f;
            o1TextPoints[1] = -21.98f * (cameraWidth * cameraWidth) + 45.47f * cameraWidth + 6.018f;
            o1TextPoints[2] = -146.5f * (cameraWidth * cameraWidth) + 570.5f * cameraWidth + 167.9f;
            o1TextPoints[3] = -40.23f * (cameraWidth * cameraWidth) + 107.1f * cameraWidth + 21.38f;

            o1ArwPoints[0] = 76.03f * (cameraWidth * cameraWidth) - 289.9f * cameraWidth - 85.13f;
            o1ArwPoints[1] = -21.98f * (cameraWidth * cameraWidth) + 45.47f * cameraWidth + 6.018f;
            o1ArwPoints[2] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;
            o1ArwPoints[3] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;

            o2TextPoints[0] = -20.14f * (cameraWidth * cameraWidth) + 54.65f * cameraWidth + 5.423f;
            o2TextPoints[1] = 8.877f * (cameraWidth * cameraWidth) - 54f * cameraWidth - 17.72f;
            o2TextPoints[2] = -146.5f * (cameraWidth * cameraWidth) + 570.5f * cameraWidth + 167.9f;
            o2TextPoints[3] = -40.23f * (cameraWidth * cameraWidth) + 107.1f * cameraWidth + 21.38f;

            o2ArwPoints[0] = 76.03f * (cameraWidth * cameraWidth) - 289.9f * cameraWidth - 85.13f;
            o2ArwPoints[1] = 8.877f * (cameraWidth * cameraWidth) - 54f * cameraWidth - 17.72f;
            o2ArwPoints[2] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;
            o2ArwPoints[3] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;

            o3TextPoints[0] = -20.14f * (cameraWidth * cameraWidth) + 54.65f * cameraWidth + 5.423f;
            o3TextPoints[1] = 71.94f * (cameraWidth * cameraWidth) - 230.3f * cameraWidth - 53.92f; 
            o3TextPoints[2] = -146.4f * (cameraWidth * cameraWidth) + 570.4f * cameraWidth + 167.9f;
            o3TextPoints[3] = -62.47f * (cameraWidth * cameraWidth) + 179.7f * cameraWidth + 39.53f;

            o3ArwPoints[0] = 76.03f * (cameraWidth * cameraWidth) - 289.9f * cameraWidth - 85.13f;
            o3ArwPoints[1] = 42.60f * (cameraWidth * cameraWidth) - 156.9f * cameraWidth - 40.92f;
            o3ArwPoints[2] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;
            o3ArwPoints[3] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;

            o4TextPoints[0] = -20.14f * (cameraWidth * cameraWidth) + 54.65f * cameraWidth + 5.423f;
            o4TextPoints[1] = 62.89f * (cameraWidth * cameraWidth) - 241.7f * cameraWidth - 67.84f;
            o4TextPoints[2] = -146.5f * (cameraWidth * cameraWidth) + 570.5f * cameraWidth + 167.9f;
            o4TextPoints[3] = -40.23f * (cameraWidth * cameraWidth) + 107.1f * cameraWidth + 21.38f;

            o4ArwPoints[0] = 76.03f * (cameraWidth * cameraWidth) - 289.9f * cameraWidth - 85.13f;
            o4ArwPoints[1] = 62.89f * (cameraWidth * cameraWidth) - 241.7f * cameraWidth - 67.84f;
            o4ArwPoints[2] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;
            o4ArwPoints[3] = -7.951f * (cameraWidth * cameraWidth) + 35.21f * cameraWidth + 11.92f;

            oFramePoints[0] = 0f;
            oFramePoints[1] = 0f;
            oFramePoints[2] = (-204.2f * (cameraWidth * cameraWidth) + 682.6f * cameraWidth + 166.0f) * 1.142857f;
            oFramePoints[3] = -204.2f * (cameraWidth * cameraWidth) + 682.6f * cameraWidth + 166.0f;

            o1Text.GetComponentInChildren<Text>().fontSize = (int)(-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f);
            o2Text.GetComponentInChildren<Text>().fontSize = (int)(-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f);
            o3Text.GetComponentInChildren<Text>().fontSize = (int)(-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f);
            o4Text.GetComponentInChildren<Text>().fontSize = (int)(-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f);
        }
        else 
        {
            // Width => change in width affects variables
            o1TextPoints[0] = -98.04f * (cameraHeight * cameraHeight) + 163.9f * cameraHeight - 27.13f;
            o1TextPoints[1] = 526.8f * (cameraHeight * cameraHeight) - 1248f * cameraHeight + 750.7f - 6f; // DC TODO -- Edit to move down from top of screen 
            o1TextPoints[2] = 40.57f * (cameraHeight * cameraHeight) + 304.9f * cameraHeight + 253.1f; 
            o1TextPoints[3] = -33.75f * (cameraHeight * cameraHeight) + 99.98f * cameraHeight + 21.85f;

            o1ArwPoints[0] = -7.749f * (cameraHeight * cameraHeight) - 171.5f * cameraHeight - 122.2f;
            o1ArwPoints[1] = 526.8f * (cameraHeight * cameraHeight) - 1248f * cameraHeight + 750.7f - 6f; // DC TODO -- Edit to move down from top of screen
            o1ArwPoints[2] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;
            o1ArwPoints[3] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;

            o2TextPoints[0] = -98.04f * (cameraHeight * cameraHeight) + 163.9f * cameraHeight - 27.13f;
            o2TextPoints[1] = 550.2f * (cameraHeight * cameraHeight) - 1337f * cameraHeight + 724.1f - 6f; // DC TODO -- Edit to move down from top of screen
            o2TextPoints[2] = 40.57f * (cameraHeight * cameraHeight) + 304.9f * cameraHeight + 253.1f;
            o2TextPoints[3] = -33.75f * (cameraHeight * cameraHeight) + 99.98f * cameraHeight + 21.85f;

            o2ArwPoints[0] = -7.749f * (cameraHeight * cameraHeight) - 171.5f * cameraHeight - 122.2f;
            o2ArwPoints[1] = 550.2f * (cameraHeight * cameraHeight) - 1337f * cameraHeight + 724.1f - 6f; // DC TODO -- Edit to move down from top of screen
            o2ArwPoints[2] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;
            o2ArwPoints[3] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;

            o3TextPoints[0] = -98.04f * (cameraHeight * cameraHeight) + 163.9f * cameraHeight - 27.13f;
            o3TextPoints[1] = 592.0f * (cameraHeight * cameraHeight) - 1490f * cameraHeight + 682.0f - 6f; // DC TODO -- Edit to move down from top of screen
            o3TextPoints[2] = 40.57f * (cameraHeight * cameraHeight) + 304.9f * cameraHeight + 253.1f;
            o3TextPoints[3] = -9.508f * (cameraHeight * cameraHeight) + 110.6f * cameraHeight + 59.79f;

            o3ArwPoints[0] = -7.749f * (cameraHeight * cameraHeight) - 171.5f * cameraHeight - 122.2f;
            o3ArwPoints[1] = 581.2f * (cameraHeight * cameraHeight) - 1435f * cameraHeight + 698.7f - 6f; // DC TODO -- Edit to move down from top of screen
            o3ArwPoints[2] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;
            o3ArwPoints[3] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;

            o4TextPoints[0] = -98.04f * (cameraHeight * cameraHeight) + 163.9f * cameraHeight - 27.13f;
            o4TextPoints[1] = 612.6f * (cameraHeight * cameraHeight) - 1537f * cameraHeight + 675.9f - 6f; // DC TODO -- Edit to move down from top of screen
            o4TextPoints[2] = 40.57f * (cameraHeight * cameraHeight) + 304.9f * cameraHeight + 253.1f;
            o4TextPoints[3] = -33.75f * (cameraHeight * cameraHeight) + 99.98f * cameraHeight + 21.85f;

            o4ArwPoints[0] = -7.749f * (cameraHeight * cameraHeight) - 171.5f * cameraHeight - 122.2f;
            o4ArwPoints[1] = 612.6f * (cameraHeight * cameraHeight) - 1537f * cameraHeight + 675.9f - 6f; // DC TODO -- Edit to move down from top of screen
            o4ArwPoints[2] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;
            o4ArwPoints[3] = -0.06944f * (cameraHeight * cameraHeight) + 30.87f * cameraHeight + 8.283f;

            oFramePoints[0] = 0f;
            oFramePoints[1] = 533.3f * (cameraHeight * cameraHeight) - 1274f * cameraHeight + 738.9f - 6f; // DC TODO -- Edit to move down from top of screen
            oFramePoints[2] = -198.5f * (cameraHeight * cameraHeight) + 733.5f * cameraHeight + 205.3f;
            oFramePoints[3] = (-198.5f * (cameraHeight * cameraHeight) + 733.5f * cameraHeight + 205.3f) / 1.142857f;

            o1Text.GetComponentInChildren<Text>().fontSize = 
            o2Text.GetComponentInChildren<Text>().fontSize = 
            o3Text.GetComponentInChildren<Text>().fontSize = 
            o4Text.GetComponentInChildren<Text>().fontSize = (int)(2.432f * (cameraHeight * cameraHeight) + 25.84f * cameraHeight + 20.05f);
        }

        // Jamma 8x16 Font Change
        o1Text.GetComponentInChildren<Text>().fontSize = (int)(o1Text.GetComponentInChildren<Text>().fontSize * 0.75f);
        o2Text.GetComponentInChildren<Text>().fontSize = (int)(o2Text.GetComponentInChildren<Text>().fontSize * 0.75f);
        o3Text.GetComponentInChildren<Text>().fontSize = (int)(o3Text.GetComponentInChildren<Text>().fontSize * 0.75f);
        o4Text.GetComponentInChildren<Text>().fontSize = (int)(o4Text.GetComponentInChildren<Text>().fontSize * 0.75f);

        // Text Options
        o1Text.rectTransform.anchoredPosition = new Vector2(o1TextPoints[0], o1TextPoints[1]);
        o1Text.rectTransform.sizeDelta = new Vector2(o1TextPoints[2], o1TextPoints[3]);

        o2Text.rectTransform.anchoredPosition = new Vector2(o2TextPoints[0], o2TextPoints[1]);
        o2Text.rectTransform.sizeDelta = new Vector2(o2TextPoints[2], o2TextPoints[3]);

        o3Text.rectTransform.anchoredPosition = new Vector2(o3TextPoints[0], o3TextPoints[1]);
        o3Text.rectTransform.sizeDelta = new Vector2(o3TextPoints[2], o3TextPoints[3]);

        o4Text.rectTransform.anchoredPosition = new Vector2(o4TextPoints[0], o4TextPoints[1]);
        o4Text.rectTransform.sizeDelta = new Vector2(o4TextPoints[2], o4TextPoints[3]);

        // Arrow Options
        o1Arw.rectTransform.anchoredPosition = new Vector2(o1ArwPoints[0], o1ArwPoints[1]);
        o1Arw.rectTransform.sizeDelta = new Vector2(o1ArwPoints[2], o1ArwPoints[3]);

        o2Arw.rectTransform.anchoredPosition = new Vector2(o2ArwPoints[0], o2ArwPoints[1]);
        o2Arw.rectTransform.sizeDelta = new Vector2(o2ArwPoints[2], o2ArwPoints[3]);

        o3Arw.rectTransform.anchoredPosition = new Vector2(o3ArwPoints[0], o3ArwPoints[1]);
        o3Arw.rectTransform.sizeDelta = new Vector2(o3ArwPoints[2], o3ArwPoints[3]);

        o4Arw.rectTransform.anchoredPosition = new Vector2(o4ArwPoints[0], o4ArwPoints[1]);
        o4Arw.rectTransform.sizeDelta = new Vector2(o4ArwPoints[2], o4ArwPoints[3]);

        // Options Frame
        oFrame.rectTransform.anchoredPosition = new Vector2(oFramePoints[0], oFramePoints[1]);
        oFrame.rectTransform.sizeDelta = new Vector2(oFramePoints[2], oFramePoints[3]);
    }

    public void PauseOptions()
    {
        bPauseOptions = true;
    }

    public void UnpauseOptions()
    {
        bPauseOptions = false;
        pauseTime = 0.333f;
    }
}
