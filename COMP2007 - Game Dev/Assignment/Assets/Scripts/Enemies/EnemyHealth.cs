using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 50f;
    public GameObject deathExplosiion;
    public Transform explosionPosition;

    public void TakeDamage(float amount)
    {
        //Remove from health the damage
        health -= amount;

        //If less than 0 kill the enemy
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        //Create explosion and destroy the enemy
        GameObject explosion = (GameObject)Instantiate(deathExplosiion, explosionPosition.position, explosionPosition.rotation);
        Destroy(gameObject);
        Destroy(explosion, 1f);
    }
}
