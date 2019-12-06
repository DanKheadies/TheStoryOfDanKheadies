// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/24/2019
// Last:  12/05/2019

using UnityEngine;

public class TD_SBF_HeroAnimator : MonoBehaviour
{
    public Animator heroAni;

    public void Idle()
    {
        heroAni.SetBool("bIsAttacking", false);
        heroAni.Play("Hero_Idle");
    }

    public void Attack()
    {
        heroAni.SetBool("bIsAttacking", true);
        heroAni.SetBool("bIsWalking", false);
    }

    public void Build()
    {
        heroAni.Play("Hero_Build");
    }

    public void GetHit()
    {
        heroAni.Play("Hero_Bonk_Down");
    }

    public void Die()
    {
        heroAni.Play("Hero_Death");
    }

    public void Oil()
    {
        heroAni.Play("Hero_Oil");
    }
}
