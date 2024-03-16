using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Rigidbody2D rb;
    private float xInput;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;

    private Animator anim;

    [SerializeField] private bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponentInChildren<Animator>();
     
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal"); //unityde input 1 -1

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        /// Debug.Log(Input.GetAxisRaw("Horizontal")); a d 1 -1

       isMoving = rb.velocity.x != 0;

      /*  if(rb.velocity.x != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
      */
        anim.SetBool("isMoving", isMoving);

    }
}
