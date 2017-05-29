using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Camera myCam;

    public GameObject player;

    private Vector2 smoothVelocity = new Vector2(0.2f, 0.2f);
    public float smoothTime = 0.2f;

    public Vector2 minCamPos;
    public Vector2 maxCamPos;

    public bool bHome = false;
    public bool bField = false;
    public bool bFarm = false;
    public bool bPlay = false;
    public bool bCampus = false;

    public bool bUpdateOn = true;

    void Start () {
        myCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
         
        bHome = true;
        minCamPos = new Vector2(1.305f, 1.14f);
        maxCamPos = new Vector2(3.815f, 3.98f);

        myCam.orthographicSize = 1.142857f;
    }

    void Update () {

        if (bUpdateOn) { 
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothVelocity.x, smoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothVelocity.y, smoothTime);

            transform.position = new Vector3(posX, posY, -10);

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
