// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  04/26/2021

using UnityEngine;

public class Chp1DirtPatch : MonoBehaviour
{
    public Chp1 chp1;
    public BoxCollider2D dirtColli;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chp1.Quest2Reward();
        }
    }
}
