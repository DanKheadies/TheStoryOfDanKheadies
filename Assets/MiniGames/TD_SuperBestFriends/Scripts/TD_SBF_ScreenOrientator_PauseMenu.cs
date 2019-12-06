﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 10/21/2019
// Last:  10/21/2019

using UnityEngine;

public class TD_SBF_ScreenOrientator_PauseMenu : MonoBehaviour
{
    public DeviceOrientation devOr;

    public bool bIsFull;
    public bool bSizingChange;

    void Start()
    {
        // Initializers
        devOr = Input.deviceOrientation;

        bIsFull = Screen.fullScreen;

        SetTransform();
    }

    void Update()
    {
        if (Input.deviceOrientation != devOr ||
            Screen.autorotateToLandscapeLeft ||
            Screen.autorotateToLandscapeRight ||
            Screen.autorotateToPortrait ||
            Screen.autorotateToPortraitUpsideDown ||
            bSizingChange)
        {
            SetTransform();

            bSizingChange = false;
        }

        if (bIsFull != Screen.fullScreen)
        {
            bIsFull = Screen.fullScreen;
            bSizingChange = true;
        }
    }

    public void SetTransform()
    {
        if (Screen.width >= Screen.height)
        {
            GetComponent<RectTransform>().anchorMin = new Vector2(0.25f, 0.1f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.75f, 0.9f);
        }
        else
        {
            GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.1f);
            GetComponent<RectTransform>().anchorMax = new Vector2(0.9f, 0.9f);
        }

        GetComponent<RectTransform>().localPosition = Vector3.zero;
    }
}