using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : Entity
{

   /* private Rigidbody2D rb;
    private Animator anim;*/
    private float xInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;


    //[SerializeField] private bool isMoving;
   


    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;


    [Header("Attack info")]
    private float comboTimeWindow;
   [SerializeField] private float comboTime = 0.3f;
    private bool isAttacking;
    private int comboCounter;
    

    protected override void Start()
    {
        base.Start();
    }

    /*  void Start()
      {
          rb = GetComponent<Rigidbody2D>();

          anim = GetComponentInChildren<Animator>();

      }*/

  protected override  void Update()
    {

        base.Update();
        Movement();
        CheckInput();


        /* if (Input.GetKeyDown(KeyCode.R))
         {
             Flip();
         }*/

        /// Debug.Log(Input.GetAxisRaw("Horizontal")); a d 1 -1
        /// 

       

       // dashTime = dashTime - Time.deltaTime;
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
      
        /*
        if (dashTime > 0)
        {
            Debug.Log("I am doing dash ability!");
        }*/

        //Debug.Log(isGrounded);

        FlipController();
        AnimatorControllers();


        /*  if(rb.velocity.x != 0)
          {
              isMoving = true;
          }
          else
          {
              isMoving = false;
          }
        */


    }


    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;

        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
      
    }


 

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal"); //unityde input 1 -1

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {

        if (!isGrounded)
        {
            return;
        }
        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }

        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if(dashCooldownTimer < 0 && !isAttacking)
        {

        dashCooldownTimer = dashCooldown;
        dashTime = dashDuration;

        }
    }

    private void Movement()
    {


        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }

        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }

       
    }

    private void Jump()
    {

        if(isGrounded)
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
    }


    private void FlipController()
    {
        if(rb.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(rb.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        {

        }
    }


}
