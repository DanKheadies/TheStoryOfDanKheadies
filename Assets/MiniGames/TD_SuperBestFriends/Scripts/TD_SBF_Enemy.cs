// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/13/2019
// Last:  09/25/2019

using UnityEngine;
using UnityEngine.UI;

public class TD_SBF_Enemy : MonoBehaviour
{
    public Animator enemyAni;

    public bool isBoss;
    public bool isDead;
    public float damage;
    public float health;
    //public float speed;
    public float startHealth;
    //public float startSpeed;
    public int worth;

    [Header("Unity Stuff")]
    public Image healthBar;

    void Start()
    {
        damage = 15f;
        isDead = false;
        health = startHealth;
        //speed = startSpeed;

        GetComponent<Pathfinding.AIDestinationSetter>().target = 
            GameObject.FindGameObjectWithTag("Throne").transform;
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

    public void TakeHeroMeleeDamage(float _amount, float _stunDur)
    {
        Debug.Log("damaging enemy");
        health -= _amount;

        healthBar.fillAmount = health / startHealth;

        GetComponent<Pathfinding.AIPath>().maxSpeed = 0;

        // bonk animation
        enemyAni.Play("Enemy_Bonk_Down");

        Invoke("ResetEnemyMovement", _stunDur);

        if (health <= 0f &&
            !isDead)
        {
            Die();
        }
    }

    public void DamageHero(GameObject hero)
    {
        //hero.GetComponent<TD_SBF_HeroStats>().TakeDamage(damage);
        Debug.Log("damaging hero");
    }

    public void DamageTower(GameObject tower)
    {
        tower.GetComponent<TD_SBF_Turret>().TakeDamage(damage, gameObject);
    }

    public void Slow(float amount)
    {
        //speed = startSpeed * (amount);
    }

    void Die()
    {
        isDead = true;

        TD_SBF_PlayerStatistics.ThoughtsPrayers += worth;
        
        enemyAni.Play("Enemy_Death");
        GetComponent<Pathfinding.AIPath>().canMove = false;
        GetComponent<Pathfinding.AIPath>().maxSpeed = 0;
        GetComponent<PolygonCollider2D>().isTrigger = true;

        TD_SBF_WaveSpawner.enemiesAlive--;

        Destroy(gameObject, 2.25f);
    }

    public void ResetEnemyMovement()
    {
        GetComponent<Pathfinding.AIPath>().maxSpeed = 3;
    }
}
