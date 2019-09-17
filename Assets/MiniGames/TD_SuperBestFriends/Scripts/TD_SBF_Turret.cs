// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/17/2019

using UnityEngine;

public class TD_SBF_Turret : MonoBehaviour
{
    public Transform target;
    public TD_SBF_Enemy targetEnemy;
    //public TD_SBF_TowerPlacer towerPlacer;

    [Header("General")]
    public float range = 15f;

    [Header("Use Bullets (default)")]
    public float fireRate = 1f; // Bullets per second
    private float fireCountdown = 0f;

    [Header("Use Laser")] // TODO: Unity Custom Editor to show only laser vs. bullets when selected
    public bool useLaser = false;
    public float slowAmount = 0.5f;
    public int damageOverTime = 3;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform partToRotate;
    public float rotateSpeed = 10f;
    public string enemyTag = "Enemy";

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

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
                    impactEffect.Stop();
                    impactLight.enabled = false;
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

    void Laser()
    {
        if (targetEnemy.health > 0)
            targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);

        if (!targetEnemy.isBoss)
            targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnMouseDown()
    {
        //RaycastHit hitInfo;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hitInfo))
        //{
        //    //towerPlacer.CheckNode(hitInfo.point);
        //    Collider[] colliders;
        //    colliders = Physics.OverlapBox(transform.position, transform.localScale);

        //    Debug.Log(colliders);

        //    if (colliders.Length > 1)
        //    //if ((colliders = Physics.OverlapShere(transform.position, 1f /* Radius */)).Length > 1) //Presuming the object you are testing also has a collider 0 otherwise
        //    {
        //        Debug.Log(colliders);
        //        //foreach (var collider in colliders)
        //        //{
        //        //    var go = collider.gameObject; //This is the game object you collided with
        //        //    if (go == gameObject) continue; //Skip the object itself
        //        //                                    //Do something
        //        //}
        //    }
        //}
        //td_sbf_buildMan.SelectNode(this);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
