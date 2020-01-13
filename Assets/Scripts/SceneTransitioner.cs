﻿// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Contributors: Nick Pettit
// Start: 04/20/2017
// Last:  12/13/2019

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
    public Transform smokeRings;

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
                    case "PookieVision":
                        sceneTitle.text = "Pookie Vision";
                        sceneSubtitle.text = "For the kids..";
                        break;
                    case "TD_Menu":
                        sceneTitle.text = "TowerDeez";
                        sceneSubtitle.text = "And it's nuts..";
                        break;
                    case "TD_SBF_Menu":
                        sceneTitle.text = "Super Best Friends TD";
                        sceneSubtitle.text = "Hoooooooooooo..";
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

        // DC TODO -- On load, need to set position, size, and/or scale of the SmokeRings gameobject to get it to render correctly
        // Currently too small on mobile devices (iPad), and other config settings stretch / screw it up

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

            // TODO: prob change this
            if (scene.name != "GuessWhoColluded")
            {
                collision.gameObject.GetComponent<Animator>().SetBool("bIsWalking", false);
            }

            StartCoroutine(DelayedTransition(2));
        }
    }

    public void PubTransition(float _time)
    {
        StartCoroutine(DelayedTransition(_time));
    }

    IEnumerator DelayedTransition(float _time)
    {
        yield return new WaitForSeconds(_time);

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

        // UI Image & Text Positioning and Sizing based off device size
        sceneSubtitle.fontSize = (int)(0.034090909090909f * Screen.width + 20.363636363636f);
        sceneTitle.fontSize = (int)(0.0625f * Screen.width + 20f);

        float scale = 0.0013849431818182f * Screen.width + 0.47727272727273f;
        smokeRings.localScale = new Vector3(scale, scale, 1);

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
            AlphaLoad = "SceneTransitioner";

        if (scene.name == "LogoSplash")
            return;
        else if (BetaLoad == "" || BetaLoad == null)
            BetaLoad = PlayerPrefs.GetString("TransferScene");
    }
}
