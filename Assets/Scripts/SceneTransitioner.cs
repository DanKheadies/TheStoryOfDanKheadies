// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Contributors: Nick Pettit
// Start: 04/20/2017
// Last:  02/21/2019

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
                        sceneTitle.text = "Chapter 1\n";
                        sceneSubtitle.text = "\nIn the beginning...";
                        break;
                    case "Minesweeper":
                        sceneTitle.text = "Minesweeper\n";
                        sceneSubtitle.text = "\nBoom baby...";
                        break;
                    case "GuessWhoColluded":
                        sceneTitle.text = "Guess Who\n";
                        sceneSubtitle.text = "\nColluded...";
                        break;
                    default:
                        sceneTitle.text = "n_n\n";
                        sceneSubtitle.text = "\nLoading some scene...";
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
        // DC TODO 02/18/19 -- Should base font-size (int) off width & line-height (float) off height
        //if (Screen.width <= Screen.height)
        //{
        //    // Height => change in height affects variables, so look at the height of the screen
        //    sceneSubtitle.fontSize = (int)((0.00005494f * (Screen.height * Screen.height) + 0.01675f * (Screen.width) + 14.66f) * 0.666f);
        //    sceneSubtitle.lineSpacing = (1f / 1864f) * (Screen.height) + (411f / 466f);
        //    sceneTitle.fontSize = (int)(0.00005494f * (Screen.height * Screen.height) + 0.01675f * (Screen.width) + 14.66f);
        //    sceneTitle.lineSpacing = (1f / 932f) * (Screen.height) + (178f / 233f);
        //}
        //else
        //{
        //    // Width => change in width affects variables, so look at the width of screen
        //    sceneSubtitle.fontSize = (int)((0.00006983f * (Screen.width * Screen.width) - 0.03739f * (Screen.width) + 42.37f) * 0.666f);
        //    sceneSubtitle.lineSpacing = -0.0006676f * (Screen.width) + 2.4393f;
        //    sceneTitle.fontSize = (int)(0.00006983f * (Screen.width * Screen.width) - 0.03739f * (Screen.width) + 42.37f);
        //    sceneTitle.lineSpacing = (1f / 1498f) * (Screen.width) + (60f / 107f);
        //}

        sceneSubtitle.fontSize = (int)(((46f / 1161f) * Screen.width + (7838f / 387f)) * 0.666f);
        sceneSubtitle.lineSpacing = (-5f / 4644f) * Screen.width + (4667f / 1548f);
        sceneTitle.fontSize = (int)((46f / 1161f) * Screen.width + (7838f / 387f));
        sceneTitle.lineSpacing = (-1f / 1935f) * Screen.width + (5101f / 2580f);

        //Debug.Log("W: " + Screen.width);
        // 1407
        // 76, 1.25
        // 51, 1.5

        // 246
        // 30, 1.85
        // 20, 2.75

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
