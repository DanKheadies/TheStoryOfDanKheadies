﻿// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/10/2016
// Last:  09/16/2019

using UnityEngine;

public class TD_SBF_CameraController : MonoBehaviour
{
    public TD_SBF_ControlManagement cMan;

    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 15f;
    public float scrollSpeed = 5f;

    void Update()
    {
        if (TD_SBF_GameManagement.IsGameOver ||
            TD_SBF_GameManagement.IsLevelWon)
        {
            enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
            doMovement = !doMovement;
        if (!doMovement)
            return;

        // Up
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.UpArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.y >= Screen.height - panBorderThickness
#endif
        )
        {
            transform.Translate(Vector2.up * panSpeed * Time.deltaTime, Space.World);
        }

        // Down
        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.y <= panBorderThickness
#endif
        )
        {
            transform.Translate(Vector2.down * panSpeed * Time.deltaTime, Space.World);
        }

        // Right
        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.x >= Screen.width - panBorderThickness
#endif
        )
        {
            transform.Translate(Vector2.right * panSpeed * Time.deltaTime, Space.World);
        }

        // Left
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.x <= panBorderThickness
#endif
        )
        {
            transform.Translate(Vector2.left * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(transform.position.x, 0f, 130f);
        currentPos.y = Mathf.Clamp(transform.position.y, -150f, 0f);
        currentPos.z = -10f;
        transform.position = currentPos;

        if (Input.GetAxis("Mouse ScrollWheel") != 0 &&
            !cMan.bAvoidCamScroll)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            // Zoom In
            if (scroll > 0 &&
                GetComponent<Camera>().orthographicSize > 5)
                GetComponent<Camera>().orthographicSize -=
                    scroll * 250 * scrollSpeed * Time.deltaTime;

            // Zoom Out
            if (scroll < 0 &&
                GetComponent<Camera>().orthographicSize < 50)
                GetComponent<Camera>().orthographicSize -=
                    scroll * 250 * scrollSpeed * Time.deltaTime;

            if (GetComponent<Camera>().orthographicSize < 0)
                GetComponent<Camera>().orthographicSize = 5f;
        }
    }
}
