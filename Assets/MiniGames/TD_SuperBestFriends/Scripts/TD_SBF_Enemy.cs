﻿// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/13/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_Enemy : MonoBehaviour
{
    //public GameObject deathEffect;

    public bool isBoss;
    public bool isDead;
    public float health;
    public float speed;
    public float startHealth;
    public float startSpeed;
    public int worth;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        isDead = false;
        health = startHealth;
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f &&
            !isDead)
        {
            Die();
        }
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (amount);
    }

    void Die()
    {
        isDead = true;

        TD_SBF_PlayerStatistics.Money += worth;

        //GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);

        TD_SBF_WaveSpawner.enemiesAlive--;

        Destroy(gameObject);
    }
}
