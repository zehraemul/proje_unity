using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private Rigidbody2D rb;
    private float xInput;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Space))

        //{
        //    Debug.Log("You holding the space button!");
        //}


        //if (Input.GetKeyDown(KeyCode.Space))

        //{
        //    Debug.Log("You pressed the space button!");
        //}


        //if (Input.GetKeyUp(KeyCode.Space))

        //{
        //    Debug.Log("You released the space button!");

        //}
        xInput = Input.GetAxisRaw("Horizontal"); //unityde input 1 -1

        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        /// Debug.Log(Input.GetAxisRaw("Horizontal")); a d 1 -1


    }
}
