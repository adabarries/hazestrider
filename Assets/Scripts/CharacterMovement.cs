using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    private Animator animator;
    public LayerMask groundLayer;

    [Header ("Movement")]
    public float movementSpeed = 3f;
    public float jumpForce = 3f;

    private bool facingRight = true;
    private float horizontalMovement = 0f;
    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player is touching the ground
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer.value);


        // Horizontal movement and walking
        float xAxis = Input.GetAxis("Horizontal"); 
        float yAxis = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(xAxis, yAxis); // direction (x and y)
        horizontalMovement = xAxis * movementSpeed;

        // Run method + animation trigger
        animator.SetFloat("SpeedX", Mathf.Abs(horizontalMovement)); 
        Run(dir);

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }

        animator.SetFloat("SpeedY", playerRigidBody.velocity.y);

        // Check current player direction and flip sprite accordingly when player moves
        if (xAxis > 0 && !facingRight) // flip to face right
        {
            Flip();
        }
        if (xAxis < 0 && facingRight) // flip to face left
        {
            Flip();
        }

    }

    private void Run(Vector2 dir)
    {
        playerRigidBody.velocity = (
            new Vector2(dir.x * movementSpeed, playerRigidBody.velocity.y)
            );
    }

    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;

        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}
