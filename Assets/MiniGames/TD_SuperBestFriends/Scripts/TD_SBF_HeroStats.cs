// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/13/2019
// Last:  10/14/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_HeroStats : MonoBehaviour
{
    public Animator heroAni;
    public TD_SBF_HeroMovement heroMove;

    public bool isInvincible;
    public bool isDead;
    public float damage;
    public float health;
    public float startHealth;
    public float stunDuration;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        damage = 5f;
        startHealth = 50f;
        health = startHealth;
        stunDuration = 0.5f;
    }

    public void TakeDamage(float amount)
    {
        healthBar.GetComponentInParent<CanvasGroup>().alpha = 1;

        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f &&
            !isDead)
        {
            Die();
        }
    }
    
    public void HealHero(float amount)
    {
        if (health >= startHealth)
            healthBar.GetComponentInParent<CanvasGroup>().alpha = 0;
    }

    // Or just handle in HeroMovement
    public void Slow(float amount)
    {
        // get speed from HeroMovement.moveSpeed
        // heroMove.moveSpeed = heroMove.moveSpeed * (amount);
    }

    void Die()
    {
        isDead = true;

        heroAni.Play("Hero_Death");
        heroMove.moveSpeed = 0;
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }
}
