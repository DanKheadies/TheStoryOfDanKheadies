// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/15/2020
// Last:  02/18/2020

using UnityEngine;

public class TD_SBF_TurretHitBox : MonoBehaviour
{
    public Collider2D[] colliders;
    public Vector3 nodePos;

    void Start()
    {
        nodePos = new Vector3(transform.parent.position.x, transform.parent.position.y, -1);
    }

    public void OnMouseEnter()
    {
        TD_SBF_BuildManager.td_sbf_instance.bOverTower = true;
        TD_SBF_BuildManager.td_sbf_instance.turretHitboxHoverPos = nodePos;
    }

    public void OnMouseExit()
    {
        TD_SBF_BuildManager.td_sbf_instance.bOverTower = false;
        TD_SBF_BuildManager.td_sbf_instance.turretHitboxHoverPos = Vector3.zero;
    }

    public void OnMouseDown()
    {
        colliders = Physics2D.OverlapCircleAll(nodePos, 0.001f);

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.tag == "UI")
                    return;

                if (collider.gameObject.tag == "GridNode")
                    collider.GetComponent<TD_SBF_Node>().SelectThisNode();
            }
        }
    }
}
