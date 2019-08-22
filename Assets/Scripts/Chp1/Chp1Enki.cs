// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  08/21/2019

using UnityEngine;

public class Chp1Enki : MonoBehaviour
{
    public Chp1 chp1;
    public PolygonCollider2D enkiColli;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chp1.SmoochyWoochyCheck();
        }
    }
}
