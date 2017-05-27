using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    Camera myCam;

    public GameObject player;

    private Vector2 velocity;
    public float smoothTimeX = 0.2f;
    public float smoothTimeY = 0.2f;

    public bool bHome = false;
    public Vector2 homeMinCamPos;
    public Vector2 homeMaxCamPos;

    public bool bField = false;
    Vector2 fieldMinCamPos;
    Vector2 fieldMaxCamPos;

    public bool bFarm = false;
    Vector2 farmMinCamPos;
    Vector2 farmMaxCamPos;

    public bool bPlay = false;
    Vector2 playMinCamPos;
    Vector2 playMaxCamPos;

    public bool bCampus = false;
    Vector2 campusMinCamPos;
    Vector2 campusMaxCamPos;



    void Start () {
        myCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
         
        bHome = true;

        //homeMinCamPos = new Vector2(homeMinCamPos.x * ((Screen.width / 100.0f) / 4f), homeMinCamPos.y);
        homeMinCamPos = new Vector2(homeMinCamPos.x, homeMinCamPos.y);

        //Screen.SetResolution(100, 80, true);
    }

    void Update () {
        //myCam.orthographicSize = (Screen.height / 100.0f) / 4.0f;
        myCam.orthographicSize = 0.66f;

        //if (Screen.width >= 1200)
        //{
        //    myCam.orthographicSize = (Screen.height / 100.0f) / 12.0f;
        //}
        //else if (Screen.width < 1200 && Screen.width >= 600)
        //{
        //    myCam.orthographicSize = (Screen.height / 100.0f) / 8.0f;
        //}
        //else if (Screen.width < 600)
        //{
        //    myCam.orthographicSize = (Screen.height / 100.0f) / 4.0f;
        //}

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, -10);

        if (bHome)
        {
            transform.position = new Vector3(
            Mathf.Clamp(
                transform.position.x,
                ( //minX based on camera / screen width
                  //0.00000004396f * (Screen.width * Screen.width) + 0.002452f * Screen.width
                  //0.0f + 1.0f * 0.66f
                  //1.0f * 0.66f
                    homeMinCamPos.x
                ),
                ( //maxX based on camera / screen width
                  //-0.00000004396f * (Screen.width * Screen.width) - 0.002452f * Screen.width + 5.11f /*6.9895f*/
                  //5.12f - 1.0f * 0.66f
                  //5.12f - 1.0f * 0.66f
                    homeMaxCamPos.x
                )),
            Mathf.Clamp(
                transform.position.y,
                ( //minY based on camera / screen height
                  //0.0025f * Screen.height
                  //0.0f + 1.0f * 0.66f
                  //0.00165f * Screen.height // height = 400
                    homeMinCamPos.y
                ),
                ( //maxY based on camera / screen height
                  //-0.0025f * Screen.height + 5.11f
                  //5.12f - 1.0f * 0.66f
                  // 4.46
                  //0.01115f * Screen.height // height = 400
                    homeMaxCamPos.y
                )
                ),
            -10);
        } else if (bField)
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
        } else if (bFarm)
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
            Debug.Log("("+Screen.width+","+Screen.height+")");
            //Debug.Log("MinX: " + myCam.transform.position.x);
            //Debug.Log("MinY: " + myCam.transform.position.y);
            //Debug.Log("MaxX: " + homeMaxCamPos.x);
            //Debug.Log("MaxY: " + homeMaxCamPos.y);
        }
    }
}
