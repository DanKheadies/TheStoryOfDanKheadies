// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  02/16/2020

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TD_SBF_Turret : MonoBehaviour
{
    public Animator turretAni;
    public Transform target;
    public TD_SBF_Enemy targetEnemy;

    [Header("General")]
    public bool isDestroyed;
    public bool bAnimateShot;
    public float health;
    public float range = 15f;
    public float startHealth = 15f;

    [Header("Use Bullets (default)")]
    public float fireRate = 1f; // Bullets per second
    private float fireCountdown = 0f;

    [Header("Use Laser")] // TODO: Unity Custom Editor to show only laser vs. bullets when selected
    public bool useLaser = false;
    public float slowAmount = 0.5f;
    public int damageOverTime = 3;
    public LineRenderer lineRenderer;
    public GameObject impactEffect;

    [Header("Unity Setup Fields")]
    public GameObject bulletPrefab;
    public Image healthBar;
    public Transform firePoint;
    public Transform partToRotate;
    public float rotateSpeed = 10f;
    public string enemyTag = "Enemy";

    void Start()
    {
        health = startHealth;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy &&
                shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<TD_SBF_Enemy>();
            }
            else
            {
                target = null;
            }
        }
    }

    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.SetActive(false);
                }
            }

            return;
        }

        //LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;

                if (bAnimateShot)
                    StartCoroutine(AnimateShot());
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    // Not used atm
    void LockOnTarget()
    {
        Vector2 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector2 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        TD_SBF_Bullet bullet = bulletGO.GetComponent<TD_SBF_Bullet>();

        if (bullet)
            bullet.Seek(target);
    }

    public IEnumerator AnimateShot()
    {
        transform.GetChild(0).transform.GetChild(0)
            .GetComponent<Animator>().SetBool("bIsShooting", true);

        yield return new WaitForSeconds(0.25f);

        transform.GetChild(0).transform.GetChild(0)
            .GetComponent<Animator>().SetBool("bIsShooting", false);
    }

    void Laser()
    {
        if (targetEnemy.health > 0)
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);

        if (!targetEnemy.isBoss)
            targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.SetActive(true);
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized / 10;
    }

    public void TakeDamage(float amount, GameObject attackingEnemy)
    {
        healthBar.GetComponentInParent<CanvasGroup>().alpha = 1;

        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f &&
            !isDestroyed)
        {
            DestroyTower();
            attackingEnemy.GetComponentInChildren<TD_SBF_EnemyPathfinding>()
                .firstTower = null;
            attackingEnemy.GetComponentInChildren<TD_SBF_EnemyPathfinding>()
                .DestroyNode();
            attackingEnemy.GetComponentInChildren<TD_SBF_EnemyPathfinding>()
                .RecheckPathing();
        }
    }

    public void RepairTower(float amount)
    {
        if (health >= startHealth)
            healthBar.GetComponentInParent<CanvasGroup>().alpha = 0;
    }

    void DestroyTower()
    {
        isDestroyed = true;

        // TODO
        // towerAni.Play("Tower_Destroyed");
        // Temp
        gameObject.transform.GetChild(0).transform.localScale = Vector3.zero;
        gameObject.transform.GetChild(1).transform.localScale = Vector3.zero;
        GameObject effect = Instantiate(TD_SBF_BuildManager.td_sbf_instance.buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        GetComponentInChildren<PolygonCollider2D>().isTrigger = true;

        // Dismiss description bar (if present)
        if (TD_SBF_BuildManager.td_sbf_instance.gameObject.GetComponent<TD_SBF_GameManagement>()
                .nodeUISel.GetComponent<TD_SBF_NodeUI>().selectionEffect)
            TD_SBF_BuildManager.td_sbf_instance.gameObject.GetComponent<TD_SBF_GameManagement>()
                .nodeUISel.GetComponent<TD_SBF_NodeUI>().DescriptionBarCheck();

        // Destroy associated node & clear from array
        var nodePos = new Vector3(transform.position.x, transform.position.y, -1);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(nodePos, 0.001f);

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "GridNode")
                {
                    TD_SBF_TowerPlacer.nodeArray.Remove(collider.transform.position);
                    Destroy(collider.gameObject, 0.125f);
                }
            }
        }

        Destroy(gameObject, 0.5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
