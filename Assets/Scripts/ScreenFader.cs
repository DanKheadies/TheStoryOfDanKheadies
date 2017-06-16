using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

    Animator anim;
    Animator playerAnim;

    PlayerMovement playerMovement;

    Scene scene;

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

    void AnimationComplete()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name != "Battle")
        {
            Debug.Log(scene.name);
            playerMovement.bStopPlayerMovement = false;
        }
    }
}
