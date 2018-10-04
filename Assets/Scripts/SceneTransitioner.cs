// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Contributors: Nick Pettit
// Start: 04/20/2017
// Last:  10/04/2018

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Transition amongst Unity Scenes
public class SceneTransitioner : MonoBehaviour
{
    public Camera mainCamera;
    public Scene scene;
    public Text sceneSubtitle;
    public Text sceneTitle;

    public bool bLoadScene;
    public bool bNeedsTimer;
    public bool bAnimationToTransitionScene;
    
    private float cameraHeight;
    private float cameraWidth;
    public float timeToLoad;

    public string AlphaLoad;
    public string BetaLoad;

    void Update()
    {
        // Quick skip on loading screens / scenes
        if (scene.name == "LogoSplash" &&
            Input.anyKeyDown)
        {
            SceneManager.LoadScene(AlphaLoad);
        }

        if (scene.name == "SceneTransitioner")
        {
            if (!bLoadScene)
            {
                bLoadScene = true;

                switch (BetaLoad)
                {
                    case "Chp1":
                        sceneTitle.text = "Chapter 1";
                        sceneSubtitle.text = "In the beginning...";
                        break;
                    case "Minesweeper":
                        sceneTitle.text = "Minesweeper";
                        sceneSubtitle.text = "Boom baby...";
                        break;
                    case "GuessWhoColluded":
                        sceneTitle.text = "Guess Who";
                        sceneSubtitle.text = "Colluded...";
                        break;
                    default:
                        sceneTitle.text = "n_n";
                        sceneSubtitle.text = "Loading some scene...";
                        break;
                }

                StartCoroutine(LoadNewScene());
            }

            if (bLoadScene)
            {
                // Animation
                //sceneSubtitle.color = new Color(sceneSubtitle.color.r, sceneSubtitle.color.g, sceneSubtitle.color.b, Mathf.PingPong(Time.time, 1));
            }
        }
    }

    // Trigger and timer for loading screens / scenes
    private IEnumerator Start()
    {
        // Initializers
        scene = SceneManager.GetActiveScene();

        CheckScenesToLoad();

        if (bNeedsTimer)
        {
            yield return new WaitForSeconds(timeToLoad);

            SceneManager.LoadScene(AlphaLoad);
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Do SceneTransitioner Animation then load
            GameObject.Find("SceneTransitioner").GetComponent<Animator>().enabled = true;

            // Stops the player's movement
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            collision.gameObject.GetComponent<PlayerMovement>().bStopPlayerMovement = true;

            if (scene.name != "GuessWhoColluded")
            {
                collision.gameObject.GetComponent<Animator>().SetBool("bIsWalking", false);
            }

            StartCoroutine(DelayedTransition());
        }
    }

    IEnumerator DelayedTransition()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(AlphaLoad);
    }

    IEnumerator LoadNewScene()
    {
        // Initializers for text sizing
        mainCamera = FindObjectOfType<Camera>();
        cameraHeight = mainCamera.rect.height;
        cameraWidth = mainCamera.rect.width;
        sceneSubtitle = GameObject.Find("Scene_Subtitle").GetComponent<Text>();
        sceneTitle = GameObject.Find("Scene_Title").GetComponent<Text>();

        // UI Image & Text Positioning and Sizing based off camera size vs device size
        // Subtitle is 2/3 the size of the title
        if (Screen.width > mainCamera.pixelWidth)
        {
            // Height => change in height affects variables, so look at the width of the camera
            sceneSubtitle.fontSize = (int)((-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f) / 1.333f);
            sceneTitle.fontSize = (int)(-6.132f * (cameraWidth * cameraWidth) + 38.80f * cameraWidth + 15.76f);
        }
        else
        {
            // Width => change in width affects variables, so look at the height of the camera
            sceneSubtitle.fontSize = (int)((2.432f * (cameraHeight * cameraHeight) + 25.84f * cameraHeight + 20.05f) / 1.333f);
            sceneTitle.fontSize = (int)(2.432f * (cameraHeight * cameraHeight) + 25.84f * cameraHeight + 20.05f);
        }

        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(BetaLoad);

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void CheckScenesToLoad()
    {
        if (AlphaLoad == "" || AlphaLoad == null)
        {
            AlphaLoad = "SceneTransitioner";
        }

        if (scene.name == "LogoSplash")
        {

        }
        else if (BetaLoad == "" || BetaLoad == null)
        {
            BetaLoad = PlayerPrefs.GetString("TransferScene");
        }
    }
}
