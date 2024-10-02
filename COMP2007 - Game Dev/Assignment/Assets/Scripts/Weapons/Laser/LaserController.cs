using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject collisionExplosion;
    public float speed;
    public float damage = 10f;

    Rigidbody rb;

    private void Start()
    {
        //Get rigidbody and add forward movement to it
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        //When the laser collides with anything exlode
        Explode();

        //Check if it hit an enemy
        EnemyHealth enemyHealth = collision.collider.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
            return;
        }

        //Check if it hit a player
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            return;
        }

        //Check if it hit a power holder
        PowerHolders powerHolder = collision.collider.GetComponent<PowerHolders>();
        if(powerHolder != null )
        {
            powerHolder.TakeDamage(damage);
            return;
        }
    }

    void Explode()
    {
        //Create explosion and destroy the laser
        GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(explosion, 1f);
    }

}
