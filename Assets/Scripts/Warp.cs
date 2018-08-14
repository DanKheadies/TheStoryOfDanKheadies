// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 04/20/2017
// Last:  08/13/2018

using System.Collections;
using UnityEngine;

// Warps player around the scene
public class Warp : MonoBehaviour
{
    public Animator pAnim;
    public CameraFollow cFollow;
    public PlayerMovement thePlayer;
    public ScreenFader sFader;
    public TouchControls touches;
    public Transform warpTarget;

    public int AnandaCoord;

    void Start()
    {
        // Initializers
        thePlayer = FindObjectOfType<PlayerMovement>();
        cFollow = FindObjectOfType<CameraFollow>();
        pAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        sFader = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();
        touches = FindObjectOfType<TouchControls>();

        // AnandaCoord = Warp.Location; set in app
    }
    

	IEnumerator OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            // Stops the player's movement
            pAnim.SetBool("bIsWalking", false);
            thePlayer.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            thePlayer.bStopPlayerMovement = true;
            touches.UnpressedAllArrows();

            // Stop player interaction
            // DC 08/13/2018

            // Fade out
            yield return StartCoroutine(sFader.FadeToBlack());

            cFollow.currentCoords = (CameraFollow.AnandaCoords)AnandaCoord;
            other.gameObject.transform.position = warpTarget.position;
            Camera.main.transform.position = warpTarget.position;
            
            // Fade in
            yield return StartCoroutine(sFader.FadeToClear());
        }
    }
}
