// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  02/25/2020

using UnityEngine;
using UnityEngine.SceneManagement;

// Camera follows player with bounds for each scene / area
public class CameraFollow : MonoBehaviour
{
    public AspectUtility aspectUtil;
    public Camera myCam;
    public ControllerSupport contSupp;
    public GameObject player;
    public Scene scene;
    public Vector2 minCamPos;
    public Vector2 maxCamPos;
    public Vector2 smoothVelocity;
    
    public bool bUpdateOn;

    [System.Flags]
    public enum AnandaCoords : int
    {
        // None = 0,
        BatteryNE = 1,
        BatteryNW = 2,
        BatterySE = 3,
        BatterySW = 4,
        BuildersNE = 5,
        BuildersNW = 6,
        BuildersSE = 7,
        BuildersSW = 8,
        Campus = 9,
        CannaFieldNW = 10,
        CannaFieldSE = 11,
        CannaFieldSW = 12,
        CannaHouse = 13,
        FarmNW = 14,
        FarmNC = 15,
        FarmNE = 16,
        FarmWC = 17,
        FarmCC = 18,
        FarmEC = 19,
        FarmSW = 20,
        FarmSC = 21,
        FarmSE = 22,
        Home = 23,
        HousesE = 24,
        HousesN = 25,
        HousesS = 26,
        HousesW = 27,
        Lake = 28,
        PlaygroundE = 29,
        PlaygroundN = 30,
        PlaygroundS = 31,
        PlaygroundW = 32,
        RaceTrackE = 33,
        River = 34,
        WoodsW = 35,
        WoodsWSecret = 36
    };

    public AnandaCoords currentCoords;

    public float depth;
    public float minCamX;
    public float minCamY;
    public float maxCamX;
    public float maxCamY;
    public float posX;
    public float posY;
    public float smoothTime;

    void Start ()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        // Camera 'bounding-box'
        minCamPos = new Vector2(1.305f, 1.14f);
        maxCamPos = new Vector2(3.815f, 3.98f);

        // Size w/ respect to AspectUtility.cs
        myCam.orthographicSize = aspectUtil._wantedAspectRatio;
        
        bUpdateOn = true;

        depth = -10f;

        smoothTime = 0.2f;
        smoothVelocity = new Vector2(0.2f, 0.2f);
    }

    void Update ()
    {
        // Temp: Update Camera display / aspect ratio
        if (Input.GetKeyUp(KeyCode.R) ||
            contSupp.ControllerMenuLeft("up"))
        {
            aspectUtil.Awake();
        }

        if (scene.name == "GuessWhoColluded")
        {
            // Camera stays w/in the bounds set here while following the player
            minCamX = 1.053f * myCam.orthographicSize - 0.9398f;
            minCamY = 0.6316f * myCam.orthographicSize - 6.564f;
            maxCamX = -1.053f * myCam.orthographicSize + 10.94f;
            maxCamY = -0.6316f * myCam.orthographicSize + 0.5639f;

            posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothVelocity.x, smoothTime);
            posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothVelocity.y, smoothTime);
            transform.position = new Vector3(
                Mathf.Clamp(posX, minCamX, maxCamX),
                Mathf.Clamp(posY, minCamY, maxCamY),
                depth);
        }
        else if (scene.name == "Minesweeper")
        {
            posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothVelocity.x, smoothTime);
            posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothVelocity.y, smoothTime);
            transform.position = new Vector3(posX, posY, depth);
        }
        else if (myCam.GetComponent<CameraSlider>())
        {
            if (!myCam.GetComponent<CameraSlider>().bIsSliding)
            {
                // Camera follows the player with a slight delay 
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref smoothVelocity.x, smoothTime);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref smoothVelocity.y, smoothTime);
                transform.position = new Vector3(posX, posY, depth);

                // Camera bounds per area
                // Areas listed alphabetically
                // Note: Float multiplier is macro-position away from bottom left (origin)
                // DC 09/25/2017 -- TODO: Possible performance upgrade based on most visited at the top
                if (currentCoords == AnandaCoords.BatteryNE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 3.0f),
                        (maxCamPos.x + 5.12f * 3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 0.0f),
                        (maxCamPos.y + 5.12f * 0.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.BatteryNW)
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
                    depth);
                }
                else if (currentCoords == AnandaCoords.BatterySE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 3.0f),
                        (maxCamPos.x + 5.12f * 3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -1.0f),
                        (maxCamPos.y + 5.12f * -1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.BatterySW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 2.0f),
                        (maxCamPos.x + 5.12f * 2.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -1.0f),
                        (maxCamPos.y + 5.12f * -1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.BuildersNE)
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
                    depth);
                }
                else if (currentCoords == AnandaCoords.BuildersNW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -1.0f),
                        (maxCamPos.x + 5.12f * -1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 1.0f),
                        (maxCamPos.y + 5.12f * 1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.BuildersSE)
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
                    depth);
                }
                else if (currentCoords == AnandaCoords.BuildersSW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -1.0f),
                        (maxCamPos.x + 5.12f * -1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 0.0f),
                        (maxCamPos.y + 5.12f * 0.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.Campus)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 0.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -2.0f),
                        (maxCamPos.y + 5.12f * -1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.CannaFieldNW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -2.0f),
                        (maxCamPos.x + 5.12f * -2.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -2.0f),
                        (maxCamPos.y + 5.12f * -2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.CannaFieldSE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -1.0f),
                        (maxCamPos.x + 5.12f * -1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -3.0f),
                        (maxCamPos.y + 5.12f * -3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.CannaFieldSW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -2.0f),
                        (maxCamPos.x + 5.12f * -2.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -3.0f),
                        (maxCamPos.y + 5.12f * -3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.CannaHouse)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -1.0f),
                        (maxCamPos.x + 5.12f * -1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -2.0f),
                        (maxCamPos.y + 5.12f * -2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmNW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 1.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -3.0f),
                        (maxCamPos.y + 5.12f * -3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmNC)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 2.0f),
                        (maxCamPos.x + 5.12f * 2.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -3.0f),
                        (maxCamPos.y + 5.12f * -3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmNE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 3.0f),
                        (maxCamPos.x + 5.12f * 3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -3.0f),
                        (maxCamPos.y + 5.12f * -3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmWC)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 1.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -4.0f),
                        (maxCamPos.y + 5.12f * -4.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmCC)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 2.0f),
                        (maxCamPos.x + 5.12f * 2.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -4.0f),
                        (maxCamPos.y + 5.12f * -4.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmEC)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 3.0f),
                        (maxCamPos.x + 5.12f * 3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -4.0f),
                        (maxCamPos.y + 5.12f * -4.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmSW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 1.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -5.0f),
                        (maxCamPos.y + 5.12f * -5.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmSC)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 2.0f),
                        (maxCamPos.x + 5.12f * 2.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -5.0f),
                        (maxCamPos.y + 5.12f * -5.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.FarmSE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 3.0f),
                        (maxCamPos.x + 5.12f * 3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -5.0f),
                        (maxCamPos.y + 5.12f * -5.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.Home)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -3.0f),
                        (maxCamPos.x + 5.12f * -3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -2.0f),
                        (maxCamPos.y + 5.12f * -2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.HousesE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 4.0f),
                        (maxCamPos.x + 5.12f * 4.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -1.0f),
                        (maxCamPos.y + 5.12f * -1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.HousesN)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 0.0f),
                        (maxCamPos.x + 5.12f * 0.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 2.0f),
                        (maxCamPos.y + 5.12f * 2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.HousesS)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 1.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -6.0f),
                        (maxCamPos.y + 5.12f * -6.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.HousesW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -4.0f),
                        (maxCamPos.x + 5.12f * -4.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -2.0f),
                        (maxCamPos.y + 5.12f * -2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.Lake)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 1.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 3.0f),
                        (maxCamPos.y + 5.12f * 3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.PlaygroundE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 5.0f),
                        (maxCamPos.x + 5.12f * 5.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -1.0f),
                        (maxCamPos.y + 5.12f * -1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.PlaygroundN)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 1.0f),
                        (maxCamPos.x + 5.12f * 1.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 2.0f),
                        (maxCamPos.y + 5.12f * 2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.PlaygroundS)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 0.0f),
                        (maxCamPos.x + 5.12f * 0.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -6.0f),
                        (maxCamPos.y + 5.12f * -6.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.PlaygroundW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -3.0f),
                        (maxCamPos.x + 5.12f * -3.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -1.0f),
                        (maxCamPos.y + 5.12f * -1.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.RaceTrackE)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 5.0f),
                        (maxCamPos.x + 5.12f * 5.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -2.0f),
                        (maxCamPos.y + 5.12f * -2.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.River)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * 0.0f),
                        (maxCamPos.x + 5.12f * 0.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 3.0f),
                        (maxCamPos.y + 5.12f * 3.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.WoodsW)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -5.0f),
                        (maxCamPos.x + 5.12f * -4.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * -1.0f),
                        (maxCamPos.y + 5.12f * 0.0f)),
                    depth);
                }
                else if (currentCoords == AnandaCoords.WoodsWSecret)
                {
                    transform.position = new Vector3(
                    Mathf.Clamp(
                        transform.position.x,
                        (minCamPos.x + 5.12f * -4.0f),
                        (maxCamPos.x + 5.12f * -4.0f)),
                    Mathf.Clamp(
                        transform.position.y,
                        (minCamPos.y + 5.12f * 1.0f),
                        (maxCamPos.y + 5.12f * 1.0f)),
                    depth);
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
}
