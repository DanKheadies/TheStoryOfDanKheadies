using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Camera myCam;

    public GameObject player;

    private Vector2 velocity;
    public float smoothTimeX = 0.2f;
    public float smoothTimeY = 0.2f;

    public bool sandbox3Bounds = false;
    public Vector2 sb3MinCamPos;
    public Vector2 sb3MaxCamPos;

    public bool sandbox4Bounds = false;
    public Vector2 sb4MinCamPos;
    public Vector2 sb4MaxCamPos;

    public bool sandbox5Bounds = false;


    void Start () {
        myCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
         
        sandbox3Bounds = true;

        sb3MinCamPos = new Vector2(sb3MinCamPos.x * ((Screen.width / 100.0f) / 4f), sb3MinCamPos.y);
    }

    void Update () {
        myCam.orthographicSize = (Screen.height / 100.0f) / 4f;

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, -10);

        if (sandbox3Bounds)
        {
            transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                ( //minX based on camera / screen width
                    0.00000004396f * (Screen.width * Screen.width) + 0.002452f * Screen.width
                ),
                ( //maxX based on camera / screen width
                    -0.00000004396f * (Screen.width * Screen.width) - 0.002452f * Screen.width + 5.11f /*6.9895f*/
                )),
            Mathf.Clamp(
                transform.position.y,
                ( //minY based on camera / screen height
                    0.0025f * Screen.height
                ),
                ( //maxY based on camera / screen height
                    -0.0025f * Screen.height + 5.11f
                )
                ),
            -10);
        } else if (sandbox4Bounds)
        {
            transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                ( //minX based on camera / screen width
                    0.00000004396f * (Screen.width * Screen.width) + 0.002452f * Screen.width + 0.2905f + 6.94f
                ),
                ( //maxX based on camera / screen width
                    -0.00000004396f * (Screen.width * Screen.width) - 0.002452f * Screen.width + 6.9895f + 6.94f
                )),
            Mathf.Clamp(
                transform.position.y,
                ( //minY based on camera / screen height
                    0.0024f * Screen.height + 0.225f
                ),
                ( //maxY based on camera / screen height
                    -0.0025f * Screen.height + 6.9f
                )
                ),
            -10);
        } else if (sandbox5Bounds)
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                -10
            );
        }
        else
        {
            transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                -10
            );
        }

        testicle();
    }

    void testicle()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Let's get high on life and drugs!");
        }
    }
}
