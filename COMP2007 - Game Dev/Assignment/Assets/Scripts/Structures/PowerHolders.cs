using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerHolders : MonoBehaviour
{
    public float health = 50f;
    public GameObject explosiion;
    public Transform explosionPosition;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject explosion = (GameObject)Instantiate(explosiion, explosionPosition.position, explosionPosition.rotation);
        Destroy(explosion, 1f);
        Destroy(gameObject);

        GetComponentInParent<PowerCoreObjective>().HolderDestroyed();
    }
}
