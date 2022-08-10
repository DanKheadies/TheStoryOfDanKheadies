// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  04/26/2021

using UnityEngine;

// Switch the track / music based on zone or scene change
public class MusicSwitcher : MonoBehaviour
{
    public MusicManager musicMan;

    public int newTrack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            musicMan.SwitchTrack(newTrack);
        }
    }
}
