// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/21/2019
// Last:  04/26/2021

using UnityEngine;
using System.Collections;

public class Chp1HideAndSeekPerimeter : MonoBehaviour
{
    public Chp1 chp1;
    public BoxCollider2D perimeterColli;

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DelayedReset());
        }
    }

    IEnumerator DelayedReset()
    {
        yield return new WaitForSeconds(1.333f);

        chp1.HideAndSeekReset();
    }
}
