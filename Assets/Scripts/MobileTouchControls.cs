// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: Unity (https://unity3d.com/learn/tutorials/topics/mobile-touch/pinch-zoom) (https://docs.unity3d.com/ScriptReference/Input.GetTouch.html)
// Contributors: David W. Corso, JoaquinRD, alberto-lara
// Start: 02/18/2019
// Last:  02/18/2019

using UnityEngine;

public class MobileTouchControls : MonoBehaviour
{
    public Camera mainCamera;
    public TouchControls touches;

    public float perspectiveZoomSpeed;
    public float orthoZoomSpeed;
    public float speed;

    void Start()
    {
        // Initializers
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        touches = FindObjectOfType<TouchControls>();

        perspectiveZoomSpeed = 0.5f;    // The rate of change of the field of view in perspective mode.
        orthoZoomSpeed = 0.5f;          // The rate of change of the orthographic size in orthographic mode.
        speed = 0.1f;
    }

        void Update()
    {
        // If GUI Controls are disabled
        if (!touches.gameObject.GetComponent<Canvas>().enabled)
        {
            // Swipe-Pan
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                //transform.Translate(-touchDeltaPosition.x * speed, -touchDeltaPosition.y * speed, 0);
                transform.Translate(-touchDeltaPosition.x * speed * Time.deltaTime, -touchDeltaPosition.y * speed * Time.deltaTime, 0);
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
                    mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize, 0.1f);
                }
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
