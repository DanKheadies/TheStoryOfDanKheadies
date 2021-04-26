// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/10/2016
// Last:  04/26/2021

using UnityEngine;

public class TD_SBF_CameraController : MonoBehaviour
{
    public ControllerSupport contSupp;
    public TD_SBF_ControlManagement cMan;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_TouchControls touchConts;
    
    public float panSpeed = 30f;
    public float scrollSpeed = 5f;

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