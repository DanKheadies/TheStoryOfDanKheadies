using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

    Animator anim;
    Animator playerAnim;

    PlayerMovement playerMovement;

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
        playerMovement.bStopPlayerMovement = false;
    }
}
