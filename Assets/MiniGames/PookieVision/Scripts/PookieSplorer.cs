﻿// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Martijn / The Art of Code
// Contributors: David W. Corso
// Start: 08/03/2019
// Last:  08/04/2019

using UnityEngine;

public class PookieSplorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle, color, symmetry;

    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;

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
        //mat.SetFloat("_Color", smoothColor);
        mat.SetFloat("_Color", color);
        mat.SetFloat("_Symmetry", symmetry);

        // if (scale > 100,00) { scale = 2.268991e-08; }
        // .00000000001
    }

    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.I) ||
            (Input.GetAxis("Controller Right Trigger") > 0))
        {
            scale *= .99f;
        }
        else if (Input.GetKey(KeyCode.K) ||
                 (Input.GetAxis("Controller Left Trigger") > 0))
        {
            scale *= 1.01f;
        }

        if (Input.GetKey(KeyCode.J) ||
            Input.GetButton("Controller Left Bumper"))
        {
            angle -= 0.01f;
        }
        else if (Input.GetKey(KeyCode.L) ||
                 Input.GetButton("Controller Right Bumper"))
        {
            angle += 0.01f;
        }

        Vector2 dir = new Vector2(0.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x * c, dir.x * s);

        if (Input.GetKey(KeyCode.A) ||
            Input.GetAxis("Controller Joystick Horizontal") < 0 ||
            Input.GetAxis("Controller DPad Horizontal") < 0)
        {
            pos -= dir;
        }
        else if (Input.GetKey(KeyCode.D) ||
                 Input.GetAxis("Controller Joystick Horizontal") > 0 ||
                 Input.GetAxis("Controller DPad Horizontal") > 0)
        {
            pos += dir;
        }

        dir = new Vector2(-dir.y, dir.x);

        if (Input.GetKey(KeyCode.S) ||
            Input.GetAxis("Controller Joystick Vertical") < 0 ||
            Input.GetAxis("Controller DPad Vertical") > 0)
        {
            pos -= dir;
        }
        else if (Input.GetKey(KeyCode.W) ||
                 Input.GetAxis("Controller Joystick Vertical") > 0 ||
                 Input.GetAxis("Controller DPad Vertical") < 0)
        {
            pos += dir;
        }


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
    }

    void FixedUpdate()
    {
        HandleInputs();
        UpdateShader();
    }
}
