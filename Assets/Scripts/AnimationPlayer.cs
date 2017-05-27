using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimationPlayer : MonoBehaviour
{
    //public Animation animationToPlay;

    public float beforeAnimationPlayDelay;
    public float afterAnimationPlayDelay;

    private IEnumerator Start()
    {
        this.GetComponent<Animator>().speed = 0.0f;
        yield return new WaitForSeconds(beforeAnimationPlayDelay);
        this.GetComponent<Animator>().speed = 1.0f;
        this.GetComponent<Animator>().Play(0);
        yield return new WaitForSeconds(afterAnimationPlayDelay);
    }
}
