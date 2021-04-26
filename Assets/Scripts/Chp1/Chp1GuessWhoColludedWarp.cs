// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/22/2019
// Last:  04/26/2021

using UnityEngine;

public class Chp1GuessWhoColludedWarp : MonoBehaviour
{
    public Chp1 chp1;
    public BoxCollider2D gwcColli;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chp1.WarpToGuessWhoColluded();
        }
    }
}
