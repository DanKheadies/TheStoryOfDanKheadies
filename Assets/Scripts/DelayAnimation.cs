// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/27/2019
// Last:  08/27/2019

using System.Collections;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    private Animator anim;

    public float delayTime;

    private void Start()
    {
        anim = GetComponent<Animator>();

        StartCoroutine(DelayAnimationStart());
    } 

    IEnumerator DelayAnimationStart()
    {
        yield return new WaitForSeconds(delayTime);
        
        anim.enabled = true;
    }
}
