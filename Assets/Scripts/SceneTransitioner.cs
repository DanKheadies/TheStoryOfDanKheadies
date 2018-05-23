// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Contributors: Nick Pettit
// Start: 04/20/2017
// Last:  05/23/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Transition amongst Unity Scenes
public class SceneTransitioner : MonoBehaviour
{
    public Scene scene;
    public Text sceneTitle;
    public Text sceneSubtitle;

    public bool bAvoidUpdate;
    public bool bLoadScene;
    public bool bNeedsTimer;

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
                sceneSubtitle.text = "derping yo";
                StartCoroutine(LoadNewScene());
            }

            if (bLoadScene)
            {
                // Animation
                sceneSubtitle.color = new Color(sceneSubtitle.color.r, sceneSubtitle.color.g, sceneSubtitle.color.b, Mathf.PingPong(Time.time, 1));
            }

            if (!bAvoidUpdate)
            {
                CheckScenesToLoad();

                bAvoidUpdate = true;
            }
        }
    }

    // Trigger and timer for loading screens / scenes
    private IEnumerator Start()
    {
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
            // Save the current inventory and load it up for the next scene
            GameObject.FindObjectOfType<SaveGame>().SaveInventoryTransfer();
            GameObject.FindObjectOfType<SaveGame>().SaveSceneLoadTransfer(BetaLoad);
            SceneManager.LoadScene(AlphaLoad);
        }
    }

    IEnumerator LoadNewScene()
    {
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

        if (BetaLoad == "" || BetaLoad == null)
        {
            BetaLoad = PlayerPrefs.GetString("TransferScene");
        }
    }
}
