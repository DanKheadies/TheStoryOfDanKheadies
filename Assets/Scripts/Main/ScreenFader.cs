// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/23/2019

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Stops player movement while fading in / out
public class ScreenFader : MonoBehaviour
{
    public Animator anim;
    public Animator playerAnim;
    public PlayerMovement playerMove;
    public Scene scene;

    public bool bAvoidAniComp;
	
    public IEnumerator FadeToClear()
    {
        playerMove.bStopPlayerMovement = true;
        anim.SetTrigger("FadeIn");
        while (playerMove.bStopPlayerMovement)
        {
            if (playerAnim)
                playerAnim.SetBool("bIsWalking", false);
            yield return null;
        }
    }

    public IEnumerator FadeToBlack()
    {
        playerMove.bStopPlayerMovement = true;
        anim.SetTrigger("FadeOut");
        while (playerMove.bStopPlayerMovement)
        {
            if (playerAnim)
                playerAnim.SetBool("bIsWalking", false);
            yield return null;
        }
    }

    // DC 06/28/19 -- Keep to avoid console error
    void AnimationComplete()
    {
        // Does nothing
    }
}
