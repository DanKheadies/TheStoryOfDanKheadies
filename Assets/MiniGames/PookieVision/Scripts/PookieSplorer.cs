// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Martijn / The Art of Code
// Contributors: David W. Corso
// Start: 08/03/2019
// Last:  08/04/2019

using UnityEngine;

public class PookieSplorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle, color, symmetry, repeat, speed;
    public bool bCycling;

    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;

    private void Start()
    {
        color = 0.5f;
        repeat = 10f;
        speed = 0.45f;
    }

    private void UpdateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, 0.0333f);
        smoothScale = Mathf.Lerp(smoothScale, scale, 0.0333f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, 0.0333f);

        float aspect = Screen.width / Screen.height;

        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1)
        {
            scaleY /= aspect;
        }
        else
        {
            scaleX *= aspect;
        }

        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        mat.SetFloat("_Angle", smoothAngle);
        mat.SetFloat("_Color", color);
        mat.SetFloat("_Repeat", repeat);
        mat.SetFloat("_Speed", speed);
        mat.SetFloat("_Symmetry", symmetry);
    }

    private void HandleInputs()
    {
        // Zoom Out
        if (Input.GetKey(KeyCode.I) ||
            (Input.GetAxis("Controller Right Trigger") > 0))
        {
            if (scale > 0.00000001f)
            {
                scale *= .99f;
            }
            // Jump to max zoomed-out level
            else
            {
                scale = 75000f;
            }
        }
        // Zoom In
        else if (Input.GetKey(KeyCode.K) ||
                 (Input.GetAxis("Controller Left Trigger") > 0))
        {
            if (scale < 75000f)
            {
                scale *= 1.01f;
            }
            // Jump to max zoomed-in level
            else
            {
                scale = 0.00000001f;
                smoothScale = Mathf.Lerp(scale, scale, 0.0333f);
                pos.x = -0.7500075f;
                pos.y = 0.003150068f;
                mat.SetVector("_Area", new Vector4(pos.x, pos.y, scale, scale));
            }
        }

        // Rotate Left
        if (Input.GetKey(KeyCode.J) ||
            Input.GetButton("Controller Left Bumper"))
        {
            angle -= 0.01f;
        }
        // Rotate Right
        else if (Input.GetKey(KeyCode.L) ||
                 Input.GetButton("Controller Right Bumper"))
        {
            angle += 0.01f;
        }

        Vector2 dir = new Vector2(0.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x * c, dir.x * s);

        // Move Left
        if (Input.GetKey(KeyCode.A) ||
            Input.GetAxis("Controller Joystick Horizontal") < 0 ||
            Input.GetAxis("Controller DPad Horizontal") < 0)
        {
            pos -= dir;
        }
        // Move Right
        else if (Input.GetKey(KeyCode.D) ||
                 Input.GetAxis("Controller Joystick Horizontal") > 0 ||
                 Input.GetAxis("Controller DPad Horizontal") > 0)
        {
            pos += dir;
        }

        dir = new Vector2(-dir.y, dir.x);

        // Move Down
        if (Input.GetKey(KeyCode.S) ||
            Input.GetAxis("Controller Joystick Vertical") < 0 ||
            Input.GetAxis("Controller DPad Vertical") > 0)
        {
            pos -= dir;
        }
        // Move Up
        else if (Input.GetKey(KeyCode.W) ||
                 Input.GetAxis("Controller Joystick Vertical") > 0 ||
                 Input.GetAxis("Controller DPad Vertical") < 0)
        {
            pos += dir;
        }

        // Cycle Colors
        if (Input.GetKey(KeyCode.U) ||
            Input.GetButton("Controller Top Button"))
        {
            if (color > 0)
            {
                color -= 0.01f;
            }
        }
        else if (Input.GetKey(KeyCode.O) ||
                 Input.GetButton("Controller Left Button"))
        {
            if (color < 1)
            {
                color += 0.01f;
            }
        }

        // Cycle 
        if (Input.GetKey(KeyCode.E) ||
            Input.GetButton("Controller Right Button"))
        {
            if (symmetry > 0)
            {
                symmetry -= 0.1f;
            }
        }
        else if (Input.GetKey(KeyCode.Q) ||
                 Input.GetButton("Controller Bottom Button"))
        {
            if (symmetry < 1)
            {
                symmetry += 0.1f;
            }
        }

        if (Input.GetKey(KeyCode.R) ||
            Input.GetButton("Controller Select Button"))
        {
            ResetShader();
        }
    }

    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }

    void ResetShader()
    {
        pos.x = -0.82f;
        pos.y = 0.011675f;
        scale = 4.083985f;
        angle = 1.575f;
        color = 0.5f;
        repeat = 10f;
        symmetry = 0f;
    }
}
