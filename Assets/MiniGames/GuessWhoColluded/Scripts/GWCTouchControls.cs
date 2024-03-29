﻿// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: Unity (https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom) (https://docs.unity3d.com/ScriptReference/Input.GetTouch.html)
// Contributors: David W. Corso, JoaquinRD, alberto-lara
// Start: 02/18/2019
// Last:  04/26/2021

using UnityEngine;

public class GWCTouchControls : MonoBehaviour
{
    public AspectUtility aUtil;
    public Camera mainCamera;
    public GameObject pause;
    public GameObject sceneTransAnim;
    public GWC_Controller gwc;
    public PlayerMovement playerMove;
    public TouchControls touches;

    public bool bPinchZooming;
    public bool bReadyToPan;

    public float maxDoubleTapTime;
    public float newTime;
    public float perspectiveZoomSpeed;
    public float orthoZoomSpeed;
    public float speed;
    public float xInput;
    public float yInput;

    public int tapCount;

    void Start()
    {
        maxDoubleTapTime = 0.333f;
        perspectiveZoomSpeed = 0.1f;       // The rate of change of the field of view in perspective mode.
        orthoZoomSpeed = 0.0125f;          // The rate of change of the orthographic size in orthographic mode.
        speed = 0.05f;
        tapCount = 0;
        
        bReadyToPan = true;
    }

    void Update()
    {
        // If GUI Controls are disabled
        if (gwc.bStartGame &&
            touches.transform.localScale == Vector3.zero &&
            pause.transform.localScale == Vector3.zero)
        {
            // Swipe-Pan
            if (bReadyToPan &&
                (Input.touchCount == 1 && 
                 Input.GetTouch(0).phase == TouchPhase.Moved))
            {
                bReadyToPan = false;

                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                // Convert touch panning to simple x & y input, i.e. GWCMove() in pMove
                if (Mathf.Abs(touchDeltaPosition.x) > Mathf.Abs(touchDeltaPosition.y))
                {
                    if (touchDeltaPosition.x > 0)
                    {
                        xInput = -1;
                    }
                    else if (touchDeltaPosition.x < 0)
                    {
                        xInput = 1;
                    }

                    playerMove.GWCMove(xInput, 0);
                }
                else if (Mathf.Abs(touchDeltaPosition.x) < Mathf.Abs(touchDeltaPosition.y))
                {
                    if (touchDeltaPosition.y > 0)
                    {
                        yInput = -1;
                    }
                    else if (touchDeltaPosition.y < 0)
                    {
                        yInput = 1;
                    }

                    playerMove.GWCMove(0, yInput);
                }
            }

            // Cycle Layers
            // If there is a double tap on the device... (and not on a character)
            // 05/15/2019 -- checking bReadyToPan means they're not panning atm
            if (Input.touchCount == 1 &&
                bReadyToPan)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    tapCount += 1;
                }

                if (tapCount == 1)
                {
                    newTime = Time.time + maxDoubleTapTime;
                }
                else if (tapCount == 2 && Time.time <= newTime)
                {
                    for (int i = 0; i < gwc.charTiles.Length; i++)
                    {
                        gwc.charTiles[i].FlipLayer();
                    }

                    tapCount = 0;
                }
            }

            // 06/09/2019 -- Keep here to run after Cycle check (avoids cycling while panning quickly)
            if (Input.touchCount > 0 &&
                Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                bReadyToPan = true;
            }

            // Reset double tap timer
            if (Time.time > newTime)
            {
                tapCount = 0;
            }

            // Pinch-Zoom
            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                bPinchZooming = true;

                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (mainCamera.orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    mainCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                    // Make sure the orthographic size never drops below zero.
                    mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, 0.875f, 5.642857f);
                }
                // DC 02/22/2019 -- This "should" never run
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    mainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 0.1f, 179.9f);
                }

                // Adjust the scene transitioner
                sceneTransAnim.transform.localScale = new Vector2
                    (Mathf.Clamp((sceneTransAnim.transform.localScale.x + (deltaMagnitudeDiff * orthoZoomSpeed) * 1.5f), 1.5f, 9f),
                     Mathf.Clamp((sceneTransAnim.transform.localScale.y + (deltaMagnitudeDiff * orthoZoomSpeed) * 1.5f), 1.5f, 9f));
            }
            // Reset check to help avoid flipping tiles via MouseUp
            else if (Input.touchCount == 0 &&
                     bPinchZooming)
            {
                bPinchZooming = false;
            }
        }
    }
}
