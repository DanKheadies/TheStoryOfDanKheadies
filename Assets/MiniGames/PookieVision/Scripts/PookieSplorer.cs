// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Martijn / The Art of Code
// Contributors: David W. Corso
// Start: 08/03/2019
// Last:  04/26/2021

using UnityEngine;

public class PookieSplorer : MonoBehaviour
{
    public ControllerSupport contSupp;

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
            contSupp.ControllerTriggerRight() > 0)
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
                 contSupp.ControllerTriggerLeft() > 0)
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
            contSupp.ControllerBumperLeft("hold"))
        {
            angle -= 0.01f;
        }
        // Rotate Right
        else if (Input.GetKey(KeyCode.L) ||
                 contSupp.ControllerBumperRight("hold"))
        {
            angle += 0.01f;
        }

        Vector2 dir = new Vector2(0.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x * c, dir.x * s);

        // Move Left
        if (Input.GetKey(KeyCode.A) ||
            contSupp.ControllerLeftJoystickHorizontal() < 0 ||
            contSupp.ControllerDirectionalPadHorizontal() < 0)
        {
            pos -= dir;
        }
        // Move Right
        else if (Input.GetKey(KeyCode.D) ||
                 contSupp.ControllerLeftJoystickHorizontal() > 0 ||
                 contSupp.ControllerDirectionalPadHorizontal() > 0)
        {
            pos += dir;
        }

        dir = new Vector2(-dir.y, dir.x);

        // Move Down
        if (Input.GetKey(KeyCode.S) ||
            contSupp.ControllerLeftJoystickVertical() < 0 ||
            contSupp.ControllerDirectionalPadVertical() < 0)
        {
            pos -= dir;
        }
        // Move Up
        else if (Input.GetKey(KeyCode.W) ||
                 contSupp.ControllerLeftJoystickVertical() > 0 ||
                 contSupp.ControllerDirectionalPadVertical() > 0)
        {
            pos += dir;
        }

        // Cycle Colors
        if (Input.GetKey(KeyCode.U) ||
            contSupp.ControllerButtonPadTop("hold"))
        {
            if (color > 0)
                color -= 0.01f;
        }
        else if (Input.GetKey(KeyCode.O) ||
                 contSupp.ControllerButtonPadLeft("hold"))
        {
            if (color < 1)
                color += 0.01f;
        }

        // Cycle 
        if (Input.GetKey(KeyCode.E) ||
            contSupp.ControllerButtonPadRight("hold"))
        {
            if (symmetry > 0)
                symmetry -= 0.1f;

            else if (symmetry < 0)
                symmetry = 0;
        }
        else if (Input.GetKey(KeyCode.Q) ||
                 contSupp.ControllerButtonPadBottom("hold"))
        {
            if (symmetry < 1)
                symmetry += 0.1f;
        }

        if (Input.GetKey(KeyCode.R) ||
            contSupp.ControllerMenuLeft("hold"))
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
