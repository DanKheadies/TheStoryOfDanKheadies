// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: Asbjørn / Brackeys
// Contributors: David W. Corso
// Start: 09/11/2019
// Last:  09/11/2019

using UnityEngine;

public class TD_SBF_Bullet : MonoBehaviour
{
    //public GameObject impactEffect;
    private Transform target;

    //public float explosionRadius = 0f;
    public float speed = 70f;
    public int damage = 5;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        //GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 5f);

        //if (explosionRadius > 0f)
        //{
        //    Explode();
        //}
        //else
        Damage(target);

        Destroy(gameObject);
    }

    //void Explode()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.tag == "Enemy")
    //        {
    //            Damage(collider.transform);
    //        }
    //    }
    //}

    void Damage(Transform enemy)
    {
        TD_SBF_Enemy e = enemy.GetComponent<TD_SBF_Enemy>();

        if (e)
        {
            e.TakeDamage(damage);
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
