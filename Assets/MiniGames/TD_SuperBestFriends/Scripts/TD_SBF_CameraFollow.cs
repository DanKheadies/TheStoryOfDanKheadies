// CC 4.0 International License: Attribution--DTFun--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/17/2016
// Last:  06/25/2021

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

    public void GetHero()
    {
        hero = GameObject.FindGameObjectWithTag("Hero");
    }
}
