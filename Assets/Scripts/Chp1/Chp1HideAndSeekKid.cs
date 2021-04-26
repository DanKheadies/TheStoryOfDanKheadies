// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  04/26/2021

using UnityEngine;

public class Chp1HideAndSeekKid : MonoBehaviour
{
    public Chp1 chp1;
    public PolygonCollider2D kidColli;

    void Start() { }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" &&
            enabled)
        {
            chp1.HideAndSeekFindingKid(gameObject.transform.parent.name);
            enabled = false;
        }
    }
}
