// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/10/2020
// Last:  04/26/2021

using UnityEngine;

public class NPCIdle : MonoBehaviour
{
    public Animator npcAni;

    public float timer;
    public float waitTime;

    public int idleDirection;

    void Start()
    {
        waitTime = 3.0f;
        timer = waitTime;
    }

    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            ChangeDirection();
            timer = waitTime;
        }
    }

    public void ChangeDirection()
    {
        idleDirection = Random.Range(0, 4);

        switch (idleDirection)
        {
            case 0:
                npcAni.SetFloat("MoveX", 0);
                npcAni.SetFloat("MoveY", -1);
                break;

            case 1:
                npcAni.SetFloat("MoveX", 0);
                npcAni.SetFloat("MoveY", 1);
                break;

            case 2:
                npcAni.SetFloat("MoveX", 1);
                npcAni.SetFloat("MoveY", 0);
                break;

            case 3:
                npcAni.SetFloat("MoveX", -1);
                npcAni.SetFloat("MoveY", 0);
                break;
        }
    }
}
