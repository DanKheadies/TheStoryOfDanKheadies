// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/29/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stops player movement while fading in / out
public class ScreenFader : MonoBehaviour
{
    private Animator anim;
    private Animator playerAnim;
    private PlayerMovement playerMovement;
    private Scene scene;

    void Start () {
        anim = GetComponent<Animator>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
	
    public IEnumerator FadeToClear()
    {
        playerMovement.bStopPlayerMovement = true;
        anim.SetTrigger("FadeIn");
        while (playerMovement.bStopPlayerMovement)
        {
            playerAnim.SetBool("IsWalking", false);
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        playerMovement.bStopPlayerMovement = true;
        anim.SetTrigger("FadeOut");
        while (playerMovement.bStopPlayerMovement)
        {
            playerAnim.SetBool("IsWalking", false);
            yield return null;
        }
    }

    // Avoid console error when no player object is present
    void AnimationComplete()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name != "Showdown")
        {
            playerMovement.bStopPlayerMovement = false;
        }
    }
}
