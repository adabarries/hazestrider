using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator enemyAnimator;
    private float cooldownTimer = Mathf.Infinity;
    public BoxCollider2D boxCollider;
    public LayerMask playerLayer;
    public float attackCooldown;
    public float castDistance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        // attack only if enemy sees player
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //attack
            }
        }
        
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * castDistance,
            boxCollider.bounds.size, 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }

}
