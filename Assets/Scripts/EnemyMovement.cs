using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header ("Enemy")]
    // enemy transform
    [SerializeField] private Transform enemy;

    [Header ("Waypoints")]
    // points enemy will patrol between
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    [Header ("Movement")]
    // parameters for movement
    private bool movingLeft;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float idleDuration;
    [SerializeField] private float idleCooldown;
    private Vector2 initialScale;

    [Header ("Animator")]
    // enemy animator
    [SerializeField] private Animator enemyAnimator;


    // Start is called before the first frame update
    void Start()
    {
        initialScale = enemy.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // whoops the input script doesn't work for non-player objects
        if (movingLeft)
        {
            if (enemy.position.x >= leftPoint.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                TurnAround();
            }
        }
        else
        {
            if (enemy.position.x <= rightPoint.position.x)
            {
                MoveInDirection(1);
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

        // flip sprite dependent on direction moving
        enemy.localScale = new Vector2(Mathf.Abs(initialScale.x) * dir, initialScale.y);

        // movement
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
}
