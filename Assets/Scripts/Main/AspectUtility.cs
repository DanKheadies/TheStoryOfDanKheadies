﻿// CC 3.0 International License: Attribution--Eric5h5--ShareALike (https://creativecommons.org/licenses/by-sa/3.0/)
// Authors: Eric Haines
// Contributors: David W. Corso
// Start: --/--/----
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.SceneManagement;

public class AspectUtility : MonoBehaviour
{
    public static Camera cam;
    public static Camera backgroundCam;
    public Scene scene;

    static float wantedAspectRatio;
    public float _wantedAspectRatio;

    // Call to refresh the camera / screen display
    public void Awake()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();
        cam = GetComponent<Camera>();

        if (scene.name == "Minesweeper")
            _wantedAspectRatio = 2.285714f;
        else if (scene.name == "Immunis")
            _wantedAspectRatio = 2.285714f;
        else if (scene.name == "PookieVision")
            _wantedAspectRatio = 1f;
        else
            _wantedAspectRatio = 1.142857f;

        wantedAspectRatio = 1.142857f;

        if (!cam)
            cam = Camera.main;

        if (!cam)
        {
            Debug.LogError("No camera available");
            return;
        }
        
        SetCamera();
    }

    public static void SetCamera()
    {
        float currentAspectRatio = (float)Screen.width / Screen.height;
        // If the current aspect ratio is already approximately equal to the desired aspect ratio,
        // use a full-screen Rect (in case it was set to something else previously)
        if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f)
        {
            cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            if (backgroundCam)
            {
                Destroy(backgroundCam.gameObject);
            }
            return;
        }
        // Pillarbox (i.e. Width > Height)
        if (currentAspectRatio > wantedAspectRatio)
        {
            float inset = 1.0f - wantedAspectRatio / currentAspectRatio;
            cam.rect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
        }
        // Letterbox (i.e. Width < Height)
        else
        {
            float inset = 1.0f - currentAspectRatio / wantedAspectRatio;
            // Option A - Middle of Screen
            // cam.rect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset); 
            // Option B - Top of Screen
            cam.rect = new Rect(0.0f, inset - 0.1f, 1.0f, 1.0f - inset);
        }
        if (!backgroundCam)
        {
            // Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
            backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
            backgroundCam.depth = int.MinValue;
            backgroundCam.clearFlags = CameraClearFlags.SolidColor;
            backgroundCam.backgroundColor = Color.black;
            backgroundCam.cullingMask = 0;

            // TODO: setting images / flavor in the background
            //if (SceneManager.GetActiveScene().name == "GuessWhoColluded")
            //    backgroundCam.backgroundColor = Color.black;
            //else
            //    backgroundCam.backgroundColor = new Color(38f / 255f, 48f / 255f, 38f / 255f);
        }
    }

    public static int screenHeight
    {
        get
        {
            return (int)(Screen.height * cam.rect.height);
        }
    }

    public static int screenWidth
    {
        get
        {
            return (int)(Screen.width * cam.rect.width);
        }
    }

    public static int xOffset
    {
        get
        {
            return (int)(Screen.width * cam.rect.x);
        }
    }

    public static int yOffset
    {
        get
        {
            return (int)(Screen.height * cam.rect.y);
        }
    }

    public static Rect screenRect
    {
        get
        {
            return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width, cam.rect.height * Screen.height);
        }
    }

    public static Vector3 mousePosition
    {
        get
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.y -= (int)(cam.rect.y * Screen.height);
            mousePos.x -= (int)(cam.rect.x * Screen.width);
            return mousePos;
        }
    }

    public static Vector2 guiMousePosition
    {
        get
        {
            Vector2 mousePos = Event.current.mousePosition;
            mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height, cam.rect.y * Screen.height + cam.rect.height * Screen.height);
            mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width, cam.rect.x * Screen.width + cam.rect.width * Screen.width);
            return mousePos;
        }
    }
}