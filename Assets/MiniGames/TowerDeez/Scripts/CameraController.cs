// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 08/12/2016
// Last:  04/26/2021

using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 15f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    void Update()
    {
        if (GameManagement.IsGameOver ||
            GameManagement.IsLevelWon)
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
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        // Down
        if (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.DownArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.y <= panBorderThickness
#endif
        )
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        
        // Right
        if (Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.RightArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.x >= Screen.width - panBorderThickness
#endif
        )
        { 
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        // Left
        if (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftArrow)
#if !UNITY_EDITOR
            || Input.mousePosition.x <= panBorderThickness
#endif
        )
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // DC TODO: clamped MIN (and prob MAX) based of zoom-level (i.e. polynomial?)
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(transform.position.x, 0f, 80f);
        currentPos.z = Mathf.Clamp(transform.position.z, -50f, 70f);
        transform.position = currentPos;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
