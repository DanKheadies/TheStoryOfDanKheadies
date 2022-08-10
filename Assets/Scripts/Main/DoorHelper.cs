// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/09/2022
// Last:  08/09/2022

using System.Collections;
using UnityEngine;

public class DoorHelper : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
            collision.gameObject.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            StartCoroutine(SlidePlayer(collision, 0.25f));
    }

    public IEnumerator SlidePlayer(Collider2D collision, float timeToMove)
    {
        var playerPos = collision.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            collision.transform.position = Vector3.Lerp(playerPos, transform.position, t);
            yield return null;
        }
    }
}
