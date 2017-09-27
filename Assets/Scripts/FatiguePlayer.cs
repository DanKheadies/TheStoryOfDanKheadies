// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  07/02/2017

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Means and methods to reduce the player's brio
public class FatiguePlayer : MonoBehaviour
{
    public int negativeBrio;

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerBrioManager>().FatiguePlayer(negativeBrio);
        }
    }
}
