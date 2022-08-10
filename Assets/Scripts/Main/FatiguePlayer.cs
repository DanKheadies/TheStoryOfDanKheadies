// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  04/26/2021

using UnityEngine;

// Means and methods to reduce the player's brio
public class FatiguePlayer : MonoBehaviour
{
    public UIManager uMan;

    public int negativeBrio;

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBrioManager>().FatiguePlayer(negativeBrio);
            uMan.UpdateBrio();
        }
    }
}
