// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/10/2016
// Last:  07/01/2021

using UnityEngine;
using UnityEngine.EventSystems;

public class TD_SBF_CameraController : MonoBehaviour
{
    public ControllerSupport contSupp;
    public DeviceDetector devDetect;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_TouchControls touchConts;
    public TD_SBF_TowerPlacer tPlacer;

    public float mobileSensitivity;
    public float mouseSensitivity;
    public float orthoZoomSpeed;
    public float perspectiveZoomSpeed;
    public float panSpeed;
    public float scrollSpeed;
    public Vector3 lastPosition;

    void Start()
    {
        mobileSensitivity = 0.0125f;
        mouseSensitivity = 0.0333f;
        perspectiveZoomSpeed = 0.1f;       // The rate of change of the field of view in perspective mode.
        orthoZoomSpeed = 0.0125f;          // The rate of change of the orthographic size in orthographic mode.
        panSpeed = 20f;
        scrollSpeed = 5f;
    }

    void Update()
    {
        if (TD_SBF_GameManagement.IsGameOver ||
            TD_SBF_GameManagement.IsLevelWon)
        {
            enabled = false;
            return;
        }

        if (!gMan.bIsHeroMode)
        {
            // Up
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.UpArrow) ||
                (contSupp.bIsMoving &&
                 (contSupp.ControllerLeftJoystickVertical() > 0 ||
                  contSupp.ControllerDirectionalPadVertical() > 0)))
            {
                transform.Translate(Vector2.up * panSpeed * Time.deltaTime, Space.World);
            }

            // Down
            if (Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.DownArrow) ||
                (contSupp.bIsMoving &&
                 (contSupp.ControllerLeftJoystickVertical() < 0 ||
                  contSupp.ControllerDirectionalPadVertical() < 0)))
            {
                transform.Translate(Vector2.down * panSpeed * Time.deltaTime, Space.World);
            }

            // Right
            if (Input.GetKey(KeyCode.D) ||
                Input.GetKey(KeyCode.RightArrow) ||
                (contSupp.bIsMoving &&
                 (contSupp.ControllerLeftJoystickHorizontal() > 0 ||
                  contSupp.ControllerDirectionalPadHorizontal() > 0)))
            {
                transform.Translate(Vector2.right * panSpeed * Time.deltaTime, Space.World);
            }

            // Left
            if (Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.LeftArrow) ||
                (contSupp.bIsMoving &&
                    (contSupp.ControllerLeftJoystickHorizontal() < 0 ||
                     contSupp.ControllerDirectionalPadHorizontal() < 0)))
            {
                transform.Translate(Vector2.left * panSpeed * Time.deltaTime, Space.World);
            }

            // Need to handle virtual joystick movement seperately, i.e. too jarring otherwise
            if (touchConts.leftFixedJoystick.bJoying)
                VirtualJoystickMove();

            CalcPosition();

            // Click and drag (mouse)
            if (!devDetect.bIsMobile)
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
            
                if (Input.GetMouseButtonDown(0) ||
                    Input.GetMouseButtonDown(1))
                    lastPosition = Input.mousePosition;

                if (Input.GetMouseButton(0) ||
                    Input.GetMouseButton(1))
                {
                    Vector3 delta = Input.mousePosition - lastPosition;
                    transform.Translate(-delta.x * mouseSensitivity, -delta.y * mouseSensitivity, -10);
                    lastPosition = Input.mousePosition;
                }
            }

            // Click and drag (touch)
            if (Input.touchCount == 1)
            {
                if (tPlacer.CheckMobileAndGUIAndBail(Input.GetTouch(0).position))
                    return;

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                    lastPosition = new Vector3(
                        Input.GetTouch(0).position.x,
                        Input.GetTouch(0).position.y,
                        0);

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector3 delta = new Vector3(Input.GetTouch(0).position.x,
                        Input.GetTouch(0).position.y,
                        1) - lastPosition;
                    transform.Translate(-delta.x * mobileSensitivity, -delta.y * mobileSensitivity, -10);
                    lastPosition = Input.mousePosition;
                }
            }

            // Pinch and zoom
            if (!cMan.bAvoidCamScroll && 
                Input.touchCount == 2)
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
                if (gameObject.GetComponent<Camera>().orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    gameObject.GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                    // Make sure the orthographic size never drops below zero.
                    gameObject.GetComponent<Camera>().orthographicSize = 
                        Mathf.Clamp(gameObject.GetComponent<Camera>().orthographicSize, 5f, 50f);
                }
                // DC 02/22/2019 -- This "should" never run
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    gameObject.GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    gameObject.GetComponent<Camera>().fieldOfView = 
                        Mathf.Clamp(gameObject.GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
                }
            }
        }

        // Mouse
        if (!cMan.bAvoidCamScroll &&
            (Input.GetAxis("Mouse ScrollWheel") != 0 ||
             touchConts.rightFixedJoystick.Vertical != 0))
        {
            float scroll = 0;

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
                scroll = Input.GetAxis("Mouse ScrollWheel");
            else
                scroll = touchConts.rightFixedJoystick.Vertical * 0.05f;

            // TODO - Investigate why mobile "locks" up on the zoom

            // Zoom In
            if (scroll > 0 &&
                GetComponent<Camera>().orthographicSize > 5)
                GetComponent<Camera>().orthographicSize -=
                    scroll * 100 * scrollSpeed * Time.deltaTime;

            // Zoom Out
            if (scroll < 0 &&
                GetComponent<Camera>().orthographicSize < 50)
                GetComponent<Camera>().orthographicSize -=
                    scroll * 100 * scrollSpeed * Time.deltaTime;

            if (GetComponent<Camera>().orthographicSize < 0)
                GetComponent<Camera>().orthographicSize = 5f;
            else if (GetComponent<Camera>().orthographicSize > 50)
                     GetComponent<Camera>().orthographicSize = 50f;
        }

        // Controller
        if (!cMan.bAvoidCamScroll &&
            //Input.GetAxis("Controller Right Trigger") > 0)
            contSupp.ControllerTriggerRight() > 0)
        {
            //float scroll = Input.GetAxis("Controller Right Trigger");
            float scroll = contSupp.ControllerTriggerRight();

            // Zoom In
            if (scroll > 0 &&
                GetComponent<Camera>().orthographicSize > 5)
                GetComponent<Camera>().orthographicSize -=
                    scroll * 5 * scrollSpeed * Time.deltaTime;

            if (GetComponent<Camera>().orthographicSize < 0)
                GetComponent<Camera>().orthographicSize = 5f;
        }

        // Controller
        if (!cMan.bAvoidCamScroll &&
            //Input.GetAxis("Controller Left Trigger") > 0)
            contSupp.ControllerTriggerLeft() > 0)
        {
            //float scroll = Input.GetAxis("Controller Left Trigger");
            float scroll = contSupp.ControllerTriggerLeft();

            // Zoom Out
            if (scroll > 0 &&
                GetComponent<Camera>().orthographicSize < 50)
                GetComponent<Camera>().orthographicSize +=
                    scroll * 5 * scrollSpeed * Time.deltaTime;

            if (GetComponent<Camera>().orthographicSize > 50)
                GetComponent<Camera>().orthographicSize = 50f;
        }
    }

    public void VirtualJoystickMove()
    {
        // Up
        if (touchConts.leftFixedJoystick.Vertical > 0)
            transform.Translate(Vector2.up * (panSpeed * 0.5f) * Time.deltaTime, Space.World);

        // Down
        if (touchConts.leftFixedJoystick.Vertical < 0)
            transform.Translate(Vector2.down * (panSpeed * 0.5f) * Time.deltaTime, Space.World);

        // Right
        if (touchConts.leftFixedJoystick.Horizontal > 0)
            transform.Translate(Vector2.right * (panSpeed * 0.5f) * Time.deltaTime, Space.World);

        // Left
        if (touchConts.leftFixedJoystick.Horizontal < 0)
            transform.Translate(Vector2.left * (panSpeed * 0.5f) * Time.deltaTime, Space.World);

        CalcPosition();
    }

    public void CalcPosition()
    {
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(transform.position.x, 5f, 65f);
        currentPos.y = Mathf.Clamp(transform.position.y, -100f, -5f);
        currentPos.z = -10f;
        transform.position = currentPos;
    }
}