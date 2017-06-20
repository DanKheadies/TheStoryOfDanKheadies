// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/19/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour {
    
    Scene scene;

    public bool bNeedsTimer;
    public float timeToLoad;
    public string loadLevel;
    
    void Update()
    {
        if ((scene.name == "LogoSplash" || scene.name == "MainMenu_Animation") && Input.anyKeyDown)
        {
            SceneManager.LoadScene(loadLevel);
        }
    }

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
        SceneManager.LoadScene(loadLevel);
    }
}
