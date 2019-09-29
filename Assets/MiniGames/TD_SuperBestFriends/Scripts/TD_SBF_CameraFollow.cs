// CC 4.0 International License: Attribution--Brackeys & HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/17/2016
// Last:  09/17/2019

using UnityEngine;

public class TD_SBF_CameraFollow : MonoBehaviour
{
    public GameObject hero;
    public Vector2 smoothVelocity;

    public float depth;
    public float posX;
    public float posY;
    public float smoothTime;

    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");

        depth = -10f;
        smoothTime = 0.2f;
        smoothVelocity = new Vector2(0.2f, 0.2f);
    }
    
    void Update()
    {
        // Camera follows the player with a slight delay 
        posX = Mathf.SmoothDamp(transform.position.x, hero.transform.position.x, ref smoothVelocity.x, smoothTime);
        posY = Mathf.SmoothDamp(transform.position.y, hero.transform.position.y, ref smoothVelocity.y, smoothTime);
        transform.position = new Vector3(posX, posY, depth);
    }
}
