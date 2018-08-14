// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/13/2018

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

    // Avoid console error when no player object is present
    // DC TODO
    void AnimationComplete()
    {
        scene = SceneManager.GetActiveScene();

        if (scene.name != "Showdown" && !bAvoidAniComp)
        {
            pMove.bStopPlayerMovement = false;
        }
    }
}
