// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/12/2018

using UnityEngine;

// Means and methods to reduce the player's brio
public class FatiguePlayer : MonoBehaviour
{
    public UIManager uiMan;

    public int negativeBrio;

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBrioManager>().FatiguePlayer(negativeBrio);
            uiMan.bUpdateBrio = true;
        }
    }
}
