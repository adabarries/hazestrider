using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRespawn : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bad"))
        {
            transform.position = spawnPoint.position;
            // refine this and add extras later
        } 
    }
}
