using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject player;
    public Animator anim;
    public Transform spawnPoint;

    
    public float attackCooldown = 0.45f;
    public int hitPoints;

    private float clickTimestamp;
    private bool canAttack;
    private bool isInvuln;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 3;

    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - clickTimestamp) > attackCooldown)
        {
            canAttack = true;
        }

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            clickTimestamp = Time.time;

            // this should collide with the enemy hitbox
            // when the attack animation plays
            // since the hitbox is synced to the animation...which SHOULD
            // trigger the enemy script to make it take damage....hopefully.
            anim.SetTrigger("attackOne");
            
            canAttack = false;

        }
    }

    // collision detection and damage from enemies
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyAttack") && isInvuln == false)
        {
            hitPoints -= 1;
            anim.SetTrigger("playerHurt");
            StartCoroutine(InvulnTimer());

            if (hitPoints == 0)
            {
                anim.SetBool("isDead", true);
                StartCoroutine(Respawn());
            }
        }
    }

    // coroutine for adding i-frames so you dont get shredded
    private IEnumerator InvulnTimer()
    {
        isInvuln = true;
        Debug.Log("player isInvuln");

        yield return new WaitForSeconds(0.5f); // length of invuln

        isInvuln = false;
        Debug.Log("player !isInvuln");
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        player.transform.position = spawnPoint.position;
        anim.SetBool("isDead", false);
        hitPoints = 3;


    }
}
