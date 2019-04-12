// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Unity (https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom) (https://docs.unity3d.com/ScriptReference/Input.GetTouch.html)
// Contributors: David W. Corso, JoaquinRD, alberto-lara
// Start: 02/18/2019
// Last:  04/11/2019

using UnityEngine;

public class GWCTouchControls : MonoBehaviour
{
    public AspectUtility aUtil;
    public Camera mainCamera;
    public GameObject pause;
    public PlayerMovement pMove;
    public TouchControls touches;
    
    public bool bReadyToPan;

    public float perspectiveZoomSpeed;
    public float orthoZoomSpeed;
    public float speed;
    public float xInput;
    public float yInput;

    void Start()
    {
        // Initializers
        aUtil = FindObjectOfType<AspectUtility>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        pause = GameObject.Find("PauseScreen");
        pMove = FindObjectOfType<PlayerMovement>();
        touches = FindObjectOfType<TouchControls>();

        perspectiveZoomSpeed = 0.1f;       // The rate of change of the field of view in perspective mode.
        orthoZoomSpeed = 0.0125f;          // The rate of change of the orthographic size in orthographic mode.
        speed = 0.05f;

        bReadyToPan = true;
    }

        void Update()
    {
        // If GUI Controls are disabled
        if (touches.transform.localScale == Vector3.zero &&
            pause.transform.localScale == Vector3.zero)
        {
            // Swipe-Pan
            if (bReadyToPan &&
                (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved))
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

                    pMove.GWCMove(xInput, 0);
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

                    pMove.GWCMove(0, yInput);
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                bReadyToPan = true;
            }

            // Pinch-Zoom
            // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
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
                    
                    Debug.Log(mainCamera.orthographicSize);
                }
                // DC 02/22/2019 -- This "should" never run
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    mainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 0.1f, 179.9f);
                }
            }
        }
    }
}
