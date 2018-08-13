// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/11/2018
// Last:  08/11/2018

using UnityEngine;

// Follows an invisible "player" for card selection and board visibility
public class CameraFollowGWC : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Camera myCam;
    public GameObject player;
    public Vector2 smoothVelocity;

    public bool bUpdateOn;

    public float smoothTime;

    public float minCamX;
    public float minCamY;
    public float maxCamX;
    public float maxCamY;


    void Start()
    {
        // Initializers
        aspectUtil = GetComponent<AspectUtility>();
        myCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Size w/ respect to AspectUtility.cs
        myCam.orthographicSize = aspectUtil._wantedAspectRatio;

        bUpdateOn = true;

        smoothTime = 0.2f;
        smoothVelocity = new Vector2(0.2f, 0.2f);
    }

    void Update()
    {
        if (bUpdateOn)
        {
            // Temp: Update Camera display / aspect ratio
            if (Input.GetKeyUp(KeyCode.R))
            {
                aspectUtil.Awake();
            }

            // Camera stays w/in the bounds set here while following the player
            minCamX = 1.053f * myCam.orthographicSize - 0.9398f;
            minCamY = 0.6316f * myCam.orthographicSize - 6.564f;
            maxCamX = -1.053f * myCam.orthographicSize + 10.94f;
            maxCamY = -0.6316f * myCam.orthographicSize + 0.5639f;

            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothVelocity.x, smoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothVelocity.y, smoothTime);
            transform.position = new Vector3(
                Mathf.Clamp(posX, minCamX, maxCamX),
                Mathf.Clamp(posY, minCamY, maxCamY),
                -10f);
        }
    }
}
