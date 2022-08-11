using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    private bool facingRight = false;
    public BoxCollider2D boxCollider;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");

        // Check current enemy direction and flip sprite accordingly when player moves
        if (xAxis > 0 && !facingRight) // flip to face right
        {
            Flip();
        }
        if (xAxis < 0 && facingRight) // flip to face left
        {
            Flip();
        }
    }

    private void Flip()
    {
        Vector2 currentScale = gameObject.transform.localScale;

        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;
    }
}
