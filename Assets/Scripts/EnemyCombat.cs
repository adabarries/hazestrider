using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public Animator anim;
    public GameObject enemy;

    public float range;
    public float distance;

    public float attackCooldown;
    public int hitPoints;

    private float cooldownTimer = Mathf.Infinity;

    // reference
    private EnemyMovement enemyMovement;
    

    void Awake()
    {
        hitPoints = 3;
        enemyMovement = GetComponentInParent<EnemyMovement>();

    }

    // Update is called once per frame
    void Update()
    {

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            // start attacking
            if (cooldownTimer >= attackCooldown)
            {
                anim.SetTrigger("slimeAttack");
                cooldownTimer = 0;
            }

        }

       if (enemyMovement != null)
        {
            enemyMovement.enabled = !PlayerInSight();
        }
    }

    // collision detection and damage from player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            hitPoints -= 1;

            if (hitPoints > 0)
            {
                anim.SetTrigger("slimeHurt");
            }
            if (hitPoints == 0)
            {
                anim.SetBool("isDead", true);
                Destroy(enemy, 1f);
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position,
            Vector2.left * range * enemy.transform.localScale, distance);

        return hit.collider != null;
    }
  
}
