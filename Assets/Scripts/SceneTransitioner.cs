// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  03/10/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Transition amongst Unity Scenes
public class SceneTransitioner : MonoBehaviour
{
    private Scene scene;

    public bool bNeedsTimer;

    public float timeToLoad;

    public string loadLevel;
    

    void Update()
    {
        // Quick skip on loading screens / scenes
        if ((scene.name == "LogoSplash") 
            && Input.anyKeyDown)
        {
            SceneManager.LoadScene(loadLevel);
        }
    }

    // Trigger and timer for loading screens / scenes
    private IEnumerator Start()
    {
        scene = SceneManager.GetActiveScene();

        if (bNeedsTimer)
        {
            yield return new WaitForSeconds(timeToLoad);

            SceneManager.LoadScene(loadLevel);
        }

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.FindObjectOfType<SaveGame>().SaveInventoryTransfer();
        SceneManager.LoadScene(loadLevel);
    }
}
