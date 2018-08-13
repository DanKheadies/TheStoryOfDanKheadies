// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 07/23/2017
// Last:  08/13/2018

using UnityEngine;

// Switch the track / music based on zone or scene change
public class MusicSwitcher : MonoBehaviour
{
    private MusicManager theMan;

    public int newTrack;

	void Start ()
    {
        // Initializers
        theMan = FindObjectOfType<MusicManager>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            theMan.SwitchTrack(newTrack);
        }
    }
}
