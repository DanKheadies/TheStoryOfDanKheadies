// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/27/2019
// Last:  05/03/2020 

using System.Collections;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    private Animator anim;

    public bool bAvoidAni;

    public float delayTime;

    private void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(DelayAnimationStart());
    } 

    IEnumerator DelayAnimationStart()
    {
        yield return new WaitForSeconds(delayTime);

        if (bAvoidAni)
            anim.enabled = false;
        else
            anim.enabled = true;
    }
}
