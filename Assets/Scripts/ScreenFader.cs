// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  06/28/2019

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stops player movement while fading in / out
public class ScreenFader : MonoBehaviour
{
    private Animator anim;
    private Animator pAnim;
    private PlayerMovement pMove;
    private Scene scene;

    public bool bAvoidAniComp;

    void Start () {
        anim = GetComponent<Animator>();
        pAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        pMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
	
    public IEnumerator FadeToClear()
    {
        pMove.bStopPlayerMovement = true;
        anim.SetTrigger("FadeIn");
        while (pMove.bStopPlayerMovement)
        {
            pAnim.SetBool("bIsWalking", false);
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        pMove.bStopPlayerMovement = true;
        anim.SetTrigger("FadeOut");
        while (pMove.bStopPlayerMovement)
        {
            pAnim.SetBool("bIsWalking", false);
            yield return null;
        }
    }

    // DC 06/28/19 -- Keep to avoid console error
    void AnimationComplete()
    {
        // Does nothing
    }
}
