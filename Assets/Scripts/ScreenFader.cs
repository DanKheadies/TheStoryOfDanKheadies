using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFader : MonoBehaviour {

    Animator anim;
    Animator playerAnim;

    public bool isFading = false;

	void Start () {
        anim = GetComponent<Animator>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }
	
    public IEnumerator FadeToClear()
    {
        isFading = true;
        anim.SetTrigger("FadeIn");
        while (isFading)
        {
            playerAnim.SetBool("IsWalking", false);
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        isFading = true;
        anim.SetTrigger("FadeOut");
        while (isFading)
        {
            playerAnim.SetBool("IsWalking", false);
            yield return null;
        }
    }

    void AnimationComplete()
    {
        isFading = false;
    }
}
