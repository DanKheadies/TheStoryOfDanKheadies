// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/10/2022
// Last:  08/10/2022

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GWCMenuControl : MonoBehaviour
{
    public AnimationCurve curve;
    public GameObject endB;
    public GameObject startB;
    public Image img;

    public void EndGame()
    {
        PlayerPrefs.SetInt("Transferring", 1);
        PlayerPrefs.SetString("TransferScene", "Chp1");
    }

    public void GoToScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("GuessWhoColluded");
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
