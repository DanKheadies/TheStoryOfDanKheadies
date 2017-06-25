// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/25/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Overworld Camera with bounds for each area
public class CameraFollow : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Camera myCam;
    public GameObject player;

    public bool bHome = false;
    public bool bField = false;
    public bool bFarm = false;
    public bool bPlay = false;
    public bool bCampus = false;

    public bool bUpdateOn = true;

    public float smoothTime = 0.2f;

    public Vector2 minCamPos;
    public Vector2 maxCamPos;
    private Vector2 smoothVelocity = new Vector2(0.2f, 0.2f);

    void Start ()
    {
        aspectUtil = GetComponent<AspectUtility>();
        myCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
         
        bHome = true;

        // Camera 'bounding-box'
        minCamPos = new Vector2(1.305f, 1.14f);
        maxCamPos = new Vector2(3.815f, 3.98f);

        // Size w/ respect to AspectUtility.cs
        myCam.orthographicSize = aspectUtil._wantedAspectRatio;
    }

    void Update ()
    {
        if (bUpdateOn)
        {
            // Temp: Update Camera display / aspect ratio
            if (Input.GetKeyUp(KeyCode.R))
            {
                aspectUtil.Awake();
            }

            // Camera follows the player with a slight delay 
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothVelocity.x, smoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothVelocity.y, smoothTime);
            transform.position = new Vector3(posX, posY, -10);

            // Camera bounds per area
            // Note: Float multiplier is macro-position away from bottom left (origin)
            if (bHome)
            {
                transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    (minCamPos.x + 5.12f * 0.0f),
                    (maxCamPos.x + 5.12f * 0.0f)),
                Mathf.Clamp(
                    transform.position.y,
                    (minCamPos.y + 5.12f * 0.0f),
                    (maxCamPos.y + 5.12f * 0.0f)),
                -10);
            }
            else if (bField)
            {
                transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    (minCamPos.x + 5.12f * 1.0f),
                    (maxCamPos.x + 5.12f * 1.0f)),
                Mathf.Clamp(
                    transform.position.y,
                    (minCamPos.y + 5.12f * 0.0f),
                    (maxCamPos.y + 5.12f * 0.0f)),
                -10);
            }
            else if (bFarm)
            {
                transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    (minCamPos.x + 5.12f * 2.0f),
                    (maxCamPos.x + 5.12f * 2.0f)),
                Mathf.Clamp(
                    transform.position.y,
                    (minCamPos.y + 5.12f * 0.0f),
                    (maxCamPos.y + 5.12f * 0.0f)),
                -10);
            }
            else if (bFarm)
            {
                transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    (minCamPos.x + 5.12f * 2.0f),
                    (maxCamPos.x + 5.12f * 2.0f)),
                Mathf.Clamp(
                    transform.position.y,
                    (minCamPos.y + 5.12f * 0.0f),
                    (maxCamPos.y + 5.12f * 0.0f)),
                -10);
            }
            else if (bPlay)
            {
                transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    (minCamPos.x + 5.12f * 0.0f),
                    (maxCamPos.x + 5.12f * 0.0f)),
                Mathf.Clamp(
                    transform.position.y,
                    (minCamPos.y + 5.12f * 1.0f),
                    (maxCamPos.y + 5.12f * 1.0f)),
                -10);
            }
            else if (bCampus)
            {
                transform.position = new Vector3(
                Mathf.Clamp(
                    transform.position.x,
                    (minCamPos.x + 5.12f * 3.0f),
                    (maxCamPos.x + 5.12f * 4.0f)),
                Mathf.Clamp(
                    transform.position.y,
                    (minCamPos.y + 5.12f * 0.0f),
                    (maxCamPos.y + 5.12f * 1.0f)),
                -10);
            }
            else
            {
                transform.position = new Vector3(
                    transform.position.x,
                    transform.position.y,
                    -10
                );
            }
        }
    }
}
