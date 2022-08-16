using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header ("Enemy")]
    // enemy transform
    public Transform enemy;

    [Header ("Waypoints")]
    // points enemy will patrol between
    public Transform leftPoint;
    public Transform rightPoint;

    [Header ("Movement")]
    // parameters for movement
    private bool movingLeft;
    private bool facingLeft;
    public float enemySpeed;
    public float idleDuration;
    private float idleCooldown;


    [Header ("Animator")]
    // enemy animator
    public Animator enemyAnimator;


    void Start()
    {
        facingLeft = transform.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        // whoops the input script doesn't work for non-player objects
        // so instead we're using transform
        if (movingLeft)
        {
            Debug.Log("movingLeft.");
            if (enemy.position.x >= leftPoint.position.x)
            {
                MoveInDirection(-1);
                if (!facingLeft)
                {
                    Flip();
                }
            }
            else
            {
                TurnAround();
            }
        }
        else
        {
            Debug.Log("No longer movingLeft.");
            if (enemy.position.x <= rightPoint.position.x)
            {
                MoveInDirection(1);
                if (facingLeft)
                {
                    Flip();
                }
            }
            else
            {
                TurnAround();
            }
        }

        
    }

   
    private void MoveInDirection(int dir)
    {
        enemyAnimator.SetBool("isMoving", true);
        idleCooldown = 0;

        // movement transforms the enemy position on the x-axis over time
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * dir, enemy.position.y);
    }

    private void TurnAround()
    {
        enemyAnimator.SetBool("isMoving", false);
        idleCooldown += Time.deltaTime;

        if (idleCooldown > idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }

    private void Flip()
    {
        Vector2 currentScale = enemy.localScale;
        currentScale.x *= -1;
        enemy.localScale = currentScale;

        facingLeft = !facingLeft;
    }

}
