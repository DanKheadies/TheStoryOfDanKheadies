using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{

    public float timeToLoad;
    public string loadLevel;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(loadLevel);
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToLoad);

        SceneManager.LoadScene(loadLevel);
    }
}
