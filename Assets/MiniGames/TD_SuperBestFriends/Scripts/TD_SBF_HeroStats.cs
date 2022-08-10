// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/13/2019
// Last:  04/26/2021

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TD_SBF_HeroStats : MonoBehaviour
{
    public TD_SBF_HeroAnimator heroAni;
    public TD_SBF_HeroBarManager heroBMan;
    public TD_SBF_HeroMovement heroMove;
    public TD_SBF_HeroUpgrade heroUpgrade;
    
    public bool bIsInvincible;
    public bool bIsDead;
    public float boostModifier;
    public float damage;
    public float health;
    public float startHealth;
    public float stunDuration;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        damage = 5f;
        health = startHealth;
        stunDuration = 0.5f;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health < startHealth)
            healthBar.GetComponentInParent<CanvasGroup>().alpha = 1;

        healthBar.fillAmount = health / startHealth;

        heroAni.GetHit();
        StartCoroutine(RestoreIdleAni());

        if (health <= 0f &&
            !bIsDead)
        {
            Die();
        }
    }
    
    public void HealHero(float amount)
    {
        if (health >= startHealth)
            healthBar.GetComponentInParent<CanvasGroup>().alpha = 0;

        health += amount;
    }

    void Die()
    {
        bIsDead = true;
        gameObject.tag = "Untagged";
        transform.GetChild(0).tag = "Untagged";

        heroAni.Die();
        heroMove.moveSpeed = 0;
        GetComponent<PolygonCollider2D>().isTrigger = true;

        heroMove.bStopPlayerMovement = true;
        heroBMan.DisableHeroAttacks();

        healthBar.GetComponentInParent<CanvasGroup>().alpha = 0;

        heroUpgrade.EnableRevive();
    }

    public void Revive(float _health)
    {
        bIsDead = false;
        gameObject.tag = "Hero";
        transform.GetChild(0).tag = "Hero";

        heroAni.Build();
        StartCoroutine(RestoreIdleAni());

        health = _health;
        healthBar.fillAmount = health / startHealth;

        heroMove.bStopPlayerMovement = false;
        heroMove.moveSpeed = 10f;

        GetComponent<PolygonCollider2D>().isTrigger = false;

        heroBMan.EnableHeroAttacks();
        heroUpgrade.DisableRevive();
    }

    public IEnumerator RestoreIdleAni()
    {
        yield return new WaitForSeconds(0.5f);

        if (!bIsDead)
            heroAni.Idle();
    }
}
