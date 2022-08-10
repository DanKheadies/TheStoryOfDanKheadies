// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 02/14/2020
// Last:  04/26/2021

using UnityEngine;

public class SideDoorCollisionChanger : MonoBehaviour
{
    public GameObject player;
    public Vector2[] ogPoints;
    public Vector2[] sideDoorPoints;

    void Start()
    {
        // Initializers
        ogPoints = player.GetComponent<PolygonCollider2D>().points;
        sideDoorPoints = new Vector2[]
        {
            new Vector2 (0, 0),
            new Vector2 (-0.0333f, -0.005f),
            new Vector2 (-0.05f, -0.0420f),
            new Vector2 (-0.05f, -0.069f),
            new Vector2 (-0.0333f, -0.1f),
            new Vector2 (0.0333f, -0.1f),
            new Vector2 (0.05f, -0.069f),
            new Vector2 (0.05f, -0.0420f),
            new Vector2 (0.0333f, -0.005f)
        };
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player.GetComponent<PolygonCollider2D>().points = sideDoorPoints;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            player.GetComponent<PolygonCollider2D>().points = ogPoints;
    }
}
