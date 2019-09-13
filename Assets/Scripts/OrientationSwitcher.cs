// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 08/27/2019
// Last:  08/27/2019

using UnityEngine;

public class OrientationSwitcher : MonoBehaviour
{
    public bool bIsHori;
    public bool bIsVert;

    public float scaleMulti = 1f;

    void Start()
    {
        OrientationCheck();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OrientationCheck();
        }
    }

    public void OrientationCheck()
    {
        // Width > height = center in the screen
        if (Screen.width >= Screen.height)
        {
            if (bIsHori)
                gameObject.transform.localScale = new Vector3(1 * scaleMulti, 1 * scaleMulti, 1);

            if (bIsVert)
                gameObject.transform.localScale = Vector3.zero;
        }
        else
        {
            if (bIsHori)
                gameObject.transform.localScale = Vector3.zero;

            if (bIsVert)
                gameObject.transform.localScale = new Vector3(1 * scaleMulti, 1 * scaleMulti, 1);
        }
    }
}
