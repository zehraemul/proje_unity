using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Rigidbody2D rb;
    private float xInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private Animator anim;

    //[SerializeField] private bool isMoving;
    private int facingDirection = 1;
    private bool isFacingRight = true;


    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    private float dashTime;

    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;


    [Header("Attack info")]
    private bool isAttacking;
    private int comboCounter;


    [Header("Collision info")]

    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
     
    }

    void Update()
    {
        Movement();
        CheckInput();


        /* if (Input.GetKeyDown(KeyCode.R))
         {
             Flip();
         }*/

        /// Debug.Log(Input.GetAxisRaw("Horizontal")); a d 1 -1
        /// 

        CollisionChecks();

       // dashTime = dashTime - Time.deltaTime;
        dashTime -= Time.deltaTime;
        dashCooldownTimer -= Time.deltaTime;

      
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
    }


    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal"); //unityde input 1 -1

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttacking = true;
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

    private void DashAbility()
    {
        if(dashCooldownTimer < 0)
        {

        dashCooldownTimer = dashCooldown;
        dashTime = dashDuration;

        }
    }

    private void Movement()
    {

        if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
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

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
