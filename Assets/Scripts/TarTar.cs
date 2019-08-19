// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 03/29/2018
// Last:  08/13/2018

using System.Collections;
using UnityEngine;

// Slows players when interacting w/ that TarTar
public class TarTar : MonoBehaviour
{
    public GameObject player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerMovement>().moveSpeed = 0.1f;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // UX: return reduced speed and after X time, go full speed
            player.GetComponent<PlayerMovement>().moveSpeed = 0.333f;
            StartCoroutine(FullSpeed());
        }
    }

    IEnumerator FullSpeed()
    {
        yield return new WaitForSeconds(0.333f);
        player.GetComponent<PlayerMovement>().moveSpeed = 1.0f;
    }
}