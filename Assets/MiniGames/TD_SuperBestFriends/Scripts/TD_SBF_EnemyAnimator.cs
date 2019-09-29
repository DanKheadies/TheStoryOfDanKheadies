// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/23/2019 
// Last:  09/24/2019

using UnityEngine;

public class TD_SBF_EnemyAnimator : MonoBehaviour
{
    public Animator enemyAni;
    public Vector3 currentPos;
    public Vector3 previousPos;

    void Start()
    {
        currentPos = transform.position;
        previousPos = transform.position;

        PositionCheck();
        InvokeRepeating("PositionCheck", 0.01f, 0.333f);
    }

    public void PositionCheck()
    {
        currentPos = transform.position;

        if (currentPos != previousPos)
        {
            enemyAni.SetBool("bIsWalking", true);
            enemyAni.SetFloat("MoveX", Mathf.Clamp(
                    currentPos.x - previousPos.x, -1, 1));
            enemyAni.SetFloat("MoveY", Mathf.Clamp(
                    currentPos.y - previousPos.y, -1, 1));

            previousPos = transform.position;
        }
        else
        {
            enemyAni.SetBool("bIsWalking", false);
        }
    }
}
