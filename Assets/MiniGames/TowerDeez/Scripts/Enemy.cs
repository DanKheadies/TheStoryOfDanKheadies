// CC 4.0 International License: Attribution--Holistic3d.com & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 07/06/2016
// Last:  08/12/2019

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathEffect;
    public float health = 10;
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;
    public int worth = 20;

    void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
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
        PlayerStatistics.Money += worth;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
