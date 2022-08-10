// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/27/2019
// Last:  04/26/2021

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
