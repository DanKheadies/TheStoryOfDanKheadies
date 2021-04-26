// CC 4.0 International License: Attribution--HolisticGaming.com--NonCommercial--ShareALike
// Authors: David W. Corso
// Start: 09/17/2019
// Last:  04/26/2021

using UnityEngine;

public class TD_SBF_HeroMovement : MonoBehaviour
{
    public Animator heroAni;
    public ControllerSupport contSupp;
    public GameObject attackPos;
    public Rigidbody2D rBody;
    public TD_SBF_GameManagement gMan;
    public TD_SBF_HeroStats heroStats;
    public TD_SBF_TouchControls touchConts;
    public Vector2 movementVector;
    
    public bool bStopPlayerMovement;

    public float moveSpeed;

    void Start()
    {
        InvokeRepeating("SetBodyMass", 1f, 1.0f);
    }

    void Update()
    {
        if (!gMan.bIsHeroMode)
            return;

        if (bStopPlayerMovement ||
            heroStats.bIsDead)
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
        else if (contSupp.bIsMoving)
        {
            MovePlayerWithController();
        }
        else if (touchConts.leftFixedJoystick.Vertical != 0 ||
                 touchConts.leftFixedJoystick.Horizontal != 0)
        {
            Move(touchConts.leftFixedJoystick.Horizontal,
                 touchConts.leftFixedJoystick.Vertical);
        }
        else
            Move(Input.GetAxisRaw("Horizontal"), 
                 Input.GetAxisRaw("Vertical"));
    }

    public void MovePlayerWithController()
    {
        if (contSupp.ControllerLeftJoystickHorizontal() != 0 ||
            contSupp.ControllerLeftJoystickVertical() != 0)
        {
            Move(contSupp.ControllerLeftJoystickHorizontal(),
                 contSupp.ControllerLeftJoystickVertical());
        }
        else if (contSupp.ControllerDirectionalPadHorizontal() != 0 ||
                 contSupp.ControllerDirectionalPadVertical() != 0)
        {
            Move(contSupp.ControllerDirectionalPadHorizontal(),
                 contSupp.ControllerDirectionalPadVertical() * -1);
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

        // Set sorting order
        transform.GetComponent<SpriteRenderer>()
            .sortingOrder = 100 + Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }

    public void SetBodyMass()
    {
        rBody.mass = 100 * Mathf.Abs(Mathf.RoundToInt(transform.position.y));
    }
}
