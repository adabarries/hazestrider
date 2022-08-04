using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    
    //private Vector2 currentVelocity;

    private Rigidbody2D playerRigidBody;
    private Animator animator;

    public float movementSpeed = 3f;
    public float jumpForce = 3f;
    private bool facingRight = true;
    private float horizontalMovement = 0f;
    private bool isGrounded = true;
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal"); // x-axis
        float y = Input.GetAxis("Vertical"); // y-axis
        Vector2 dir = new Vector2(x, y); // direction (x and y)
        horizontalMovement = x * movementSpeed;
        animator.SetFloat("SpeedX", Mathf.Abs(horizontalMovement)); // run animation trigger

        Walk(dir);

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
        }

        // check current player direction and flip sprite accordingly when player moves
        if (x > 0 && !facingRight) // flip to face right
        {
            Flip();
        }
        if (x < 0 && facingRight) // flip to face left
        {
            Flip();
        }

    }

    private void Walk(Vector2 dir)
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
