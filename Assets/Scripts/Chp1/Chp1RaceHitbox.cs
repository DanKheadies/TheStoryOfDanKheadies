// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/10/2022
// Last:  08/08/2022

using UnityEngine;

public class Chp1RaceHitbox : MonoBehaviour
{
    public Chp1RacePerimeter chp1RP;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Shrink player's hitbox while within the race area
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("enter");
            chp1RP.ActivatePlayerRacingHitbox();
        }
    }
}
