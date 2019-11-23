// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/17/2019
// Last:  11/20/2019

using UnityEngine;

public class TD_SBF_HeroMovement : MonoBehaviour
{
    public Animator heroAni;
    public GameObject attackPos;
    public Rigidbody2D rBody;
    public TD_SBF_ControllerSupport contSupp;
    public TD_SBF_GameManagement gMan;
    public Vector2 movementVector;
    
    public bool bStopPlayerMovement;

    public float moveSpeed;

    void Start()
    {
        // Initializers
        contSupp = GameObject.Find("GameSupport").GetComponent<TD_SBF_ControllerSupport>();
        gMan = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<TD_SBF_GameManagement>();

        InvokeRepeating("SetBodyMass", 1f, 1.0f);
    }

    void Update()
    {
        if (!gMan.bIsHeroMode)
            return;

        if (bStopPlayerMovement)
        {
            movementVector = Vector2.zero;
            rBody.velocity = Vector2.zero;
        }
        // Suspend mobile for the moment
        //else if (touches.bDown ||
        //         touches.bLeft ||
        //         touches.bRight ||
        //         touches.bUp ||
        //         touches.bUpRight ||
        //         touches.bUpLeft ||
        //         touches.bDownRight ||
        //         touches.bDownLeft)
        //{
        //    // No action; just need to avoid MovePlayer() here b/c it's cancelling out
        //    // the Touches script by passing in Move(0,0) while touches passes Move(X,Y)
        //}
        //else if (fixJoystick.bJoying)
        //{
        //    // DC TODO -- See if sliding; then restrict movement in that previous direction for 
        //    //            0.X seconds AFTER re-contact w/ Joystick
        //    Move(fixJoystick.Horizontal, fixJoystick.Vertical);
        //}
        else if (contSupp.bIsControlling)
        {
            MovePlayerWithController();
        }
        else
        {
            Move(Input.GetAxisRaw("Horizontal"), 
                 Input.GetAxisRaw("Vertical"));
        }
    }

    public void MovePlayerWithController()
    {
        if (Input.GetAxis("Controller Joystick Horizontal") != 0 ||
            Input.GetAxis("Controller Joystick Vertical") != 0)
        {
            Move(Input.GetAxis("Controller Joystick Horizontal"), 
                 Input.GetAxis("Controller Joystick Vertical"));
        }
        else if (Input.GetAxis("Controller DPad Horizontal") != 0 ||
                 Input.GetAxis("Controller DPad Vertical") != 0)
        {
            Move(Input.GetAxis("Controller DPad Horizontal"), 
                 (Input.GetAxis("Controller DPad Vertical") * -1));
        }
        else
            Move(0, 0);
    }

    public void Move(float xInput, float yInput)
    {
        movementVector = moveSpeed * new Vector2(xInput, yInput);

        // Animate movement
        if (heroAni)
        {
            if (movementVector != Vector2.zero)
            {
                heroAni.SetBool("bIsWalking", true);
                heroAni.SetFloat("MoveX", movementVector.x);
                heroAni.SetFloat("MoveY", movementVector.y);
            }
            else
            {
                heroAni.SetBool("bIsWalking", false);
            }
        }

        rBody.velocity = movementVector;

        //if (playerAnim)
        //    playerAnim.speed = 1.0f;

        // Set sorting order
        transform.GetComponent<SpriteRenderer>()
            .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(transform.position.y));

        // Set animation direction
        //heroAni.GetComponent<TD_SBF_HeroAnimator>()
        //    .MovementDirection(xInput, yInput);
    }

    public void SetBodyMass()
    {
        rBody.mass = 100 * Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }
}
